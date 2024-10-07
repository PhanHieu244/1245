using System;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
	public GameObject platformDestructionPoint;

	private void Start()
	{
		this.platformDestructionPoint = GameObject.Find("DestructionPoint");
	}

	private void Update()
	{
		if (base.transform.position.x < this.platformDestructionPoint.transform.position.x)
		{
			base.gameObject.SetActive(false);
		}
	}
}
