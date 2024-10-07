using System;
using UnityEngine;

public class TestParticleCollision : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnParticleCollision(GameObject other)
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"OnParticleCollision ",
			other.gameObject.name,
			" ",
			other.transform.position
		}));
	}
}
