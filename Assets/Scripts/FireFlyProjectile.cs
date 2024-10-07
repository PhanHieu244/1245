using System;
using UnityEngine;

public class FireFlyProjectile : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ground")
		{
			GameObject pooledObject = FireDropPooler.instance.GetPooledObject();
			pooledObject.SetActive(true);
			pooledObject.transform.position = base.transform.position;
			base.gameObject.SetActive(false);
		}
	}
}
