using System;
using UnityEngine;

public class Canon : MonoBehaviour
{
	[Range(0f, 20f), SerializeField]
	private float m_launchIntensity;

	[SerializeField]
	private GameObject m_projectile;

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
		{
			this.Fire();
		}
	}

	private void Fire()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_projectile, base.transform.position, base.transform.rotation);
		if (gameObject.GetComponent<Rigidbody2D>())
		{
			gameObject.GetComponent<Rigidbody2D>().AddForce(base.transform.forward * this.m_launchIntensity, ForceMode2D.Impulse);
		}
		else if (gameObject.GetComponent<Rigidbody>())
		{
			gameObject.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.m_launchIntensity, ForceMode.Impulse);
		}
	}
}
