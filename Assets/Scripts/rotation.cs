using System;
using UnityEngine;

public class rotation : MonoBehaviour
{
	public float xRotation;

	public float yRotation;

	public float zRotation;

	private void OnEnable()
	{
		base.InvokeRepeating("rotate", 0f, 0.0167f);
	}

	private void OnDisable()
	{
		base.CancelInvoke();
	}

	public void clickOn()
	{
		base.InvokeRepeating("rotate", 0f, 0.0167f);
	}

	public void clickOff()
	{
		base.CancelInvoke();
	}

	private void rotate()
	{
		base.transform.localEulerAngles += new Vector3(this.xRotation, this.yRotation, this.zRotation);
	}
}
