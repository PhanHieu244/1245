using System;
using UnityEngine;

public class PoolerManager : MonoBehaviour
{
	public static PoolerManager instance;

	public ObjectPooler groundPooler;

	public ObjectPooler projectilePooler;

	public ObjectPooler normalEnemyPooler;

	public ObjectPooler topEnemyPooler;

	public ObjectPooler item1Pooler;

	public ObjectPooler item2Pooler;

	public ObjectPooler item2SmallPooler;

	public ObjectPooler item3Pooler;

	public ObjectPooler item4Pooler;

	public ObjectPooler item5Pooler;

	public ObjectPooler item6Pooler;

	public ObjectPooler item7Pooler;

	public ObjectPooler item8Pooler;

	public ObjectPooler hitEffectPooler;

	public ObjectPooler touchCrosshairPooler;

	public ObjectPooler effectBulletHitPooler;

	public ObjectPooler muzzleFlashPooler;

	public ObjectPooler effectBlockExplosionPooler;

	public ObjectPooler diamondPooler;

	public ObjectPooler coinPooler;

	public ObjectPooler fireDropPooler;

	public ObjectPooler firePooler;

	private void Awake()
	{
		PoolerManager.instance = this;
	}

	public void ChangeObjectPool(int tankIndex)
	{
		if (this.projectilePooler)
		{
			UnityEngine.Object.Destroy(this.projectilePooler.gameObject);
		}
		if (tankIndex != 1)
		{
			if (tankIndex != 2)
			{
				GameObject original = Resources.Load("Prefabs/ObjectPools/object_pool_tank2") as GameObject;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, base.transform);
				gameObject.transform.position = Vector3.zero;
				this.projectilePooler = gameObject.GetComponent<ObjectPooler>();
			}
			else
			{
				GameObject original2 = Resources.Load("Prefabs/ObjectPools/object_pool_tank2") as GameObject;
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2, base.transform);
				gameObject2.transform.position = Vector3.zero;
				this.projectilePooler = gameObject2.GetComponent<ObjectPooler>();
			}
		}
		else
		{
			GameObject original3 = Resources.Load("Prefabs/ObjectPools/object_pool_tank1") as GameObject;
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(original3, base.transform);
			gameObject3.transform.position = Vector3.zero;
			this.projectilePooler = gameObject3.GetComponent<ObjectPooler>();
		}
	}
}
