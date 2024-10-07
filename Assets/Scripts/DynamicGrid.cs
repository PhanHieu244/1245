using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DynamicGrid : MonoBehaviour
{
	private sealed class _MoveToDiamondPanel_c__AnonStorey3
	{
		internal GameObject obj;

		internal void __m__0()
		{
			this.obj.SetActive(false);
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextDiamond();
			}
		}
	}

	private sealed class _CheckEndOfLevelXX_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _currentLevel___0;

		internal DynamicGrid _this;

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

		public _CheckEndOfLevelXX_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("CheckEndOfLevel");
				this._current = new WaitUntil(() => this._this.IsAllEnemyDestroy());
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				SoundController.instance.PlaySoundBonusLevel();
				this._currentLevel___0 = GameController.instance.CurrentLevel;
				if (this._currentLevel___0 >= 6 && this._currentLevel___0 % 5 != 0)
				{
					//if (AdsControl.Instance.GetInterstitalAvailable())
					if(false)
					{
						if ((float)UnityEngine.Random.Range(0, 101) < BaseValue.interstitial_percent)
						{
							this._this.ClearAllEnemy();
							GameController.instance.isIntersShowed = true;
							GameController.instance.isDisableTouch = true;
							this._this.Invoke("ShowInterstitial", 1f);
							this._this.Invoke("GotoNextLevel", 2f);
						}
						else
						{
							this._this.ClearAllEnemy();
							GameController.instance.GoToNextLevel();
							GameController.instance.isDisableTouch = true;
						}
					}
					else
					{
						this._this.ClearAllEnemy();
						GameController.instance.GoToNextLevel();
						GameController.instance.isDisableTouch = true;
					}
				}
				else
				{
					this._this.ClearAllEnemy();
					GameController.instance.GoToNextLevel();
					GameController.instance.isDisableTouch = true;
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

		internal bool __m__0()
		{
			return this._this.IsAllEnemyDestroy();
		}
	}

	private sealed class _CheckEndOfLevelX_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _currentLevel___0;

		internal DynamicGrid _this;

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

		public _CheckEndOfLevelX_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("CheckEndOfLevel");
				this._current = new WaitUntil(() => this._this.IsAllEnemyDestroy());
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				SoundController.instance.PlaySoundBonusLevel();
				this._currentLevel___0 = GameController.instance.CurrentLevel;
				if (this._currentLevel___0 >= 6 && this._currentLevel___0 % 5 != 0)
				{
					this._this.StartCoroutine(this._this.checkInternetConnection(delegate(bool isConnected)
					{
						if (isConnected)
						{
							/*if (AdsControl.Instance)
							{
								if (AdsControl.Instance.GetInterstitalAvailable())
								{
									if (AdsControl.Instance.GetInterstitalAvailable())
									{
										if ((float)UnityEngine.Random.Range(0, 101) < BaseValue.interstitial_percent)
										{
											this._this.ClearAllEnemy();
											GameController.instance.isIntersShowed = true;
											GameController.instance.isDisableTouch = true;
											this._this.Invoke("ShowInterstitial", 1f);
											this._this.Invoke("GotoNextLevel", 2f);
										}
										else
										{
											this._this.ClearAllEnemy();
											GameController.instance.GoToNextLevel();
											GameController.instance.isDisableTouch = true;
										}
									}
									else
									{
										this._this.ClearAllEnemy();
										GameController.instance.GoToNextLevel();
										GameController.instance.isDisableTouch = true;
									}
								}
								else
								{
									this._this.ClearAllEnemy();
									GameController.instance.GoToNextLevel();
									GameController.instance.isDisableTouch = true;
								}
							}
							else*/
							{
								this._this.ClearAllEnemy();
								GameController.instance.GoToNextLevel();
								GameController.instance.isDisableTouch = true;
							}
						}
						else
						{
							this._this.ClearAllEnemy();
							GameController.instance.GoToNextLevel();
							GameController.instance.isDisableTouch = true;
						}
					}));
				}
				else
				{
					this._this.ClearAllEnemy();
					GameController.instance.GoToNextLevel();
					GameController.instance.isDisableTouch = true;
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

		internal bool __m__0()
		{
			return this._this.IsAllEnemyDestroy();
		}

		internal void __m__1(bool isConnected)
		{
			if (isConnected)
			{
				/*if (AdsControl.Instance)
				{
					if (AdsControl.Instance.GetInterstitalAvailable())
					{
						if (AdsControl.Instance.GetInterstitalAvailable())
						{
							if ((float)UnityEngine.Random.Range(0, 101) < BaseValue.interstitial_percent)
							{
								this._this.ClearAllEnemy();
								GameController.instance.isIntersShowed = true;
								GameController.instance.isDisableTouch = true;
								this._this.Invoke("ShowInterstitial", 1f);
								this._this.Invoke("GotoNextLevel", 2f);
							}
							else
							{
								this._this.ClearAllEnemy();
								GameController.instance.GoToNextLevel();
								GameController.instance.isDisableTouch = true;
							}
						}
						else
						{
							this._this.ClearAllEnemy();
							GameController.instance.GoToNextLevel();
							GameController.instance.isDisableTouch = true;
						}
					}
					else
					{
						this._this.ClearAllEnemy();
						GameController.instance.GoToNextLevel();
						GameController.instance.isDisableTouch = true;
					}
				}
				else*/
				{
					this._this.ClearAllEnemy();
					GameController.instance.GoToNextLevel();
					GameController.instance.isDisableTouch = true;
				}
			}
			else
			{
				this._this.ClearAllEnemy();
				GameController.instance.GoToNextLevel();
				GameController.instance.isDisableTouch = true;
			}
		}
	}

	private sealed class _checkInternetConnection_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal WWW _www___0;

		internal Action<bool> action;

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

		public _checkInternetConnection_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._www___0 = new WWW("http://google.com");
				this._current = this._www___0;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (this._www___0.error != null)
				{
					this.action(false);
				}
				else
				{
					this.action(true);
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

	public static DynamicGrid instance;

	public ObjectPooler enemyObjectPooler;

	public ObjectPooler enemyTopObjectPooler;

	public int numOfStaticEnemy;

	private int numOfEnemy;

	[SerializeField]
	public int rows;

	[SerializeField]
	public int cols;

	[SerializeField]
	public Vector2 gridSize;

	[SerializeField]
	public Vector2 gridOffset;

	[SerializeField]
	private Sprite cellSpriteOdd;

	[SerializeField]
	private Sprite cellSpriteBonus;

	[SerializeField]
	private Sprite cellSpriteBonusTop;

	[SerializeField]
	private Sprite cellSpriteStatic;

	public Vector2 cellSize;

	public Vector2 cellScale;

	private List<Sprite> listCellSprite;

	private List<Sprite> listTopCellSprite;

	private int lowestRow;

	private int shotToStaticStack;

	private List<Color> pColor = new List<Color>
	{
		DynamicGrid.GetColor(187f, 134f, 252f, 1f),
		DynamicGrid.GetColor(152f, 94f, 255f, 1f),
		DynamicGrid.GetColor(127f, 57f, 251f, 1f),
		DynamicGrid.GetColor(98f, 0f, 238f, 1f)
	};

	private List<Color> sColor = new List<Color>
	{
		DynamicGrid.GetColor(3f, 218f, 198f, 1f),
		DynamicGrid.GetColor(0f, 196f, 180f, 1f),
		DynamicGrid.GetColor(0f, 179f, 166f, 1f),
		DynamicGrid.GetColor(0f, 162f, 153f, 1f)
	};

	public List<int> listRandomMap;

	public List<Enemy> enemies;

	private static Func<Enemy, bool> __f__am_cache0;

	private static Func<Enemy, bool> __f__am_cache1;

	private static Func<Enemy, bool> __f__am_cache2;

	private static Func<Enemy, bool> __f__am_cache3;

	public int NumOfStaticEnemy
	{
		get
		{
			return this.numOfStaticEnemy;
		}
		set
		{
			this.numOfStaticEnemy = value;
		}
	}

	public int NumOfEnemy
	{
		get
		{
			return this.numOfEnemy;
		}
		set
		{
			this.numOfEnemy = value;
		}
	}

	internal Sprite GetStaticSprite()
	{
		return this.cellSpriteStatic;
	}

	private void Awake()
	{
		DynamicGrid.instance = this;
		LoadSpriteEnemy.instance.GetEnemySprites(out this.listCellSprite, out this.listTopCellSprite);
	}

	private void Start()
	{
	}

	private void InitListRandomLevel(int currentLvl)
	{
		if (currentLvl < 1 || currentLvl > 30)
		{
			if (currentLvl >= 31 && currentLvl <= 59)
			{
				this.listRandomMap = BaseValue.list_normal_easy.ToList<int>();
			}
			else if (currentLvl >= 61 && currentLvl <= 79)
			{
				this.listRandomMap = BaseValue.list_normal_medium.ToList<int>();
			}
		}
		if (currentLvl >= 81 && currentLvl <= 99)
		{
			this.listRandomMap = BaseValue.list_normal_hard.ToList<int>();
		}
	}

	public void InitNextMap()
	{
		this.enemies = new List<Enemy>();
		List<JSONObject> mapData = this.InitGridInfo();
		this.InitCells(mapData, GameController.instance.CurrentLevel % 5 == 0);
	}

	public static Color GetColor(float r, float g, float b, float a)
	{
		return new Color(r / 255f, g / 255f, b / 255f, a);
	}

	public List<JSONObject> InitGridInfo()
	{
		int currentLevel = GameController.instance.CurrentLevel;
		string text = string.Empty;
		if (currentLevel % 5 == 0)
		{
			if (currentLevel == 30 || currentLevel == 60 || currentLevel == 80 || currentLevel == 100)
			{
				this.listRandomMap.Clear();
			}
			text = string.Format("MapData/Boss/boss_{0}.json", BaseValue.list_boss_level[currentLevel / 5 - 1].ToString("D2"));
			UnityEngine.Debug.Log("Boss: " + text);
		}
		else if (currentLevel >= 1 && currentLevel <= 30)
		{
			text = string.Format("MapData/Default/normal_{0}.json", currentLevel.ToString("D2"));
		}
		else
		{
			if (currentLevel >= 31 && currentLevel <= 59)
			{
				if (this.listRandomMap.Count == 0)
				{
					UnityEngine.Debug.Log("GetNewListEasy: " + this.listRandomMap.Count);
					UnityEngine.Debug.Log(BaseValue.list_normal_easy.Count<int>());
					this.listRandomMap = BaseValue.list_normal_easy.ToList<int>();
					UnityEngine.Debug.Log("GetNewListEasy: " + this.listRandomMap.Count);
				}
			}
			else if (currentLevel >= 61 && currentLevel <= 79)
			{
				if (this.listRandomMap.Count == 0)
				{
					UnityEngine.Debug.Log("GetNewListEasy: " + this.listRandomMap.Count);
					UnityEngine.Debug.Log(BaseValue.list_normal_medium.Count<int>());
					this.listRandomMap = BaseValue.list_normal_medium.ToList<int>();
					UnityEngine.Debug.Log("GetNewListEasy: " + this.listRandomMap.Count);
				}
			}
			else if (currentLevel >= 81 && currentLevel <= 99 && this.listRandomMap.Count == 0)
			{
				UnityEngine.Debug.Log("GetNewListEasy: " + this.listRandomMap.Count);
				UnityEngine.Debug.Log(BaseValue.list_normal_hard.Count<int>());
				this.listRandomMap = BaseValue.list_normal_hard.ToList<int>();
				UnityEngine.Debug.Log("GetNewListEasy: " + this.listRandomMap.Count);
			}
			int item = this.listRandomMap[UnityEngine.Random.Range(0, this.listRandomMap.Count)];
			this.listRandomMap.Remove(item);
			text = string.Format("MapData/Random/normal_{0}.json", item.ToString("D2"));
		}
		JSONObject jSONObject = new JSONObject(Utilities.LoadResourceTextfile(text), -2, false, false);
		UnityEngine.Debug.Log(jSONObject.GetField("map").list.Count);
		this.rows = jSONObject.GetField("map").list.Count;
		this.cols = jSONObject.GetField("map").list[0].Count;
		this.gridSize.x = (float)(this.cols * 2);
		this.gridSize.y = (float)(this.rows * 2);
		if (this.rows == 7)
		{
			this.gridSize.x = this.gridSize.x * 0.8f;
			this.gridSize.y = this.gridSize.y * 0.8f;
		}
		else if (this.rows == 6)
		{
			this.gridSize.x = this.gridSize.x * 0.9f;
			this.gridSize.y = this.gridSize.y * 0.9f;
		}
		Vector3 position = base.transform.position;
		position.y = -2.15f;
		base.transform.position = position;
		this.cellSize = this.cellSpriteOdd.bounds.size * 1f;
		Vector2 vector = new Vector2(this.gridSize.x / (float)this.cols, this.gridSize.x / (float)this.cols);
		this.cellScale.x = vector.x / this.cellSize.x;
		this.cellScale.y = vector.y / this.cellSize.y;
		this.cellSize = vector;
		this.gridOffset.x = -(this.gridSize.x / 2f) + this.cellSize.x / 2f;
		this.gridOffset.y = 0f;
		return jSONObject.GetField("map").list;
	}

	public string GetNormalMapPath(List<int> list)
	{
		int item = list[UnityEngine.Random.Range(0, list.Count)];
		list.Remove(item);
		return string.Format("MapData/Normal/normal_{0}.json", item.ToString("D2"));
	}

	public void AddStack()
	{
		this.shotToStaticStack++;
		if (this.shotToStaticStack >= 5)
		{
			this.shotToStaticStack = 0;
			this.ChangeLowestRow();
		}
	}

	public void ResetStack()
	{
		this.shotToStaticStack = 0;
	}

	public void InitGridInfoBonus()
	{
		Vector3 position = base.transform.position;
		position.y = 0.83f;
		base.transform.position = position;
		this.cellSize = this.cellSpriteOdd.bounds.size * 1f;
		this.rows = 4;
		this.cols = 5;
		this.gridSize.x = (float)(this.cols * 2);
		this.gridSize.y = (float)(this.rows * 2);
		Vector2 vector = new Vector2(this.gridSize.x / (float)this.cols, this.gridSize.y / (float)this.rows);
		this.cellScale.x = vector.x / this.cellSize.x;
		this.cellScale.y = vector.y / this.cellSize.y;
		this.cellSize = vector;
		this.gridOffset.x = -(this.gridSize.x / 2f) + this.cellSize.x / 2f;
		this.gridOffset.y = -(this.gridSize.y / 2f) + this.cellSize.y / 2f;
	}

	public void InitCells(List<JSONObject> mapData, bool isBossLevel = false)
	{
		this.numOfStaticEnemy = 0;
		this.numOfEnemy = 0;
		List<Enemy> list = new List<Enemy>();
		long[,] array = this.RandomArray();
		foreach (JSONObject current in mapData)
		{
			foreach (JSONObject current2 in current.list)
			{
				int num = int.Parse(current2.ToString());
				if (num != 0)
				{
					int num2 = mapData.IndexOf(current);
					int num3 = current.list.IndexOf(current2);
					if (isBossLevel)
					{
						Enemy item = this.DrawEnemyBoss(this.rows - 1 - num2, num3, num);
						this.enemies.Add(item);
					}
					else
					{
						Enemy item2 = this.DrawEnemyNormal(this.rows - 1 - num2, num3, array[this.rows - 1 - num2, num3], num);
						this.enemies.Add(item2);
					}
				}
			}
		}
		if (isBossLevel)
		{
			GameController.instance.canvasBossFight.AnimateBossFight(false);
		}
		list = this.FindLastEmelents();
		if (list.Count != 0)
		{
			int num4 = UnityEngine.Random.Range(1, 101);
			if (num4 < 50)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (i < 1)
					{
						list[i].IsDiamond = true;
					}
				}
			}
			else if (num4 >= 50 && num4 <= 100)
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (j < 2)
					{
						list[j].IsDiamond = true;
					}
				}
			}
		}
	}

	public void InitCells()
	{
		List<Enemy> list = new List<Enemy>();
		List<Enemy> list2 = new List<Enemy>();
		List<Enemy> list3 = new List<Enemy>();
		List<Enemy> list4 = new List<Enemy>();
		long[,] array = this.RandomArray();
		int num = 2;
		for (int i = this.rows - 1; i >= 0; i--)
		{
			for (int j = 0; j < this.cols; j++)
			{
				if (i < this.rows - 2)
				{
					if (j > 0 && j < this.cols - 1)
					{
						Enemy item = this.DrawEnemy(i, j, array[i, j], EnemyType.Normal, false);
						this.enemies.Add(item);
						list.Add(item);
						if (i == 1)
						{
							list3.Add(item);
						}
					}
					else if (i == 1)
					{
						Enemy enemy = this.DrawEnemy(i, j, array[i, j], EnemyType.Top, false);
						this.enemies.Add(enemy);
						enemy.IsTop = true;
						list4.Add(enemy);
					}
					else
					{
						Enemy item2 = this.DrawEnemy(i, j, array[i, j], EnemyType.Normal, false);
						this.enemies.Add(item2);
					}
				}
				else if (j >= num && j < this.cols - num)
				{
					if (j > num && j < this.cols - num - 1)
					{
						Enemy item3 = this.DrawEnemy(i, j, array[i, j], EnemyType.Normal, false);
						this.enemies.Add(item3);
						if (num == 1)
						{
							list.Add(item3);
							list3.Add(item3);
						}
					}
					else
					{
						Enemy item4 = this.DrawEnemy(i, j, array[i, j], EnemyType.Top, false);
						this.enemies.Add(item4);
						list4.Add(item4);
					}
				}
			}
			if (i >= this.rows - 2)
			{
				num--;
			}
		}
	}

	public List<Enemy> FindLastEmelentsOld()
	{
		int num = 0;
		List<Enemy> list = new List<Enemy>();
		for (int i = base.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = base.transform.GetChild(i);
			if (num < 5 && !child.GetComponent<Enemy>().IsStatic && child.transform.position.x + 1.2f >= base.transform.position.x && child.transform.position.y - 1f <= base.transform.position.y)
			{
				list.Add(child.GetComponent<Enemy>());
				num++;
			}
		}
		return list;
	}

	public List<Enemy> FindLastEmelents()
	{
		int num = 0;
		List<Enemy> list = new List<Enemy>();
		for (int i = base.transform.childCount - 1; i >= 0; i--)
		{
			Enemy component = base.transform.GetChild(i).GetComponent<Enemy>();
			if (num < 5 && !component.IsStatic && component.transform.position.x + 3f >= base.transform.position.x && component.transform.position.y >= base.transform.position.y)
			{
				list.Add(component);
				num++;
			}
		}
		return list;
	}

	public Enemy DrawEnemy(int row, int col, long initHealPointValue, EnemyType enemyType, bool isDiamond = false)
	{
		Vector2 vector = new Vector2((float)col * this.cellSize.x + this.gridOffset.x + base.transform.position.x, (float)row * this.cellSize.y + this.cellSize.y / 2f + this.gridOffset.y + base.transform.position.y);
		GameObject pooledObject;
		if (enemyType == EnemyType.Normal)
		{
			pooledObject = this.enemyObjectPooler.GetPooledObject();
		}
		else
		{
			pooledObject = this.enemyTopObjectPooler.GetPooledObject();
		}
		Enemy component = pooledObject.GetComponent<Enemy>();
		pooledObject.SetActive(true);
		component.SetDefaultValue();
		pooledObject.transform.position = new Vector3(vector.x, vector.y, -7f);
		pooledObject.transform.rotation = base.transform.rotation;
		pooledObject.transform.localScale = this.cellScale;
		pooledObject.name = string.Concat(new object[]
		{
			"cellmold_",
			row,
			"_",
			col
		});
		pooledObject.transform.parent = base.transform;
		if (enemyType == EnemyType.Top)
		{
			component.IsTop = true;
		}
		component.defaultColor = Color.white;
		if (GameController.instance.isBonus)
		{
			component.IsStaticInBonusLevel = true;
			if (component.IsTop)
			{
				component._renderer.sprite = this.cellSpriteBonusTop;
			}
			else
			{
				component._renderer.sprite = this.cellSpriteBonus;
			}
		}
		else
		{
			component.InitHealthPoint(initHealPointValue);
		}
		return component;
	}

	public Enemy DrawEnemyNormal(int row, int col, long initHealPointValue, int enemyIndex = 0)
	{
		Vector2 vector = new Vector2((float)col * this.cellSize.x + this.gridOffset.x + base.transform.position.x, (float)row * this.cellSize.y + this.cellSize.y / 2f + this.gridOffset.y + base.transform.position.y);
		GameObject gameObject = null;
		if (enemyIndex > 0)
		{
			gameObject = this.enemyObjectPooler.GetPooledObject();
		}
		else if (enemyIndex < 0)
		{
			gameObject = this.enemyTopObjectPooler.GetPooledObject();
		}
		Enemy component = gameObject.GetComponent<Enemy>();
		gameObject.SetActive(true);
		gameObject.transform.position = new Vector3(vector.x, vector.y, -7f);
		gameObject.transform.rotation = base.transform.rotation;
		gameObject.transform.localScale = this.cellScale;
		gameObject.name = string.Concat(new object[]
		{
			"cellmold_",
			row,
			"_",
			col
		});
		gameObject.transform.parent = base.transform;
		component.SetDefaultValue();
		if (enemyIndex < 0)
		{
			component.IsTop = true;
		}
		component.defaultColor = Color.white;
		if (GameController.instance.isBonus)
		{
			component.IsStaticInBonusLevel = true;
			if (component.IsTop)
			{
				component._renderer.sprite = this.cellSpriteBonusTop;
			}
			else
			{
				component._renderer.sprite = this.cellSpriteBonus;
			}
		}
		else
		{
			if (Mathf.Abs(enemyIndex) >= 1 && Mathf.Abs(enemyIndex) <= 16)
			{
				initHealPointValue = this.RandomValueByColorIndex(enemyIndex);
			}
			component.InitHealthPoint(initHealPointValue);
			if (enemyIndex == 20)
			{
				component.IsStatic = true;
				component._renderer.sprite = this.cellSpriteStatic;
				this.numOfStaticEnemy++;
			}
			else
			{
				this.numOfEnemy++;
			}
		}
		return component;
	}

	public Enemy DrawEnemyBoss(int row, int col, int enemyIndex = 0)
	{
		Vector2 vector = new Vector2((float)col * this.cellSize.x + this.gridOffset.x + base.transform.position.x, (float)row * this.cellSize.y + this.cellSize.y / 2f + this.gridOffset.y + base.transform.position.y);
		GameObject gameObject = null;
		if (enemyIndex > 0)
		{
			gameObject = this.enemyObjectPooler.GetPooledObject();
		}
		else if (enemyIndex < 0)
		{
			gameObject = this.enemyTopObjectPooler.GetPooledObject();
		}
		Enemy component = gameObject.GetComponent<Enemy>();
		gameObject.SetActive(true);
		gameObject.transform.position = new Vector3(vector.x, vector.y, -7f);
		gameObject.transform.rotation = base.transform.rotation;
		gameObject.transform.localScale = this.cellScale;
		gameObject.name = string.Concat(new object[]
		{
			"cellmold_",
			row,
			"_",
			col
		});
		gameObject.transform.parent = base.transform;
		component.SetDefaultValue();
		if (enemyIndex < 0)
		{
			component.IsTop = true;
		}
		component.defaultColor = Color.white;
		if (GameController.instance.isBonus)
		{
			component.IsStaticInBonusLevel = true;
			if (component.IsTop)
			{
				component._renderer.sprite = this.cellSpriteBonusTop;
			}
			else
			{
				component._renderer.sprite = this.cellSpriteBonus;
			}
		}
		else
		{
			long value = this.RandomValueByColorIndex(enemyIndex);
			component.InitHealthPoint(value);
			if (enemyIndex == 20)
			{
				component.IsStatic = true;
				component._renderer.sprite = this.cellSpriteStatic;
				this.numOfStaticEnemy++;
			}
			else
			{
				this.numOfEnemy++;
			}
		}
		return component;
	}

	public long RandomValueByColorIndex(int colorIndex)
	{
		switch (Mathf.Abs(colorIndex))
		{
		case 1:
			return this.LongRandom(1L, (long)(BaseValue.list_color_block[0] + 1), new System.Random());
		case 2:
			return this.LongRandom((long)(BaseValue.list_color_block[0] + 1), (long)(BaseValue.list_color_block[1] + 1), new System.Random());
		case 3:
			return this.LongRandom((long)(BaseValue.list_color_block[1] + 1), (long)(BaseValue.list_color_block[2] + 1), new System.Random());
		case 4:
			return this.LongRandom((long)(BaseValue.list_color_block[2] + 1), (long)(BaseValue.list_color_block[3] + 1), new System.Random());
		case 5:
			return this.LongRandom((long)(BaseValue.list_color_block[3] + 1), (long)(BaseValue.list_color_block[4] + 1), new System.Random());
		case 6:
			return this.LongRandom((long)(BaseValue.list_color_block[4] + 1), (long)(BaseValue.list_color_block[5] + 1), new System.Random());
		case 7:
			return this.LongRandom((long)(BaseValue.list_color_block[5] + 1), (long)(BaseValue.list_color_block[6] + 1), new System.Random());
		case 8:
			return this.LongRandom((long)(BaseValue.list_color_block[6] + 1), (long)(BaseValue.list_color_block[7] + 1), new System.Random());
		case 9:
			return this.LongRandom((long)(BaseValue.list_color_block[7] + 1), (long)(BaseValue.list_color_block[8] + 1), new System.Random());
		case 10:
			return this.LongRandom((long)(BaseValue.list_color_block[8] + 1), (long)(BaseValue.list_color_block[9] + 1), new System.Random());
		case 11:
			return this.LongRandom((long)(BaseValue.list_color_block[9] + 1), (long)(BaseValue.list_color_block[10] + 1), new System.Random());
		case 12:
			return this.LongRandom((long)(BaseValue.list_color_block[10] + 1), (long)(BaseValue.list_color_block[11] + 1), new System.Random());
		case 13:
			return this.LongRandom((long)(BaseValue.list_color_block[11] + 1), (long)(BaseValue.list_color_block[12] + 1), new System.Random());
		case 14:
			return this.LongRandom((long)(BaseValue.list_color_block[12] + 1), (long)(BaseValue.list_color_block[13] + 1), new System.Random());
		case 15:
			return this.LongRandom((long)(BaseValue.list_color_block[13] + 1), (long)(BaseValue.list_color_block[14] + 1), new System.Random());
		case 16:
			return this.LongRandom((long)(BaseValue.list_color_block[14] + 1), (long)(BaseValue.list_color_block[15] + 1), new System.Random());
		default:
			return 1L;
		}
	}

	private long LongRandom(long min, long max, System.Random rand)
	{
		byte[] array = new byte[8];
		rand.NextBytes(array);
		long num = BitConverter.ToInt64(array, 0);
		return Math.Abs(num % (max - min)) + min;
	}

	public long[,] RandomArray()
	{
		long[,] array = new long[this.rows, this.cols];
		List<long> list = new List<long>();
		for (int i = 0; i < this.rows * this.cols; i++)
		{
			list.Add(this.RandomValue(GameController.instance.CurrentLevel));
		}
		list.Sort();
		for (int j = 0; j < this.rows; j++)
		{
			for (int k = this.cols - 1; k >= 0; k--)
			{
				array[j, k] = list[j + k * this.rows];
			}
		}
		for (int l = 0; l < array.GetLength(1); l++)
		{
			for (int m = 0; m < array.GetLength(0) - 1; m++)
			{
				for (int n = m + 1; n < array.GetLength(0); n++)
				{
					if (array[n, l] > array[m, l])
					{
						long num = array[n, l];
						array[n, l] = array[m, l];
						array[m, l] = num;
					}
				}
			}
		}
		return array;
	}

	public long RandomValue(int lv)
	{
		switch (lv)
		{
		case 1:
			return (long)UnityEngine.Random.Range(1, 3);
		case 2:
			return (long)UnityEngine.Random.Range(2, 20);
		case 3:
			return (long)UnityEngine.Random.Range(10, 50);
		case 4:
			return (long)UnityEngine.Random.Range(40, 150);
		default:
			return (long)UnityEngine.Random.Range((lv + 1) * lv * 5, (lv + 2) * (lv + 1) * 7);
		}
	}

	public Vector3 GetHighestCell()
	{
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.transform.position.y > gameObject.transform.position.y)
				{
					gameObject = transform.gameObject;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		return gameObject.transform.position;
	}

	public Sprite GetSprite(double value)
	{
		if (value >= 0.0 && value <= (double)BaseValue.list_color_block[0])
		{
			return this.listCellSprite[0];
		}
		if (value > (double)BaseValue.list_color_block[0] && value <= (double)BaseValue.list_color_block[1])
		{
			return this.listCellSprite[1];
		}
		if (value > (double)BaseValue.list_color_block[1] && value <= (double)BaseValue.list_color_block[2])
		{
			return this.listCellSprite[2];
		}
		if (value > (double)BaseValue.list_color_block[2] && value <= (double)BaseValue.list_color_block[3])
		{
			return this.listCellSprite[3];
		}
		if (value > (double)BaseValue.list_color_block[3] && value <= (double)BaseValue.list_color_block[4])
		{
			return this.listCellSprite[4];
		}
		if (value > (double)BaseValue.list_color_block[4] && value <= (double)BaseValue.list_color_block[5])
		{
			return this.listCellSprite[5];
		}
		if (value > (double)BaseValue.list_color_block[5] && value <= (double)BaseValue.list_color_block[6])
		{
			return this.listCellSprite[6];
		}
		if (value > (double)BaseValue.list_color_block[6] && value <= (double)BaseValue.list_color_block[7])
		{
			return this.listCellSprite[7];
		}
		if (value > (double)BaseValue.list_color_block[7] && value <= (double)BaseValue.list_color_block[8])
		{
			return this.listCellSprite[8];
		}
		if (value > (double)BaseValue.list_color_block[8] && value <= (double)BaseValue.list_color_block[9])
		{
			return this.listCellSprite[9];
		}
		if (value > (double)BaseValue.list_color_block[9] && value <= (double)BaseValue.list_color_block[10])
		{
			return this.listCellSprite[10];
		}
		if (value > (double)BaseValue.list_color_block[10] && value <= (double)BaseValue.list_color_block[11])
		{
			return this.listCellSprite[11];
		}
		if (value > (double)BaseValue.list_color_block[11] && value <= (double)BaseValue.list_color_block[12])
		{
			return this.listCellSprite[12];
		}
		if (value > (double)BaseValue.list_color_block[12] && value <= (double)BaseValue.list_color_block[13])
		{
			return this.listCellSprite[13];
		}
		if (value > (double)BaseValue.list_color_block[13] && value <= (double)BaseValue.list_color_block[14])
		{
			return this.listCellSprite[14];
		}
		return this.listCellSprite[15];
	}

	public Sprite GetSpriteForTopEnemy(double value)
	{
		if (value >= 0.0 && value <= (double)BaseValue.list_color_block[0])
		{
			return this.listTopCellSprite[0];
		}
		if (value > (double)BaseValue.list_color_block[0] && value <= (double)BaseValue.list_color_block[1])
		{
			return this.listTopCellSprite[1];
		}
		if (value > (double)BaseValue.list_color_block[1] && value <= (double)BaseValue.list_color_block[2])
		{
			return this.listTopCellSprite[2];
		}
		if (value > (double)BaseValue.list_color_block[2] && value <= (double)BaseValue.list_color_block[3])
		{
			return this.listTopCellSprite[3];
		}
		if (value > (double)BaseValue.list_color_block[3] && value <= (double)BaseValue.list_color_block[4])
		{
			return this.listTopCellSprite[4];
		}
		if (value > (double)BaseValue.list_color_block[4] && value <= (double)BaseValue.list_color_block[5])
		{
			return this.listTopCellSprite[5];
		}
		if (value > (double)BaseValue.list_color_block[5] && value <= (double)BaseValue.list_color_block[6])
		{
			return this.listTopCellSprite[6];
		}
		if (value > (double)BaseValue.list_color_block[6] && value <= (double)BaseValue.list_color_block[7])
		{
			return this.listTopCellSprite[7];
		}
		if (value > (double)BaseValue.list_color_block[7] && value <= (double)BaseValue.list_color_block[8])
		{
			return this.listTopCellSprite[8];
		}
		if (value > (double)BaseValue.list_color_block[8] && value <= (double)BaseValue.list_color_block[9])
		{
			return this.listTopCellSprite[9];
		}
		if (value > (double)BaseValue.list_color_block[9] && value <= (double)BaseValue.list_color_block[10])
		{
			return this.listTopCellSprite[10];
		}
		if (value > (double)BaseValue.list_color_block[10] && value <= (double)BaseValue.list_color_block[11])
		{
			return this.listTopCellSprite[11];
		}
		if (value > (double)BaseValue.list_color_block[11] && value <= (double)BaseValue.list_color_block[12])
		{
			return this.listTopCellSprite[12];
		}
		if (value > (double)BaseValue.list_color_block[12] && value <= (double)BaseValue.list_color_block[13])
		{
			return this.listTopCellSprite[13];
		}
		if (value > (double)BaseValue.list_color_block[13] && value <= (double)BaseValue.list_color_block[14])
		{
			return this.listTopCellSprite[14];
		}
		return this.listTopCellSprite[15];
	}

	public void ClearAllEnemy()
	{
		while (base.transform.childCount != 0)
		{
			Transform child = base.transform.GetChild(0);
			child.SetParent(this.enemyObjectPooler.transform);
			child.gameObject.SetActive(false);
			GameObject pooledObject = ParticleNormalBlockDestroyPooler.instance.GetPooledObject("static_particle");
			pooledObject.SetActive(true);
			pooledObject.transform.position = child.transform.position;
		}
		this.enemies.Clear();
	}

	public void _ClearAllEnemy(bool isBonus = false)
	{
		while (base.transform.childCount != 0)
		{
			Transform child = base.transform.GetChild(0);
			child.SetParent(this.enemyObjectPooler.transform);
			child.gameObject.SetActive(false);
			GameObject pooledObject = ParticleNormalBlockDestroyPooler.instance.GetPooledObject((!isBonus) ? "static_particle" : "bonus_particle");
			pooledObject.SetActive(true);
			pooledObject.transform.position = child.transform.position;
		}
		this.enemies.Clear();
	}

	public void ClearAllEnemyAfterBonus()
	{
		while (base.transform.childCount != 0)
		{
			Transform child = base.transform.GetChild(0);
			if (child.GetComponent<Enemy>().EnemyType == EnemyType.Normal)
			{
				child.SetParent(this.enemyObjectPooler.transform);
			}
			else
			{
				child.SetParent(this.enemyTopObjectPooler.transform);
			}
			child.gameObject.SetActive(false);
			GameObject pooledObject = ParticleNormalBlockDestroyPooler.instance.GetPooledObject("bonus_particle");
			pooledObject.SetActive(true);
			pooledObject.transform.position = child.transform.position;
		}
		this.enemies.Clear();
	}

	public void RandomDiamond()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				Enemy component = transform.GetComponent<Enemy>();
				if (!component.IsStatic && component.gameObject != base.gameObject)
				{
					component.IsDiamond = true;
					break;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	internal void MoveToDiamondPanel(GameObject obj, Vector3 pos)
	{
		SoundController.instance.PlaySoundDiamond();
		pos.z = obj.transform.position.z;
		obj.transform.SetParent(DiamondObjectPooler.instance.transform);
		obj.transform.DOPath(this.ConvertPathFromBaveValue(BaseValue.paths, obj.transform.position, pos), 0.6f, PathType.Linear, PathMode.Sidescroller2D, 20, null).OnComplete(delegate
		{
			obj.SetActive(false);
			if (CanvasTop.instance)
			{
				CanvasTop.instance.AnimateTextDiamond();
			}
		});
	}

	public Vector3[] ConvertPathFromBaveValue(Vector3[] paths, Vector3 source, Vector3 destination)
	{
		return new Vector3[]
		{
			paths[0] + source,
			paths[1] + source,
			paths[2] + source,
			destination
		};
	}

	public bool IsAllEnemyDestroy()
	{
		return base.transform.childCount == this.numOfStaticEnemy;
	}

	public Enemy GetLowestEnemyX()
	{
		List<Enemy> list = (from e in this.enemies
		where !e.isStatic
		select e).ToList<Enemy>();
		if (list.Count<Enemy>() != 0)
		{
			Enemy enemy = list[0];
			foreach (Enemy current in list)
			{
				if (current.transform.position.y < enemy.transform.position.y + this.cellSize.x / 2f)
				{
					if (current.transform.position.y + this.cellSize.x / 2f > enemy.transform.position.y)
					{
						if (current.transform.position.x < enemy.transform.position.x)
						{
							enemy = current;
						}
					}
					else
					{
						enemy = current;
					}
				}
			}
			return enemy;
		}
		return null;
	}

	public Enemy GetLowestEnemy()
	{
		List<Enemy> list = (from e in this.enemies
		where !e.isStatic
		select e).ToList<Enemy>();
		if (list.Count<Enemy>() != 0)
		{
			float num = base.transform.position.y + (float)this.lowestRow * this.cellSize.x + this.cellSize.x / 2f;
			Enemy enemy = list[list.Count - 1];
			foreach (Enemy current in list)
			{
				if (current.transform.position.y < num + this.cellSize.x / 2f)
				{
					if (current.transform.position.y + this.cellSize.x / 2f > num)
					{
						if (current.transform.position.x < enemy.transform.position.x)
						{
							enemy = current;
							return enemy;
						}
					}
					else
					{
						enemy = current;
					}
				}
			}
			return enemy;
		}
		this.lowestRow++;
		if (this.lowestRow >= this.rows)
		{
			this.lowestRow = 0;
		}
		return null;
	}

	public void ChangeLowestRow()
	{
		this.lowestRow++;
		if (this.lowestRow >= this.rows)
		{
			this.lowestRow = 0;
		}
	}

	public int GetFirstNumberFromString(string name)
	{
		string s = name.Substring(9, 1);
		return int.Parse(s);
	}

	public Enemy FindEnemy()
	{
		if (GameController.instance.isBonus)
		{
			Enemy enemy = this.FindEnemyX();
			if (enemy != null)
			{
				return enemy;
			}
			return null;
		}
		else
		{
			Enemy lowestEnemy = this.GetLowestEnemy();
			if (lowestEnemy != null)
			{
				return lowestEnemy;
			}
			return null;
		}
	}

	public Enemy FindEnemyX()
	{
		if (GameController.instance.isBonus)
		{
			if (this.enemies.Count != 0)
			{
				return this.enemies[UnityEngine.Random.Range(0, this.enemies.Count)];
			}
			return null;
		}
		else
		{
			List<Enemy> list = (from enemy in this.enemies
			where !enemy.isStatic
			select enemy).ToList<Enemy>();
			if (list.Count != 0)
			{
				return list[UnityEngine.Random.Range(0, list.Count)];
			}
			return null;
		}
	}

	public Enemy FindStaticEnemy()
	{
		List<Enemy> list = (from enemy in this.enemies
		where enemy.isStatic
		select enemy).ToList<Enemy>();
		if (list.Count != 0)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return null;
	}

	public void RemoveEnemyFromList(Enemy enemy)
	{
		if (this.enemies.Count > 0)
		{
			this.ChangeLowestRow();
			this.enemies.Remove(enemy);
			this.NumOfEnemy--;
			if (this.numOfStaticEnemy == base.transform.childCount)
			{
				this.CheckEndOfLevel();
			}
		}
	}

	public void CheckEndOfLevel()
	{
		SoundController.instance.PlaySoundBonusLevel();
		int currentLevel = GameController.instance.CurrentLevel;
		if (currentLevel >= 6 && currentLevel % 5 != 0)
		{
			/*if (AdsControl.Instance.GetInterstitalAvailable())
			{
				if ((float)UnityEngine.Random.Range(0, 101) < BaseValue.interstitial_percent)
				{
					this.ClearAllEnemy();
					GameController.instance.isIntersShowed = true;
					GameController.instance.isDisableTouch = true;
					base.Invoke("ShowInterstitial", 1f);
					base.Invoke("GotoNextLevel", 2f);
				}
				else
				{
					this.ClearAllEnemy();
					GameController.instance.GoToNextLevel();
					GameController.instance.isDisableTouch = true;
				}
			}
			else*/
			{
				this.ClearAllEnemy();
				GameController.instance.GoToNextLevel();
				GameController.instance.isDisableTouch = true;
			}
		}
		else
		{
			this.ClearAllEnemy();
			GameController.instance.GoToNextLevel();
			GameController.instance.isDisableTouch = true;
		}
	}

	public IEnumerator CheckEndOfLevelXX()
	{
		DynamicGrid._CheckEndOfLevelXX_c__Iterator0 _CheckEndOfLevelXX_c__Iterator = new DynamicGrid._CheckEndOfLevelXX_c__Iterator0();
		_CheckEndOfLevelXX_c__Iterator._this = this;
		return _CheckEndOfLevelXX_c__Iterator;
	}

	public IEnumerator CheckEndOfLevelX()
	{
		DynamicGrid._CheckEndOfLevelX_c__Iterator1 _CheckEndOfLevelX_c__Iterator = new DynamicGrid._CheckEndOfLevelX_c__Iterator1();
		_CheckEndOfLevelX_c__Iterator._this = this;
		return _CheckEndOfLevelX_c__Iterator;
	}

	private IEnumerator checkInternetConnection(Action<bool> action)
	{
		DynamicGrid._checkInternetConnection_c__Iterator2 _checkInternetConnection_c__Iterator = new DynamicGrid._checkInternetConnection_c__Iterator2();
		_checkInternetConnection_c__Iterator.action = action;
		return _checkInternetConnection_c__Iterator;
	}

	private void GotoNextLevel()
	{
		GameController.instance.GoToNextLevel();
	}

	private void ShowInterstitial()
	{
		GameController.instance.ShowInterstitial();
	}

	public string GetHtmlFromUri(string resource)
	{
		string text = string.Empty;
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				bool flag = httpWebResponse.StatusCode < (HttpStatusCode)299 && httpWebResponse.StatusCode >= HttpStatusCode.OK;
				if (flag)
				{
					using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
					{
						char[] array = new char[80];
						streamReader.Read(array, 0, array.Length);
						char[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							char c = array2[i];
							text += c;
						}
					}
				}
			}
		}
		catch
		{
			return string.Empty;
		}
		return text;
	}
}
