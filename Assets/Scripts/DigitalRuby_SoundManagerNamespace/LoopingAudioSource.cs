using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.SoundManagerNamespace
{
	public class LoopingAudioSource
	{
		private AudioSource _AudioSource_k__BackingField;

		private float _TargetVolume_k__BackingField;

		private float _OriginalTargetVolume_k__BackingField;

		private bool _Stopping_k__BackingField;

		private bool _Persist_k__BackingField;

		private int _Tag_k__BackingField;

		private float startVolume;

		private float startMultiplier;

		private float stopMultiplier;

		private float currentMultiplier;

		private float timestamp;

		private bool paused;

		public AudioSource AudioSource
		{
			get;
			private set;
		}

		public float TargetVolume
		{
			get;
			set;
		}

		public float OriginalTargetVolume
		{
			get;
			private set;
		}

		public bool Stopping
		{
			get;
			private set;
		}

		public bool Persist
		{
			get;
			private set;
		}

		public int Tag
		{
			get;
			set;
		}

		public LoopingAudioSource(AudioSource audioSource, float startMultiplier, float stopMultiplier, bool persist)
		{
			this.AudioSource = audioSource;
			if (audioSource != null)
			{
				this.AudioSource.loop = true;
				this.AudioSource.volume = 0f;
				this.AudioSource.Stop();
			}
			this.currentMultiplier = startMultiplier;
			this.startMultiplier = startMultiplier;
			this.stopMultiplier = stopMultiplier;
			this.Persist = persist;
		}

		public void Play(bool isMusic)
		{
			this.Play(1f, isMusic);
		}

		public bool Play(float targetVolume, bool isMusic)
		{
			if (this.AudioSource != null)
			{
				this.AudioSource.volume = (this.startVolume = ((!this.AudioSource.isPlaying) ? 0f : this.AudioSource.volume));
				this.AudioSource.loop = true;
				this.currentMultiplier = this.startMultiplier;
				this.OriginalTargetVolume = targetVolume;
				this.TargetVolume = targetVolume;
				this.Stopping = false;
				this.timestamp = 0f;
				if (!this.AudioSource.isPlaying)
				{
					this.AudioSource.Play();
					return true;
				}
			}
			return false;
		}

		public void Stop()
		{
			if (this.AudioSource != null && this.AudioSource.isPlaying && !this.Stopping)
			{
				this.startVolume = this.AudioSource.volume;
				this.TargetVolume = 0f;
				this.currentMultiplier = this.stopMultiplier;
				this.Stopping = true;
				this.timestamp = 0f;
			}
		}

		public void Pause()
		{
			if (this.AudioSource != null && !this.paused && this.AudioSource.isPlaying)
			{
				this.paused = true;
				this.AudioSource.Pause();
			}
		}

		public void Resume()
		{
			if (this.AudioSource != null && this.paused)
			{
				this.paused = false;
				this.AudioSource.UnPause();
			}
		}

		public bool Update()
		{
			if (!(this.AudioSource != null) || !this.AudioSource.isPlaying)
			{
				return !this.paused;
			}
			float num = Mathf.Lerp(this.startVolume, this.TargetVolume, (this.timestamp += Time.deltaTime) / this.currentMultiplier);
			this.AudioSource.volume = num;
			if (num == 0f && this.Stopping)
			{
				this.AudioSource.Stop();
				this.Stopping = false;
				return true;
			}
			return false;
		}
	}
}
