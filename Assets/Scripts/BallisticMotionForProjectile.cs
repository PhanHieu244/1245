using System;
using UnityEngine;

public class BallisticMotionForProjectile : MonoBehaviour
{
	private Vector3 lastPos;

	private Vector3 impulse;

	private float gravity;

	private float timeScale = 1f;

	public Vector3 targetPos;

	private Vector3 startPos;

	public float speed;

	public float arcHeight = 1f;

	private bool isReady;

	private void Awake()
	{
	}

	public void Initialize(Vector3 pos, float gravity, float _timeScale)
	{
		base.transform.position = pos;
		this.lastPos = base.transform.position;
		this.gravity = gravity;
		this.timeScale = _timeScale;
	}

	public float GetGravity()
	{
		return this.gravity;
	}

	public float GetTimeScale()
	{
		return this.timeScale;
	}

	public void Initialize(Vector3 _startPos, Vector3 _targetPos, float _speed)
	{
		this.startPos = _startPos;
		this.targetPos = _targetPos;
		this.speed = _speed;
	}

	public void StartFire()
	{
		this.isReady = true;
	}

	private void FixedUpdate()
	{
		float d = Time.fixedDeltaTime * this.timeScale;
		Vector3 a = -this.gravity * Vector3.up;
		Vector3 position = base.transform.position;
		Vector3 vector = position + (position - this.lastPos) + this.impulse * d + a * d * d;
		this.lastPos = position;
		vector.z = 0f;
		base.transform.position = vector;
		base.transform.rotation = BallisticMotionForProjectile.LookAt2D(vector - this.lastPos);
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

	private static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0f, 0f, Mathf.Atan2(forward.y, forward.x) * 57.29578f);
	}
}
