using DG.Tweening;
using PreviewLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameController : MonoBehaviour
{
	private sealed class __AnimateDamageFly_c__AnonStorey1
	{
		internal GameObject coinGO;

		internal void __m__0()
		{
			this.coinGO.GetComponent<MeshRenderer>().material.DOFade(0f, 0.22f).SetDelay(0.22f);
		}

		internal void __m__1()
		{
			this.coinGO.SetActive(false);
		}
	}

	private sealed class _AnimateCoinFly_c__AnonStorey2
	{
		internal GameObject coinGO;

		internal Vector3 transformPos;

		internal GameController _this;

		internal void __m__0()
		{
			this.coinGO.transform.DOMove(this.transformPos, 0.2f, false);
			this.coinGO.GetComponent<MeshRenderer>().material.DOFade(1f, 0.2f);
		}

		internal void __m__1()
		{
			this.coinGO.transform.DOScale(1f, 0.35f);
			this.coinGO.transform.DOScale(0f, 0.1f).SetDelay(0.35f);
		}

		internal void __m__2()
		{
			this.coinGO.SetActive(false);
			this._this.UpdateTextCoin();
			CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction((double)this._this.coin);
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
		}
	}

	private sealed class __StopBonus_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GameController _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public __StopBonus_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("StopBonus");
				this._this.timeRemaining = 0f;
				this._this.isBonus = false;
				SoundController.instance.PlaySoundEndBonus();
				DynamicGrid.instance.ClearAllEnemyAfterBonus();
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (this._this.CurrentLevel % BaseValue.num_level_change_bg == 0)
				{
					DynamicGrid.instance.transform.position += new Vector3(30f, 0f, 0f);
					CompleteCameraController.instance.FadeInFog();
				}
				else
				{
					DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
				}
				this._this.CurrentLevel++;
				Tracking.LogEvent("PLAY_LEVEL", this._this.currentLevel);
				DynamicGrid.instance.InitNextMap();
				Tank.instance.StartMove();
				if (CanvasTop.instance)
				{
					CanvasTop.instance.HideTextBonus();
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static GameController instance;

	public int factorSpeed = 1;

	public int factorMoney = 1;

	public float factorCooldown = 1f;

	public GameData game = new GameData();

	public bool isBonus;

	public bool isPlayingBonus;

	public bool isIntersShowed;

	public bool isDisableTouch;

	public bool isBonusSoundPlaying;

	[SerializeField]
	public Toggle toggleAutoUseItem;

	[SerializeField]
	public CanvasPopUp canvasPopUp;

	[SerializeField]
	public CanvasBossFight canvasBossFight;

	[SerializeField]
	public Transform diamondImagePos;

	[SerializeField]
	public CanvasUnlock prefab_CanvasUnlock;

	[SerializeField]
	public CanvasReach100Level prefab_CanvasReach100Level;

	[SerializeField]
	public Button buttonBG;

	[SerializeField]
	public Button buttonForeGround;

	public Tank tank;

	private long coin;

	private long diamond;

	private double offlineEarning;

	private float timeRemaining;

	private int currentBg;

	private int currentLevel = 1;

	public int CurrentLevel
	{
		get
		{
			return this.currentLevel;
		}
		set
		{
			this.currentLevel = value;
			this.UpdateTextLevel();
		}
	}

	public long Coin
	{
		get
		{
			return this.coin;
		}
		set
		{
			this.coin = value;
		}
	}

	public long Diamond
	{
		get
		{
			return this.diamond;
		}
		set
		{
			this.diamond = value;
			this.UpdateTextDiamond();
		}
	}

	public double OfflineEarning
	{
		get
		{
			return this.offlineEarning;
		}
		set
		{
			this.offlineEarning = value;
		}
	}

	public void UpdateTextLevel()
	{
		if (CanvasTop.instance)
		{
			CanvasTop.instance.SetTextCurrentLevel("LEVEL " + this.currentLevel.ToString());
		}
	}

	private void UpdateTextDiamond()
	{
		if (CanvasTop.instance)
		{
			CanvasTop.instance.SetTextDiamond(Utilities.getStringFromNumber((double)this.diamond, false));
		}
	}

	public void UpdateTextCoin()
	{
		CanvasTop.instance.SetTextCoin("O " + Utilities.getStringFromNumber((double)this.coin, false));
	}

	internal void UpdateOfflineEarningValue(double value)
	{
		this.OfflineEarning = value;
	}

	public void LoadOfflineEarningValue()
	{
		this.OfflineEarning = (double)this.game.Upgrade_offlineearning_value;
	}

	public void SaveOfflineEarningValue()
	{
		this.game.Upgrade_offlineearning_value = (float)this.OfflineEarning;
	}

	public void AddCoin(long value, Vector3 enemyPos)
	{
		this.Coin += value;
		this._AnimateCoin(enemyPos, CanvasTop.instance.GetTextCoinPos(), value);
	}

	public void AddCoinForBonus(long value, Vector3 enemyPos)
	{
		this.Coin += value;
		this.AnimateCoinFly(enemyPos, CanvasTop.instance.GetTextCoinPos(), value);
	}

	public void SubtractDamage(long value, Vector3 enemyPos)
	{
		this._AnimateDamageFly(enemyPos, value);
	}

	private void Awake()
	{
		GameController.instance = this;
        Application.targetFrameRate = 60;
		GameObject gameObject = GameObject.FindWithTag("World");
		if (gameObject)
		{
			UnityEngine.Debug.Log("World existed");
			UnityEngine.Debug.Log(this.game.Num_of_time_reset);
			int num_of_time_reset = this.game.Num_of_time_reset;
			if (num_of_time_reset <= 1)
			{
				GameObject original = Resources.Load("Prefabs/Tanks/CustomTank") as GameObject;
				UnityEngine.Object.Instantiate<GameObject>(original, gameObject.transform);
			}
			else
			{
				GameObject original2 = Resources.Load("Prefabs/Tanks/GreenTank") as GameObject;
				UnityEngine.Object.Instantiate<GameObject>(original2, gameObject.transform);
			}
		}
		else
		{
			UnityEngine.Debug.LogError("World doesn't exist");
		}
	}

	private void Start()
	{
		this.LoadData();
		if (CanvasUpgrade.instance)
		{
			CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction((double)this.coin);
		}
		this.UpdateTextCoin();
		this.UpdateTextLevel();
		this.UpdateTextDiamond();
		PoolerManager.instance.ChangeObjectPool(this.game.Num_of_time_reset);
	}

	public void _AnimateDamageFly(Vector3 transformPos, long value)
	{
		GameObject coinGO = CoinObjectPooler.instance.GetPooledObject();
		coinGO.SetActive(true);
		Color white = Color.white;
		white.a = 1f;
		coinGO.transform.localScale = Vector3.one;
		coinGO.GetComponent<MeshRenderer>().material.color = white;
		transformPos.x += UnityEngine.Random.Range(-DynamicGrid.instance.cellSize.x, DynamicGrid.instance.cellSize.x) / 2f;
		coinGO.transform.position = transformPos;
		CoinText component = coinGO.GetComponent<CoinText>();
		component.SetText((-value).ToString());
		component.SetSize(0.03f);
		coinGO.transform.DOMove(transformPos + Vector3.up * 2f, 0.5f, false).OnStart(delegate
		{
			coinGO.GetComponent<MeshRenderer>().material.DOFade(0f, 0.22f).SetDelay(0.22f);
		}).OnComplete(delegate
		{
			coinGO.SetActive(false);
		});
	}

	public void _AnimateCoin(Vector3 transformPos, Vector3 targetPos, long value)
	{
		this.UpdateTextCoin();
		CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction((double)this.coin);
		if (CanvasTop.instance)
		{
			CanvasTop.instance.AnimateTextCoin();
		}
	}

	public void AnimateCoinFly(Vector3 transformPos, Vector3 targetPos, long value)
	{
		GameController._AnimateCoinFly_c__AnonStorey2 _AnimateCoinFly_c__AnonStorey = new GameController._AnimateCoinFly_c__AnonStorey2();
		_AnimateCoinFly_c__AnonStorey.transformPos = transformPos;
		_AnimateCoinFly_c__AnonStorey._this = this;
		_AnimateCoinFly_c__AnonStorey.coinGO = CoinObjectPooler.instance.GetPooledObject();
		_AnimateCoinFly_c__AnonStorey.coinGO.SetActive(true);
		Color white = Color.white;
		white.a = 0f;
		_AnimateCoinFly_c__AnonStorey.coinGO.GetComponent<MeshRenderer>().material.color = white;
		_AnimateCoinFly_c__AnonStorey.coinGO.transform.localScale = Vector3.zero;
		_AnimateCoinFly_c__AnonStorey.coinGO.transform.position = _AnimateCoinFly_c__AnonStorey.transformPos;
		CoinText component = _AnimateCoinFly_c__AnonStorey.coinGO.GetComponent<CoinText>();
		component.SetText(value.ToString());
		component.SetSize(0.06f);
		targetPos.z = 0f;
		targetPos.x -= 1f;
		GameController._AnimateCoinFly_c__AnonStorey2 expr_D2_cp_0 = _AnimateCoinFly_c__AnonStorey;
		expr_D2_cp_0.transformPos.y = expr_D2_cp_0.transformPos.y + 1.5f;
		Sequence sequence = DOTween.Sequence();
		Tweener t = _AnimateCoinFly_c__AnonStorey.coinGO.transform.DOScale(0.5f, 0.2f).OnStart(delegate
		{
			_AnimateCoinFly_c__AnonStorey.coinGO.transform.DOMove(_AnimateCoinFly_c__AnonStorey.transformPos, 0.2f, false);
			_AnimateCoinFly_c__AnonStorey.coinGO.GetComponent<MeshRenderer>().material.DOFade(1f, 0.2f);
		});
		sequence.Append(t);
		Tweener t2 = _AnimateCoinFly_c__AnonStorey.coinGO.transform.DOMove(targetPos, 0.5f, false).OnStart(delegate
		{
			_AnimateCoinFly_c__AnonStorey.coinGO.transform.DOScale(1f, 0.35f);
			_AnimateCoinFly_c__AnonStorey.coinGO.transform.DOScale(0f, 0.1f).SetDelay(0.35f);
		}).OnComplete(delegate
		{
			_AnimateCoinFly_c__AnonStorey.coinGO.SetActive(false);
			_AnimateCoinFly_c__AnonStorey._this.UpdateTextCoin();
			CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction((double)_AnimateCoinFly_c__AnonStorey._this.coin);
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextCoin();
			}
		});
		sequence.Append(t2);
		sequence.Play<Sequence>();
	}

	public void ShowPanelUnlock(ButtonItem btnItem)
	{
		UnityEngine.Debug.Log("ShowPanelUnlock");
		SoundController.instance.PlaySoundUnlockNewItem();
		CanvasUnlock canvasUnlock = UnityEngine.Object.Instantiate<CanvasUnlock>(this.prefab_CanvasUnlock, CanvasItem.instance.transform.parent);
		CanvasUnlock component = canvasUnlock.GetComponent<CanvasUnlock>();
		component.InitValue(btnItem);
	}

	private void LoadCoin()
	{
		this.Coin = this.game.Coin;
	}

	private void LoadDiamond()
	{
		this.Diamond = this.game.Diamond;
	}

	private void SaveCoin()
	{
		this.game.Coin = this.coin;
	}

	private void SaveDiamond()
	{
		this.game.Diamond = this.diamond;
	}

	private void LoadLevel()
	{
		this.CurrentLevel = this.game.Level;
	}

	private void SaveLevel()
	{
		if (this.isBonus)
		{
			this.game.Level = this.currentLevel + 1;
		}
		else
		{
			this.game.Level = this.currentLevel;
		}
	}

	public void NextLevel()
	{
		this.ChangeBackground();
		DynamicGrid.instance._ClearAllEnemy(this.isBonus);
		DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
		DiamondObjectPooler.instance.DisableAllDiamond();
		if (GameController.instance.CurrentLevel % 5 == 0)
		{
			UnityEngine.Debug.Log("Bonus Level");
			this.canvasBossFight.AnimateBossFight(true);
			if (CanvasTop.instance)
			{
				CanvasTop.instance.SetTextCurrentLevel("BONUS LEVEL");
			}
			this.isBonus = true;
			DynamicGrid.instance.InitGridInfoBonus();
			DynamicGrid.instance.InitCells();
		}
		else
		{
			this.isBonus = false;
			this.CurrentLevel++;
			int[] level_unlock = BaseValue.level_unlock;
			UnityEngine.Debug.Log(level_unlock.Length + "_________");
			for (int i = 0; i < level_unlock.Length; i++)
			{
				if (level_unlock[i] == this.CurrentLevel)
				{
					this.ShowPanelUnlock(CanvasItem.instance.GetListBtnItem()[i]);
				}
			}
			DynamicGrid.instance.InitNextMap();
		}
		Tank.instance.StartMove();
	}

	public void _GoToNextLevel()
	{
		this.isIntersShowed = false;
		DiamondObjectPooler.instance.DisableAllDiamond();
		DynamicGrid.instance.ClearAllEnemy();
		DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
		int[] level_unlock = BaseValue.level_unlock;
		UnityEngine.Debug.Log(level_unlock.Length + "_________");
		for (int i = 0; i < level_unlock.Length; i++)
		{
			if (level_unlock[i] == GameController.instance.CurrentLevel)
			{
				GameController.instance.ShowPanelUnlock(CanvasItem.instance.GetListBtnItem()[i]);
			}
		}
		DynamicGrid.instance.InitNextMap();
		Tank.instance.StartMove();
	}

	public void _GoToNextLevelWithoutSound()
	{
		this.isIntersShowed = false;
		DiamondObjectPooler.instance.DisableAllDiamond();
		DynamicGrid.instance.ClearAllEnemy();
		DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
		int[] level_unlock = BaseValue.level_unlock;
		UnityEngine.Debug.Log(level_unlock.Length + "_________");
		for (int i = 0; i < level_unlock.Length; i++)
		{
			if (level_unlock[i] == GameController.instance.CurrentLevel)
			{
				GameController.instance.ShowPanelUnlock(CanvasItem.instance.GetListBtnItem()[i]);
			}
		}
		DynamicGrid.instance.InitNextMap();
		Tank.instance.StartMove();
	}

	public bool CheckIsEnoughDiamond(BoosterType type)
	{
		int num = 0;
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
		return this.Diamond >= (long)num;
	}

	public void GotoLevel100()
	{
		this.isIntersShowed = false;
		this.isDisableTouch = true;
		DiamondObjectPooler.instance.DisableAllDiamond();
		this.isBonus = false;
		Gun.instance.ClearAllTouch();
		DynamicGrid.instance.ClearAllEnemy();
		Gun.instance._touchPos = Vector2.one * -100f;
		this.isBonus = false;
		DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
		this.CurrentLevel = 100;
		Gun.instance.Power = 1000000L;
		int[] level_unlock = BaseValue.level_unlock;
		for (int i = 0; i < level_unlock.Length; i++)
		{
			if (level_unlock[i] == GameController.instance.CurrentLevel)
			{
				GameController.instance.ShowPanelUnlock(CanvasItem.instance.GetListBtnItem()[i]);
			}
		}
		DynamicGrid.instance.InitNextMap();
		Tank.instance.StartMove();
	}

	public void GoToNextLevel()
	{
		this.isIntersShowed = false;
		this.isDisableTouch = true;
		Gun.instance.ClearAllTouch();
		Gun.instance._touchPos = Vector2.one * -100f;
		if (this.currentLevel % 5 == 0 && this.currentLevel < BaseValue.level_reset)
		{
			DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
			this.canvasBossFight.AnimateBossFight(true);
			UnityEngine.Debug.Log("Bonus Level");
			if (CanvasTop.instance)
			{
				CanvasTop.instance.SetTextCurrentLevel("BONUS LEVEL");
			}
			this.isBonus = true;
			DynamicGrid.instance.InitGridInfoBonus();
			DynamicGrid.instance.InitCells();
			Tracking.LogEvent("PLAY_BONUS_LEVEL", this.currentLevel);
		}
		else
		{
			this.isBonus = false;
			if (this.currentLevel < BaseValue.level_reset && this.currentLevel > 0)
			{
				DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
				this.CurrentLevel++;
				Tracking.LogEvent("PLAY_LEVEL", this.currentLevel);
				int[] level_unlock = BaseValue.level_unlock;
				UnityEngine.Debug.Log(level_unlock.Length + "_________");
				for (int i = 0; i < level_unlock.Length; i++)
				{
					if (level_unlock[i] == GameController.instance.CurrentLevel)
					{
						GameController.instance.ShowPanelUnlock(CanvasItem.instance.GetListBtnItem()[i]);
					}
				}
				DynamicGrid.instance.InitNextMap();
			}
			else
			{
				Sprite tankSprite = Utilities.LoadImageFrom("Textures/Tanks/Tank2/Tank2");
				int num_of_time_reset = this.game.Num_of_time_reset;
				if (num_of_time_reset != 1)
				{
					if (num_of_time_reset != 2)
					{
						tankSprite = Utilities.LoadImageFrom("Textures/Tanks/Tank3/Tank3");
						this.ShowPanelReach100Level(tankSprite, true);
					}
					else
					{
						tankSprite = Utilities.LoadImageFrom("Textures/Tanks/Tank3/Tank3");
						this.ShowPanelReach100Level(tankSprite, true);
					}
				}
				else
				{
					tankSprite = Utilities.LoadImageFrom("Textures/Tanks/Tank2/Tank2");
					this.ShowPanelReach100Level(tankSprite, false);
				}
				Tracking.LogEvent("REACH_LEVEL_100_ROUND", this.game.Num_of_time_reset);
			}
		}
		Tank.instance.StartMove();
	}

	public void ShowPanelReach100Level(Sprite tankSprite, bool isComingSoon = false)
	{
		UnityEngine.Debug.Log("ShowPanelReach100Level");
		SoundController.instance.PlaySoundReachLevel100();
		CanvasReach100Level canvasReach100Level = UnityEngine.Object.Instantiate<CanvasReach100Level>(this.prefab_CanvasReach100Level, CanvasItem.instance.transform.parent);
		CanvasReach100Level component = canvasReach100Level.GetComponent<CanvasReach100Level>();
		component.InitValue(tankSprite, isComingSoon);
	}

	private void GetOfflineRevenue()
	{
		this.LoadOfflineEarningValue();
		UnityEngine.Debug.Log("GetOfflineRevenue");
		long num = Convert.ToInt64(this.game.Last_time_close_app);
		UnityEngine.Debug.Log("lastTimeCloseApp: " + num);
		double totalSeconds = DateTime.Now.Subtract(DateTime.FromBinary(num)).TotalSeconds;
		if (totalSeconds <= 0.0)
		{
			UnityEngine.Debug.Log("No value");
		}
		else
		{
			UnityEngine.Debug.Log("OFfline time: " + totalSeconds);
			UnityEngine.Debug.Log("Date time now: " + DateTime.Now.TimeOfDay.TotalSeconds);
			UnityEngine.Debug.Log("Last time close app: " + num);
			if (totalSeconds <= 0.0 || totalSeconds <= (double)BaseValue.limit_time_for_next_offline_earning)
			{
				return;
			}
			UnityEngine.Debug.Log(totalSeconds + "__" + this.offlineEarning);
			PreviewLabs.PlayerPrefs.DeleteKey(PlayerPrefKey.KEY_LAST_TIME_CLOSE_APP);
			this.canvasPopUp.gameObject.SetActive(true);
			UnityEngine.Debug.Log("Offline earning value: " + this.offlineEarning);
			UnityEngine.Debug.Log("Money to get: " + (long)this.offlineEarning * (long)(totalSeconds / 60.0));
			this.canvasPopUp.OfflineMoney = (long)this.offlineEarning * (long)(totalSeconds / 60.0);
			this.canvasPopUp.UpdateText();
		}
	}

	public void StartReset()
	{
		this.isIntersShowed = false;
		DiamondObjectPooler.instance.DisableAllDiamond();
		DynamicGrid.instance.ClearAllEnemy();
		DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
		Tank.instance.StartMoveWithoutSound();
		SoundController.instance.StopSoundSpeedBoosterUsing();
		SoundController.instance.PlaySoundTankRunning();
		CompleteCameraController.instance.FadeInFogToChangeTank();
	}

	public void ResetGame()
	{
		this.isBonus = false;
		this.game.Num_of_time_reset++;
		this.ResetOfflineEarning();
		this.ResetLevel();
		CanvasItem.instance.CheckUnlockItem(1);
		this.ResetUpgrade();
		this.ResetBooster();
		this.ResetStats(this.game.Num_of_time_reset);
		this.ResetCoin();
		this.ResetItem();
		this._GoToNextLevelWithoutSound();
		PreviewLabs.PlayerPrefs.Flush();
		GameObject gameObject = GameObject.FindWithTag("World");
		CompleteCameraController.instance.isFollowTank = false;
		BackGround.instance.isFollowTank = false;
		Vector3 position = Tank.instance.transform.position;
		UnityEngine.Object.Destroy(Tank.instance.gameObject);
		if (gameObject)
		{
			int num_of_time_reset = this.game.Num_of_time_reset;
			if (num_of_time_reset != 1)
			{
				if (num_of_time_reset != 2)
				{
					GameObject original = Resources.Load("Prefabs/Tanks/GreenTank") as GameObject;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original, position, Quaternion.identity, gameObject.transform);
					Tank component = gameObject2.GetComponent<Tank>();
					this.tank = component;
					CompleteCameraController.instance.SetTank(component);
					BackGround.instance.SetTank(component);
				}
				else
				{
					GameObject original2 = Resources.Load("Prefabs/Tanks/GreenTank") as GameObject;
					GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(original2, position, Quaternion.identity, gameObject.transform);
					Tank component2 = gameObject3.GetComponent<Tank>();
					this.tank = component2;
					CompleteCameraController.instance.SetTank(component2);
					BackGround.instance.SetTank(component2);
				}
			}
			else
			{
				GameObject original3 = Resources.Load("Prefabs/Tanks/RedTank") as GameObject;
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(original3, position, Quaternion.identity, gameObject.transform);
				Tank component3 = gameObject4.GetComponent<Tank>();
				this.tank = component3;
				CompleteCameraController.instance.SetTank(component3);
				BackGround.instance.SetTank(component3);
			}
		}
		this.LoadCoin();
		this.LoadDiamond();
		this.LoadLevel();
		if (Gun.instance)
		{
			Gun.instance.LoadStats();
		}
		else
		{
			UnityEngine.Debug.LogError("Gun not found!!!");
		}
		if (CanvasUpgrade.instance)
		{
			CanvasUpgrade.instance.LoadUpgradeData();
			CanvasUpgrade.instance.LoadBoosterData();
		}
		else
		{
			UnityEngine.Debug.LogError("Canvas Upgrade not found!!!");
		}
		if (CanvasItem.instance)
		{
			CanvasItem.instance.LoadItemData();
			CanvasItem.instance.CheckUnlockItem(this.currentLevel);
		}
		else
		{
			UnityEngine.Debug.LogError("Canvas Item not found!!!");
		}
		if (CanvasUpgrade.instance)
		{
			CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction((double)this.coin);
		}
		this.UpdateTextCoin();
		this.UpdateTextLevel();
		this.UpdateTextDiamond();
		PoolerManager.instance.ChangeObjectPool(this.game.Num_of_time_reset);
	}

	public void _ResetGame()
	{
		this.game.Num_of_time_reset++;
		this.ResetOfflineEarning();
		this.ResetLevel();
		this.ResetUpgrade();
		this.ResetBooster();
		this.ResetStats(this.game.Num_of_time_reset);
		this.ResetCoin();
		this.ResetItem();
		PreviewLabs.PlayerPrefs.Flush();
		SoundController.instance.StopSoundSpeedBoosterUsing();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ResetOfflineEarning()
	{
		this.offlineEarning = (double)BaseValue.basevalue_offlineearning;
		this.game.Upgrade_offlineearning_value = (float)BaseValue.basevalue_offlineearning;
		UnityEngine.Debug.Log("ResetOfflineEarning: ");
		UnityEngine.Debug.Log("Runtime: " + this.offlineEarning);
		UnityEngine.Debug.Log("KeyValue: " + this.game.Upgrade_offlineearning_value);
	}

	public void ResetCoin()
	{
		this.Coin = 0L;
		this.game.Coin = 0L;
		this.UpdateTextCoin();
		CanvasUpgrade.instance.UpdateAllButtonUpgradeInteraction(0.0);
		UnityEngine.Debug.Log("ResetCoin: ");
		UnityEngine.Debug.Log("Runtime: " + this.Coin);
		UnityEngine.Debug.Log("KeyValue: " + this.game.Coin);
	}

	public void ResetLevel()
	{
		this.CurrentLevel = 1;
		this.game.Level = 1;
		UnityEngine.Debug.Log("ResetLevel: ");
		UnityEngine.Debug.Log("Runtime: " + this.CurrentLevel);
		UnityEngine.Debug.Log("KeyValue: " + this.game.Level);
	}

	public void ResetUpgrade()
	{
		CanvasUpgrade.instance.ResetUpgradeData();
	}

	public void ResetItem()
	{
		CanvasItem.instance.ResetItemDate();
	}

	public void ResetBooster()
	{
		CanvasUpgrade.instance.ResetBoosterData();
	}

	public void ResetStats(int numOfTimeReset)
	{
		Gun.instance.ResetStats(numOfTimeReset);
		UnityEngine.Debug.Log("ResetStats");
		UnityEngine.Debug.Log(Gun.instance.Power);
		UnityEngine.Debug.Log(Gun.instance.BulletSpeed);
		UnityEngine.Debug.Log(Gun.instance.ReloadSpeed);
	}

	private void SaveLastTimeCloseApp()
	{
		UnityEngine.Debug.Log("SaveLastTimeCloseApp");
		this.game.Last_time_close_app = DateTime.Now.ToBinary().ToString();
	}

	public void SaveData()
	{
		this.SaveLevel();
		this.SaveCoin();
		this.SaveDiamond();
		this.SaveOfflineEarningValue();
		this.SaveLastTimeCloseApp();
		if (CanvasUpgrade.instance)
		{
			CanvasUpgrade.instance.SaveUpgradeData();
			CanvasUpgrade.instance.SaveBoosterData();
		}
		else
		{
			UnityEngine.Debug.LogError("Canvas Upgrade not found!!!");
		}
		if (CanvasItem.instance)
		{
			CanvasItem.instance.SaveItemData();
		}
		else
		{
			UnityEngine.Debug.LogError("Canvas Item not found!!!");
		}
		if (Gun.instance)
		{
			Gun.instance.SaveStats();
		}
		else
		{
			UnityEngine.Debug.LogError("Gun not found!!!");
		}
		PreviewLabs.PlayerPrefs.Flush();
	}

	public void LoadData()
	{
		this.LoadCoin();
		this.LoadDiamond();
		this.LoadLevel();
		if (Gun.instance)
		{
			Gun.instance.LoadStats();
		}
		else
		{
			UnityEngine.Debug.LogError("Gun not found!!!");
		}
		if (CanvasUpgrade.instance)
		{
			CanvasUpgrade.instance.LoadUpgradeData();
			CanvasUpgrade.instance.LoadBoosterData();
		}
		else
		{
			UnityEngine.Debug.LogError("Canvas Upgrade not found!!!");
		}
		if (CanvasItem.instance)
		{
			CanvasItem.instance.LoadItemData();
			CanvasItem.instance.CheckUnlockItem(this.currentLevel);
		}
		else
		{
			UnityEngine.Debug.LogError("Canvas Item not found!!!");
		}
		this.GetOfflineRevenue();
		base.InvokeRepeating("PushData", 0f, 60f);
		if (this.currentLevel % 5 == 0)
		{
			this.canvasBossFight.AnimateBossFight(false);
		}
		if (Tank.instance)
		{
			this.tank = Tank.instance;
		}
		DynamicGrid.instance.InitNextMap();
		this.LoadSoundData();
		this.LoadAutoUseItemData();
		//Admob.instance.InitAdmob();
		CanvasUpgrade.instance.ShowEffect();
	}

	public void SaveDataInvoke()
	{
		this.SaveOfflineEarningValue();
		this.SaveCoin();
		this.SaveDiamond();
		this.SaveLevel();
		if (CanvasUpgrade.instance)
		{
			CanvasUpgrade.instance.SaveUpgradeData();
			CanvasUpgrade.instance.SaveBoosterData();
		}
		else
		{
			UnityEngine.Debug.LogError("Canvas Upgrade not found!!!");
		}
		if (Gun.instance)
		{
			Gun.instance.SaveStats();
		}
		else
		{
			UnityEngine.Debug.LogError("Gun not found!!!");
		}
		PreviewLabs.PlayerPrefs.Flush();
	}

	private void Update()
	{
		if (this.isBonus && this.isPlayingBonus)
		{
			this.timeRemaining -= Time.deltaTime;
			CanvasTop.instance.SetTextTimeBonus(Utilities.GetFormattedTime((double)this.timeRemaining) + "s");
			if (this.timeRemaining <= 5f && !this.isBonusSoundPlaying)
			{
				SoundController.instance.PlaySoundTimeUp();
				this.isBonusSoundPlaying = true;
			}
			if (this.timeRemaining <= 0f)
			{
				this.isPlayingBonus = false;
				this.isBonusSoundPlaying = false;
				SoundController.instance.StopInvokeSoundTimeUp();
				base.StartCoroutine(this._StopBonus());
			}
		}
	}

	public void StartBonus()
	{
		UnityEngine.Debug.Log("StartBonus");
		this.timeRemaining = (float)BaseValue.bonus_time;
		if (CanvasTop.instance)
		{
			CanvasTop.instance.ShowTextBonus();
		}
		this.isPlayingBonus = true;
	}

	public void StopBonus()
	{
		UnityEngine.Debug.Log("StopBonus");
		this.timeRemaining = 0f;
		this.isBonus = false;
		DynamicGrid.instance.ClearAllEnemyAfterBonus();
		DynamicGrid.instance.transform.position += new Vector3(20f, 0f, 0f);
		this.CurrentLevel++;
		DynamicGrid.instance.InitNextMap();
		Tank.instance.StartMove();
		if (CanvasTop.instance)
		{
			CanvasTop.instance.HideTextBonus();
		}
	}

	private IEnumerator _StopBonus()
	{
		GameController.__StopBonus_c__Iterator0 __StopBonus_c__Iterator = new GameController.__StopBonus_c__Iterator0();
		__StopBonus_c__Iterator._this = this;
		return __StopBonus_c__Iterator;
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			this.SaveData();
		}
		else
		{
			this.GetOfflineRevenue();
		}
	}

	private void OnApplicationQuit()
	{
		this.SaveData();
		PreviewLabs.PlayerPrefs.Flush();
	}

	public void PushData()
	{
		this.SaveDataInvoke();
	}

	public void ShowInterstitial()
	{

		
		if ((float)UnityEngine.Random.Range(0, 101) < BaseValue.interstitial_percent)
		{
			Debug.LogError("CANT SHOW ADS");
            //AdsControl.Instance.showAds();

        }
	}

	public void LoadSoundData()
	{
		bool check_sound_enable = this.game.Check_sound_enable;
		SoundController.instance.isSoundEnable = check_sound_enable;
		if (check_sound_enable)
		{
			this.buttonBG.gameObject.SetActive(false);
			this.buttonForeGround.gameObject.SetActive(true);
		}
		else
		{
			this.buttonBG.gameObject.SetActive(true);
			this.buttonForeGround.gameObject.SetActive(false);
		}
	}

	public void TurnOnSound()
	{
		SoundController.instance.TurnOnSound();
	}

	public void TurnOffSound()
	{
		SoundController.instance.TurnOffSound();
	}

	public void OnToggleSound()
	{
		if (SoundController.instance.isSoundEnable)
		{
			SoundController.instance.PlaySoundButtonDefault();
			this.TurnOffSound();
			this.game.Check_sound_enable = false;
			this.buttonBG.gameObject.SetActive(true);
			this.buttonForeGround.gameObject.SetActive(false);
		}
		else
		{
			this.TurnOnSound();
			if (CanvasUpgrade.instance.buttonBoosterSpeed.IsBoosterUsed)
			{
				SoundController.instance.PlaySoundSpeedBoosterUsing();
			}
			this.game.Check_sound_enable = true;
			this.buttonBG.gameObject.SetActive(false);
			this.buttonForeGround.gameObject.SetActive(true);
		}
	}

	public void LoadAutoUseItemData()
	{
		bool check_auto_use_item = this.game.Check_auto_use_item;
		CanvasItem.instance.isAutoUse = check_auto_use_item;
		this.toggleAutoUseItem.isOn = check_auto_use_item;
		this.toggleAutoUseItem.onValueChanged.AddListener(delegate
		{
			this.OnToggleAutoUseItem(this.toggleAutoUseItem);
		});
	}

	public void TurnOnAutoUseItem()
	{
		CanvasItem.instance.isAutoUse = true;
		this.game.Check_auto_use_item = true;
	}

	public void TurnOffAutoUseItem()
	{
		CanvasItem.instance.isAutoUse = false;
		this.game.Check_auto_use_item = false;
	}

	public void OnToggleAutoUseItem(Toggle toggle)
	{
		SoundController.instance.PlaySoundButtonDefault();
		if (CanvasItem.instance.isAutoUse)
		{
			this.TurnOffAutoUseItem();
		}
		else
		{
			this.TurnOnAutoUseItem();
		}
	}

	public void SetBackground(int index)
	{
		switch (index)
		{
		case 0:
		{
			List<Sprite> background = BackgroundGetter.GetBackground(BackgroundType.Plain);
			List<GameObject> allCreateSprite = BackGround.instance.allCreateSprite;
			int i = 0;
			while (i < allCreateSprite.Count)
			{
				GameObject gameObject = allCreateSprite[i];
				switch (i / 3)
				{
				case 0:
					gameObject.GetComponent<SpriteRenderer>().sprite = background[0];
					break;
				case 1:
					gameObject.GetComponent<SpriteRenderer>().sprite = background[1];
					break;
				case 2:
					gameObject.GetComponent<SpriteRenderer>().sprite = background[2];
					break;
				case 5:
					gameObject.GetComponent<SpriteRenderer>().sprite = background[3];
					break;
				}
				IL_C2:
				i++;
				continue;
				goto IL_C2;
			}
			GroundObjectPooler.instance.SetSpriteAllGround(background[4]);
			break;
		}
		case 1:
		{
			List<Sprite> background2 = BackgroundGetter.GetBackground(BackgroundType.Desert);
			List<GameObject> allCreateSprite2 = BackGround.instance.allCreateSprite;
			int j = 0;
			while (j < allCreateSprite2.Count)
			{
				GameObject gameObject2 = allCreateSprite2[j];
				switch (j / 3)
				{
				case 0:
					gameObject2.GetComponent<SpriteRenderer>().sprite = background2[0];
					break;
				case 1:
					gameObject2.GetComponent<SpriteRenderer>().sprite = background2[1];
					break;
				case 2:
					gameObject2.GetComponent<SpriteRenderer>().sprite = background2[2];
					break;
				case 5:
					gameObject2.GetComponent<SpriteRenderer>().sprite = background2[3];
					break;
				}
				IL_1A2:
				j++;
				continue;
				goto IL_1A2;
			}
			GroundObjectPooler.instance.SetSpriteAllGround(background2[4]);
			break;
		}
		case 2:
		{
			List<Sprite> background3 = BackgroundGetter.GetBackground(BackgroundType.City);
			List<GameObject> allCreateSprite3 = BackGround.instance.allCreateSprite;
			int k = 0;
			while (k < allCreateSprite3.Count)
			{
				GameObject gameObject3 = allCreateSprite3[k];
				switch (k / 3)
				{
				case 0:
					gameObject3.GetComponent<SpriteRenderer>().sprite = background3[0];
					break;
				case 1:
					gameObject3.GetComponent<SpriteRenderer>().sprite = background3[1];
					break;
				case 2:
					gameObject3.GetComponent<SpriteRenderer>().sprite = background3[2];
					break;
				case 5:
					gameObject3.GetComponent<SpriteRenderer>().sprite = background3[3];
					break;
				}
				IL_287:
				k++;
				continue;
				goto IL_287;
			}
			GroundObjectPooler.instance.SetSpriteAllGround(background3[4]);
			break;
		}
		}
	}

	public void ChangeBackground()
	{
		int num;
		for (num = UnityEngine.Random.Range(0, 3); num == this.currentBg; num = UnityEngine.Random.Range(0, 3))
		{
		}
		this.currentBg = num;
		this.SetBackground(this.currentBg);
	}
}
