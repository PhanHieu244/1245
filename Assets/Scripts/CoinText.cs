using System;
using UnityEngine;

public class CoinText : MonoBehaviour
{
	[SerializeField]
	private TextMesh textCoin;

	public void SetText(string value)
	{
		this.textCoin.text = value;
	}

	public void SetSize(float size)
	{
		this.textCoin.characterSize = size;
	}
}
