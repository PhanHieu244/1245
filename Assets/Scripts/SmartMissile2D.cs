using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SmartMissile2D : SmartMissile<Rigidbody2D, Vector2>
{
	private void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody2D>();
	}

	protected override Transform findNewTarget()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.m_searchRange);
		for (int i = 0; i < array.Length; i++)
		{
			Collider2D collider2D = array[i];
			if (collider2D.gameObject.CompareTag(this.m_targetTag) && this.isWithinRange(collider2D.transform.position))
			{
				this.m_targetDistance = Vector2.Distance(collider2D.transform.position, base.transform.position);
				return collider2D.transform;
			}
		}
		return null;
	}

	protected override bool isWithinRange(Vector3 Coordinates)
	{
		return Vector2.Distance(Coordinates, base.transform.position) < this.m_targetDistance && Vector2.Angle(base.transform.forward, Coordinates - base.transform.position) < (float)(this.m_searchAngle / 2);
	}

	protected override void goToTarget()
	{
		this.m_direction = (this.m_target.position + (Vector3)this.m_targetOffset - base.transform.position).normalized * this.m_distanceInfluence.Evaluate(1f - (this.m_target.position + (Vector3)this.m_targetOffset - base.transform.position).magnitude / this.m_searchRange);
		this.m_rigidbody.velocity = Vector2.ClampMagnitude(this.m_rigidbody.velocity + this.m_direction * this.m_guidanceIntensity, this.m_rigidbody.velocity.magnitude);
		if (this.m_rigidbody.velocity != Vector2.zero)
		{
			base.transform.LookAt(new Vector3(this.m_rigidbody.velocity.x, this.m_rigidbody.velocity.y, base.transform.position.z));
		}
	}
}
