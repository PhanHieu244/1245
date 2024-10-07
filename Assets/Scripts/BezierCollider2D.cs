using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class BezierCollider2D : MonoBehaviour
{
	public Vector2 firstPoint;

	public Vector2 secondPoint;

	public Vector2 handlerFirstPoint;

	public Vector2 handlerSecondPoint;

	public int pointsQuantity;

	private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 handlerP0, Vector3 handlerP1, Vector3 p1)
	{
		float num = 1f - t;
		float num2 = t * t;
		float num3 = num * num;
		float d = num3 * num;
		float d2 = num2 * t;
		Vector3 a = d * p0;
		a += 3f * num3 * t * handlerP0;
		a += 3f * num * num2 * handlerP1;
		return a + d2 * p1;
	}

	public Vector2[] calculate2DPoints()
	{
		List<Vector2> list = new List<Vector2>();
		list.Add(this.firstPoint);
		for (int i = 1; i < this.pointsQuantity; i++)
		{
			list.Add(this.CalculateBezierPoint(1f / (float)this.pointsQuantity * (float)i, this.firstPoint, this.handlerFirstPoint, this.handlerSecondPoint, this.secondPoint));
		}
		list.Add(this.secondPoint);
		return list.ToArray();
	}
}
