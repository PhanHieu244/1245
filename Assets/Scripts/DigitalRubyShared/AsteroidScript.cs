using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class AsteroidScript : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
		}

		private void OnBecameInvisible()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
