using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParticleObjectPooler : ObjectPooler
{
	public static ParticleObjectPooler instance;

	[SerializeField]
	private GameObject item1_prefab;

	[SerializeField]
	private GameObject item2_prefab;

	[SerializeField]
	private GameObject item3_prefab;

	[SerializeField]
	private GameObject item4_prefab;

	[SerializeField]
	private GameObject item5_prefab;

	[SerializeField]
	private GameObject item6_prefab;

	[SerializeField]
	private GameObject item8_prefab;

	private static Dictionary<string, int> __f__switch_map0;

	public override void Awake()
	{
		this.pooledObjects = new List<GameObject>();
		ParticleObjectPooler.instance = this;
		for (int i = 0; i < 2; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObject);
			gameObject.SetActive(false);
			gameObject.name = "item2_particle_split";
			gameObject.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject);
			this.pooledAmount++;
		}
		for (int j = 0; j < 2; j++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.item1_prefab);
			gameObject2.SetActive(false);
			gameObject2.name = "item1_particle";
			gameObject2.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject2);
			this.pooledAmount++;
		}
		for (int k = 0; k < 4; k++)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.item2_prefab);
			gameObject3.SetActive(false);
			gameObject3.name = "item2_particle";
			gameObject3.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject3);
			this.pooledAmount++;
		}
		for (int l = 0; l < 5; l++)
		{
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.item3_prefab);
			gameObject4.SetActive(false);
			gameObject4.name = "item3_particle";
			gameObject4.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject4);
			this.pooledAmount++;
		}
		for (int m = 0; m < 7; m++)
		{
			GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.item4_prefab);
			gameObject5.SetActive(false);
			gameObject5.name = "item4_particle";
			gameObject5.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject5);
			this.pooledAmount++;
		}
		for (int n = 0; n < 4; n++)
		{
			GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.item5_prefab);
			gameObject6.SetActive(false);
			gameObject6.name = "item5_particle";
			gameObject6.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject6);
			this.pooledAmount++;
		}
		for (int num = 0; num < 1; num++)
		{
			GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(this.item6_prefab);
			gameObject7.SetActive(false);
			gameObject7.name = "item6_particle";
			gameObject7.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject7);
			this.pooledAmount++;
		}
		for (int num2 = 0; num2 < 1; num2++)
		{
			GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(this.item8_prefab);
			gameObject8.SetActive(false);
			gameObject8.name = "item8_particle";
			gameObject8.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject8);
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
		switch (name)
		{
		case "item1_particle":
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.item1_prefab);
			gameObject.transform.SetParent(base.transform);
			gameObject.SetActive(false);
			gameObject.name = "item1_particle";
			this.pooledObjects.Add(gameObject);
			this.pooledAmount++;
			return gameObject;
		}
		case "item2_particle":
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.item2_prefab);
			gameObject2.transform.SetParent(base.transform);
			gameObject2.SetActive(false);
			gameObject2.name = "item2_particle";
			this.pooledObjects.Add(gameObject2);
			this.pooledAmount++;
			return gameObject2;
		}
		case "item3_particle":
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.item3_prefab);
			gameObject3.transform.SetParent(base.transform);
			gameObject3.SetActive(false);
			gameObject3.name = "item3_particle";
			this.pooledObjects.Add(gameObject3);
			this.pooledAmount++;
			return gameObject3;
		}
		case "item4_particle":
		{
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.item4_prefab);
			gameObject4.transform.SetParent(base.transform);
			gameObject4.SetActive(false);
			gameObject4.name = "item4_particle";
			this.pooledObjects.Add(gameObject4);
			this.pooledAmount++;
			return gameObject4;
		}
		case "item5_particle":
		{
			GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.item5_prefab);
			gameObject5.transform.SetParent(base.transform);
			gameObject5.SetActive(false);
			gameObject5.name = "item5_particle";
			this.pooledObjects.Add(gameObject5);
			this.pooledAmount++;
			return gameObject5;
		}
		case "item6_particle":
		{
			GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.item6_prefab);
			gameObject6.transform.SetParent(base.transform);
			gameObject6.SetActive(false);
			gameObject6.name = "item6_particle";
			this.pooledObjects.Add(gameObject6);
			this.pooledAmount++;
			return gameObject6;
		}
		case "item8_particle":
		{
			GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(this.item8_prefab);
			gameObject7.transform.SetParent(base.transform);
			gameObject7.SetActive(false);
			gameObject7.name = "item8_particle";
			this.pooledObjects.Add(gameObject7);
			this.pooledAmount++;
			return gameObject7;
		}
		case "item2_particle_split":
		{
			GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(this.pooledObject);
			gameObject8.transform.SetParent(base.transform);
			gameObject8.SetActive(false);
			gameObject8.name = "item2_particle_split";
			this.pooledObjects.Add(gameObject8);
			this.pooledAmount++;
			return gameObject8;
		}
		}
		return null;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
