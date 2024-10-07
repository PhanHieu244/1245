using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUnlock : MonoBehaviour
{
	public static CanvasUnlock instance;

	[SerializeField]
	private GameObject particle;

	[SerializeField]
	private Image imgWeapon;

	[SerializeField]
	private Text txtCongrat;

	[SerializeField]
	private Text txtMsg;

	[SerializeField]
	private Image imgOverlay;

	[SerializeField]
	private Transform movePoint;

	private Vector3 targetPos;

	private void Awake()
	{
		CanvasUnlock.instance = this;
	}

	public void SetImage(Sprite sprite)
	{
		this.imgWeapon.sprite = sprite;
	}

	public void ShowLight()
	{
	}

	public void HideLight()
	{
	}

	public void ShowText()
	{
		this.txtCongrat.DOFade(1f, 0.3f);
		this.txtMsg.DOFade(1f, 0.3f);
	}

	public void HideText()
	{
		this.txtCongrat.DOFade(0f, 0.3f);
		this.txtMsg.DOFade(0f, 0.3f);
	}

	public void ShowImageWeapon()
	{
		this.imgWeapon.transform.DOScale(0.8f, 1.4f);
	}

	public void HideImageWeapon()
	{
		this.imgWeapon.DOFade(0f, 0.3f).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}

	public void HideWeapon()
	{
		UnityEngine.Debug.Log(this.targetPos);
		this.movePoint.transform.DOMove(this.targetPos, 0.5f, false).OnStart(delegate
		{
			this.HideText();
			this.HideOverlay();
			this.particle.SetActive(false);
			this.imgWeapon.transform.DOScale(0.575f, 0.3f);
		}).OnComplete(delegate
		{
			CanvasItem.instance.CheckUnlockItem(GameController.instance.CurrentLevel);
			this.HideImageWeapon();
			this.HideLight();
		});
	}

	public void HideOverlay()
	{
		this.imgOverlay.DOFade(0f, 0.3f);
	}

	public void ShowOverlay()
	{
		this.imgOverlay.DOFade(1f, 0.3f);
	}

	public void InitValue(ButtonItem btnItem)
	{
		this.targetPos = Camera.main.WorldToScreenPoint(btnItem.transform.position);
		this.SetImage(btnItem.GetComponent<Image>().sprite);
		this.ShowLight();
		this.ShowImageWeapon();
		this.ShowText();
		base.Invoke("HideWeapon", 3f);
	}

	public void Animate()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
