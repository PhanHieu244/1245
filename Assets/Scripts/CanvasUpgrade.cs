using System;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUpgrade : MonoBehaviour
{
	public static CanvasUpgrade instance;

	public ButtonUpgrade buttonUpgradePower;

	public ButtonUpgrade buttonUpgradeReloadSpeed;

	public ButtonUpgrade buttonUpgradeOfflineEaring;

	public ButtonBooster buttonBoosterSpeed;

	public ButtonBooster buttonBoosterTimeWarp;

	public ButtonBooster buttonBoosterCooldown;

	public ButtonBooster buttonBoosterRevenue;

	public PanelBooster panelBooster;

	private List<int> listBoosterValue;

	private void Awake()
	{
		CanvasUpgrade.instance = this;
	}

	public void LoadUpgradeData()
	{
		this.buttonUpgradePower.LoadUpgradeData();
		this.buttonUpgradeReloadSpeed.LoadUpgradeData();
		this.buttonUpgradeOfflineEaring.LoadUpgradeData();
	}

	public void SaveUpgradeData()
	{
		this.buttonUpgradePower.SaveUpgradeData();
		this.buttonUpgradeReloadSpeed.SaveUpgradeData();
		this.buttonUpgradeOfflineEaring.SaveUpgradeData();
	}

	public void ResetUpgradeData()
	{
		this.buttonUpgradePower.ResetUpgrade();
		this.buttonUpgradeReloadSpeed.ResetUpgrade();
		this.buttonUpgradeOfflineEaring.ResetUpgrade();
	}

	public void UpdateAllButtonUpgradeInteraction(double coin)
	{
		this.buttonUpgradePower.UpdateButtonInteration(coin);
		this.buttonUpgradeReloadSpeed.UpdateButtonInteration(coin);
		this.buttonUpgradeOfflineEaring.UpdateButtonInteration(coin);
	}

	public void UpgradePower()
	{
		this.buttonUpgradePower.BuyUpgrade();
		this.UpdateAllButtonUpgradeInteraction((double)GameController.instance.Coin);
	}

	public void UpgradeBulletSpeed()
	{
		this.UpdateAllButtonUpgradeInteraction((double)GameController.instance.Coin);
	}

	public void UpgradeReloadSpeed()
	{
		this.buttonUpgradeReloadSpeed.BuyUpgrade();
		this.UpdateAllButtonUpgradeInteraction((double)GameController.instance.Coin);
	}

	public void UpgradeOfflineEarning()
	{
		this.buttonUpgradeOfflineEaring.BuyUpgrade();
		this.UpdateAllButtonUpgradeInteraction((double)GameController.instance.Coin);
	}

	public void LoadFirebaseData()
	{
		this.listBoosterValue = new List<int>
		{
			FirebaseRemoteConfig.booster_value1,
			FirebaseRemoteConfig.booster_value2,
			FirebaseRemoteConfig.booster_value3,
			FirebaseRemoteConfig.booster_value4
		};
	}

	public void LoadBoosterData()
	{
		this.LoadFirebaseData();
		this.buttonBoosterSpeed.SetBoosterValue((double)this.listBoosterValue[0]);
		this.buttonBoosterSpeed.LoadBoosterData();
		this.buttonBoosterTimeWarp.SetBoosterValue((double)this.listBoosterValue[1]);
		this.buttonBoosterTimeWarp.LoadBoosterData();
		this.buttonBoosterCooldown.SetBoosterValue((double)this.listBoosterValue[2]);
		this.buttonBoosterCooldown.LoadBoosterData();
		this.buttonBoosterRevenue.SetBoosterValue((double)this.listBoosterValue[3]);
		this.buttonBoosterRevenue.LoadBoosterData();
	}

	public void ShowEffect()
	{
		this.buttonBoosterSpeed.ShowEffect();
		this.buttonBoosterTimeWarp.ShowEffect();
		this.buttonBoosterCooldown.ShowEffect();
		this.buttonBoosterRevenue.ShowEffect();
	}

	public void SaveBoosterData()
	{
		this.buttonBoosterSpeed.SaveRemainingTime();
		this.buttonBoosterTimeWarp.SaveRemainingTime();
		this.buttonBoosterCooldown.SaveRemainingTime();
		this.buttonBoosterRevenue.SaveRemainingTime();
	}

	public void ResetBoosterData()
	{
		this.buttonBoosterSpeed.ResetBooster();
		this.buttonBoosterTimeWarp.ResetBooster();
		this.buttonBoosterCooldown.ResetBooster();
		this.buttonBoosterRevenue.ResetBooster();
	}

	public void UpdateAllButtonBoosterInteraction()
	{
		bool buttonActive = false; //AdsControl.Instance.GetRewardAvailable();
		this.buttonBoosterSpeed.SetButtonActive(buttonActive);
		this.buttonBoosterTimeWarp.SetButtonActive(buttonActive);
		this.buttonBoosterCooldown.SetButtonActive(buttonActive);
		this.buttonBoosterRevenue.SetButtonActive(buttonActive);
	}

	public void UpdateAllButtonBoosterInteraction(bool isRewardAdsLoaded)
	{
		if (!this.buttonBoosterSpeed.IsBoosterUsed)
		{
			this.buttonBoosterSpeed.SetButtonActive(isRewardAdsLoaded);
		}
		this.buttonBoosterTimeWarp.SetButtonActive(isRewardAdsLoaded);
		if (!this.buttonBoosterCooldown.IsBoosterUsed)
		{
			this.buttonBoosterCooldown.SetButtonActive(isRewardAdsLoaded);
		}
		if (!this.buttonBoosterRevenue.IsBoosterUsed)
		{
			this.buttonBoosterRevenue.SetButtonActive(isRewardAdsLoaded);
		}
	}

	public void DisableAllButtonBoosterInteraction()
	{
		this.buttonBoosterSpeed.SetButtonActive(false);
		this.buttonBoosterTimeWarp.SetButtonActive(false);
		this.buttonBoosterCooldown.SetButtonActive(false);
		this.buttonBoosterRevenue.SetButtonActive(false);
	}

	public void CheclAllButtonBoosterInteraction()
	{
		int num = BaseValue.diamond_booster[0];
		int num2 = BaseValue.diamond_booster[1];
		int num3 = BaseValue.diamond_booster[2];
		int num4 = BaseValue.diamond_booster[3];
		int arg_38_0 = (GameController.instance.Diamond < (long)num) ? 0 : 1;
		int arg_52_0 = (GameController.instance.Diamond < (long)num2) ? 0 : 1;
		int arg_6C_0 = (GameController.instance.Diamond < (long)num3) ? 0 : 1;
		int arg_86_0 = (GameController.instance.Diamond < (long)num4) ? 0 : 1;
		bool flag = false; //AdsControl.Instance.GetRewardAvailable();
		if (!this.buttonBoosterSpeed.isBoosterUsed)
		{
			this.buttonBoosterSpeed.SetButtonActive(true);
		}
		else
		{
			this.buttonBoosterSpeed.ResumeBooster();
		}
		if (!this.buttonBoosterTimeWarp.isBoosterUsed)
		{
			this.buttonBoosterTimeWarp.SetButtonActive(true);
		}
		else
		{
			this.buttonBoosterTimeWarp.ResumeBooster();
		}
		if (!this.buttonBoosterCooldown.isBoosterUsed)
		{
			this.buttonBoosterCooldown.SetButtonActive(true);
		}
		else
		{
			this.buttonBoosterCooldown.ResumeBooster();
		}
		if (!this.buttonBoosterRevenue.isBoosterUsed)
		{
			this.buttonBoosterRevenue.SetButtonActive(true);
		}
		else
		{
			this.buttonBoosterRevenue.ResumeBooster();
		}
	}

	public void EnableAllButtonBoosterInteraction()
	{
		this.buttonBoosterSpeed.SetButtonActive(true);
		this.buttonBoosterTimeWarp.SetButtonActive(true);
		this.buttonBoosterCooldown.SetButtonActive(true);
		this.buttonBoosterRevenue.SetButtonActive(true);
	}

	public void SpeedBooster()
	{
		SoundController.instance.PlaySoundButtonDefault();
		//AdsControl.Instance.ShowRewardBasedVideo(BoosterType.Speed);
	}

	public void TimeWarp()
	{
		SoundController.instance.PlaySoundButtonDefault();
		//AdsControl.Instance.ShowRewardBasedVideo(BoosterType.TimeWarp);
	}

	public void CooldownReducer()
	{
		SoundController.instance.PlaySoundButtonDefault();
        //AdsControl.Instance.ShowRewardBasedVideo(BoosterType.Cooldown);
	}

	public void MoneyBooster()
	{
		SoundController.instance.PlaySoundButtonDefault();
        //AdsControl.Instance.ShowRewardBasedVideo(BoosterType.Revenue);
	}

	public void StartSpeedBooster()
	{
		UnityEngine.Debug.Log("StartSpeedBooster");
		Tracking.LogEvent("USE_SPEED_BOOSTER");
		this.buttonBoosterSpeed.StartBooster();
		if (Tank.instance)
		{
			Tank.instance.ShowEffectLightning();
		}
	}

	public void StartCooldownReducer()
	{
		UnityEngine.Debug.Log("StartCooldownReducer");
		SoundController.instance.PlaySoundCooldown();
		Tracking.LogEvent("USE_COOLDOWN_REDUCER");
		this.buttonBoosterCooldown.StartBooster();
		if (CanvasItem.instance)
		{
			CanvasItem.instance.ShowCooldownEffect();
			CanvasItem.instance.ReduceRemainingTime();
		}
	}

	public void StartMoneyBooster()
	{
		UnityEngine.Debug.Log("StartMoneyBooster");
		SoundController.instance.PlaySoundMoneyBooster();
		Tracking.LogEvent("USE_MONEY_BOOSTER");
		this.buttonBoosterRevenue.StartBooster();
		if (CanvasTop.instance)
		{
			CanvasTop.instance.ShowMoneyBoosterEffect();
		}
	}

	public void GetTimeWarp()
	{
		UnityEngine.Debug.Log("GetTimeWarp");
		SoundController.instance.PlaySoundTimeWarp();
		Tracking.LogEvent("USE_TIME_WARP");
		GameController.instance.canvasPopUp.AnimateTextTimeWarp(this.GetMoneyAfterTimeWarp((double)BaseValue.booster_value2));
	}

	public long GetMoneyAfterTimeWarp(double hours)
	{
		int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
		double num = (double)(BaseValue.coin_per_hit * coefLevel_) * Gun.instance.ReloadSpeed * hours * 3600.0 / 4.0;
		switch (CanvasItem.instance.GetNumItemUnlocked())
		{
		case 1:
			num += (double)(BaseValue.coin_per_item1_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item1_reload_time;
			break;
		case 2:
			num += (double)(BaseValue.coin_per_item1_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item1_reload_time;
			num += (double)(BaseValue.coin_per_item2_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item2_reload_time;
			break;
		case 3:
			num += (double)(BaseValue.coin_per_item1_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item1_reload_time;
			num += (double)(BaseValue.coin_per_item2_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item2_reload_time;
			num += (double)(BaseValue.coin_per_item3_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item3_reload_time;
			break;
		case 4:
			num += (double)(BaseValue.coin_per_item1_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item1_reload_time;
			num += (double)(BaseValue.coin_per_item2_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item2_reload_time;
			num += (double)(BaseValue.coin_per_item3_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item3_reload_time;
			num += (double)(BaseValue.coin_per_item4_hit * (long)coefLevel_ * 3600L) * hours / (double)BaseValue.item4_reload_time;
			break;
		}
		return (long)(num / 2.0);
	}

	public void ShowPanelBooster(BoosterType type)
	{
		UnityEngine.Debug.Log("ShowPanelBooster");
		SoundController.instance.PlaySoundButtonDefault();
		if (this.panelBooster.gameObject.activeSelf)
		{
			if (type == this.panelBooster.type)
			{
				this.panelBooster.gameObject.SetActive(false);
				this.panelBooster.overlay.SetActive(false);
			}
			else
			{
				this.panelBooster.gameObject.SetActive(true);
				this.panelBooster.overlay.SetActive(true);
				int num = 5;
				switch (type)
				{
				case BoosterType.Speed:
					num = BaseValue.diamond_booster[0];
					break;
				case BoosterType.TimeWarp:
					num = BaseValue.diamond_booster[1];
					break;
				case BoosterType.Cooldown:
					num = BaseValue.diamond_booster[2];
					break;
				case BoosterType.Revenue:
					num = BaseValue.diamond_booster[3];
					break;
				}
				bool isEnoughDiamond = GameController.instance.Diamond >= (long)num;
				//this.panelBooster.InitValue(AdsControl.Instance.GetRewardAvailable(), isEnoughDiamond, type);
				this.panelBooster.InitValue(false, isEnoughDiamond, type);
				switch (type)
				{
				case BoosterType.Speed:
				{
					Vector3 position = this.buttonBoosterSpeed.transform.position;
					position.y = this.panelBooster.transform.position.y;
					this.panelBooster.transform.position = position;
					break;
				}
				case BoosterType.TimeWarp:
				{
					Vector3 position2 = this.buttonBoosterTimeWarp.transform.position;
					position2.y = this.panelBooster.transform.position.y;
					this.panelBooster.transform.position = position2;
					break;
				}
				case BoosterType.Cooldown:
				{
					Vector3 position3 = this.buttonBoosterCooldown.transform.position;
					position3.y = this.panelBooster.transform.position.y;
					this.panelBooster.transform.position = position3;
					break;
				}
				case BoosterType.Revenue:
				{
					Vector3 position4 = this.buttonBoosterRevenue.transform.position;
					position4.y = this.panelBooster.transform.position.y;
					this.panelBooster.transform.position = position4;
					break;
				}
				}
			}
		}
		else
		{
			this.panelBooster.gameObject.SetActive(true);
			this.panelBooster.overlay.SetActive(true);
			int num2 = 5;
			switch (type)
			{
			case BoosterType.Speed:
				num2 = BaseValue.diamond_booster[0];
				break;
			case BoosterType.TimeWarp:
				num2 = BaseValue.diamond_booster[1];
				break;
			case BoosterType.Cooldown:
				num2 = BaseValue.diamond_booster[2];
				break;
			case BoosterType.Revenue:
				num2 = BaseValue.diamond_booster[3];
				break;
			}
			bool isEnoughDiamond2 = GameController.instance.Diamond >= (long)num2;
			//this.panelBooster.InitValue(AdsControl.Instance.GetRewardAvailable(), isEnoughDiamond2, type);
			panelBooster.InitValue(false, isEnoughDiamond2, type);
			switch (type)
			{
			case BoosterType.Speed:
			{
				Vector3 position5 = this.buttonBoosterSpeed.transform.position;
				position5.y = this.panelBooster.transform.position.y;
				this.panelBooster.transform.position = position5;
				break;
			}
			case BoosterType.TimeWarp:
			{
				Vector3 position6 = this.buttonBoosterTimeWarp.transform.position;
				position6.y = this.panelBooster.transform.position.y;
				this.panelBooster.transform.position = position6;
				break;
			}
			case BoosterType.Cooldown:
			{
				Vector3 position7 = this.buttonBoosterCooldown.transform.position;
				position7.y = this.panelBooster.transform.position.y;
				this.panelBooster.transform.position = position7;
				break;
			}
			case BoosterType.Revenue:
			{
				Vector3 position8 = this.buttonBoosterRevenue.transform.position;
				position8.y = this.panelBooster.transform.position.y;
				this.panelBooster.transform.position = position8;
				break;
			}
			}
		}
	}

	public void HidePanelBooster()
	{
		this.panelBooster.gameObject.SetActive(false);
		this.panelBooster.overlay.SetActive(false);
	}
}
