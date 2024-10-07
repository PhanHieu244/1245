using DigitalRuby.SoundManagerNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	private sealed class _FadeOut_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal AudioSource audioSource;

		internal float _startVolume___0;

		internal float _t___0;

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

		public _FadeOut_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("FadeOut");
				this._startVolume___0 = this.audioSource.volume;
				this._t___0 = this._startVolume___0;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (this._t___0 > 0f)
			{
				this._t___0 -= Time.deltaTime * 2f;
				this.audioSource.volume = this._t___0;
				this._current = new WaitForSeconds(0f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this.audioSource.Stop();
			this.audioSource.volume = this._startVolume___0;
			this._PC = -1;
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

	private sealed class _FadeIn_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal AudioSource audioSource;

		internal float _startVolume___0;

		internal float _t___0;

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

		public _FadeIn_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("FadeIn");
				this._startVolume___0 = this.audioSource.volume;
				this.audioSource.PlayOneShotSoundManaged(this.audioSource.clip, 0f);
				this._t___0 = 0f;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (this._t___0 <= 1f)
			{
				this._t___0 += Time.deltaTime;
				this.audioSource.volume = this._t___0;
				this._current = new WaitForSeconds(0f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
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

	public static SoundController instance;

	public AudioSource[] SoundAudioSources;

	public AudioSource[] MusicAudioSources;

	public bool isSoundEnable;

	private void PlaySound(int index, float volume)
	{
		if (this.isSoundEnable)
		{
			this.SoundAudioSources[index].PlayOneShotSoundManaged(this.SoundAudioSources[index].clip, volume);
		}
	}

	private void PlayLoopSound(int index, float volume)
	{
		if (this.isSoundEnable)
		{
			AudioSource audioSource = this.SoundAudioSources[index];
			audioSource.volume = volume;
			audioSource.PlayLoopingSoundManaged();
		}
	}

	private void _PlaySound(int index, float volume)
	{
		if (this.isSoundEnable)
		{
			this.SoundAudioSources[index].PlayOneShot(this.SoundAudioSources[index].clip, volume);
		}
	}

	private void StopSound(int index)
	{
		this.SoundAudioSources[index].Stop();
	}

	public void PlaySoundButtonDefault()
	{
		this.PlaySound(2, 0.8f);
	}

	public void PlaySoundTankFire()
	{
		this.PlaySound(7, 0.15f);
	}

	public void PlaySoundButtonUpgrade()
	{
		this.PlaySound(4, 0.8f);
	}

	public void PlaySoundReachLevel100()
	{
		this.PlaySound(9, 1f);
	}

	public void PlaySoundDiamond()
	{
		this.PlaySound(6, 0.7f);
	}

	public void PlaySoundBossFight()
	{
		this.PlaySound(1, 1f);
	}

	public void PlaySoundSpeedBooster()
	{
		this.PlaySound(10, 1f);
	}

	public void PlaySoundTimeWarp()
	{
		this.PlaySound(12, 1f);
	}

	public void PlaySoundCooldown()
	{
		this.PlaySound(5, 1f);
	}

	public void PlaySoundMoneyBooster()
	{
		this.PlaySound(8, 1f);
	}

	public void PlaySoundUnlockNewItem()
	{
		this.PlaySound(16, 0.5f);
	}

	public void PlaySoundEnemyDestroy()
	{
		this.PlaySound(13, 1f);
	}

	public void PlaySoundUseItem4Launch()
	{
		this.PlaySound(24, 1f);
	}

	public void PlaySoundUseItem1()
	{
		this.PlaySound(29, 1f);
	}

	public void PlaySoundUseItem123()
	{
		this.PlaySound(25, 1f);
	}

	public void PlaySoundEndBonus()
	{
		this.PlaySound(31, 1f);
	}

	public void PlaySoundUseItem3()
	{
		this.PlaySound(30, 1f);
	}

	public void PlaySoundUseItem4()
	{
		this.PlaySound(24, 1f);
	}

	public void PlaySoundItem5Launch()
	{
		this.PlaySound(33, 1f);
	}

	public void PlaySoundItem5()
	{
		this.PlaySound(32, 1f);
	}

	public void PlaySoundItem6Launch()
	{
		this.PlaySound(35, 1f);
	}

	public void StopSoundItem6Launch()
	{
		this.StopSound(35);
	}

	public void PlaySoundItem6()
	{
		this.PlaySound(34, 1f);
	}

	public void PlaySoundButtonDisable()
	{
		this.PlaySound(3, 0.8f);
	}

	public void PlaySoundBonusLevel()
	{
		this.PlaySound(27, 1f);
	}

	public void PlaySoundPassLevel()
	{
		this.PlaySound(28, 1f);
	}

	public void PlaySoundItem1()
	{
		this.PlaySound(19, 1f);
	}

	public void PlaySoundItem2()
	{
		this._PlaySound(20, 1f);
	}

	public void PlaySoundItem2Split()
	{
		this.PlaySound(26, 1f);
	}

	public void PlaySoundItem3()
	{
		this._PlaySound(21, 1f);
	}

	public void PlaySoundItem4()
	{
		this._PlaySound(22, 1f);
	}

	public void PlaySoundTankRunning()
	{
		if (this.isSoundEnable)
		{
			AudioSource source = this.SoundAudioSources[17];
			source.PlayLoopingSoundManaged(0.7f, 0.3f);
		}
	}

	public void PlaySoundSpeedBoosterUsing()
	{
		if (this.isSoundEnable)
		{
			AudioClip clip = this.SoundAudioSources[23].clip;
			float length = clip.length;
			base.InvokeRepeating("_PlaySoundUsingSpeedBooster", 0f, clip.length);
		}
	}

	public void PlaySoundItem7Firing()
	{
		if (this.isSoundEnable)
		{
			AudioClip clip = this.SoundAudioSources[36].clip;
			float length = clip.length;
			base.InvokeRepeating("_PlaySoundItem7Firing", 0f, clip.length);
		}
	}

	public void StopSoundSpeedBoosterUsing()
	{
		this.StopInvokeSoundUsingSpeedBooster();
	}

	public void StopSoundTankRunning()
	{
		if (this.isSoundEnable)
		{
			AudioSource audioSource = this.SoundAudioSources[17];
			base.StartCoroutine(SoundController.FadeOut(audioSource, 0.1f));
		}
	}

	public void TurnOffSound()
	{
		SoundManager.MusicVolume = 0f;
		SoundManager.SoundVolume = 0f;
		this.isSoundEnable = false;
	}

	private void TurnOffAllPlayingSound()
	{
		SoundManager.StopAll();
	}

	public void TurnOnSound()
	{
		SoundManager.MusicVolume = 1f;
		SoundManager.SoundVolume = 1f;
		this.isSoundEnable = true;
	}

	public bool CheckSoundEnable()
	{
		return GameController.instance.game.Check_sound_enable;
	}

	public void PlaySoundTimeUp()
	{
		if (this.isSoundEnable)
		{
			AudioClip clip = this.SoundAudioSources[0].clip;
			float length = clip.length;
			base.InvokeRepeating("_PlaySoundTimeUp", 0f, 1f);
		}
	}

	public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		SoundController._FadeOut_c__Iterator0 _FadeOut_c__Iterator = new SoundController._FadeOut_c__Iterator0();
		_FadeOut_c__Iterator.audioSource = audioSource;
		return _FadeOut_c__Iterator;
	}

	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
	{
		SoundController._FadeIn_c__Iterator1 _FadeIn_c__Iterator = new SoundController._FadeIn_c__Iterator1();
		_FadeIn_c__Iterator.audioSource = audioSource;
		return _FadeIn_c__Iterator;
	}

	public void _PlaySoundTimeUp()
	{
		this.PlaySound(0, 1f);
	}

	public void _PlaySoundUsingSpeedBooster()
	{
		this.PlaySound(23, 1f);
	}

	public void _PlaySoundItem7Firing()
	{
		this.PlaySound(36, 1f);
	}

	public void StopInvokeSoundUsingSpeedBooster()
	{
		if (base.IsInvoking("_PlaySoundUsingSpeedBooster"))
		{
			base.CancelInvoke("_PlaySoundUsingSpeedBooster");
		}
	}

	public void StopInvokeSoundTimeUp()
	{
		if (base.IsInvoking("_PlaySoundTimeUp"))
		{
			base.CancelInvoke("_PlaySoundTimeUp");
		}
	}

	public void StopInvokeSoundItem7Firing()
	{
		if (base.IsInvoking("_PlaySoundItem7Firing"))
		{
			base.CancelInvoke("_PlaySoundItem7Firing");
		}
	}

	private void Awake()
	{
		SoundController.instance = this;
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void Start()
	{
		SoundManager.MaxDuplicateAudioClips = 6;
	}

	private void Update()
	{
	}
}
