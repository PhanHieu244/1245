using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Item5Projectile : Projectile
{
	private sealed class _OnTriggerEnter2D_c__AnonStorey1
	{
		internal Enemy tt;

		internal bool __m__0(Collider2D e)
		{
			return e.GetComponent<Enemy>() != this.tt && e.GetComponent<Enemy>();
		}

		internal bool __m__1(Collider2D e)
		{
			return e.GetComponent<Enemy>() != this.tt && e.GetComponent<Enemy>();
		}
	}

	private sealed class _HideProjectile_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Sprite _sprite___0;

		internal Item5Projectile _this;

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

		public _HideProjectile_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._sprite___0 = this._this._renderer.sprite;
				this._this._renderer.sprite = null;
				this._this._particle.Stop();
				this._this._rigid.constraints = RigidbodyConstraints2D.FreezeAll;
				this._current = new WaitUntil(() => !this._this._particle.IsAlive());
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this._rigid.constraints = RigidbodyConstraints2D.None;
				this._this._renderer.sprite = this._sprite___0;
				this._this.gameObject.SetActive(false);
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

		internal bool __m__0()
		{
			return !this._this._particle.IsAlive();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Enemy tt = other.GetComponent<Enemy>();
		if (tt)
		{
			if (!this.isInCollision)
			{
				this.isInCollision = true;
				Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 2f);
				int num = 6;
				int num2 = (from e in array
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				Collider2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Collider2D collider2D = array2[i];
					int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
					Enemy component = collider2D.GetComponent<Enemy>();
					if (collider2D.GetComponent<Enemy>())
					{
						if (component == tt)
						{
							SoundController.instance.PlaySoundItem5();
							component.CallFlash((double)(BaseValue.item5_base_damage * (long)coefLevel_ / (long)num), BaseValue.coin_per_item5_hit / (long)num, ProjectileType.Non_Projectile);
						}
						else
						{
							component.CallFlash((double)((long)((float)(BaseValue.item5_base_damage * (long)coefLevel_) / (100f / (float)BaseValue.damage_percent_item * (float)num2) / (float)num)), (long)((float)BaseValue.coin_per_item5_hit / (100f / (float)BaseValue.damage_percent_item * (float)num * (float)num2)), ProjectileType.Non_Projectile);
						}
					}
				}
				GameObject pooledObject = ParticleObjectPooler.instance.GetPooledObject("item5_particle");
				pooledObject.SetActive(true);
				pooledObject.transform.position = base.transform.position;
				base.StartCoroutine(this.HideProjectile());
			}
			else
			{
				this.isInCollision = false;
			}
		}
		if (other.tag == "Ground")
		{
			if (!this.isInCollision)
			{
				this.isInCollision = true;
				int num3 = 6;
				SoundController.instance.PlaySoundItem5();
				Collider2D[] array3 = Physics2D.OverlapCircleAll(base.transform.position, 2f);
				int num4 = (from e in array3
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				Collider2D[] array4 = array3;
				for (int j = 0; j < array4.Length; j++)
				{
					Collider2D collider2D2 = array4[j];
					Enemy component2 = collider2D2.GetComponent<Enemy>();
					if (component2)
					{
						int coefLevel_2 = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
						component2.CallFlash((double)((long)((float)(BaseValue.item5_base_damage * (long)coefLevel_2) / (100f / (float)BaseValue.damage_percent_item * (float)num4 * (float)num3))), BaseValue.coin_per_item5_hit / (long)(2 * num3 * num4), ProjectileType.Non_Projectile);
					}
				}
				GameObject pooledObject2 = ParticleObjectPooler.instance.GetPooledObject("item5_particle");
				pooledObject2.SetActive(true);
				pooledObject2.transform.position = base.transform.position;
				base.StartCoroutine(this.HideProjectile());
			}
			else
			{
				this.isInCollision = false;
			}
		}
	}

	public IEnumerator HideProjectile()
	{
		Item5Projectile._HideProjectile_c__Iterator0 _HideProjectile_c__Iterator = new Item5Projectile._HideProjectile_c__Iterator0();
		_HideProjectile_c__Iterator._this = this;
		return _HideProjectile_c__Iterator;
	}
}
