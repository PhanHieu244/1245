using System;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
	public static PlatformGenerator instance;

	public Transform generationPoint;

	private float platformWidth;

	public ObjectPooler objectPooler;

	private void Awake()
	{
		PlatformGenerator.instance = this;
	}

	private void Start()
	{
		this.platformWidth = 37.44f;
	}

	private void Update()
	{
		if (base.transform.position.x < this.generationPoint.position.x)
		{
			base.transform.position = new Vector3(base.transform.position.x + this.platformWidth, this.objectPooler.transform.position.y, base.transform.position.z);
			GameObject pooledObject = this.objectPooler.GetPooledObject();
			pooledObject.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -7f);
			pooledObject.transform.rotation = base.transform.rotation;
			pooledObject.SetActive(true);
		}
	}
}
