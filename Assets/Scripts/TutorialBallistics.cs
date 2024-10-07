using System;
using UnityEngine;

public class TutorialBallistics : MonoBehaviour
{
	public static TutorialBallistics instance;

	public Transform targetObj;

	public Transform gunObj;

	public static float bulletSpeed = 20f;

	private static float h;

	private LineRenderer lineRenderer;

	private void Awake()
	{
		TutorialBallistics.instance = this;
		TutorialBallistics.h = Time.fixedDeltaTime * 1f;
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	private void Update()
	{
		this.RotateGun();
	}

	private void RotateGun()
	{
		float num = 0f;
		float num2 = 0f;
		this.CalculateAngleToHitTarget(out num, out num2);
		float num3 = num;
		if (num != -1f)
		{
			this.gunObj.localEulerAngles = new Vector3(360f - num3, 0f, 0f);
			base.transform.LookAt(this.targetObj);
			base.transform.eulerAngles = new Vector3(0f, base.transform.rotation.eulerAngles.y, 0f);
		}
	}

	private void CalculateAngleToHitTarget(out float theta1, out float theta2)
	{
		float num = TutorialBallistics.bulletSpeed;
		Vector3 vector = this.targetObj.position - this.gunObj.position;
		float y = vector.y;
		vector.y = 0f;
		float magnitude = vector.magnitude;
		float num2 = 9.81f;
		float num3 = num * num;
		float num4 = num3 * num3 - num2 * (num2 * magnitude * magnitude + 2f * y * num3);
		if (num4 >= 0f)
		{
			float num5 = Mathf.Sqrt(num4);
			float y2 = num3 + num5;
			float y3 = num3 - num5;
			float x = num2 * magnitude;
			theta1 = Mathf.Atan2(y2, x) * 57.29578f;
			theta2 = Mathf.Atan2(y3, x) * 57.29578f;
		}
		else
		{
			theta1 = -1f;
			theta2 = -1f;
		}
	}
}
