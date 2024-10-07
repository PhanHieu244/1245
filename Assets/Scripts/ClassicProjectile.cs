using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClassicProjectile : Projectile
{
	private sealed class _WaitForTrailEnd_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal TrailRenderer _trail___0;

		internal SpriteRenderer _sprRenderer___0;

		internal ClassicProjectile _this;

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

		public _WaitForTrailEnd_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._trail___0 = this._this.GetComponent<TrailRenderer>();
				this._sprRenderer___0 = this._this.GetComponent<SpriteRenderer>();
				if (this._trail___0 == null)
				{
					this._this.gameObject.SetActive(false);
					this._current = 0;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				break;
			case 1u:
				break;
			case 2u:
				this._sprRenderer___0.enabled = true;
				this._this.gameObject.SetActive(false);
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._sprRenderer___0 != null)
			{
				this._sprRenderer___0.enabled = false;
			}
			this._current = new WaitForSeconds(this._trail___0.time);
			if (!this._disposing)
			{
				this._PC = 2;
			}
			return true;
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

	public TrailRenderer trail;

	public bool isMainBullet;

	public bool isVisible = true;

	public override void Awake()
	{
		this._renderer = base.GetComponent<SpriteRenderer>();
		this.trail = base.GetComponent<TrailRenderer>();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("NongSung"))
		{
			if (this.isVisible)
			{
				if (this.isMainBullet)
				{
					Vector2 vector = base.transform.position - other.transform.position;
					float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
					Gun.instance.transform.rotation = rotation;
				}
				this.trail.time = 0.07f;
				this._renderer.color = Utilities.hexToColor("#FFFFFF");
			}
			else
			{
				if (this.isMainBullet)
				{
					Vector2 vector2 = base.transform.position - other.transform.position;
					float angle2 = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
					Quaternion rotation2 = Quaternion.AngleAxis(angle2, Vector3.forward);
					Gun.instance.transform.rotation = rotation2;
				}
				base.gameObject.SetActive(false);
			}
		}
	}

	private static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0f, 0f, Mathf.Atan2(forward.y, forward.x) * 57.29578f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Enemy component = other.GetComponent<Enemy>();
		if (component)
		{
			if (component.isStatic)
			{
				DynamicGrid.instance.AddStack();
			}
			else
			{
				DynamicGrid.instance.ResetStack();
			}
			if (!this.isInCollision)
			{
				this.isInCollision = true;
				component.CallFlash((double)Gun.instance.Power, 5L, ProjectileType.Projectile);
				base.gameObject.SetActive(false);
				this.PlayParticleDestroy(base.transform.position);
			}
			else
			{
				this.isInCollision = false;
			}
		}
		if (other.tag == "Ground")
		{
			base.gameObject.SetActive(false);
			this.PlayParticleDestroy(base.transform.position);
		}
	}

	public void PlayParticleDestroy(Vector3 pos)
	{
		GameObject pooledObject = ParticleBulletHit.instance.GetPooledObject();
		pooledObject.SetActive(true);
		pooledObject.transform.position = new Vector3(pos.x + 0.1f, pos.y, -9f);
	}

	public IEnumerator WaitForTrailEnd()
	{
		ClassicProjectile._WaitForTrailEnd_c__Iterator0 _WaitForTrailEnd_c__Iterator = new ClassicProjectile._WaitForTrailEnd_c__Iterator0();
		_WaitForTrailEnd_c__Iterator._this = this;
		return _WaitForTrailEnd_c__Iterator;
	}

	public virtual void OnBecameInvisible()
	{
		base.gameObject.SetActive(false);
	}
}
