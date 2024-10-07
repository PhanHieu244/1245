using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleNormalBlockDestroyPooler : ObjectPooler
{
	public static ParticleNormalBlockDestroyPooler instance;

	[SerializeField]
	private GameObject normal_particle_prefab;

	[SerializeField]
	private GameObject static_particle_prefab;

	[SerializeField]
	private GameObject bonus_particle_prefab;

	public override void Awake()
	{
		ParticleNormalBlockDestroyPooler.instance = this;
		this.pooledObjects = new List<GameObject>();
		for (int i = 0; i < 2; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.normal_particle_prefab);
			gameObject.SetActive(false);
			gameObject.name = "normal_particle";
			gameObject.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject);
			this.pooledAmount++;
		}
		for (int j = 0; j < 2; j++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.static_particle_prefab);
			gameObject2.SetActive(false);
			gameObject2.name = "static_particle";
			gameObject2.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject2);
			this.pooledAmount++;
		}
		for (int k = 0; k < 2; k++)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.bonus_particle_prefab);
			gameObject3.SetActive(false);
			gameObject3.name = "bonus_particle";
			gameObject3.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject3);
			this.pooledAmount++;
		}
	}

	public GameObject GetPooledObject(string name)
	{
		for (int i = 0; i < this.pooledAmount; i++)
		{
			if (!this.pooledObjects[i].activeInHierarchy && this.pooledObjects[i].name == name)
			{
				return this.pooledObjects[i];
			}
		}
		if (name != null)
		{
			if (name == "normal_particle")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.normal_particle_prefab);
				gameObject.transform.SetParent(base.transform);
				gameObject.SetActive(false);
				gameObject.name = "normal_particle";
				this.pooledObjects.Add(gameObject);
				this.pooledAmount++;
				return gameObject;
			}
			if (name == "static_particle")
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.static_particle_prefab);
				gameObject2.transform.SetParent(base.transform);
				gameObject2.SetActive(false);
				gameObject2.name = "static_particle";
				this.pooledObjects.Add(gameObject2);
				this.pooledAmount++;
				return gameObject2;
			}
			if (name == "bonus_particle")
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.bonus_particle_prefab);
				gameObject3.transform.SetParent(base.transform);
				gameObject3.SetActive(false);
				gameObject3.name = "bonus_particle";
				this.pooledObjects.Add(gameObject3);
				this.pooledAmount++;
				return gameObject3;
			}
		}
		return null;
	}
}
