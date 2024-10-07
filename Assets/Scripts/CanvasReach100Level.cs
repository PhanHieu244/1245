using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasReach100Level : MonoBehaviour
{
	public static CanvasReach100Level instance;

	[SerializeField]
	private ParticleSystem particle;

	[SerializeField]
	private Image imgTank;

	[SerializeField]
	private Text txtCongrat;

	[SerializeField]
	private Text txtMsg;

	[SerializeField]
	private Text txtMsg2;

	[SerializeField]
	private Image imgOverlay;

	[SerializeField]
	private Button btnRestart;

	private void Awake()
	{
		CanvasReach100Level.instance = this;
	}

	public void SetImage(Sprite sprite)
	{
		this.imgTank.sprite = sprite;
	}

	public void ShowText()
	{
		this.txtCongrat.DOFade(1f, 0.3f);
		this.txtMsg.DOFade(1f, 0.3f);
		this.txtMsg2.DOFade(1f, 0.3f);
	}

	public void HideText()
	{
		this.txtCongrat.DOFade(0f, 0.3f);
		this.txtMsg.DOFade(0f, 0.3f);
		this.txtMsg2.DOFade(0f, 0.3f);
	}

	public void ShowButton()
	{
		this.btnRestart.GetComponent<Image>().DOFade(1f, 0.3f).OnComplete(delegate
		{
			this.btnRestart.interactable = true;
		});
	}

	public void HideButton()
	{
		this.btnRestart.interactable = false;
		this.btnRestart.GetComponent<Image>().DOFade(0f, 0.3f);
	}

	public void ShowImageTank()
	{
		this.imgTank.transform.DOScale(1f, 1.4f);
	}

	public void HideImageTank()
	{
		this.imgTank.DOFade(0f, 0.3f).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}

	public void HideTank()
	{
		this.particle.Stop();
		this.HideImageTank();
	}

	public void HideOverlay()
	{
		this.imgOverlay.DOFade(0f, 0.3f);
	}

	public void ShowOverlay()
	{
		this.imgOverlay.DOFade(0.8f, 0.3f);
	}

	public void InitValue(Sprite sprite, bool isComingSoon = false)
	{
		this.SetImage(sprite);
		this.ShowImageTank();
		this.ShowText();
		this.ShowButton();
		this.ShowOverlay();
		if (isComingSoon)
		{
			this.txtMsg2.text = "NEW TANK COMING SOON";
		}
		else
		{
			this.txtMsg2.text = "NEW TANK UNLOCKED";
		}
	}

	public void ButtonRestart()
	{
		SoundController.instance.PlaySoundButtonDefault();
		GameController.instance.StartReset();
		this.HidePanel();
	}

	public void HidePanel()
	{
		this.HideText();
		this.HideTank();
		this.HideButton();
		this.HideOverlay();
	}
}
