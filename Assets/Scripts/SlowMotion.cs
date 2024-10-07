using System;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
	[Range(0f, 5f)]
	public float slowMotion = 1f;

	private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKey(KeyCode.S))
		{
			Time.timeScale = 0.2f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.F))
		{
			Time.timeScale = 5f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Z))
		{
			Time.timeScale = 0f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.X))
		{
			Time.timeScale = 1f;
		}
	}
}
