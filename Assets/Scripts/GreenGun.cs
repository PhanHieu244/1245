using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GreenGun : Gun
{
	private sealed class _FireBullet_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Vector3 __muzzlePosUp___0;

		internal Vector3 __muzzlePosDown___0;

		internal Vector2 velocity;

		internal float _angle___0;

		internal Quaternion _rotation___1;

		internal Vector3 pos;

		internal float gravity;

		internal float timeScale;

		internal float distance;

		internal Quaternion _rotation___2;

		internal GreenGun _this;

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

		public _FireBullet_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				Tank.instance._animation.Stop();
				Tank.instance._animation.Play("FireAnimUp");
				this.__muzzlePosUp___0 = this._this.muzzlePosUp.transform.position;
				this.__muzzlePosDown___0 = this._this.muzzlePosDown.transform.position;
				this._current = new WaitForSeconds(0.05f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._angle___0 = Mathf.Atan2(this.velocity.y, this.velocity.x) * 57.29578f;
				if (this._angle___0 > 45f)
				{
					this._angle___0 -= 5f;
					this._rotation___1 = Quaternion.AngleAxis(this._angle___0, Vector3.forward);
					this._this.transform.rotation = this._rotation___1;
					this._this.DrawBullet(this.pos, this.velocity, this.gravity, this.timeScale, true, false);
					this._this.DrawBullet(this.pos - Vector3.right * this.distance * 0.8f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
					this._this.DrawMuzzle(this.__muzzlePosUp___0 + Vector3.up * this.distance, this._this.muzzlePosUp);
					this._current = new WaitForSeconds(0.08f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				this._rotation___2 = Quaternion.AngleAxis(this._angle___0, Vector3.forward);
				this._this.transform.rotation = this._rotation___2;
				this._this.DrawBullet(this.pos, this.velocity, this.gravity, this.timeScale, true, false);
				this._this.DrawBullet(this.pos + Vector3.up * this.distance * 0.8f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this.DrawMuzzle(this.__muzzlePosUp___0 + Vector3.up * this.distance, this._this.muzzlePosUp);
				this._current = new WaitForSeconds(0.08f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 2u:
				this._this.DrawBullet(this.pos + Vector3.right * this.distance * 0.8f - Vector3.up * this.distance * 0.8f, this.velocity * 0.98f, this.gravity * 0.98f, this.timeScale * 0.9f, false, true);
				this._this.DrawMuzzle(this.__muzzlePosDown___0 - Vector3.up * this.distance, this._this.muzzlePosDown);
				break;
			case 3u:
				this._this.DrawBullet(this.pos - Vector3.up * this.distance * 0.8f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this.DrawMuzzle(this.__muzzlePosDown___0 - Vector3.up * this.distance, this._this.muzzlePosDown);
				break;
			default:
				return false;
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

	private sealed class _FireBullet2_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Vector3 __muzzlePosUp___0;

		internal Vector3 __muzzlePosDown___0;

		internal Vector2 velocity;

		internal float _angle___0;

		internal Vector3 pos;

		internal float gravity;

		internal float timeScale;

		internal float distance;

		internal GreenGun _this;

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

		public _FireBullet2_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				Tank.instance._animation.Stop();
				Tank.instance._animation.Play("FireAnimUp");
				this.__muzzlePosUp___0 = this._this.muzzlePosUp.transform.position;
				this.__muzzlePosDown___0 = this._this.muzzlePosDown.transform.position;
				this._current = new WaitForSeconds(0.05f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._angle___0 = Mathf.Atan2(this.velocity.y, this.velocity.x) * 57.29578f;
				if (this._angle___0 > 45f)
				{
					this._this._DrawBullet(this.pos, this.velocity, this.gravity, this.timeScale * 0.9f, true, false);
					this._this._DrawBullet(this.pos - Vector3.right * this.distance * 0.5f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
					this._this._DrawBullet(this.pos - Vector3.right * this.distance * 1.1f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
					this._this.DrawMuzzle(this.__muzzlePosUp___0 + Vector3.up * this.distance, this._this.muzzlePosUp);
					this._current = new WaitForSeconds(0.08f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				this._this._DrawBullet(this.pos, this.velocity, this.gravity, this.timeScale * 0.9f, true, false);
				this._this._DrawBullet(this.pos + Vector3.up * this.distance * 0.5f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this._DrawBullet(this.pos + Vector3.up * this.distance * 1.1f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this.DrawMuzzle(this.__muzzlePosUp___0 + Vector3.up * this.distance, this._this.muzzlePosUp);
				this._current = new WaitForSeconds(0.08f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 2u:
				this._this._DrawBullet(this.pos + Vector3.right * this.distance * 1f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this._DrawBullet(this.pos + Vector3.right * this.distance * 1.6f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this.DrawMuzzle(this.__muzzlePosDown___0 - Vector3.up * this.distance, this._this.muzzlePosDown);
				break;
			case 3u:
				this._this._DrawBullet(this.pos - Vector3.up * this.distance * 0.7f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this._DrawBullet(this.pos - Vector3.up * this.distance * 1.3f, this.velocity, this.gravity, this.timeScale * 0.9f, false, true);
				this._this.DrawMuzzle(this.__muzzlePosDown___0 - Vector3.up * this.distance, this._this.muzzlePosDown);
				break;
			default:
				return false;
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

	private sealed class _DrawMuzzle_c__AnonStorey2
	{
		internal GameObject particle;

		internal void __m__0()
		{
			this.particle.transform.SetParent(ParticleMuzzleFlashPooler.instance.transform);
			this.particle.SetActive(false);
		}
	}

	private int fireState;

	[SerializeField]
	private GameObject barrelUp;

	[SerializeField]
	private GameObject barrelDown;

	[SerializeField]
	private Transform muzzlePosUp;

	[SerializeField]
	private Transform muzzlePosDown;

	[SerializeField]
	private Transform startPosUp;

	[SerializeField]
	private Transform startPosDown;

	public void Awake()
	{
		Gun.instance = this;
	}

	public override void Fire(Vector2 touchPos)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
		if (array.Length != 0)
		{
			this.ShotBulletParabol(touchPos);
		}
	}

	public override void FirePerRate(Vector3 target)
	{
		this.ShotBulletParabol(target);
		this.state = Gun.State.Firing;
	}

	public override void FirePerRateWhenTap(Vector2 touchPos)
	{
		this.ShotBulletParabol(touchPos);
		this.state = Gun.State.Firing;
	}

	private void ShotBulletParabol(Vector2 curTargetPos)
	{
		float gravity = 1f;
		float timeScale = 0f;
		float y_distance_between_2_bullet = BaseValue.y_distance_between_2_bullet;
		Vector3 v = this._CalculateVeloc(base.transform.position, curTargetPos, (float)(base.BulletSpeed * (long)GameController.instance.factorSpeed), out gravity, out timeScale);
		int level = CanvasUpgrade.instance.buttonUpgradeReloadSpeed.upgradeData.Level;
		if (level < BaseValue.level_add_bullet1_tank2)
		{
			base.StartCoroutine(this.FireBullet(base.transform.position, curTargetPos, v, gravity, y_distance_between_2_bullet, timeScale));
		}
		else if (level >= BaseValue.level_add_bullet1_tank2)
		{
			base.StartCoroutine(this.FireBullet2(base.transform.position, v, gravity, y_distance_between_2_bullet, timeScale));
		}
	}

	private IEnumerator FireBullet(Vector3 pos, Vector3 target, Vector2 velocity, float gravity, float distance, float timeScale)
	{
		GreenGun._FireBullet_c__Iterator0 _FireBullet_c__Iterator = new GreenGun._FireBullet_c__Iterator0();
		_FireBullet_c__Iterator.velocity = velocity;
		_FireBullet_c__Iterator.pos = pos;
		_FireBullet_c__Iterator.gravity = gravity;
		_FireBullet_c__Iterator.timeScale = timeScale;
		_FireBullet_c__Iterator.distance = distance;
		_FireBullet_c__Iterator._this = this;
		return _FireBullet_c__Iterator;
	}

	private IEnumerator FireBullet2(Vector3 pos, Vector2 velocity, float gravity, float distance, float timeScale)
	{
		GreenGun._FireBullet2_c__Iterator1 _FireBullet2_c__Iterator = new GreenGun._FireBullet2_c__Iterator1();
		_FireBullet2_c__Iterator.velocity = velocity;
		_FireBullet2_c__Iterator.pos = pos;
		_FireBullet2_c__Iterator.gravity = gravity;
		_FireBullet2_c__Iterator.timeScale = timeScale;
		_FireBullet2_c__Iterator.distance = distance;
		_FireBullet2_c__Iterator._this = this;
		return _FireBullet2_c__Iterator;
	}

	private void DrawMuzzle(Vector3 pos, Transform _transform)
	{
		GameObject particle = ParticleMuzzleFlashPooler.instance.GetPooledObject();
		particle.SetActive(true);
		particle.transform.SetParent(_transform);
		particle.transform.position = new Vector3(_transform.position.x, _transform.position.y, -9f);
		particle.transform.rotation = _transform.rotation;
		particle.transform.DOScaleX(particle.transform.localScale.x, 0.05f).OnComplete(delegate
		{
			particle.transform.SetParent(ParticleMuzzleFlashPooler.instance.transform);
			particle.SetActive(false);
		});
	}

	public void _DrawBullet(Vector3 pos, Vector2 velocity, float gravity, float timeScale, bool isMainBullet = false, bool isVisible = true)
	{
		GameObject gameObject = PoolerManager.instance.projectilePooler._GetPooledObject();
		gameObject.SetActive(true);
		ClassicProjectile component = gameObject.GetComponent<ClassicProjectile>();
		if (component.trail != null)
		{
			component.trail.Clear();
		}
		component.isVisible = isVisible;
		component.isMainBullet = isMainBullet;
		component.trail.time = 0f;
		component.GetRenderer().color = Color.clear;
		gameObject.transform.localScale = Vector3.one * 0.7f;
		gameObject.transform.position = pos;
		BallisticMotionForProjectile component2 = gameObject.GetComponent<BallisticMotionForProjectile>();
		component2.Initialize(pos, gravity, timeScale * 0.9f);
		component2.AddImpulse(velocity);
	}

	public void DrawBullet(Vector3 pos, Vector2 velocity, float gravity, float timeScale, bool isMainBullet = false, bool isVisible = true)
	{
		GameObject gameObject = PoolerManager.instance.projectilePooler._GetPooledObject();
		gameObject.SetActive(true);
		ClassicProjectile component = gameObject.GetComponent<ClassicProjectile>();
		if (component.trail != null)
		{
			component.trail.Clear();
		}
		component.isVisible = isVisible;
		component.isMainBullet = isMainBullet;
		component.trail.time = 0f;
		component.GetRenderer().color = Color.clear;
		gameObject.transform.position = pos;
		BallisticMotionForProjectile component2 = gameObject.GetComponent<BallisticMotionForProjectile>();
		component2.Initialize(pos, gravity, timeScale * 0.9f);
		component2.AddImpulse(velocity);
	}

	public Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
	{
		Vector3 vector = target - origin;
		Vector3 vector2 = vector;
		vector2.y = 0f;
		float y = vector.y;
		float magnitude = vector2.magnitude;
		float d = magnitude / time;
		float y2 = y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
		Vector3 vector3 = vector2.normalized;
		vector3 *= d;
		vector3.y = y2;
		return vector3;
	}

	public Vector3 _CalculateVeloc(Vector3 origin, Vector3 target, float speed, out float gravity, out float timeScale)
	{
		gravity = 1f;
		timeScale = 1f;
		target.z = origin.z;
		float max_height = (target.y - origin.y >= 0f) ? (target.y - origin.y) : 0f;
		Vector3 result;
		if (fts.solve_ballistic_arc_lateral(origin, speed, target, max_height, out result, out gravity, out timeScale))
		{
			float num = Vector2.Distance(origin, target);
			timeScale /= num / speed;
			return result;
		}
		return Vector3.zero;
	}

	public override void PlayFireAnimation()
	{
		Tank.instance.GetComponent<Animation>().Stop();
		if (this.fireState == 0)
		{
			this.fireState = 1;
			if (Tank.instance.GetComponent<Animation>())
			{
				Tank.instance.GetComponent<Animation>().Play("FireAnimUp");
			}
		}
		else
		{
			this.fireState = 0;
			if (Tank.instance.GetComponent<Animation>())
			{
				Tank.instance.GetComponent<Animation>().Play("FireAnimDown");
			}
		}
	}
}
