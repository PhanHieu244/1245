using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UB;
using UnityEngine;

public class CompleteCameraController : MonoBehaviour
{
	private sealed class _FadeOutFogChangeTank_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal CompleteCameraController _this;

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

		public _FadeOutFogChangeTank_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.d2FogsNoiseTexPE.Density = 5f;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (this._this.d2FogsNoiseTexPE.Density >= 0f)
			{
				this._this.d2FogsNoiseTexPE.Density -= Time.deltaTime * 5f;
				this._current = new WaitForSeconds(Time.deltaTime);
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

	public static CompleteCameraController instance;

	private Tank tank;

	[SerializeField]
	private GameObject prefab_fog;

	[SerializeField]
	private Transform startEffectFogPos;

	[SerializeField]
	private D2FogsPE d2FogsNoiseTexPE;

	private GameObject fogEffect;

	private Vector3 offset;

	public bool isFollowTank;

	private static TweenCallback __f__am_cache0;

	private void Start()
	{
		CompleteCameraController.instance = this;
		if ((float)Screen.height * 1f / (float)Screen.width >= 0.7f)
		{
			Camera.main.orthographicSize = 12f;
			Camera.main.transform.position += Vector3.up * 2f;
		}
		if (Tank.instance)
		{
			this.tank = Tank.instance;
			this.offset = base.transform.position - this.tank.transform.position;
			this.isFollowTank = true;
		}
		this.d2FogsNoiseTexPE.enabled = false;
	}

	public void SetTank(Tank _tank)
	{
		this.tank = _tank;
		this.offset = base.transform.position - this.tank.transform.position;
		this.isFollowTank = true;
	}

	private void LateUpdate()
	{
		if (this.isFollowTank)
		{
			base.transform.position = this.tank.transform.position + this.offset;
		}
	}

	public void FadeInFog()
	{
		this.ShowFogEffect();
		if (this.fogEffect)
		{
			base.Invoke("ChangeBackground", 1.875f);
			this.fogEffect.transform.DOLocalMoveX(this.fogEffect.transform.localPosition.x - 277.2f, 5f, false).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this.fogEffect.gameObject);
			});
		}
	}

	public void ChangeBackground()
	{
		GameController.instance.ChangeBackground();
	}

	public void ResetGame()
	{
		GameController.instance.ResetGame();
		this.ChangeBackground();
	}

	public void FadeOutFog()
	{
		if (this.fogEffect)
		{
			this.fogEffect.transform.DOLocalMoveX(this.fogEffect.transform.localPosition.x - 138.6f, 3f, false).OnStart(delegate
			{
				GameController.instance.ChangeBackground();
			}).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this.fogEffect.gameObject);
			}).SetDelay(0.5f);
		}
	}

	public void FadeInFogToChangeTank()
	{
		this.ShowFogEffect();
		if (this.fogEffect)
		{
			base.Invoke("ResetGame", 1.5f);
			this.fogEffect.transform.DOLocalMoveX(this.fogEffect.transform.localPosition.x - 277.2f, 5f, false).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this.fogEffect.gameObject);
			});
		}
	}

	public IEnumerator FadeOutFogChangeTank()
	{
		CompleteCameraController._FadeOutFogChangeTank_c__Iterator0 _FadeOutFogChangeTank_c__Iterator = new CompleteCameraController._FadeOutFogChangeTank_c__Iterator0();
		_FadeOutFogChangeTank_c__Iterator._this = this;
		return _FadeOutFogChangeTank_c__Iterator;
	}

	private void ShowFogEffect()
	{
		Vector3 position = this.startEffectFogPos.position;
		position.z = 0f;
		this.fogEffect = UnityEngine.Object.Instantiate<GameObject>(this.prefab_fog, position, Quaternion.identity, base.transform);
	}
}
