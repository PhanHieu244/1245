using System;
using UnityEngine;

[ExecuteInEditMode]
public class FireDropProjectile : Projectile
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		Enemy component = other.GetComponent<Enemy>();
		if (component)
		{
			int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
			long damage = (long)((float)coefLevel_ * ((float)BaseValue.napalm_base_damage * 4f / 7f) / 224f);
			long coin = (long)((float)coefLevel_ * ((float)BaseValue.coin_per_item4_hit * 4f / 7f) / 224f);
			if (component.gameObject.activeSelf)
			{
				component.StartBurn(damage, coin);
			}
		}
		if (other.tag == "Ground")
		{
			UnityEngine.Debug.Log("a---------------");
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
	}
}
