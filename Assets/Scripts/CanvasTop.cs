using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTop : MonoBehaviour
{
	public static CanvasTop instance;

	[SerializeField]
	private Text txtCurrentLevel;

	[SerializeField]
	private Text txtTimeBonus;

	[SerializeField]
	private Text txtCoin;

	[SerializeField]
	private Text txtDiamond;

	[SerializeField]
	private Image imgDiamond;

	[SerializeField]
	private GameObject particle_star;

	private const float duration = 0.05f;

	public void SetTextCurrentLevel(string value)
	{
		this.txtCurrentLevel.text = value;
	}

	public void SetTextCurrentLevelColor(Color color)
	{
		this.txtCurrentLevel.color = color;
	}

	public void SetTextDiamond(string value)
	{
		this.txtDiamond.text = value;
	}

	public void SetTextCoin(string value)
	{
		this.txtCoin.text = value;
	}

	public void SetTextTimeBonus(string value)
	{
		this.txtTimeBonus.text = value;
	}

	private void Awake()
	{
		CanvasTop.instance = this;
	}

	private void Start()
	{
		this.txtTimeBonus.gameObject.SetActive(false);
	}

	private void Update()
	{
	}

	internal void ShowTextBonus()
	{
		this.txtTimeBonus.gameObject.SetActive(true);
	}

	internal void HideTextBonus()
	{
		this.txtTimeBonus.DOKill(false);
		this.txtTimeBonus.gameObject.SetActive(false);
	}

	internal void SetTextBonusColor(Color color)
	{
		this.txtTimeBonus.color = color;
	}

	internal Vector3 GetTextCoinPos()
	{
		return this.txtCoin.transform.position;
	}

	internal Vector3 GetTextDiamondPos()
	{
		return this.txtDiamond.transform.position;
	}

	internal void AnimateTextCoin()
	{
		this.txtCoin.transform.DOScale(1.2f, 0.05f).OnComplete(delegate
		{
			this.txtCoin.transform.DOScale(1f, 0.05f);
		});
	}

	internal void AnimateTextDiamond()
	{
		this.txtDiamond.transform.DOScale(0.75f, 0.05f).OnComplete(delegate
		{
			this.txtDiamond.transform.DOScale(0.5f, 0.05f);
		});
		this.imgDiamond.transform.DOScale(0.75f, 0.05f).OnComplete(delegate
		{
			this.imgDiamond.transform.DOScale(0.5f, 0.05f);
		});
	}

	public void ShowMoneyBoosterEffect()
	{
		this.particle_star.SetActive(true);
		this.txtCoin.color = Utilities.hexToColor("#FFFF00");
	}

	public void HideMoneyBoosterEffect()
	{
		this.particle_star.SetActive(false);
		this.txtCoin.color = Utilities.hexToColor("#FFFFFF");
	}

	internal void AnimateBonus()
	{
	}
}
