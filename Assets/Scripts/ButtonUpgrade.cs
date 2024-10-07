using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgrade : MonoBehaviour
{
	public Text txtLevel;

	public Text txtUpgradeName;

	public Text txtPrice;

	public UpgradeData upgradeData;

	public UpgradeType upgradeType;

	[SerializeField]
	private Image imgOverlay;

	private void Awake()
	{
		this.upgradeData = new UpgradeData();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void UpdateText()
	{
		this.txtLevel.text = "LEVEL " + this.upgradeData.Level.ToString();
		this.txtPrice.text = "O " + Utilities.getStringFromNumber((double)this.upgradeData.CurrentPrice, false);
		UpgradeType upgradeType = this.upgradeType;
		if (upgradeType != UpgradeType.BulletSpeed)
		{
			if (upgradeType == UpgradeType.ReloadSpeed)
			{
				if (this.upgradeData.Level == BaseValue.limit_upgrade_reloadspeed)
				{
					this.txtLevel.text = "MAX LEVEL";
					this.txtPrice.text = string.Empty;
				}
			}
		}
		else if (this.upgradeData.Level == BaseValue.limit_upgrade_bulletspeed)
		{
			this.txtLevel.text = "MAX LEVEL";
			this.txtPrice.text = string.Empty;
		}
	}

	public void UpdateButtonInteration(double coin)
	{
		UpgradeType upgradeType = this.upgradeType;
		if (upgradeType != UpgradeType.BulletSpeed)
		{
			if (upgradeType == UpgradeType.ReloadSpeed)
			{
				if (this.upgradeData.Level == BaseValue.limit_upgrade_reloadspeed)
				{
					return;
				}
			}
			if ((double)this.upgradeData.CurrentPrice > coin)
			{
				base.GetComponent<Button>().interactable = false;
				this.Hover();
			}
			else
			{
				if (!this.imgOverlay.gameObject.activeSelf)
				{
					this.Release();
				}
				else if (!base.GetComponent<Button>().interactable)
				{
					this.imgOverlay.gameObject.SetActive(false);
				}
				base.GetComponent<Button>().interactable = true;
			}
			return;
		}
		base.GetComponent<Button>().interactable = false;
	}

	public void BuyUpgrade()
	{
		SoundController.instance.PlaySoundButtonUpgrade();
		UnityEngine.Debug.Log(this.upgradeData.Level);
		if (GameController.instance.Coin >= this.upgradeData.CurrentPrice)
		{
			GameController.instance.Coin -= this.upgradeData.CurrentPrice;
			GameController.instance.UpdateTextCoin();
			NextValue nextValue = BaseValue.CalculateNextCost(this.upgradeData, 1, this.upgradeType);
			this.upgradeData.Value = nextValue.nextValue;
			this.upgradeData.CurrentPrice = (long)nextValue.nextCost;
			UnityEngine.Debug.Log(this.upgradeData.Level);
			UpgradeType upgradeType = this.upgradeType;
			if (upgradeType != UpgradeType.BulletSpeed)
			{
				if (upgradeType == UpgradeType.ReloadSpeed)
				{
					if (this.upgradeData.Level >= BaseValue.limit_upgrade_reloadspeed)
					{
						base.GetComponent<Button>().interactable = false;
						this.Hover();
					}
				}
			}
			else if (this.upgradeData.Level >= BaseValue.limit_upgrade_bulletspeed)
			{
				base.GetComponent<Button>().interactable = false;
				this.Hover();
			}
			this.UpdateText();
			this.AnimateEffect();
			if (this.upgradeType != UpgradeType.OfflineEarning)
			{
				if (this.upgradeType == UpgradeType.ReloadSpeed)
				{
					int level = this.upgradeData.Level;
					int num_of_time_reset = GameController.instance.game.Num_of_time_reset;
					if (num_of_time_reset != 1)
					{
						if (num_of_time_reset != 2)
						{
							if (level == BaseValue.level_add_bullet1_tank2)
							{
								this.upgradeData.Value = (double)(BaseValue.basevalue_reloadspeed_tank2 + 1.5f);
							}
							Gun.instance.UpdateStat(this.upgradeData.Value, this.upgradeType);
						}
						else
						{
							if (level == BaseValue.level_add_bullet1_tank2)
							{
								this.upgradeData.Value = (double)(BaseValue.basevalue_reloadspeed_tank2 + 1.5f);
							}
							Gun.instance.UpdateStat(this.upgradeData.Value, this.upgradeType);
						}
					}
					else
					{
						if (level == BaseValue.level_add_bullet1)
						{
							this.upgradeData.Value = (double)(BaseValue.basevalue_reloadspeed + 1f);
						}
						else if (level == BaseValue.level_add_bullet2)
						{
							this.upgradeData.Value = (double)(BaseValue.basevalue_reloadspeed + 3f);
						}
						Gun.instance.UpdateStat(this.upgradeData.Value, this.upgradeType);
					}
				}
				else
				{
					Gun.instance.UpdateStat(this.upgradeData.Value, this.upgradeType);
				}
			}
			else
			{
				GameController.instance.UpdateOfflineEarningValue(this.upgradeData.Value);
			}
			return;
		}
	}

	public void SaveUpgradeData()
	{
		switch (this.upgradeType)
		{
		case UpgradeType.Power:
			GameController.instance.game.Upgrade_power_level = this.upgradeData.Level;
			GameController.instance.game.Upgrade_power_price = this.upgradeData.CurrentPrice;
			break;
		case UpgradeType.BulletSpeed:
			GameController.instance.game.Upgrade_bulletspeed_level = this.upgradeData.Level;
			GameController.instance.game.Upgrade_bulletspeed_price = this.upgradeData.CurrentPrice;
			break;
		case UpgradeType.ReloadSpeed:
			GameController.instance.game.Upgrade_reloadspeed_level = this.upgradeData.Level;
			GameController.instance.game.Upgrade_reloadspeed_price = this.upgradeData.CurrentPrice;
			break;
		case UpgradeType.OfflineEarning:
			GameController.instance.game.Upgrade_offlineearning_level = this.upgradeData.Level;
			GameController.instance.game.Upgrade_offlineearning_price = this.upgradeData.CurrentPrice;
			break;
		}
	}

	public void LoadUpgradeData()
	{
		switch (this.upgradeType)
		{
		case UpgradeType.Power:
			this.upgradeData.Level = GameController.instance.game.Upgrade_power_level;
			this.upgradeData.CurrentPrice = GameController.instance.game.Upgrade_power_price;
			this.upgradeData.Value = (double)Gun.instance.Power;
			break;
		case UpgradeType.BulletSpeed:
			this.upgradeData.Level = GameController.instance.game.Upgrade_bulletspeed_level;
			this.upgradeData.CurrentPrice = GameController.instance.game.Upgrade_bulletspeed_price;
			this.upgradeData.Value = (double)Gun.instance.BulletSpeed;
			if (this.upgradeData.Level == BaseValue.limit_upgrade_bulletspeed)
			{
				base.GetComponent<Button>().interactable = false;
				this.Hover();
			}
			break;
		case UpgradeType.ReloadSpeed:
			this.upgradeData.Level = GameController.instance.game.Upgrade_reloadspeed_level;
			this.upgradeData.CurrentPrice = GameController.instance.game.Upgrade_reloadspeed_price;
			this.upgradeData.Value = Gun.instance.ReloadSpeed;
			if (this.upgradeData.Level == BaseValue.limit_upgrade_reloadspeed)
			{
				base.GetComponent<Button>().interactable = false;
				this.Hover();
			}
			break;
		case UpgradeType.OfflineEarning:
			this.upgradeData.Level = GameController.instance.game.Upgrade_offlineearning_level;
			this.upgradeData.CurrentPrice = GameController.instance.game.Upgrade_offlineearning_price;
			this.upgradeData.Value = GameController.instance.OfflineEarning;
			break;
		}
		this.UpdateText();
	}

	public void Release()
	{
		if (base.GetComponent<Button>().interactable)
		{
			this.imgOverlay.gameObject.SetActive(false);
		}
	}

	public void Hover()
	{
		this.imgOverlay.gameObject.SetActive(true);
	}

	public void AnimateEffect()
	{
		GameObject pooledObject = UpgradeLevelArrowPooler.instance.GetPooledObject();
		pooledObject.SetActive(true);
	}

	public void ResetUpgrade()
	{
		this.upgradeData.Level = 1;
		this.upgradeData.Value = (double)Gun.instance.Power;
		switch (this.upgradeType)
		{
		case UpgradeType.Power:
			this.upgradeData.CurrentPrice = BaseValue.basecost_power;
			this.upgradeData.Value = (double)BaseValue.basevalue_power;
			break;
		case UpgradeType.BulletSpeed:
			this.upgradeData.CurrentPrice = BaseValue.basecost_bulletspeed;
			this.upgradeData.Value = (double)BaseValue.basevalue_bulletspeed;
			break;
		case UpgradeType.ReloadSpeed:
			this.upgradeData.CurrentPrice = BaseValue.basecost_reloadspeed;
			this.upgradeData.Value = (double)BaseValue.basevalue_reloadspeed;
			break;
		case UpgradeType.OfflineEarning:
			this.upgradeData.CurrentPrice = BaseValue.basecost_offlineearning;
			this.upgradeData.Value = (double)BaseValue.basevalue_offlineearning;
			break;
		}
		this.UpdateText();
		this.SaveUpgradeData();
	}

	public void OnPointerClick()
	{
		if (!base.GetComponent<Button>().interactable)
		{
			SoundController.instance.PlaySoundButtonDisable();
		}
	}
}
