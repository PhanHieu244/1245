using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DigitalRuby.SoundManagerNamespace
{
	public class SoundManagerDemo : MonoBehaviour
	{
		public Slider SoundSlider;

		public Slider MusicSlider;

		public InputField SoundCountTextBox;

		public Toggle PersistToggle;

		public AudioSource[] SoundAudioSources;

		public AudioSource[] MusicAudioSources;

		private void PlaySound(int index)
		{
			int num;
			if (!int.TryParse(this.SoundCountTextBox.text, out num))
			{
				num = 1;
			}
			while (num-- > 0)
			{
				this.SoundAudioSources[index].PlayOneShotSoundManaged(this.SoundAudioSources[index].clip);
			}
		}

		private void PlayMusic(int index)
		{
			this.MusicAudioSources[index].PlayLoopingMusicManaged(1f, 1f, this.PersistToggle.isOn);
		}

		private void CheckPlayKey()
		{
			if (this.SoundCountTextBox.isFocused)
			{
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.PlaySound(0);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.PlaySound(1);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.PlaySound(2);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.PlaySound(3);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
			{
				this.PlaySound(4);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.PlaySound(5);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha7))
			{
				this.PlaySound(6);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha8))
			{
				this.PlayMusic(0);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha9))
			{
				this.PlayMusic(1);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha0))
			{
				this.PlayMusic(2);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.A))
			{
				this.PlayMusic(3);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.R))
			{
				UnityEngine.Debug.LogWarning("Reloading level");
				if (!this.PersistToggle.isOn)
				{
					SoundManager.StopAll();
				}
				SceneManager.LoadScene(0, LoadSceneMode.Single);
			}
		}

		private void Start()
		{
		}

		private void Update()
		{
			this.CheckPlayKey();
		}

		public void SoundVolumeChanged()
		{
			SoundManager.SoundVolume = this.SoundSlider.value;
		}

		public void MusicVolumeChanged()
		{
			SoundManager.MusicVolume = this.MusicSlider.value;
		}
	}
}
