using System;
using UnityEngine;

public class TestCollide : MonoBehaviour
{
	public float range = 0.5f;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		UnityEngine.Debug.Log("OnTriggerEnter2D");
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.range);
		for (int i = 0; i < array.Length; i++)
		{
			Collider2D collider2D = array[i];
			UnityEngine.Debug.Log(collider2D.name);
		}
		UnityEngine.Debug.Log("EndOnTriggerEnter2D");
	}
}
