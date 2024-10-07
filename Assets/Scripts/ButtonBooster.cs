using PreviewLabs;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBooster : MonoBehaviour
{
	[SerializeField]
	private Button btnBooster;

	[SerializeField]
	private Text txtUsageTime;

	[SerializeField]
	private Text txtCooldownTime;

	[SerializeField]
	private Text txtDiamond;

	[SerializeField]
	private Image imgDiamond;

	[SerializeField]
	private BoosterType boosterType;

	[SerializeField]
	private Image imgOverlay;

	public double usageTime = 60.0;

	public double timewarp = 3.0;

	public double timeRemaining;

	public bool isBoosterUsed;

	public bool IsBoosterUsed
	{
		get
		{
			return this.isBoosterUsed;
		}
		set
		{
			this.isBoosterUsed = value;
			this.SetFactor(value);
		}
	}

	public void SetBoosterValue(double value)
	{
		if (this.boosterType == BoosterType.TimeWarp)
		{
			this.timewarp = value;
		}
		else
		{
			this.usageTime = value;
		}
		this.UpdateTextBoosterValue(value);
	}

	public double GetBoosterValue()
	{
		if (this.boosterType == BoosterType.TimeWarp)
		{
			return this.timewarp;
		}
		return this.usageTime;
	}

	public void UpdateTextBoosterValue(double value)
	{
		UnityEngine.Debug.Log("UpdateTextBoosterValue");
		if (this.boosterType == BoosterType.TimeWarp)
		{
			this.txtUsageTime.text = string.Format("{0} HOURS", value);
		}
		else if (value / 60.0 == 1.0)
		{
			this.txtUsageTime.text = string.Format("{0} MIN", value / 60.0);
		}
		else
		{
			this.txtUsageTime.text = string.Format("{0} MINS", value / 60.0);
		}
	}

	public void StartBooster()
	{
		if (this.boosterType != BoosterType.TimeWarp)
		{
			this.timeRemaining = this.usageTime;
			this.IsBoosterUsed = true;
			this.SetButtonActive(false);
			UnityEngine.Debug.Log(this.timeRemaining);
			UnityEngine.Debug.Log(this.IsBoosterUsed);
			this.SaveRemainingTime();
		}
	}

	public void StopBooster()
	{
	}

	public void ResumeBooster()
	{
		this.timeRemaining = this.GetRemainingTime();
		if (this.timeRemaining <= 0.0)
		{
			//if (AdsControl.Instance.GetRewardAvailable() || GameController.instance.CheckIsEnoughDiamond(this.boosterType))
			if(GameController.instance.CheckIsEnoughDiamond(this.boosterType))
			{
				this.SetButtonActive(true);
				this.IsBoosterUsed = false;
			}
			else
			{
				this.SetButtonActive(true);
				this.IsBoosterUsed = false;
			}
		}
		else
		{
			this.SetButtonActive(false);
			this.IsBoosterUsed = true;
		}
	}

	public void SetButtonActive(bool isActive)
	{
		if (this.isBoosterUsed && isActive)
		{
			return;
		}
		if (isActive)
		{
			this.imgOverlay.gameObject.SetActive(false);
			this.btnBooster.interactable = true;
			this.txtCooldownTime.gameObject.SetActive(false);
			this.txtUsageTime.gameObject.SetActive(true);
		}
		else
		{
			this.imgOverlay.gameObject.SetActive(true);
			this.btnBooster.interactable = false;
			if (this.isBoosterUsed)
			{
				this.txtCooldownTime.gameObject.SetActive(true);
				this.txtUsageTime.gameObject.SetActive(false);
			}
			else
			{
				this.txtCooldownTime.gameObject.SetActive(false);
				this.txtUsageTime.gameObject.SetActive(true);
			}
		}
	}

	public void ShowEffect()
	{
		if (this.isBoosterUsed)
		{
			switch (this.boosterType)
			{
			case BoosterType.Speed:
				if (GameController.instance.tank != null)
				{
					UnityEngine.Debug.Log("ShowEffect");
					Tank.instance.ShowEffectLightning();
					SoundController.instance.PlaySoundSpeedBoosterUsing();
				}
				else
				{
					UnityEngine.Debug.Log("CantShowEffect");
				}
				break;
			case BoosterType.Cooldown:
				if (CanvasItem.instance)
				{
					CanvasItem.instance.ShowCooldownEffect();
				}
				break;
			case BoosterType.Revenue:
				if (CanvasTop.instance)
				{
					CanvasTop.instance.ShowMoneyBoosterEffect();
				}
				break;
			}
		}
	}

	public virtual void LoadBoosterData()
	{
		this.timeRemaining = this.GetRemainingTime();
		if (this.timeRemaining <= 0.0)
		{
			this.SetButtonActive(true);
			this.IsBoosterUsed = false;
		}
		else
		{
			this.SetButtonActive(false);
			this.IsBoosterUsed = true;
		}
	}

	public double GetLastTimeUsed()
	{
		switch (this.boosterType)
		{
		case BoosterType.Speed:
			return GameController.instance.game.Booster_speed_last_used;
		case BoosterType.Cooldown:
			return GameController.instance.game.Booster_cooldown_last_used;
		case BoosterType.Revenue:
			return GameController.instance.game.Booster_money_last_used;
		}
		return -1.0;
	}

	public double GetRemainingTime()
	{
		switch (this.boosterType)
		{
		case BoosterType.Speed:
			return GameController.instance.game.Booster_speed_time_remaining;
		case BoosterType.TimeWarp:
			return -1.0;
		case BoosterType.Cooldown:
			return GameController.instance.game.Booster_cooldown_time_remaining;
		case BoosterType.Revenue:
			return GameController.instance.game.Booster_money_time_remaining;
		default:
			return -1.0;
		}
	}

	public void SaveRemainingTime()
	{
		if (this.IsBoosterUsed)
		{
			switch (this.boosterType)
			{
			case BoosterType.Speed:
				GameController.instance.game.Booster_speed_time_remaining = this.timeRemaining;
				break;
			case BoosterType.Cooldown:
				GameController.instance.game.Booster_cooldown_time_remaining = this.timeRemaining;
				break;
			case BoosterType.Revenue:
				GameController.instance.game.Booster_money_time_remaining = this.timeRemaining;
				break;
			}
		}
		else
		{
			switch (this.boosterType)
			{
			case BoosterType.Speed:
				GameController.instance.game.Booster_speed_time_remaining = 0.0;
				break;
			case BoosterType.Cooldown:
				GameController.instance.game.Booster_cooldown_time_remaining = 0.0;
				break;
			case BoosterType.Revenue:
				GameController.instance.game.Booster_money_time_remaining = 0.0;
				break;
			}
		}
	}

	public void SaveBoosterData()
	{
		switch (this.boosterType)
		{
		case BoosterType.Speed:
			if (this.IsBoosterUsed)
			{
				GameController.instance.game.Booster_speed_last_used = DateTime.Now.TimeOfDay.TotalSeconds;
			}
			else
			{
				GameController.instance.game.Booster_speed_last_used = -1.0;
			}
			break;
		case BoosterType.Cooldown:
			if (this.IsBoosterUsed)
			{
				GameController.instance.game.Booster_cooldown_last_used = DateTime.Now.TimeOfDay.TotalSeconds;
			}
			else
			{
				GameController.instance.game.Booster_cooldown_last_used = -1.0;
			}
			break;
		case BoosterType.Revenue:
			if (this.IsBoosterUsed)
			{
				GameController.instance.game.Booster_money_last_used = DateTime.Now.TimeOfDay.TotalSeconds;
			}
			else
			{
				GameController.instance.game.Booster_money_last_used = -1.0;
			}
			break;
		}
	}

	internal void UpdateButtonInteration()
	{
		switch (this.boosterType)
		{
		}
	}

	public void UpdateText()
	{
		this.txtCooldownTime.text = Utilities.GetFormattedTime(this.timeRemaining) + "s";
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.isBoosterUsed)
		{
			this.timeRemaining -= (double)Time.deltaTime;
			this.UpdateText();
			if (this.timeRemaining <= 0.0)
			{
				this.isBoosterUsed = false;
				this.timeRemaining = 0.0;
				this.SetButtonActive(true);
				this.SetFactor(false);
				this.ClearKey();
				return;
			}
		}
	}

	private void ClearKey()
	{
		switch (this.boosterType)
		{
		case BoosterType.Speed:
			GameData.DeleteKey(PlayerPrefKey.KEY_BOOSTER_SPEED_TIME_REMAING);
			break;
		case BoosterType.Cooldown:
			GameData.DeleteKey(PlayerPrefKey.KEY_BOOSTER_COOLDOWN_TIME_REMAING);
			break;
		case BoosterType.Revenue:
			GameData.DeleteKey(PlayerPrefKey.KEY_BOOSTER_REVENUE_TIME_REMAING);
			break;
		}
		PreviewLabs.PlayerPrefs.Flush();
	}

	internal void SetFactor(bool _isBoosterUsed)
	{
		switch (this.boosterType)
		{
		case BoosterType.Speed:
			if (_isBoosterUsed)
			{
				GameController.instance.factorSpeed = 2;
			}
			else
			{
				if (GameController.instance.tank != null)
				{
					GameController.instance.tank.HideEffectLightning();
				}
				GameController.instance.factorSpeed = 1;
			}
			break;
		case BoosterType.Cooldown:
			if (_isBoosterUsed)
			{
				GameController.instance.factorCooldown = 2f;
			}
			else
			{
				if (CanvasItem.instance)
				{
					CanvasItem.instance.HideCooldownEffect();
				}
				GameController.instance.factorCooldown = 1f;
			}
			break;
		case BoosterType.Revenue:
			if (_isBoosterUsed)
			{
				GameController.instance.factorMoney = 2;
			}
			else
			{
				if (CanvasTop.instance)
				{
					CanvasTop.instance.HideMoneyBoosterEffect();
				}
				GameController.instance.factorMoney = 1;
			}
			break;
		}
	}

	public void ButtonBoosterPressed()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			switch (this.boosterType)
			{
			case BoosterType.Speed:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.Speed);
				break;
			case BoosterType.TimeWarp:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.TimeWarp);
				break;
			case BoosterType.Cooldown:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.Cooldown);
				break;
			case BoosterType.Revenue:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.Revenue);
				break;
			}
		}
		else
		{
			switch (this.boosterType)
			{
			case BoosterType.Speed:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.Speed);
				break;
			case BoosterType.TimeWarp:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.TimeWarp);
				break;
			case BoosterType.Cooldown:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.Cooldown);
				break;
			case BoosterType.Revenue:
				CanvasUpgrade.instance.ShowPanelBooster(BoosterType.Revenue);
				break;
			}
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			this.ResumeBooster();
		}
	}

	public void Release()
	{
		if (this.btnBooster.interactable)
		{
			this.imgOverlay.gameObject.SetActive(false);
		}
	}

	public void Hover()
	{
		this.imgOverlay.gameObject.SetActive(true);
	}

	public void ResetBooster()
	{
		this.IsBoosterUsed = false;
		this.ClearKey();
		this.SetButtonActive(true);
		this.timeRemaining = 0.0;
	}

	public void OnPointerClick()
	{
		if (!this.btnBooster.interactable)
		{
			SoundController.instance.PlaySoundButtonDisable();
		}
	}
}
