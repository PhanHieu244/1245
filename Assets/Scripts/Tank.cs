using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tank : MonoBehaviour
{
	private sealed class _WaitForParticleStop_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		private sealed class _WaitForParticleStop_c__AnonStorey1
		{
			internal ParticleSystem _particleSystem;

			internal bool __m__0()
			{
				return !this._particleSystem.IsAlive();
			}
		}

		internal ParticleSystem _particleSystem;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private Tank._WaitForParticleStop_c__Iterator0._WaitForParticleStop_c__AnonStorey1 _locvar0;

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

		public _WaitForParticleStop_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._locvar0 = new Tank._WaitForParticleStop_c__Iterator0._WaitForParticleStop_c__AnonStorey1();
				this._locvar0._particleSystem = this._particleSystem;
				this._current = new WaitUntil(new Func<bool>(this._locvar0.__m__0));
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._locvar0._particleSystem.gameObject.SetActive(false);
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

	[SerializeField]
	private ParticleSystem particle_lightning;

	[SerializeField]
	private ParticleSystem particle_smoke;

	public static Tank instance;

	protected float gravity;

	public float speed;

	protected Rigidbody2D myBody;

	public bool isMoving;

	public Animator animator;

	public Animation _animation;

	private float moveTime;

	public virtual void Move(float horizontalInput)
	{
		if (horizontalInput > 0f)
		{
			this.animator.SetFloat("Speed", horizontalInput);
		}
		Vector2 velocity = this.myBody.velocity;
		velocity.x = horizontalInput * this.speed;
		this.myBody.velocity = velocity;
	}

	public virtual void StopMove()
	{
		this.isMoving = false;
		this.particle_smoke.Stop();
		base.StartCoroutine(this.WaitForParticleStop(this.particle_smoke));
		this.animator.SetFloat("Speed", 0f);
		Vector2 zero = Vector2.zero;
		this.myBody.velocity = zero;
		this.myBody.constraints = RigidbodyConstraints2D.FreezePositionX;
		SoundController.instance.StopSoundTankRunning();
		GameController.instance.isDisableTouch = false;
	}

	public virtual void StartMove()
	{
		Gun.instance.transform.DORotate(new Vector3(0f, 0f, 10f), 0.3f, RotateMode.Fast);
		this.myBody.constraints = RigidbodyConstraints2D.None;
		GameController.instance.isDisableTouch = true;
		SoundController.instance.PlaySoundTankRunning();
		this.particle_smoke.gameObject.SetActive(true);
		this.isMoving = true;
	}

	public void StartMoveWithoutSound()
	{
		this.myBody.constraints = RigidbodyConstraints2D.None;
		GameController.instance.isDisableTouch = true;
		this.particle_smoke.gameObject.SetActive(true);
		this.isMoving = true;
	}

	private void Awake()
	{
		Tank.instance = this;
	}

	public virtual void Start()
	{
		this.myBody = base.GetComponent<Rigidbody2D>();
		this.StartMove();
		if (CanvasUpgrade.instance.buttonBoosterSpeed.IsBoosterUsed)
		{
		}
		this.animator = base.GetComponent<Animator>();
		this._animation = base.GetComponent<Animation>();
		this.myBody = base.GetComponent<Rigidbody2D>();
	}

	private IEnumerator WaitForParticleStop(ParticleSystem _particleSystem)
	{
		Tank._WaitForParticleStop_c__Iterator0 _WaitForParticleStop_c__Iterator = new Tank._WaitForParticleStop_c__Iterator0();
		_WaitForParticleStop_c__Iterator._particleSystem = _particleSystem;
		return _WaitForParticleStop_c__Iterator;
	}

	private void Update()
	{
		if (base.transform.eulerAngles != Vector3.zero)
		{
			base.transform.eulerAngles = Vector3.zero;
		}
		if (this.isMoving)
		{
			this.speed = 6f;
			this.moveTime += Time.deltaTime;
			this.Move(1f);
			float sqrMagnitude = (base.transform.position - DynamicGrid.instance.transform.position).sqrMagnitude;
			if (sqrMagnitude <= 306.25f)
			{
				this.moveTime = 0f;
				this.StopMove();
				if (GameController.instance && GameController.instance.isBonus)
				{
					GameController.instance.StartBonus();
				}
			}
		}
	}

	public void SetWriteDefault()
	{
	}

	public void ShowEffectLightning()
	{
		UnityEngine.Debug.Log("ShowEffectLightning");
		SoundController.instance.PlaySoundSpeedBoosterUsing();
		this.particle_lightning.gameObject.SetActive(true);
	}

	public void HideEffectLightning()
	{
		SoundController.instance.StopSoundSpeedBoosterUsing();
		this.particle_lightning.gameObject.SetActive(false);
	}
}
