using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float range;

	protected bool isInCollision;

	protected SpriteRenderer _renderer;

	protected Rigidbody2D _rigid;

	protected ParticleSystem _particle;

	public virtual void Awake()
	{
		this._renderer = base.GetComponent<SpriteRenderer>();
		this._rigid = base.GetComponent<Rigidbody2D>();
		this._particle = base.GetComponent<ParticleSystem>();
	}

	public SpriteRenderer GetRenderer()
	{
		if (this._renderer)
		{
			return this._renderer;
		}
		return null;
	}

	public Rigidbody2D GetRigidbody()
	{
		if (this._rigid)
		{
			return this._rigid;
		}
		return null;
	}

	public List<GameObject> FindGameobjectsInRange(Vector2 center, float range)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
		List<GameObject> list = new List<GameObject>();
		GameObject[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			GameObject gameObject = array2[i];
			if (Vector2.Distance(center, gameObject.transform.position) <= range)
			{
				list.Add(gameObject);
			}
		}
		return list;
	}

	public void OnEnable()
	{
		this.isInCollision = false;
	}
}
