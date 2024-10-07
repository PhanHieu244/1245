using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelBooster : MonoBehaviour
{
	public Button btnUseDiamond;

	public Button btnWatchAds;

	public Text txtNumOfDiamond;

	public BoosterType type;

	public GameObject overlay;

	public int[] diamond_booster = new int[]
	{
		5,
		7,
		8,
		5
	};

	public void InitValue(bool isRewardLoaded, bool isEnoughDiamond, BoosterType _type)
	{
		if (isRewardLoaded)
		{
			this.btnWatchAds.interactable = true;
		}
		else
		{
			this.btnWatchAds.interactable = false;
		}
		if (isEnoughDiamond)
		{
			this.btnUseDiamond.interactable = true;
		}
		else
		{
			this.btnUseDiamond.interactable = false;
		}
		this.type = _type;
		this.UpdateUI(_type);
	}

	public void UpdateUI(BoosterType _type)
	{
		switch (_type)
		{
		case BoosterType.Speed:
			this.txtNumOfDiamond.text = this.diamond_booster[0].ToString();
			break;
		case BoosterType.TimeWarp:
			this.txtNumOfDiamond.text = this.diamond_booster[1].ToString();
			break;
		case BoosterType.Cooldown:
			this.txtNumOfDiamond.text = this.diamond_booster[2].ToString();
			break;
		case BoosterType.Revenue:
			this.txtNumOfDiamond.text = this.diamond_booster[3].ToString();
			break;
		}
	}

	public void BuyBooster()
	{
		int num = 5;
		switch (this.type)
		{
		case BoosterType.Speed:
			num = this.diamond_booster[0];
			break;
		case BoosterType.TimeWarp:
			num = this.diamond_booster[1];
			break;
		case BoosterType.Cooldown:
			num = this.diamond_booster[2];
			break;
		case BoosterType.Revenue:
			num = this.diamond_booster[3];
			break;
		}
		if (GameController.instance.Diamond >= (long)num)
		{
			GameController.instance.Diamond -= (long)num;
			Tracking.LogEvent("BUY_BOOSTER");
			switch (this.type)
			{
			case BoosterType.Speed:
				CanvasUpgrade.instance.StartSpeedBooster();
				break;
			case BoosterType.TimeWarp:
				CanvasUpgrade.instance.GetTimeWarp();
				break;
			case BoosterType.Cooldown:
				CanvasUpgrade.instance.StartCooldownReducer();
				break;
			case BoosterType.Revenue:
				CanvasUpgrade.instance.StartMoneyBooster();
				break;
			}
			base.gameObject.SetActive(false);
			return;
		}
	}

	public void WatchAds()
	{
		Debug.LogError("CANT SHOW ADS");
		Tracking.LogEvent("WATCH_ADS_BOOSTER");
		/*switch (this.type)
		{
		case BoosterType.Speed:
               AdsControl.Instance.ShowRewardBasedVideo(BoosterType.Speed);
			break;
		case BoosterType.TimeWarp:
                AdsControl.Instance.ShowRewardBasedVideo(BoosterType.TimeWarp);
			break;
		case BoosterType.Cooldown:
                AdsControl.Instance.ShowRewardBasedVideo(BoosterType.Cooldown);
			break;
		case BoosterType.Revenue:
                AdsControl.Instance.ShowRewardBasedVideo(BoosterType.Revenue);
			break;
		}*/
		base.gameObject.SetActive(false);
	}

	public void LoadDiamondBoosterValue()
	{
		this.diamond_booster = BaseValue.diamond_booster;
	}

	public void OnEnable()
	{
		this.LoadDiamondBoosterValue();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
