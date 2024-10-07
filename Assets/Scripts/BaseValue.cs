using System;
using System.Linq;
using UnityEngine;

public class BaseValue
{
	public static float coef = 1.1f;

	public static float autocost = 1f;

	public static float adding_reloadspeed = 0.5f;

	public static long basecost_power = 100L;

	public static long basecost_bulletspeed = 110L;

	public static long basecost_reloadspeed = 120L;

	public static long basecost_offlineearning = 80L;

	public static long upgrade_level_power = 1L;

	public static float upgrade_level_bulletspeed = 0.05f;

	public static float upgrade_level_reloadspeed = 0.05f;

	public static long upgrade_level_offlineearning = 1L;

	public static long basevalue_power = 1L;

	public static long basevalue_bulletspeed = 15L;

	public static float basevalue_reloadspeed = 2f;

	public static long basevalue_offlineearning = 5L;

	public static int level_add_bullet1 = 70;

	public static int level_add_bullet2 = 140;

	public static long basevalue_power_tank2 = 1L;

	public static long basevalue_bulletspeed_tank2 = 15L;

	public static float basevalue_reloadspeed_tank2 = 2f;

	public static long basevalue_offlineearning_tank2 = 5L;

	public static int level_add_bullet1_tank2 = 100;

	public static long auto_cost_power = 10L;

	public static long auto_cost_bulletspeed = 12L;

	public static long auto_cost_reloadspeed = 15L;

	public static long auto_cost_offlineearning = 7L;

	public static int coin_per_hit = 5;

	public static int item1_reload_time = 4;

	public static int item2_reload_time = 10;

	public static int item3_reload_time = 20;

	public static int item4_reload_time = 40;

	public static int item5_reload_time = 30;

	public static int item6_reload_time = 25;

	public static int item7_reload_time = 60;

	public static long vertical_slam_base_damage = 5L;

	public static long spliter_chain_base_damage = 50L;

	public static long air_strike_base_damage = 150L;

	public static long napalm_base_damage = 700L;

	public static long item5_base_damage = 600L;

	public static long item6_base_damage = 400L;

	public static long item7_base_damage = 1500L;

	public static long coin_per_item1_hit = 20L;

	public static long coin_per_item2_hit = 200L;

	public static long coin_per_item3_hit = 450L;

	public static long coin_per_item4_hit = 1500L;

	public static long coin_per_item5_hit = 1000L;

	public static long coin_per_item6_hit = 600L;

	public static long coin_per_item7_hit = 2500L;

	public static long limit_time_for_next_offline_earning = 180L;

	public static int damage_percent_item = 50;

	public static int bonus_time = 15;

	public static int[] level_unlock = new int[]
	{
		3,
		9,
		17,
		23,
		34,
		47,
		59
	};

	public static int booster_value1 = 60;

	public static int booster_value2 = 2;

	public static int booster_value3 = 120;

	public static int booster_value4 = 120;

	public static float interstitial_percent = 100f;

	public static int limit_upgrade_bulletspeed = 300;

	public static int limit_upgrade_reloadspeed = 200;

	public static float y_distance_between_2_bullet = 0.29f;

	public static int level_reset = 100;

	public static int num_level_change_bg = 5;

	public static int[] diamond_booster = new int[]
	{
		5,
		7,
		8,
		5
	};

	public static Vector3[] paths = new Vector3[]
	{
		new Vector3(1.2f, 1.2f, 0f),
		new Vector3(1.6f, 2.5f, 0f),
		new Vector3(2f, 3.8f, 0f)
	};

	public static int[] list_boss_level = new int[]
	{
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20
	};

	public static int[] list_normal_easy = new int[]
	{
		31,
		32,
		33,
		34,
		35,
		36,
		37,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49
	};

	public static int[] list_normal_medium = new int[]
	{
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56
	};

	public static int[] list_normal_hard = new int[]
	{
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		62,
		63,
		64,
		65,
		66,
		67
	};

	public static int[] list_color_block = new int[]
	{
		30,
		100,
		200,
		400,
		700,
		1100,
		1800,
		3000,
		5500,
		10000,
		16000,
		25000,
		38000,
		55000,
		75000,
		150000
	};

	public static void UpdateStaticData()
	{
		BaseValue.adding_reloadspeed = FirebaseRemoteConfig.adding_reload_speed;
		BaseValue.coin_per_hit = FirebaseRemoteConfig.coin_per_hit;
		BaseValue.item1_reload_time = FirebaseRemoteConfig.item1_reload_time;
		BaseValue.item2_reload_time = FirebaseRemoteConfig.item2_reload_time;
		BaseValue.item3_reload_time = FirebaseRemoteConfig.item3_reload_time;
		BaseValue.item4_reload_time = FirebaseRemoteConfig.item4_reload_time;
		BaseValue.item5_reload_time = FirebaseRemoteConfig.item5_reload_time;
		BaseValue.item6_reload_time = FirebaseRemoteConfig.item6_reload_time;
		BaseValue.item7_reload_time = FirebaseRemoteConfig.item7_reload_time;
		BaseValue.vertical_slam_base_damage = (long)FirebaseRemoteConfig.base_damage_item1;
		BaseValue.spliter_chain_base_damage = (long)FirebaseRemoteConfig.base_damage_item2;
		BaseValue.air_strike_base_damage = (long)FirebaseRemoteConfig.base_damage_item3;
		BaseValue.napalm_base_damage = (long)FirebaseRemoteConfig.base_damage_item4;
		BaseValue.item5_base_damage = (long)FirebaseRemoteConfig.base_damage_item5;
		BaseValue.item6_base_damage = (long)FirebaseRemoteConfig.base_damage_item6;
		BaseValue.item7_base_damage = (long)FirebaseRemoteConfig.base_damage_item7;
		BaseValue.coin_per_item1_hit = (long)FirebaseRemoteConfig.base_coin_item1;
		BaseValue.coin_per_item2_hit = (long)FirebaseRemoteConfig.base_coin_item2;
		BaseValue.coin_per_item3_hit = (long)FirebaseRemoteConfig.base_coin_item3;
		BaseValue.coin_per_item4_hit = (long)FirebaseRemoteConfig.base_coin_item4;
		BaseValue.coin_per_item5_hit = (long)FirebaseRemoteConfig.base_coin_item5;
		BaseValue.coin_per_item6_hit = (long)FirebaseRemoteConfig.base_coin_item6;
		BaseValue.coin_per_item7_hit = (long)FirebaseRemoteConfig.base_coin_item7;
		BaseValue.limit_time_for_next_offline_earning = (long)FirebaseRemoteConfig.limit_time_next_offline_earning;
		BaseValue.damage_percent_item = (int)FirebaseRemoteConfig.damage_percent_item;
		BaseValue.bonus_time = (int)FirebaseRemoteConfig.bonus_time;
		string[] array = FirebaseRemoteConfig.level_unlock;
		if (array.Count<string>() != 0)
		{
			for (int i = 0; i < array.Count<string>(); i++)
			{
				BaseValue.level_unlock[i] = int.Parse(array[i]);
			}
		}
		BaseValue.booster_value1 = FirebaseRemoteConfig.booster_value1;
		BaseValue.booster_value2 = FirebaseRemoteConfig.booster_value2;
		BaseValue.booster_value3 = FirebaseRemoteConfig.booster_value3;
		BaseValue.booster_value4 = FirebaseRemoteConfig.booster_value4;
		BaseValue.interstitial_percent = FirebaseRemoteConfig.interstitial_percent;
		BaseValue.limit_upgrade_bulletspeed = FirebaseRemoteConfig.limit_upgrade_bulletspeed;
		BaseValue.limit_upgrade_reloadspeed = FirebaseRemoteConfig.limit_upgrade_reloadspeed;
		string[] array2 = FirebaseRemoteConfig.diamond_booster;
		if (array2.Count<string>() != 0)
		{
			for (int j = 0; j < array2.Count<string>(); j++)
			{
				BaseValue.diamond_booster[j] = int.Parse(array2[j]);
			}
		}
	}

	public static float CalculateNextCost(float currentValue, int round)
	{
		return (float)BaseValue.basecost_power * Mathf.Pow(BaseValue.coef, (float)round);
	}

	public static NextValue CalculateNextCost(UpgradeData upgradeData, int round, UpgradeType type)
	{
		upgradeData.Level++;
		double num = 0.0;
		NextValue nextValue = new NextValue();
		switch (type)
		{
		case UpgradeType.Power:
		{
			long num2 = BaseValue.auto_cost_power;
			num = (double)BaseValue.upgrade_level_power;
			break;
		}
		case UpgradeType.BulletSpeed:
		{
			long num2 = BaseValue.auto_cost_bulletspeed;
			num = (double)BaseValue.upgrade_level_bulletspeed;
			break;
		}
		case UpgradeType.ReloadSpeed:
		{
			long num2 = BaseValue.auto_cost_reloadspeed;
			num = (double)BaseValue.upgrade_level_reloadspeed;
			break;
		}
		case UpgradeType.OfflineEarning:
		{
			long num2 = BaseValue.auto_cost_offlineearning;
			num = (double)BaseValue.upgrade_level_offlineearning;
			break;
		}
		}
		nextValue.nextCost = (double)((float)upgradeData.CurrentPrice * BaseValue.coef);
		if (type == UpgradeType.Power)
		{
			nextValue.nextValue = upgradeData.Value + num * (double)BaseValue.GetCoefPowerLevel(upgradeData.Level);
			UnityEngine.Debug.Log(nextValue.nextValue + " BASE VALUE");
		}
		else
		{
			nextValue.nextValue = upgradeData.Value + num;
			UnityEngine.Debug.Log(nextValue.nextValue + " BASE VALUE");
		}
		return nextValue;
	}

	public static double GetBaseCost(UpgradeType type)
	{
		float num = 0f;
		switch (type)
		{
		case UpgradeType.Power:
			num = (float)BaseValue.basecost_power;
			break;
		case UpgradeType.BulletSpeed:
			num = (float)BaseValue.basecost_bulletspeed;
			break;
		case UpgradeType.ReloadSpeed:
			num = (float)BaseValue.basecost_reloadspeed;
			break;
		case UpgradeType.OfflineEarning:
			num = (float)BaseValue.basecost_offlineearning;
			break;
		}
		return (double)num;
	}

	public static double GetBaseValue(UpgradeType type)
	{
		float num = 0f;
		switch (type)
		{
		case UpgradeType.Power:
			num = (float)BaseValue.basevalue_power;
			break;
		case UpgradeType.BulletSpeed:
			num = (float)BaseValue.basevalue_bulletspeed;
			break;
		case UpgradeType.ReloadSpeed:
			num = BaseValue.basevalue_reloadspeed;
			break;
		case UpgradeType.OfflineEarning:
			num = (float)BaseValue.basevalue_offlineearning;
			break;
		}
		return (double)num;
	}

	public static int GetCoefPowerLevel(int level)
	{
		return level / 10 + 1;
	}

	public static int GetCoefLevel(int level)
	{
		return level / 7 + 1;
	}

	public static int GetCoefLevel_2(int level)
	{
		return level / 10 + 1;
	}

	public static int GetCoefLevelBonus(int level)
	{
		return level / 10 + 1;
	}

	public static int GetCoefBonus(int level)
	{
		return 5 * (5 + (level / 5 - 1));
	}
}
