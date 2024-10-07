using System;
using UnityEngine;

public class TestLaser : MonoBehaviour
{
	private float range = 100f;

	public LayerMask whatToHit;

	public LineRenderer _lineRenderer;

	private void Awake()
	{
		this._lineRenderer = base.GetComponent<LineRenderer>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		this.ShotRay(base.transform.position, Vector2.right);
	}

	public void ShotRay(Vector2 origin, Vector2 direction)
	{
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, direction, this.range, this.whatToHit);
		if (raycastHit2D.collider != null)
		{
			this._lineRenderer.SetPosition(0, base.transform.position);
			this._lineRenderer.SetPosition(1, raycastHit2D.point);
			UnityEngine.Debug.Log("We have hit something!");
			UnityEngine.Debug.Log(raycastHit2D.collider.gameObject.name);
		}
		else
		{
			this._lineRenderer.SetPosition(0, base.transform.position);
			this._lineRenderer.SetPosition(1, new Vector2(100f, base.transform.position.y));
		}
	}
}
