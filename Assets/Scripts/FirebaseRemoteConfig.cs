using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseRemoteConfig : MonoBehaviour
{
	private static string PREFIX = "idle_tank_";

	private static string FBASE_ADDING_RELOADSPEED = global::FirebaseRemoteConfig.PREFIX + "adding_reload_speed";

	private static string FBASE_COIN_PER_HIT = global::FirebaseRemoteConfig.PREFIX + "coin_per_hit";

	private static string FBASE_ITEM1_RELOAD_TIME = global::FirebaseRemoteConfig.PREFIX + "item1_reload_time";

	private static string FBASE_ITEM2_RELOAD_TIME = global::FirebaseRemoteConfig.PREFIX + "item2_reload_time";

	private static string FBASE_ITEM3_RELOAD_TIME = global::FirebaseRemoteConfig.PREFIX + "item3_reload_time";

	private static string FBASE_ITEM4_RELOAD_TIME = global::FirebaseRemoteConfig.PREFIX + "item4_reload_time";

	private static string FBASE_ITEM5_RELOAD_TIME = global::FirebaseRemoteConfig.PREFIX + "item5_reload_time";

	private static string FBASE_ITEM6_RELOAD_TIME = global::FirebaseRemoteConfig.PREFIX + "item6_reload_time";

	private static string FBASE_ITEM7_RELOAD_TIME = global::FirebaseRemoteConfig.PREFIX + "item7_reload_time";

	private static string FBASE_BASE_DAMAGE_ITEM1 = global::FirebaseRemoteConfig.PREFIX + "base_damage_item1";

	private static string FBASE_BASE_DAMAGE_ITEM2 = global::FirebaseRemoteConfig.PREFIX + "base_damage_item2";

	private static string FBASE_BASE_DAMAGE_ITEM3 = global::FirebaseRemoteConfig.PREFIX + "base_damage_item3";

	private static string FBASE_BASE_DAMAGE_ITEM4 = global::FirebaseRemoteConfig.PREFIX + "base_damage_item4";

	private static string FBASE_BASE_DAMAGE_ITEM5 = global::FirebaseRemoteConfig.PREFIX + "base_damage_item5";

	private static string FBASE_BASE_DAMAGE_ITEM6 = global::FirebaseRemoteConfig.PREFIX + "base_damage_item6";

	private static string FBASE_BASE_DAMAGE_ITEM7 = global::FirebaseRemoteConfig.PREFIX + "base_damage_item7";

	private static string FBASE_BASE_COIN_ITEM1 = global::FirebaseRemoteConfig.PREFIX + "base_coin_item1";

	private static string FBASE_BASE_COIN_ITEM2 = global::FirebaseRemoteConfig.PREFIX + "base_coin_item2";

	private static string FBASE_BASE_COIN_ITEM3 = global::FirebaseRemoteConfig.PREFIX + "base_coin_item3";

	private static string FBASE_BASE_COIN_ITEM4 = global::FirebaseRemoteConfig.PREFIX + "base_coin_item4";

	private static string FBASE_BASE_COIN_ITEM5 = global::FirebaseRemoteConfig.PREFIX + "base_coin_item5";

	private static string FBASE_BASE_COIN_ITEM6 = global::FirebaseRemoteConfig.PREFIX + "base_coin_item6";

	private static string FBASE_BASE_COIN_ITEM7 = global::FirebaseRemoteConfig.PREFIX + "base_coin_item7";

	private static string FBASE_LIMIT_TIME_FOR_NEXT_OFFLINE_EARNING = global::FirebaseRemoteConfig.PREFIX + "limit_time_next_offline_earning";

	private static string FBASE_DAMAGE_PERCENT_ITEM = global::FirebaseRemoteConfig.PREFIX + "damage_percent_item";

	private static string FBASE_BONUS_TIME = global::FirebaseRemoteConfig.PREFIX + "bonus_time";

	private static string FBASE_LEVEL_UNLOCK = global::FirebaseRemoteConfig.PREFIX + "level_unlock";

	private static string FBASE_BOOSTER_VALUE1 = global::FirebaseRemoteConfig.PREFIX + "booster_value1";

	private static string FBASE_BOOSTER_VALUE2 = global::FirebaseRemoteConfig.PREFIX + "booster_value2";

	private static string FBASE_BOOSTER_VALUE3 = global::FirebaseRemoteConfig.PREFIX + "booster_value3";

	private static string FBASE_BOOSTER_VALUE4 = global::FirebaseRemoteConfig.PREFIX + "booster_value4";

	private static string FBASE_INTERSTITIAL_PERCENT = global::FirebaseRemoteConfig.PREFIX + "interstitial_percent";

	private static string FBASE_LIMIT_UPGRADE_BULLETSPEED = global::FirebaseRemoteConfig.PREFIX + "limit_upgrade_bulletspeed";

	private static string FBASE_LIMIT_UPGRADE_RELOADSPEED = global::FirebaseRemoteConfig.PREFIX + "limit_upgrade_reloadspeed";

	private static string FBASE_DIAMOND_BOOSTER = global::FirebaseRemoteConfig.PREFIX + "diamond_booster";

	public static float adding_reload_speed = 1.5f;

	public static int coin_per_hit = 5;

	public static int item1_reload_time = 4;

	public static int item2_reload_time = 10;

	public static int item3_reload_time = 20;

	public static int item4_reload_time = 40;

	public static int item5_reload_time = 30;

	public static int item6_reload_time = 25;

	public static int item7_reload_time = 60;

	public static int base_damage_item1 = 5;

	public static int base_damage_item2 = 50;

	public static int base_damage_item3 = 150;

	public static int base_damage_item4 = 700;

	public static int base_damage_item5 = 600;

	public static int base_damage_item6 = 400;

	public static int base_damage_item7 = 1500;

	public static int base_coin_item1 = 20;

	public static int base_coin_item2 = 200;

	public static int base_coin_item3 = 450;

	public static int base_coin_item4 = 1500;

	public static int base_coin_item5 = 1000;

	public static int base_coin_item6 = 600;

	public static int base_coin_item7 = 2500;

	public static int limit_time_next_offline_earning = 180;

	public static float damage_percent_item = 50f;

	public static float bonus_time = 10f;

	public static string[] level_unlock = new string[]
	{
		"3",
		"9",
		"16",
		"23",
		"34",
		"46",
		"59"
	};

	public static int booster_value1 = 60;

	public static int booster_value2 = 2;

	public static int booster_value3 = 120;

	public static int booster_value4 = 120;

	public static float interstitial_percent = 100f;

	public static int limit_upgrade_bulletspeed = 300;

	public static int limit_upgrade_reloadspeed = 300;

	public static string[] diamond_booster = new string[]
	{
		"5",
		"7",
		"8",
		"5"
	};

//	private static Action<Task> __f__am_cache0;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void InitFirebase()
	{
        /*
		Dictionary<string, object> defaults = new Dictionary<string, object>();
		Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
		Task task2 = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync(new TimeSpan(0L));
		task2.ContinueWith(delegate(Task task)
		{
			if (task.IsCanceled || task.IsFaulted)
			{
				UnityEngine.Debug.Log("Load Config Failed.");
			}
			else
			{
				UnityEngine.Debug.Log("Load Config Completed.");
				Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
			}
			global::FirebaseRemoteConfig.adding_reload_speed = (float)((int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ADDING_RELOADSPEED, null).LongValue);
			global::FirebaseRemoteConfig.coin_per_hit = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_COIN_PER_HIT, null).LongValue;
			global::FirebaseRemoteConfig.item1_reload_time = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ITEM1_RELOAD_TIME, null).LongValue;
			global::FirebaseRemoteConfig.item2_reload_time = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ITEM2_RELOAD_TIME, null).LongValue;
			global::FirebaseRemoteConfig.item3_reload_time = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ITEM3_RELOAD_TIME, null).LongValue;
			global::FirebaseRemoteConfig.item4_reload_time = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ITEM4_RELOAD_TIME, null).LongValue;
			global::FirebaseRemoteConfig.item5_reload_time = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ITEM5_RELOAD_TIME, null).LongValue;
			global::FirebaseRemoteConfig.item6_reload_time = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ITEM6_RELOAD_TIME, null).LongValue;
			global::FirebaseRemoteConfig.item7_reload_time = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_ITEM7_RELOAD_TIME, null).LongValue;
			global::FirebaseRemoteConfig.base_damage_item1 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_DAMAGE_ITEM1, null).LongValue;
			global::FirebaseRemoteConfig.base_damage_item2 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_DAMAGE_ITEM2, null).LongValue;
			global::FirebaseRemoteConfig.base_damage_item3 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_DAMAGE_ITEM3, null).LongValue;
			global::FirebaseRemoteConfig.base_damage_item4 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_DAMAGE_ITEM4, null).LongValue;
			global::FirebaseRemoteConfig.base_damage_item5 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_DAMAGE_ITEM5, null).LongValue;
			global::FirebaseRemoteConfig.base_damage_item6 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_DAMAGE_ITEM6, null).LongValue;
			global::FirebaseRemoteConfig.base_damage_item7 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_DAMAGE_ITEM7, null).LongValue;
			global::FirebaseRemoteConfig.base_coin_item1 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_COIN_ITEM1, null).LongValue;
			global::FirebaseRemoteConfig.base_coin_item2 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_COIN_ITEM2, null).LongValue;
			global::FirebaseRemoteConfig.base_coin_item3 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_COIN_ITEM3, null).LongValue;
			global::FirebaseRemoteConfig.base_coin_item4 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_COIN_ITEM4, null).LongValue;
			global::FirebaseRemoteConfig.base_coin_item5 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_COIN_ITEM5, null).LongValue;
			global::FirebaseRemoteConfig.base_coin_item6 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_COIN_ITEM6, null).LongValue;
			global::FirebaseRemoteConfig.base_coin_item7 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BASE_COIN_ITEM7, null).LongValue;
			global::FirebaseRemoteConfig.limit_time_next_offline_earning = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_LIMIT_TIME_FOR_NEXT_OFFLINE_EARNING, null).LongValue;
			global::FirebaseRemoteConfig.damage_percent_item = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_DAMAGE_PERCENT_ITEM, null).DoubleValue;
			global::FirebaseRemoteConfig.bonus_time = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BONUS_TIME, null).DoubleValue;
			string stringValue = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_LEVEL_UNLOCK, null).StringValue;
			global::FirebaseRemoteConfig.level_unlock = stringValue.Split(new char[]
			{
				'|'
			});
			global::FirebaseRemoteConfig.booster_value1 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BOOSTER_VALUE1, null).LongValue;
			global::FirebaseRemoteConfig.booster_value2 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BOOSTER_VALUE2, null).LongValue;
			global::FirebaseRemoteConfig.booster_value3 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BOOSTER_VALUE3, null).LongValue;
			global::FirebaseRemoteConfig.booster_value4 = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_BOOSTER_VALUE4, null).LongValue;
			global::FirebaseRemoteConfig.interstitial_percent = (float)((int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_INTERSTITIAL_PERCENT, null).DoubleValue);
			global::FirebaseRemoteConfig.limit_upgrade_bulletspeed = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_LIMIT_UPGRADE_BULLETSPEED, null).LongValue;
			global::FirebaseRemoteConfig.limit_upgrade_reloadspeed = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_LIMIT_UPGRADE_RELOADSPEED, null).LongValue;
			string stringValue2 = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(global::FirebaseRemoteConfig.FBASE_DIAMOND_BOOSTER, null).StringValue;
			global::FirebaseRemoteConfig.diamond_booster = stringValue2.Split(new char[]
			{
				'|'
			});
			BaseValue.UpdateStaticData();
			UnityEngine.Debug.Log("adding_reload_speed: " + global::FirebaseRemoteConfig.adding_reload_speed);
			UnityEngine.Debug.Log("coin_per_hit: " + global::FirebaseRemoteConfig.coin_per_hit);
			UnityEngine.Debug.Log("item1_reload_time: " + global::FirebaseRemoteConfig.item1_reload_time);
			UnityEngine.Debug.Log("item2_reload_time: " + global::FirebaseRemoteConfig.item2_reload_time);
			UnityEngine.Debug.Log("item3_reload_time: " + global::FirebaseRemoteConfig.item3_reload_time);
			UnityEngine.Debug.Log("item4_reload_time: " + global::FirebaseRemoteConfig.item4_reload_time);
			UnityEngine.Debug.Log("base_damage_item1: " + global::FirebaseRemoteConfig.base_damage_item1);
			UnityEngine.Debug.Log("base_damage_item2: " + global::FirebaseRemoteConfig.base_damage_item2);
			UnityEngine.Debug.Log("base_damage_item3: " + global::FirebaseRemoteConfig.base_damage_item3);
			UnityEngine.Debug.Log("base_damage_item4: " + global::FirebaseRemoteConfig.base_damage_item4);
			UnityEngine.Debug.Log("base_coin_item1: " + global::FirebaseRemoteConfig.base_coin_item1);
			UnityEngine.Debug.Log("base_coin_item2: " + global::FirebaseRemoteConfig.base_coin_item2);
			UnityEngine.Debug.Log("base_coin_item3: " + global::FirebaseRemoteConfig.base_coin_item3);
			UnityEngine.Debug.Log("base_coin_item4: " + global::FirebaseRemoteConfig.base_coin_item4);
			UnityEngine.Debug.Log("limit_time_next_offline_earning: " + global::FirebaseRemoteConfig.limit_time_next_offline_earning);
			UnityEngine.Debug.Log("damage_percent_item: " + global::FirebaseRemoteConfig.damage_percent_item);
			UnityEngine.Debug.Log("bonus_time: " + global::FirebaseRemoteConfig.bonus_time);
			UnityEngine.Debug.Log("base_coin_item3: " + global::FirebaseRemoteConfig.base_coin_item3);
			UnityEngine.Debug.Log("base_coin_item4: " + global::FirebaseRemoteConfig.base_coin_item4);
			UnityEngine.Debug.Log("limit_time_next_offline_earning: " + global::FirebaseRemoteConfig.limit_time_next_offline_earning);
			UnityEngine.Debug.Log("damage_percent_item: " + global::FirebaseRemoteConfig.damage_percent_item);
			UnityEngine.Debug.Log("bonus_time: " + global::FirebaseRemoteConfig.bonus_time);
			UnityEngine.Debug.Log("booster_value1: " + global::FirebaseRemoteConfig.booster_value1);
			UnityEngine.Debug.Log("booster_value2: " + global::FirebaseRemoteConfig.booster_value2);
			UnityEngine.Debug.Log("booster_value3: " + global::FirebaseRemoteConfig.booster_value3);
			UnityEngine.Debug.Log("booster_value4: " + global::FirebaseRemoteConfig.booster_value4);
			UnityEngine.Debug.Log("interstitial_percent: " + global::FirebaseRemoteConfig.interstitial_percent);
			UnityEngine.Debug.Log("limit_upgrade_bulletspeed: " + global::FirebaseRemoteConfig.limit_upgrade_bulletspeed);
			UnityEngine.Debug.Log("limit_upgrade_reloadspeed: " + global::FirebaseRemoteConfig.limit_upgrade_reloadspeed);
		});
		*/
	}

	public void Start()
	{
		this.InitFirebase();
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void Update()
	{
	}
}
