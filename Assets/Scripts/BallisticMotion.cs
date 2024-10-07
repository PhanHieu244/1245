using System;
using UnityEngine;

public class BallisticMotion : MonoBehaviour
{
	private Vector3 lastPos;

	private Vector3 impulse;

	private float gravity;

	private void Awake()
	{
	}

	public void Initialize(Vector3 pos, float gravity)
	{
		base.transform.position = pos;
		this.lastPos = base.transform.position;
		this.gravity = gravity;
	}

	private void FixedUpdate()
	{
		float fixedDeltaTime = Time.fixedDeltaTime;
		Vector3 a = -this.gravity * Vector3.up;
		Vector3 position = base.transform.position;
		Vector3 a2 = position + (position - this.lastPos) + this.impulse * fixedDeltaTime + a * fixedDeltaTime * fixedDeltaTime;
		this.lastPos = position;
		a2.z = 0f;
		Vector2 vector = a2 - this.lastPos;
		float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		base.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		this.impulse = Vector3.zero;
		if (base.transform.position.y < -5f)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void AddImpulse(Vector3 impulse)
	{
		this.impulse += impulse;
	}
}
