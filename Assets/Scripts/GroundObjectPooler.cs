using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundObjectPooler : ObjectPooler
{
	public static GroundObjectPooler instance;

	public List<GameObject> listPrefabPooledObject;

	private int index;

	public override void Awake()
	{
		GroundObjectPooler.instance = this;
		this.pooledAmount = 0;
		this.pooledObjects = new List<GameObject>();
		foreach (GameObject current in this.listPrefabPooledObject)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(current);
			gameObject.SetActive(false);
			gameObject.transform.SetParent(base.transform);
			this.pooledObjects.Add(gameObject);
			this.pooledAmount++;
		}
	}

	public override GameObject GetPooledObject()
	{
		int num = this.index;
		this.index++;
		if (this.index >= this.listPrefabPooledObject.Count)
		{
			this.index = 0;
		}
		return this.pooledObjects[num];
	}
}
