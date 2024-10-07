using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBossFight : MonoBehaviour
{
	private sealed class _AnimateBossFight_c__AnonStorey0
	{
		internal bool isBonus;

		internal CanvasBossFight _this;

		internal void __m__0()
		{
			this._this.SetUp(this.isBonus);
		}

		internal void __m__1()
		{
			this._this.imgNoti.DOFade(1f, 0.3f);
		}

		internal void __m__2()
		{
			this._this.imgNoti.DOFade(0f, 0.3f);
		}

		internal void __m__3()
		{
			this._this.panelBossFight.gameObject.SetActive(false);
		}
	}

	[SerializeField]
	private Image panelBossFight;

	[SerializeField]
	private Text txtBossFight;

	private static Color _color = new Color(0f, 0f, 0f, 0f);

	public Sprite sprBossFight;

	public Sprite sprBonus;

	public Image imgNoti;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetUp()
	{
		this.panelBossFight.gameObject.SetActive(true);
		this.panelBossFight.GetComponent<Image>().color = CanvasBossFight._color;
	}

	public void SetUp(bool isBonus = false)
	{
		this.panelBossFight.gameObject.SetActive(true);
		this.panelBossFight.GetComponent<Image>().color = CanvasBossFight._color;
		this.txtBossFight.transform.localPosition = Vector3.zero;
		this.txtBossFight.transform.localScale = Vector3.one * 2f;
		if (isBonus)
		{
			this.imgNoti.sprite = this.sprBonus;
		}
		else
		{
			this.imgNoti.sprite = this.sprBossFight;
		}
	}

	public void AnimateBossFight(bool isBonus = false)
	{
		if (isBonus)
		{
			SoundController.instance.PlaySoundBonusLevel();
		}
		else
		{
			SoundController.instance.PlaySoundBossFight();
		}
		Sequence sequence = DOTween.Sequence();
		sequence.OnStart(delegate
		{
			this.SetUp(isBonus);
		});
		Tweener t = this.panelBossFight.GetComponent<Image>().DOFade(0.7f, 0.3f).OnStart(delegate
		{
			this.imgNoti.DOFade(1f, 0.3f);
		});
		sequence.Append(t);
		Tweener t2 = base.transform.DOScale(base.transform.localScale.x, 1.5f);
		sequence.Append(t2);
		Tweener t3 = this.panelBossFight.GetComponent<Image>().DOFade(0f, 0.3f).OnStart(delegate
		{
			this.imgNoti.DOFade(0f, 0.3f);
		});
		sequence.Append(t3);
		sequence.AppendCallback(delegate
		{
			this.panelBossFight.gameObject.SetActive(false);
		});
	}
}
