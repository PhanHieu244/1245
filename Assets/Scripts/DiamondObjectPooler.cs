using System;
using System.Collections.Generic;
using UnityEngine;

public class DiamondObjectPooler : ObjectPooler
{
	public static DiamondObjectPooler instance;

	public override void Awake()
	{
		base.Awake();
		DiamondObjectPooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public List<GameObject> GetListPooledObject()
	{
		return this.pooledObjects;
	}

	public void DisableAllDiamond()
	{
		foreach (GameObject current in this.pooledObjects)
		{
			if (current.activeSelf)
			{
				current.transform.SetParent(base.transform);
				current.SetActive(false);
			}
		}
	}

	public bool AnyActive()
	{
		foreach (GameObject current in this.pooledObjects)
		{
			if (current.activeSelf)
			{
				return true;
			}
		}
		return false;
	}
}
