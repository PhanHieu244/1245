using System;
using UnityEngine;

public class Item7Projectile : Projectile
{
	public LayerMask whatToHit;

	private GameObject particleStart;

	private GameObject particleHit;

	private bool isShotLaser;

	private float waitTime;

	private void Start()
	{
		this.particleStart = base.transform.GetChild(0).gameObject;
		this.particleHit = base.transform.GetChild(1).gameObject;
		this.particleStart.SetActive(false);
		this.particleHit.SetActive(false);
		this.range = 100f;
	}

	private void Update()
	{
		if (this.isShotLaser)
		{
			this.ShotRay(base.transform.position, Vector2.down);
		}
	}

	public void StartShot()
	{
		this.isShotLaser = true;
		this.particleStart.SetActive(true);
		this.particleHit.SetActive(true);
	}

	public void StopShot()
	{
		this.isShotLaser = false;
		this.particleStart.SetActive(false);
		this.particleHit.SetActive(false);
	}

	public void ShotRay(Vector2 origin, Vector2 direction)
	{
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, direction, this.range, this.whatToHit);
		if (raycastHit2D.collider != null)
		{
			Collider2D collider = raycastHit2D.collider;
			UnityEngine.Debug.Log("We have hit something!");
			UnityEngine.Debug.Log(collider.name);
			this.particleHit.transform.position = raycastHit2D.point;
			if (collider.CompareTag("Enemy"))
			{
				this.waitTime -= Time.deltaTime;
				if (this.waitTime <= 0f)
				{
					int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
					collider.GetComponent<Enemy>().CallFlash((double)(BaseValue.item7_base_damage * (long)coefLevel_ / 4L), BaseValue.coin_per_item7_hit / 4L, ProjectileType.Non_Projectile);
					this.waitTime = 0.08f;
				}
			}
		}
	}
}
