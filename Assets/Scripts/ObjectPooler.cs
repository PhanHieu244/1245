using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public GameObject pooledObject;

	public int pooledAmount;

	protected List<GameObject> pooledObjects;

	private static Func<GameObject, bool> __f__am_cache0;

	public virtual void Awake()
	{
		this.pooledObjects = new List<GameObject>();
		for (int i = 0; i < this.pooledAmount; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObject);
			gameObject.transform.SetParent(base.transform);
			gameObject.SetActive(false);
			this.pooledObjects.Add(gameObject);
		}
	}

	public virtual GameObject GetPooledObject()
	{
		for (int i = 0; i < this.pooledAmount; i++)
		{
			if (!this.pooledObjects[i].activeInHierarchy)
			{
				return this.pooledObjects[i];
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObject);
		gameObject.transform.SetParent(base.transform);
		gameObject.SetActive(false);
		this.pooledObjects.Add(gameObject);
		this.pooledAmount++;
		return gameObject;
	}

	public virtual GameObject _GetPooledObject()
	{
		for (int i = 0; i < this.pooledAmount; i++)
		{
			if (!this.pooledObjects[i].activeInHierarchy)
			{
				return this.pooledObjects[i];
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObject);
		gameObject.transform.SetParent(base.transform);
		gameObject.SetActive(false);
		this.pooledObjects.Add(gameObject);
		this.pooledAmount++;
		return gameObject;
	}

	public virtual bool IsAllObjectDisable()
	{
		return !this.pooledObjects.Any((GameObject GO) => GO.activeInHierarchy);
	}

	public void SetSpriteAllGround(Sprite sprite)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			child.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
		}
	}
}
