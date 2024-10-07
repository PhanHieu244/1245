using PreviewLabs;
using System;
using UnityEngine;

public class GameData
{
	private int upgrade_power_level;

	private long upgrade_power_price;

	private int upgrade_bulletspeed_level;

	private long upgrade_bulletspeed_price;

	private int upgrade_reloadspeed_level;

	private long upgrade_reloadspeed_price;

	private float upgrade_offlineearning_value;

	private int upgrade_offlineearning_level;

	private long upgrade_offlineearning_price;

	private long gun_power;

	private long gun_bulletspeed;

	private double gun_reloadspeed;

	private int level;

	private long coin;

	private long diamond;

	private double booster_speed_last_used;

	private double booster_speed_time_remaining;

	private bool is_booster_timewarp_used;

	private double booster_cooldown_last_used;

	private double booster_cooldown_time_remaining;

	private bool is_booster_cooldown_used;

	private double booster_money_last_used;

	private double booster_money_time_remaining;

	private bool is_booster_money_used;

	private double item1_last_used;

	private double item1_time_remaining;

	private double item2_last_used;

	private double item2_time_remaining;

	private double item3_last_used;

	private double item3_time_remaining;

	private double item4_last_used;

	private double item4_time_remaining;

	private double item5_last_used;

	private double item5_time_remaining;

	private double item6_last_used;

	private double item6_time_remaining;

	private double item7_last_used;

	private double item7_time_remaining;

	private string last_time_close_app;

	private int num_of_time_reset;

	private bool check_sound_enable;

	private bool check_auto_use_item;

	private bool check_passed_100_level;

	public int Upgrade_power_level
	{
		get
		{
			this.upgrade_power_level = PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_POWER, 1);
			return this.upgrade_power_level;
		}
		set
		{
			this.upgrade_power_level = value;
			PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_POWER, this.upgrade_power_level);
		}
	}

	public long Upgrade_power_price
	{
		get
		{
			this.upgrade_power_price = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_POWER, (long)BaseValue.GetBaseCost(UpgradeType.Power));
			return this.upgrade_power_price;
		}
		set
		{
			this.upgrade_power_price = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_POWER, this.upgrade_power_price);
		}
	}

	public int Upgrade_bulletspeed_level
	{
		get
		{
			this.upgrade_bulletspeed_level = PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_BULLETSPEED, 1);
			UnityEngine.Debug.Log(PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_BULLETSPEED));
			UnityEngine.Debug.Log(PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_BULLETSPEED, 1));
			UnityEngine.Debug.Log(this.upgrade_bulletspeed_level);
			return this.upgrade_bulletspeed_level;
		}
		set
		{
			this.upgrade_bulletspeed_level = value;
			PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_BULLETSPEED, this.upgrade_bulletspeed_level);
		}
	}

	public long Upgrade_bulletspeed_price
	{
		get
		{
			this.upgrade_bulletspeed_price = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_BULLETSPEED, (long)BaseValue.GetBaseCost(UpgradeType.BulletSpeed));
			return this.upgrade_bulletspeed_price;
		}
		set
		{
			this.upgrade_bulletspeed_price = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_BULLETSPEED, this.upgrade_bulletspeed_price);
		}
	}

	public int Upgrade_reloadspeed_level
	{
		get
		{
			this.upgrade_reloadspeed_level = PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_RELOADSPEED, 1);
			return this.upgrade_reloadspeed_level;
		}
		set
		{
			this.upgrade_reloadspeed_level = value;
			PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_RELOADSPEED, this.upgrade_reloadspeed_level);
		}
	}

	public long Upgrade_reloadspeed_price
	{
		get
		{
			this.upgrade_reloadspeed_price = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_RELOADSPEED, (long)BaseValue.GetBaseCost(UpgradeType.ReloadSpeed));
			return this.upgrade_reloadspeed_price;
		}
		set
		{
			this.upgrade_reloadspeed_price = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_RELOADSPEED, this.upgrade_reloadspeed_price);
		}
	}

	public float Upgrade_offlineearning_value
	{
		get
		{
			this.upgrade_offlineearning_value = PreviewLabs.PlayerPrefs.GetFloat(PlayerPrefKey.KEY_UPGRADE_VALUE_OFFLINEEARNING, (float)BaseValue.basevalue_offlineearning);
			return this.upgrade_offlineearning_value;
		}
		set
		{
			this.upgrade_offlineearning_value = value;
			PreviewLabs.PlayerPrefs.SetFloat(PlayerPrefKey.KEY_UPGRADE_VALUE_OFFLINEEARNING, this.upgrade_offlineearning_value);
		}
	}

	public int Upgrade_offlineearning_level
	{
		get
		{
			this.upgrade_offlineearning_level = PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_OFFLINEEARNING, 1);
			return this.upgrade_offlineearning_level;
		}
		set
		{
			this.upgrade_offlineearning_level = value;
			PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_UPGRADE_LEVEL_OFFLINEEARNING, this.upgrade_offlineearning_level);
		}
	}

	public long Upgrade_offlineearning_price
	{
		get
		{
			this.upgrade_offlineearning_price = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_OFFLINEEARNING, (long)BaseValue.GetBaseCost(UpgradeType.OfflineEarning));
			return this.upgrade_offlineearning_price;
		}
		set
		{
			this.upgrade_offlineearning_price = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_UPGRADE_PRICE_OFFLINEEARNING, this.upgrade_offlineearning_price);
		}
	}

	public long Gun_power
	{
		get
		{
			this.gun_power = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_TANK_POWER, (long)BaseValue.GetBaseValue(UpgradeType.Power));
			return this.gun_power;
		}
		set
		{
			this.gun_power = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_TANK_POWER, this.gun_power);
		}
	}

	public long Gun_bulletspeed
	{
		get
		{
			this.gun_bulletspeed = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_TANK_BULLETSPEED, (long)BaseValue.GetBaseValue(UpgradeType.BulletSpeed));
			return this.gun_bulletspeed;
		}
		set
		{
			this.gun_bulletspeed = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_TANK_BULLETSPEED, this.gun_bulletspeed);
		}
	}

	public double Gun_reloadspeed
	{
		get
		{
			this.gun_reloadspeed = GameData.GetDouble(PlayerPrefKey.KEY_TANK_RELOADSPEED, BaseValue.GetBaseValue(UpgradeType.ReloadSpeed));
			return this.gun_reloadspeed;
		}
		set
		{
			this.gun_reloadspeed = value;
			GameData.SetDouble(PlayerPrefKey.KEY_TANK_RELOADSPEED, this.gun_reloadspeed);
		}
	}

	public int Level
	{
		get
		{
			this.level = PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_LEVEL, 1);
			return this.level;
		}
		set
		{
			this.level = value;
			PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_LEVEL, this.level);
		}
	}

	public long Coin
	{
		get
		{
			this.coin = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_COIN, 0L);
			return this.coin;
		}
		set
		{
			this.coin = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_COIN, this.coin);
		}
	}

	public long Diamond
	{
		get
		{
			this.diamond = PreviewLabs.PlayerPrefs.GetLong(PlayerPrefKey.KEY_DIAMOND, 0L);
			return this.diamond;
		}
		set
		{
			this.diamond = value;
			PreviewLabs.PlayerPrefs.SetLong(PlayerPrefKey.KEY_DIAMOND, this.diamond);
		}
	}

	public double Booster_speed_last_used
	{
		get
		{
			this.booster_speed_last_used = GameData.GetDouble(PlayerPrefKey.KEY_BOOSTER_SPEED_LAST_TIME, -1.0);
			return this.booster_speed_last_used;
		}
		set
		{
			this.booster_speed_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_BOOSTER_SPEED_LAST_TIME, this.booster_speed_last_used);
		}
	}

	public double Booster_speed_time_remaining
	{
		get
		{
			this.booster_speed_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_BOOSTER_SPEED_TIME_REMAING, -1.0);
			return this.booster_speed_time_remaining;
		}
		set
		{
			this.booster_speed_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_BOOSTER_SPEED_TIME_REMAING, this.booster_speed_time_remaining);
		}
	}

	public bool Is_booster_timewarp_used
	{
		get
		{
			this.is_booster_timewarp_used = PreviewLabs.PlayerPrefs.GetBool(PlayerPrefKey.KEY_BOOSTER_TIMEWARP_IS_USED, false);
			return this.is_booster_timewarp_used;
		}
		set
		{
			this.is_booster_timewarp_used = value;
			PreviewLabs.PlayerPrefs.SetBool(PlayerPrefKey.KEY_BOOSTER_TIMEWARP_IS_USED, this.is_booster_timewarp_used);
		}
	}

	public double Booster_cooldown_last_used
	{
		get
		{
			this.booster_cooldown_last_used = GameData.GetDouble(PlayerPrefKey.KEY_BOOSTER_COOLDOWN_LAST_TIME, -1.0);
			return this.booster_cooldown_last_used;
		}
		set
		{
			this.booster_cooldown_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_BOOSTER_COOLDOWN_LAST_TIME, this.booster_cooldown_last_used);
		}
	}

	public double Booster_cooldown_time_remaining
	{
		get
		{
			this.booster_cooldown_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_BOOSTER_COOLDOWN_TIME_REMAING, -1.0);
			return this.booster_cooldown_time_remaining;
		}
		set
		{
			this.booster_cooldown_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_BOOSTER_COOLDOWN_TIME_REMAING, this.booster_cooldown_time_remaining);
		}
	}

	public bool Is_booster_cooldown_used
	{
		get
		{
			this.is_booster_cooldown_used = PreviewLabs.PlayerPrefs.GetBool(PlayerPrefKey.KEY_BOOSTER_COOLDOWN_IS_USED, false);
			return this.is_booster_cooldown_used;
		}
		set
		{
			this.is_booster_cooldown_used = value;
			PreviewLabs.PlayerPrefs.SetBool(PlayerPrefKey.KEY_BOOSTER_COOLDOWN_IS_USED, this.is_booster_cooldown_used);
		}
	}

	public double Booster_money_last_used
	{
		get
		{
			this.booster_money_last_used = GameData.GetDouble(PlayerPrefKey.KEY_BOOSTER_REVENUE_LAST_TIME, -1.0);
			return this.booster_money_last_used;
		}
		set
		{
			this.booster_money_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_BOOSTER_REVENUE_LAST_TIME, this.booster_money_last_used);
		}
	}

	public double Booster_money_time_remaining
	{
		get
		{
			this.booster_money_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_BOOSTER_REVENUE_TIME_REMAING, -1.0);
			return this.booster_money_time_remaining;
		}
		set
		{
			this.booster_money_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_BOOSTER_REVENUE_TIME_REMAING, this.booster_money_time_remaining);
		}
	}

	public bool Is_booster_money_used
	{
		get
		{
			this.is_booster_money_used = PreviewLabs.PlayerPrefs.GetBool(PlayerPrefKey.KEY_BOOSTER_REVENUE_IS_USED, false);
			return this.is_booster_money_used;
		}
		set
		{
			this.is_booster_money_used = value;
			PreviewLabs.PlayerPrefs.SetBool(PlayerPrefKey.KEY_BOOSTER_REVENUE_IS_USED, this.is_booster_money_used);
		}
	}

	public double Item1_last_used
	{
		get
		{
			this.item1_last_used = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_1_LAST_TIME, -1.0);
			return this.item1_last_used;
		}
		set
		{
			this.item1_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_1_LAST_TIME, this.item1_last_used);
		}
	}

	public double Item1_time_remaining
	{
		get
		{
			this.item1_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_1_TIME_REMAINING, -1.0);
			return this.item1_time_remaining;
		}
		set
		{
			this.item1_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_1_TIME_REMAINING, this.item1_time_remaining);
		}
	}

	public double Item2_last_used
	{
		get
		{
			this.item2_last_used = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_2_LAST_TIME, -1.0);
			return this.item2_last_used;
		}
		set
		{
			this.item2_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_2_LAST_TIME, this.item2_last_used);
		}
	}

	public double Item2_time_remaining
	{
		get
		{
			this.item2_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_2_TIME_REMAINING, -1.0);
			return this.item2_time_remaining;
		}
		set
		{
			this.item2_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_2_TIME_REMAINING, this.item2_time_remaining);
		}
	}

	public double Item3_last_used
	{
		get
		{
			this.item3_last_used = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_3_LAST_TIME, -1.0);
			return this.item3_last_used;
		}
		set
		{
			this.item3_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_3_LAST_TIME, this.item3_last_used);
		}
	}

	public double Item3_time_remaining
	{
		get
		{
			this.item3_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_3_TIME_REMAINING, -1.0);
			return this.item3_time_remaining;
		}
		set
		{
			this.item3_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_3_TIME_REMAINING, this.item3_time_remaining);
		}
	}

	public double Item4_last_used
	{
		get
		{
			this.item4_last_used = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_4_LAST_TIME, -1.0);
			return this.item4_last_used;
		}
		set
		{
			this.item4_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_4_LAST_TIME, this.item4_last_used);
		}
	}

	public double Item4_time_remaining
	{
		get
		{
			this.item4_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_4_TIME_REMAINING, -1.0);
			return this.item4_time_remaining;
		}
		set
		{
			this.item4_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_4_TIME_REMAINING, this.item4_time_remaining);
		}
	}

	public double Item5_last_used
	{
		get
		{
			this.item5_last_used = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_5_LAST_TIME, -1.0);
			return this.item5_last_used;
		}
		set
		{
			this.item5_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_5_LAST_TIME, this.item5_last_used);
		}
	}

	public double Item5_time_remaining
	{
		get
		{
			this.item5_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_5_TIME_REMAINING, -1.0);
			return this.item5_time_remaining;
		}
		set
		{
			this.item5_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_5_TIME_REMAINING, this.item5_time_remaining);
		}
	}

	public double Item6_last_used
	{
		get
		{
			this.item6_last_used = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_6_LAST_TIME, -1.0);
			return this.item6_last_used;
		}
		set
		{
			this.item6_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_6_LAST_TIME, this.item6_last_used);
		}
	}

	public double Item6_time_remaining
	{
		get
		{
			this.item6_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_6_TIME_REMAINING, -1.0);
			return this.item6_time_remaining;
		}
		set
		{
			this.item6_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_6_TIME_REMAINING, this.item6_time_remaining);
		}
	}

	public double Item7_last_used
	{
		get
		{
			this.item7_last_used = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_7_LAST_TIME, -1.0);
			return this.item7_last_used;
		}
		set
		{
			this.item7_last_used = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_7_LAST_TIME, this.item7_last_used);
		}
	}

	public double Item7_time_remaining
	{
		get
		{
			this.item7_time_remaining = GameData.GetDouble(PlayerPrefKey.KEY_ITEM_7_TIME_REMAINING, -1.0);
			return this.item7_time_remaining;
		}
		set
		{
			this.item7_time_remaining = value;
			GameData.SetDouble(PlayerPrefKey.KEY_ITEM_7_TIME_REMAINING, this.item7_time_remaining);
		}
	}

	public string Last_time_close_app
	{
		get
		{
			this.last_time_close_app = PreviewLabs.PlayerPrefs.GetString(PlayerPrefKey.KEY_LAST_TIME_CLOSE_APP, DateTime.Now.ToBinary().ToString());
			return this.last_time_close_app;
		}
		set
		{
			this.last_time_close_app = value;
			PreviewLabs.PlayerPrefs.SetString(PlayerPrefKey.KEY_LAST_TIME_CLOSE_APP, this.last_time_close_app);
		}
	}

	public int Num_of_time_reset
	{
		get
		{
			this.num_of_time_reset = PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_NUM_OF_TIME_RESET, 1);
			return this.num_of_time_reset;
		}
		set
		{
			this.num_of_time_reset = value;
			PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_NUM_OF_TIME_RESET, this.num_of_time_reset);
		}
	}

	public bool Check_sound_enable
	{
		get
		{
			this.check_sound_enable = PreviewLabs.PlayerPrefs.GetBool(PlayerPrefKey.KEY_SOUND, true);
			return this.check_sound_enable;
		}
		set
		{
			this.check_sound_enable = value;
			PreviewLabs.PlayerPrefs.SetBool(PlayerPrefKey.KEY_SOUND, this.check_sound_enable);
		}
	}

	public bool Check_auto_use_item
	{
		get
		{
			this.check_auto_use_item = PreviewLabs.PlayerPrefs.GetBool(PlayerPrefKey.KEY_AUTO_USE, true);
			return this.check_auto_use_item;
		}
		set
		{
			this.check_auto_use_item = value;
			PreviewLabs.PlayerPrefs.SetBool(PlayerPrefKey.KEY_AUTO_USE, this.check_auto_use_item);
		}
	}

	public bool Check_passed_100_level
	{
		get
		{
			this.check_passed_100_level = PreviewLabs.PlayerPrefs.GetBool(PlayerPrefKey.KEY_CHECK_PASSED_100_LEVEL, true);
			return this.check_passed_100_level;
		}
		set
		{
			this.check_passed_100_level = value;
			PreviewLabs.PlayerPrefs.SetBool(PlayerPrefKey.KEY_CHECK_PASSED_100_LEVEL, this.check_passed_100_level);
		}
	}

	public static void DeleteKey(string key)
	{
		PreviewLabs.PlayerPrefs.DeleteKey(key);
	}

	public static void SetDouble(string key, double value)
	{
		PreviewLabs.PlayerPrefs.SetString(key, GameData.DoubleToString(value));
	}

	public static double GetDouble(string key, double defaultValue)
	{
		string defaultValue2 = GameData.DoubleToString(defaultValue);
		return GameData.StringToDouble(PreviewLabs.PlayerPrefs.GetString(key, defaultValue2));
	}

	public static double GetDouble(string key)
	{
		return GameData.GetDouble(key, 0.0);
	}

	private static string DoubleToString(double target)
	{
		return target.ToString("R");
	}

	private static double StringToDouble(string target)
	{
		if (string.IsNullOrEmpty(target))
		{
			return 0.0;
		}
		return double.Parse(target);
	}
}
