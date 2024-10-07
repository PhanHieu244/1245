using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DigitalRuby.SoundManagerNamespace
{
	public class SoundManager : MonoBehaviour
	{
		private sealed class _RemoveVolumeFromClip_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal AudioClip clip;

			internal List<float> _volumes___0;

			internal float volume;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _RemoveVolumeFromClip_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(this.clip.length);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (SoundManager.soundsOneShot.TryGetValue(this.clip, out this._volumes___0))
					{
						this._volumes___0.Remove(this.volume);
					}
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private static int persistTag = 0;

		private static bool needsInitialize = true;

		private static GameObject root;

		private static SoundManager instance;

		private static readonly List<LoopingAudioSource> music = new List<LoopingAudioSource>();

		private static readonly List<AudioSource> musicOneShot = new List<AudioSource>();

		private static readonly List<LoopingAudioSource> sounds = new List<LoopingAudioSource>();

		private static readonly HashSet<LoopingAudioSource> persistedSounds = new HashSet<LoopingAudioSource>();

		private static readonly Dictionary<AudioClip, List<float>> soundsOneShot = new Dictionary<AudioClip, List<float>>();

		private static float soundVolume = 1f;

		private static float musicVolume = 1f;

		private static bool updated;

		private static bool pauseSoundsOnApplicationPause = true;

		public static int MaxDuplicateAudioClips = 4;

		public static float MusicVolume
		{
			get
			{
				return SoundManager.musicVolume;
			}
			set
			{
				if (value != SoundManager.musicVolume)
				{
					SoundManager.musicVolume = value;
					SoundManager.UpdateMusic();
				}
			}
		}

		public static float SoundVolume
		{
			get
			{
				return SoundManager.soundVolume;
			}
			set
			{
				if (value != SoundManager.soundVolume)
				{
					SoundManager.soundVolume = value;
					SoundManager.UpdateSounds();
				}
			}
		}

		public static bool PauseSoundsOnApplicationPause
		{
			get
			{
				return SoundManager.pauseSoundsOnApplicationPause;
			}
			set
			{
				SoundManager.pauseSoundsOnApplicationPause = value;
			}
		}

		private static void EnsureCreated()
		{
			if (SoundManager.needsInitialize)
			{
				SoundManager.needsInitialize = false;
				SoundManager.root = new GameObject();
				SoundManager.root.name = "DigitalRubySoundManager";
				SoundManager.root.hideFlags = HideFlags.HideAndDontSave;
				SoundManager.instance = SoundManager.root.AddComponent<SoundManager>();
				UnityEngine.Object.DontDestroyOnLoad(SoundManager.root);
			}
		}

		private void StopLoopingListOnLevelLoad(IList<LoopingAudioSource> list)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (!list[i].Persist || !list[i].AudioSource.isPlaying)
				{
					list.RemoveAt(i);
				}
			}
		}

		private void ClearPersistedSounds()
		{
			foreach (LoopingAudioSource current in SoundManager.persistedSounds)
			{
				if (!current.AudioSource.isPlaying)
				{
					UnityEngine.Object.Destroy(current.AudioSource.gameObject);
				}
			}
			SoundManager.persistedSounds.Clear();
		}

		private void SceneManagerSceneLoaded(Scene s, LoadSceneMode m)
		{
			if (SoundManager.updated)
			{
				SoundManager.persistTag++;
				SoundManager.updated = false;
				UnityEngine.Debug.LogWarningFormat("Reloaded level, new sound manager persist tag: {0}", new object[]
				{
					SoundManager.persistTag
				});
				SoundManager.StopNonLoopingSounds();
				this.StopLoopingListOnLevelLoad(SoundManager.sounds);
				this.StopLoopingListOnLevelLoad(SoundManager.music);
				SoundManager.soundsOneShot.Clear();
				this.ClearPersistedSounds();
			}
		}

		private void Start()
		{
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.SceneManagerSceneLoaded);
		}

		private void Update()
		{
			SoundManager.updated = true;
			for (int i = SoundManager.sounds.Count - 1; i >= 0; i--)
			{
				if (SoundManager.sounds[i].Update())
				{
					SoundManager.sounds.RemoveAt(i);
				}
			}
			for (int j = SoundManager.music.Count - 1; j >= 0; j--)
			{
				bool flag = SoundManager.music[j] == null || SoundManager.music[j].AudioSource == null;
				if (flag || SoundManager.music[j].Update())
				{
					if (!flag && SoundManager.music[j].Tag != SoundManager.persistTag)
					{
						UnityEngine.Debug.LogWarning("Destroying persisted audio from previous scene: " + SoundManager.music[j].AudioSource.gameObject.name);
						UnityEngine.Object.Destroy(SoundManager.music[j].AudioSource.gameObject);
					}
					SoundManager.music.RemoveAt(j);
				}
			}
			for (int k = SoundManager.musicOneShot.Count - 1; k >= 0; k--)
			{
				if (!SoundManager.musicOneShot[k].isPlaying)
				{
					SoundManager.musicOneShot.RemoveAt(k);
				}
			}
		}

		private void OnApplicationFocus(bool paused)
		{
			if (SoundManager.PauseSoundsOnApplicationPause)
			{
				if (paused)
				{
					SoundManager.ResumeAll();
				}
				else
				{
					SoundManager.PauseAll();
				}
			}
		}

		private static void UpdateSounds()
		{
			foreach (LoopingAudioSource current in SoundManager.sounds)
			{
				current.TargetVolume = current.OriginalTargetVolume * SoundManager.soundVolume;
			}
		}

		private static void UpdateMusic()
		{
			foreach (LoopingAudioSource current in SoundManager.music)
			{
				if (!current.Stopping)
				{
					current.TargetVolume = current.OriginalTargetVolume * SoundManager.musicVolume;
				}
			}
			foreach (AudioSource current2 in SoundManager.musicOneShot)
			{
				current2.volume = SoundManager.musicVolume;
			}
		}

		private static IEnumerator RemoveVolumeFromClip(AudioClip clip, float volume)
		{
			SoundManager._RemoveVolumeFromClip_c__Iterator0 _RemoveVolumeFromClip_c__Iterator = new SoundManager._RemoveVolumeFromClip_c__Iterator0();
			_RemoveVolumeFromClip_c__Iterator.clip = clip;
			_RemoveVolumeFromClip_c__Iterator.volume = volume;
			return _RemoveVolumeFromClip_c__Iterator;
		}

		private static void PlayLooping(AudioSource source, List<LoopingAudioSource> sources, float volumeScale, float fadeSeconds, bool persist, bool stopAll)
		{
			SoundManager.EnsureCreated();
			for (int i = sources.Count - 1; i >= 0; i--)
			{
				LoopingAudioSource loopingAudioSource = sources[i];
				if (loopingAudioSource.AudioSource == source)
				{
					sources.RemoveAt(i);
				}
				if (stopAll)
				{
					loopingAudioSource.Stop();
				}
			}
			source.gameObject.SetActive(true);
			LoopingAudioSource loopingAudioSource2 = new LoopingAudioSource(source, fadeSeconds, fadeSeconds, persist);
			loopingAudioSource2.Play(volumeScale, true);
			loopingAudioSource2.Tag = SoundManager.persistTag;
			sources.Add(loopingAudioSource2);
			if (persist)
			{
				if (!source.gameObject.name.StartsWith("PersistedBySoundManager-"))
				{
					source.gameObject.name = string.Concat(new object[]
					{
						"PersistedBySoundManager-",
						source.gameObject.name,
						"-",
						source.gameObject.GetInstanceID()
					});
				}
				source.gameObject.transform.parent = null;
				UnityEngine.Object.DontDestroyOnLoad(source.gameObject);
				SoundManager.persistedSounds.Add(loopingAudioSource2);
			}
		}

		private static void StopLooping(AudioSource source, List<LoopingAudioSource> sources)
		{
			foreach (LoopingAudioSource current in sources)
			{
				if (current.AudioSource == source)
				{
					current.Stop();
					source = null;
					break;
				}
			}
			if (source != null)
			{
				source.Stop();
			}
		}

		public static void PlayOneShotSound(AudioSource source, AudioClip clip)
		{
			SoundManager.PlayOneShotSound(source, clip, 1f);
		}

		public static void PlayOneShotSound(AudioSource source, AudioClip clip, float volumeScale)
		{
			SoundManager.EnsureCreated();
			List<float> list;
			if (!SoundManager.soundsOneShot.TryGetValue(clip, out list))
			{
				list = new List<float>();
				SoundManager.soundsOneShot[clip] = list;
			}
			else if (list.Count == SoundManager.MaxDuplicateAudioClips)
			{
				return;
			}
			float num = 3.40282347E+38f;
			float num2 = -3.40282347E+38f;
			foreach (float b in list)
			{
				num = Mathf.Min(num, b);
				num2 = Mathf.Max(num2, b);
			}
			float num3 = volumeScale * SoundManager.soundVolume;
			if (num2 > 0.5f)
			{
				num3 = (num + num2) / (float)(list.Count + 2);
			}
			list.Add(num3);
			source.PlayOneShot(clip, num3);
			SoundManager.instance.StartCoroutine(SoundManager.RemoveVolumeFromClip(clip, num3));
		}

		public static void PlayLoopingSound(AudioSource source)
		{
			SoundManager.PlayLoopingSound(source, 1f, 1f);
		}

		public static void PlayLoopingSound(AudioSource source, float volumeScale, float fadeSeconds)
		{
			SoundManager.PlayLooping(source, SoundManager.sounds, volumeScale, fadeSeconds, false, false);
			SoundManager.UpdateSounds();
		}

		public static void PlayOneShotMusic(AudioSource source, AudioClip clip)
		{
			SoundManager.PlayOneShotMusic(source, clip, 1f);
		}

		public static void PlayOneShotMusic(AudioSource source, AudioClip clip, float volumeScale)
		{
			SoundManager.EnsureCreated();
			int num = SoundManager.musicOneShot.IndexOf(source);
			if (num >= 0)
			{
				SoundManager.musicOneShot.RemoveAt(num);
			}
			source.PlayOneShot(clip, volumeScale * SoundManager.musicVolume);
			SoundManager.musicOneShot.Add(source);
		}

		public static void PlayLoopingMusic(AudioSource source)
		{
			SoundManager.PlayLoopingMusic(source, 1f, 1f, false);
		}

		public static void PlayLoopingMusic(AudioSource source, float volumeScale, float fadeSeconds, bool persist)
		{
			SoundManager.PlayLooping(source, SoundManager.music, volumeScale, fadeSeconds, persist, true);
			SoundManager.UpdateMusic();
		}

		public static void StopLoopingSound(AudioSource source)
		{
			SoundManager.StopLooping(source, SoundManager.sounds);
		}

		public static void StopLoopingMusic(AudioSource source)
		{
			SoundManager.StopLooping(source, SoundManager.music);
		}

		public static void StopAll()
		{
			SoundManager.StopAllLoopingSounds();
			SoundManager.StopNonLoopingSounds();
		}

		public static void StopAllLoopingSounds()
		{
			foreach (LoopingAudioSource current in SoundManager.sounds)
			{
				current.Stop();
			}
			foreach (LoopingAudioSource current2 in SoundManager.music)
			{
				current2.Stop();
			}
		}

		public static void StopNonLoopingSounds()
		{
			foreach (AudioSource current in SoundManager.musicOneShot)
			{
				current.Stop();
			}
		}

		public static void PauseAll()
		{
			foreach (LoopingAudioSource current in SoundManager.sounds)
			{
				current.Pause();
			}
			foreach (LoopingAudioSource current2 in SoundManager.music)
			{
				current2.Pause();
			}
		}

		public static void ResumeAll()
		{
			foreach (LoopingAudioSource current in SoundManager.sounds)
			{
				current.Resume();
			}
			foreach (LoopingAudioSource current2 in SoundManager.music)
			{
				current2.Resume();
			}
		}
	}
}
