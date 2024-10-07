using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Napalm : Projectile
{
	private sealed class _OnTriggerEnter2D_c__AnonStorey0
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		Enemy tt = other.GetComponent<Enemy>();
		if (tt)
		{
			if (!this.isInCollision)
			{
				this.isInCollision = true;
				Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 2f);
				int num = (from e in array
				where e.GetComponent<Enemy>() != tt && e.GetComponent<Enemy>()
				select e).Count<Collider2D>();
				Collider2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Collider2D collider2D = array2[i];
					Enemy component = collider2D.GetComponent<Enemy>();
					if (collider2D.GetComponent<Enemy>())
					{
						int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
						if (component == tt)
						{
							SoundController.instance.PlaySoundItem4();
							component.CallFlash((double)(BaseValue.napalm_base_damage * (long)coefLevel_ / 7L), BaseValue.napalm_base_damage * (long)coefLevel_ / 7L, ProjectileType.Non_Projectile);
						}
						else
						{
							component.CallFlash((double)((long)((float)(BaseValue.napalm_base_damage * (long)coefLevel_) / (100f / (float)BaseValue.damage_percent_item * (float)num) / 7f)), (long)((float)(BaseValue.napalm_base_damage * (long)coefLevel_) / (100f / (float)BaseValue.damage_percent_item * (float)num) / 7f), ProjectileType.Non_Projectile);
						}
					}
				}
				GameObject pooledObject = FirePooler.instance.GetPooledObject();
				pooledObject.SetActive(true);
				pooledObject.transform.position = base.transform.position;
				GameObject pooledObject2 = FirePooler.instance.GetPooledObject();
				pooledObject2.SetActive(true);
				pooledObject2.transform.position = base.transform.position;
				this.DropFire(pooledObject);
				this.DropFire(pooledObject2);
				GameObject pooledObject3 = ParticleObjectPooler.instance.GetPooledObject("item4_particle");
				pooledObject3.SetActive(true);
				pooledObject3.transform.position = base.transform.position;
				base.gameObject.SetActive(false);
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
				SoundController.instance.PlaySoundItem4();
				this.isInCollision = true;
				Collider2D[] array3 = Physics2D.OverlapCircleAll(base.transform.position, 2f);
				int num2 = (from e in array3
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
						component2.CallFlash((double)((long)((float)(BaseValue.napalm_base_damage * (long)coefLevel_2) / (100f / (float)BaseValue.damage_percent_item * (float)num2 * 7f))), (long)((float)BaseValue.coin_per_item4_hit / (100f / (float)BaseValue.damage_percent_item * 7f * (float)num2)), ProjectileType.Non_Projectile);
					}
				}
				GameObject pooledObject4 = ParticleObjectPooler.instance.GetPooledObject("item4_particle");
				pooledObject4.SetActive(true);
				pooledObject4.transform.position = base.transform.position;
				base.gameObject.SetActive(false);
			}
			else
			{
				this.isInCollision = false;
			}
		}
	}

	private void DropFire(GameObject obj)
	{
		Rigidbody2D component = obj.GetComponent<Rigidbody2D>();
		Vector3 position = DynamicGrid.instance.transform.position;
		Vector3 position2 = obj.transform.position;
		position.x = UnityEngine.Random.Range(position2.x + 2f, position2.x + 10f);
		Vector3 v = this.CalculateVelocity(position, position2, UnityEngine.Random.Range(0.7f, 1.2f));
		component.velocity = v;
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
}
