using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPopUp : MonoBehaviour
{
	private sealed class _ButtonGetMoney_c__AnonStorey0
	{
		internal GameObject textcoinGO;

		internal CanvasPopUp _this;

		internal void __m__0()
		{
			this.textcoinGO.transform.DOScale(0f, 0.5f);
		}

		internal void __m__1()
		{
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
			GameController.instance.Coin += this._this.offlineMoney;
			GameController.instance.UpdateTextCoin();
			UnityEngine.Object.Destroy(this.textcoinGO.gameObject);
		}
	}

	private sealed class _GetMoneyX2_c__AnonStorey1
	{
		internal GameObject textcoinGO;

		internal CanvasPopUp _this;

		internal void __m__0()
		{
			this._this.txtMoney.text = string.Format("O {0}", Utilities.getStringFromNumber((double)(this._this.offlineMoney * 2L), false));
		}

		internal void __m__1()
		{
			this._this.gameObject.SetActive(false);
			this.textcoinGO.transform.DOScale(0f, 0.5f);
		}

		internal void __m__2()
		{
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
			GameController.instance.Coin += this._this.offlineMoney * 2L;
			GameController.instance.UpdateTextCoin();
			UnityEngine.Object.Destroy(this.textcoinGO.gameObject);
		}
	}

	private sealed class _AnimateTextTimeWarp_c__AnonStorey2
	{
		internal GameObject textcoinGO;

		internal long value;

		internal void __m__0()
		{
			this.textcoinGO.transform.DOScale(0f, 0.5f);
		}

		internal void __m__1()
		{
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
			GameController.instance.Coin += this.value;
			GameController.instance.UpdateTextCoin();
			CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction((double)GameController.instance.Coin);
			UnityEngine.Object.Destroy(this.textcoinGO.gameObject);
		}
	}

	public static CanvasPopUp instance;

	[SerializeField]
	private Text txtMoney;

	[SerializeField]
	private Button btnx2Money;

	[SerializeField]
	private GameObject prefab_text_coin;

	private long offlineMoney;

	public long OfflineMoney
	{
		get
		{
			return this.offlineMoney;
		}
		set
		{
			this.offlineMoney = value;
		}
	}

	public void ButtonGetMoney()
	{
		SoundController.instance.PlaySoundButtonDefault();
		SoundController.instance.PlaySoundMoneyBooster();
		GameObject textcoinGO = UnityEngine.Object.Instantiate<GameObject>(this.prefab_text_coin, Camera.main.ScreenToWorldPoint(this.txtMoney.transform.position), Quaternion.identity, CanvasUpgrade.instance.transform);
		Vector3 textCoinPos = CanvasTop.instance.GetTextCoinPos();
		textCoinPos.z = 0f;
		textCoinPos.y -= 0.5f;
		textCoinPos.x = textcoinGO.transform.position.x;
		textcoinGO.GetComponent<Text>().text = this.txtMoney.text;
		textcoinGO.transform.DOMove(textCoinPos, 0.5f, false).SetDelay(0.2f).OnStart(delegate
		{
			textcoinGO.transform.DOScale(0f, 0.5f);
		}).OnComplete(delegate
		{
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
			GameController.instance.Coin += this.offlineMoney;
			GameController.instance.UpdateTextCoin();
			UnityEngine.Object.Destroy(textcoinGO.gameObject);
		});
		UnityEngine.Debug.Log("Get offline money: " + this.offlineMoney);
		base.gameObject.SetActive(false);
	}

	public void ButtonGetMoneyx2()
	{
		SoundController.instance.PlaySoundButtonDefault();
		UnityEngine.Debug.Log("Get offline money x2: " + this.offlineMoney);
        //Admob.instance.ShowRewardBasedVideo();
	}

	public void GetMoneyX2()
	{
		UnityEngine.Debug.Log("GetMoneyX2");
		SoundController.instance.PlaySoundMoneyBooster();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(this.txtMoney.transform.DOScale(1f, 0.4f));
		Tweener t = this.txtMoney.transform.DOScale(1.2f, 0.2f).OnComplete(delegate
		{
			this.txtMoney.text = string.Format("O {0}", Utilities.getStringFromNumber((double)(this.offlineMoney * 2L), false));
		});
		sequence.Append(t);
		Tweener t2 = this.txtMoney.transform.DOScale(1f, 0.2f);
		sequence.Append(t2);
		sequence.Append(this.txtMoney.transform.DOScale(1f, 1f));
		GameObject textcoinGO = UnityEngine.Object.Instantiate<GameObject>(this.prefab_text_coin, Camera.main.ScreenToWorldPoint(this.txtMoney.transform.position), Quaternion.identity, CanvasUpgrade.instance.transform);
		Vector3 textCoinPos = CanvasTop.instance.GetTextCoinPos();
		textCoinPos.z = 0f;
		textCoinPos.y -= 0.5f;
		textCoinPos.x = textcoinGO.transform.position.x;
		textcoinGO.GetComponent<Text>().text = string.Format("O {0}", Utilities.getStringFromNumber((double)(this.offlineMoney * 2L), false));
		Tweener t3 = textcoinGO.transform.DOMove(textCoinPos, 0.5f, false).SetDelay(0.2f).OnStart(delegate
		{
			this.gameObject.SetActive(false);
			textcoinGO.transform.DOScale(0f, 0.5f);
		}).OnComplete(delegate
		{
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
			GameController.instance.Coin += this.offlineMoney * 2L;
			GameController.instance.UpdateTextCoin();
			UnityEngine.Object.Destroy(textcoinGO.gameObject);
		});
		sequence.Append(t3);
		sequence.Play<Sequence>();
	}

	public void AnimateTextTimeWarp(long value)
	{
		GameObject textcoinGO = UnityEngine.Object.Instantiate<GameObject>(this.prefab_text_coin, Vector3.zero, Quaternion.identity, CanvasUpgrade.instance.transform);
		textcoinGO.transform.localPosition = Vector3.zero;
		Vector3 textCoinPos = CanvasTop.instance.GetTextCoinPos();
		textCoinPos.z = 0f;
		textCoinPos.y -= 0.5f;
		textcoinGO.GetComponent<Text>().text = Utilities.getStringFromNumber((double)value, false);
		textcoinGO.GetComponent<Text>().color = Color.white;
		textcoinGO.transform.DOMove(textCoinPos, 0.5f, false).SetDelay(0.4f).OnStart(delegate
		{
			textcoinGO.transform.DOScale(0f, 0.5f);
		}).OnComplete(delegate
		{
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
			GameController.instance.Coin += value;
			GameController.instance.UpdateTextCoin();
			CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction((double)GameController.instance.Coin);
			UnityEngine.Object.Destroy(textcoinGO.gameObject);
		});
	}

	public void UpdateText()
	{
		this.txtMoney.text = "O " + Utilities.getStringFromNumber((double)this.offlineMoney, false);
	}

	public void OnEnable()
	{
		this.btnx2Money.interactable = false;
	}

	private void Awake()
	{
		CanvasPopUp.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
