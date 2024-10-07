using System;
using UnityEngine;

public class NormalEnemy : Enemy
{
	private void Start()
	{
		Enemy.instance = this;
	}

	public override void SetText(double value)
	{
		this.textValue.text = Utilities.getStringFromNumber(value, false);
	}

	public override void SetText(string value)
	{
		this.textValue.text = value;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
	}
}
