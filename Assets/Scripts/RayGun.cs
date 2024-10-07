using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayGun : MonoBehaviour
{
	public Transform startPoint;

	public Transform endPoint;

	private LineRenderer lineRenderer;

	private Vector2 mouse;

	private float range = 100f;

	private void Start()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.lineRenderer.positionCount = 2;
		this.lineRenderer.startWidth = 0.2f;
		this.lineRenderer.endWidth = 0.2f;
	}

	private void Update()
	{
		this.lineRenderer.SetPosition(0, this.startPoint.transform.position);
		this.lineRenderer.SetPosition(1, this.endPoint.transform.position);
	}
}
