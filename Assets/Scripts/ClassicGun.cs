using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClassicGun : Gun
{
	private sealed class _ShotBullet_c__AnonStorey0
	{
		internal GameObject particle;

		internal void __m__0()
		{
			this.particle.SetActive(false);
		}
	}

	private sealed class _ShotBulletParabol_c__AnonStorey1
	{
		internal GameObject particle;

		internal void __m__0()
		{
			this.particle.SetActive(false);
			this.particle.transform.SetParent(ParticleMuzzleFlashPooler.instance.transform);
		}
	}

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

	private void ShotBullet(Vector3 pos)
	{
		GameObject particle = ParticleMuzzleFlashPooler.instance.GetPooledObject();
		particle.SetActive(true);
		Vector2 vector = pos - base.transform.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		if (num > 70f)
		{
			vector = new Vector3(Mathf.Cos(1.22173047f) + base.transform.position.x, Mathf.Sin(1.22173047f) + base.transform.position.y) - base.transform.position;
			num = 70f;
		}
		if (num < 10f)
		{
			vector = new Vector3(Mathf.Cos(0.17453292f) + base.transform.position.x, Mathf.Sin(0.17453292f) + base.transform.position.y) - base.transform.position;
			num = 10f;
		}
		Quaternion rotation = Quaternion.AngleAxis(num, Vector3.forward);
		base.transform.rotation = rotation;
		Vector2 normalized = vector.normalized;
		GameObject pooledObject = PoolerManager.instance.projectilePooler.GetPooledObject();
		pooledObject.transform.position = this.muzzle.transform.position;
		pooledObject.transform.rotation = base.transform.rotation;
		pooledObject.SetActive(true);
		pooledObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
		pooledObject.GetComponent<Rigidbody2D>().AddForce(normalized * (float)base.BulletSpeed * (float)GameController.instance.factorSpeed);
		particle.transform.position = new Vector3(this.muzzle.transform.position.x, this.muzzle.transform.position.y, -9f);
		particle.transform.rotation = base.transform.rotation;
		particle.transform.DOMoveX(particle.transform.position.x, 0.1f, false).OnComplete(delegate
		{
			particle.SetActive(false);
		});
	}

	private void ShotBulletParabol(Vector2 curTargetPos)
	{
		float gravity = 1f;
		float num = 0f;
		float y_distance_between_2_bullet = BaseValue.y_distance_between_2_bullet;
		Vector3 v = this._CalculateVeloc(base.transform.position, curTargetPos, (float)(base.BulletSpeed * (long)GameController.instance.factorSpeed), out gravity, out num);
		float num2 = Mathf.Atan2(v.y, v.x) * 57.29578f;
		if (num2 > 45f)
		{
			int level = CanvasUpgrade.instance.buttonUpgradeReloadSpeed.upgradeData.Level;
			if (level < BaseValue.level_add_bullet1)
			{
				this.DrawBullet(base.transform.position, v, gravity, num * 0.9f, true, true);
			}
			else if (level >= BaseValue.level_add_bullet1 && level < BaseValue.level_add_bullet2)
			{
				this._DrawBullet(base.transform.position - Vector3.right * y_distance_between_2_bullet * 0.5f, v, gravity, num * 0.9f, false, true);
				this._DrawBullet(base.transform.position, v, gravity, num * 0.9f, true, false);
				this._DrawBullet(base.transform.position + Vector3.right * y_distance_between_2_bullet * 0.5f, v, gravity, num * 0.9f, false, true);
			}
			else if (level >= BaseValue.level_add_bullet2)
			{
				this._DrawBullet(base.transform.position - Vector3.right * y_distance_between_2_bullet * 0.7f, v, gravity, num * 0.9f, false, true);
				this._DrawBullet(base.transform.position, v, gravity, num * 0.9f, true, true);
				this._DrawBullet(base.transform.position + Vector3.right * y_distance_between_2_bullet * 0.7f, v, gravity, num * 0.9f, false, true);
			}
		}
		else
		{
			int level2 = CanvasUpgrade.instance.buttonUpgradeReloadSpeed.upgradeData.Level;
			if (level2 < BaseValue.level_add_bullet1)
			{
				this.DrawBullet(base.transform.position, v, gravity, num * 0.9f, true, true);
			}
			else if (level2 >= BaseValue.level_add_bullet1 && level2 < BaseValue.level_add_bullet2)
			{
				this._DrawBullet(base.transform.position - Vector3.up * y_distance_between_2_bullet * 0.4f, v, gravity, num * 0.9f, false, true);
				this._DrawBullet(base.transform.position, v, gravity, num * 0.9f, true, false);
				this._DrawBullet(base.transform.position + Vector3.up * y_distance_between_2_bullet * 0.4f, v, gravity, num * 0.9f, false, true);
			}
			else if (level2 >= BaseValue.level_add_bullet2)
			{
				this._DrawBullet(base.transform.position - Vector3.up * y_distance_between_2_bullet * 0.7f, v, gravity, num * 0.9f, false, true);
				this._DrawBullet(base.transform.position, v, gravity, num * 0.9f, true, true);
				this._DrawBullet(base.transform.position + Vector3.up * y_distance_between_2_bullet * 0.7f, v, gravity, num * 0.9f, false, true);
			}
		}
		GameObject particle = ParticleMuzzleFlashPooler.instance.GetPooledObject();
		particle.SetActive(true);
		particle.transform.SetParent(this.muzzle.transform);
		particle.transform.position = new Vector3(this.muzzle.transform.position.x, this.muzzle.transform.position.y, -9f);
		particle.transform.rotation = base.transform.rotation;
		particle.transform.DOScaleX(particle.transform.localScale.x, 0.1f).OnComplete(delegate
		{
			particle.SetActive(false);
			particle.transform.SetParent(ParticleMuzzleFlashPooler.instance.transform);
		});
		this.PlayFireAnimation();
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
}
