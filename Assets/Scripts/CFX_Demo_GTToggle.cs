using System;
using UnityEngine;
using UnityEngine.UI;

public class CFX_Demo_GTToggle : MonoBehaviour
{
	public Texture Normal;

	public Texture Hover;

	public Color NormalColor = new Color32(128, 128, 128, 128);

	public Color DisabledColor = new Color32(128, 128, 128, 48);

	public bool State = true;

	public string Callback;

	public GameObject Receiver;

	private Rect CollisionRect;

	private bool Over;

	private Text Label;

	private void Awake()
	{
		this.CollisionRect = base.GetComponent<Image>().rectTransform.rect;
		this.Label = base.GetComponentInChildren<Text>();
		this.UpdateTexture();
	}

	private void Update()
	{
		if (this.CollisionRect.Contains(UnityEngine.Input.mousePosition))
		{
			this.Over = true;
			if (Input.GetMouseButtonDown(0))
			{
				this.OnClick();
			}
		}
		else
		{
			this.Over = false;
			base.GetComponent<Image>().color = this.NormalColor;
		}
		this.UpdateTexture();
	}

	private void OnClick()
	{
		this.State = !this.State;
		this.Receiver.SendMessage(this.Callback);
	}

	private void UpdateTexture()
	{
		Color color = (!this.State) ? this.DisabledColor : this.NormalColor;
		/*if (this.Over)
		{
			base.GetComponent<Image>().sprite.text = this.Hover;
		}
		else
		{
			base.GetComponent<Image>().texture = this.Normal;
		}*/
		base.GetComponent<Image>().color = color;
		if (this.Label != null)
		{
			this.Label.color = color * 1.75f;
		}
	}
}
