using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private sealed class _Burn_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal long damage;

		internal long coin;

		internal Enemy _this;

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

		public _Burn_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 4;
				}
				return true;
			case 4u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 5;
				}
				return true;
			case 5u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 6;
				}
				return true;
			case 6u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 7;
				}
				return true;
			case 7u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 8;
				}
				return true;
			case 8u:
				this._this.StopCoroutine("Burn");
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

	private sealed class _Burn_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal long damage;

		internal long coin;

		internal float time;

		internal float _numOfBurnPerHalfSecond___0;

		internal int _i___1;

		internal Enemy _this;

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

		public _Burn_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 4;
				}
				return true;
			case 4u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 5;
				}
				return true;
			case 5u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 6;
				}
				return true;
			case 6u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 7;
				}
				return true;
			case 7u:
				this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
				this._numOfBurnPerHalfSecond___0 = this.time / 0.5f;
				this._i___1 = 0;
				break;
			case 8u:
				this._i___1++;
				break;
			case 9u:
				this._this.StopCoroutine("Burn");
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if ((float)this._i___1 >= this._numOfBurnPerHalfSecond___0)
			{
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 9;
				}
				return true;
			}
			this._this.CallFlash((double)this.damage, this.coin, ProjectileType.Non_Projectile);
			this._current = new WaitForSeconds(0.5f);
			if (!this._disposing)
			{
				this._PC = 8;
			}
			return true;
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

	public static Enemy instance;

	public double healthPoint;

	public double initHealthPoint;

	public bool isStatic;

	public bool isDiamond;

	public bool isTop;

	[SerializeField]
	public TextMesh textValue;

	[SerializeField]
	public SpriteRenderer _renderer;

	[SerializeField]
	private EnemyType enemyType;

	private Rigidbody2D rigid;

	private bool isFlash;

	public static float animateDuration = 0.1f;

	public Color defaultColor;

	public Color hitColor;

	public Shader defaultShader;

	public Shader hitShader;

	protected double HealthPoint
	{
		get
		{
			return this.healthPoint;
		}
		set
		{
			this.healthPoint = value;
			if (this.healthPoint < 0.0)
			{
				this.healthPoint = 0.0;
			}
			if (this.isTop)
			{
				base.GetComponent<SpriteRenderer>().sprite = DynamicGrid.instance.GetSpriteForTopEnemy(this.healthPoint);
			}
			else
			{
				base.GetComponent<SpriteRenderer>().sprite = DynamicGrid.instance.GetSprite(this.healthPoint);
			}
			this.SetText(this.healthPoint);
		}
	}

	public bool IsStaticInBonusLevel
	{
		get
		{
			return this.isStatic;
		}
		set
		{
			this.isStatic = value;
			if (this.isStatic)
			{
				this.HideText();
				this.Rigid.gravityScale = 0f;
				this.Rigid.constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
	}

	public bool IsStatic
	{
		get
		{
			return this.isStatic;
		}
		set
		{
			this.isStatic = value;
			if (this.isStatic)
			{
				this.HideText();
				this.Rigid.gravityScale = 0f;
				this.Rigid.constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
	}

	public Rigidbody2D Rigid
	{
		get
		{
			return this.rigid;
		}
		set
		{
			this.rigid = value;
		}
	}

	public bool IsDiamond
	{
		get
		{
			return this.isDiamond;
		}
		set
		{
			this.isDiamond = value;
			this.HideText();
			if (this.isDiamond)
			{
				UnityEngine.Debug.Log("Create Diamond");
				GameObject pooledObject = DiamondObjectPooler.instance.GetPooledObject();
				pooledObject.SetActive(true);
				pooledObject.transform.SetParent(base.transform);
				pooledObject.transform.position = base.transform.position;
				pooledObject.transform.eulerAngles = Vector3.zero;
				pooledObject.name = "diamond";
			}
		}
	}

	public bool IsTop
	{
		get
		{
			return this.isTop;
		}
		set
		{
			this.isTop = value;
		}
	}

	public EnemyType EnemyType
	{
		get
		{
			return this.enemyType;
		}
		set
		{
			this.enemyType = value;
		}
	}

	public virtual void SetHealthPoint(double val)
	{
		this.HealthPoint = val;
	}

	public virtual double GetHealPoint()
	{
		return this.healthPoint;
	}

	public virtual void SubtractHealthPoint(double valueToSubtract)
	{
		this.HealthPoint -= valueToSubtract;
	}

	public virtual void DestroyEnemy()
	{
	}

	public virtual void AnimateHitEffect()
	{
	}

	public virtual void SetText(double value)
	{
	}

	public virtual void SetText(string value)
	{
	}

	public void HideText()
	{
		this.textValue.gameObject.SetActive(false);
	}

	public void ShowText()
	{
		this.textValue.gameObject.SetActive(true);
	}

	public virtual long RandomValue(int lvl)
	{
		return (long)UnityEngine.Random.Range(lvl * lvl * 4, lvl * lvl * 4 + 20 * lvl);
	}

	public double InitHealthPoint(long value)
	{
		this.SetHealthPoint((double)value);
		this.initHealthPoint = (double)value;
		return (double)value;
	}

	private void Awake()
	{
		this.defaultShader = Shader.Find("Sprites/Default");
		this.hitShader = Shader.Find("Mobile/Particles/Additive");
	}

	private void Start()
	{
		this.Rigid = base.GetComponent<Rigidbody2D>();
		this._renderer.color = Color.white;
		this.hitColor = new Color(0.819607854f, 0.819607854f, 0.819607854f, 1f);
	}

	public void SetDefaultValue()
	{
		if (this.Rigid)
		{
			this.isStatic = false;
			this.isDiamond = false;
			this.isTop = false;
			this.Rigid.constraints = RigidbodyConstraints2D.None;
			this.Rigid.gravityScale = 3f;
			this.ShowText();
		}
		else
		{
			this.Rigid = base.GetComponent<Rigidbody2D>();
			this.isStatic = false;
			this.isDiamond = false;
			this.isTop = false;
			this.Rigid.constraints = RigidbodyConstraints2D.None;
			this.Rigid.gravityScale = 3f;
			this.ShowText();
		}
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
	}

	public void CallFlash(double value, long coinToAdd = 5L, ProjectileType projectileType = ProjectileType.Projectile)
	{
		if (!this.isStatic)
		{
			this.isFlash = true;
			if (projectileType == ProjectileType.Projectile)
			{
				int coefLevel = BaseValue.GetCoefLevel(GameController.instance.CurrentLevel);
				GameController.instance.AddCoin((long)(BaseValue.coin_per_hit * GameController.instance.factorMoney * coefLevel), base.transform.position);
				GameController.instance.SubtractDamage((long)value, base.transform.position);
			}
			else
			{
				int coefLevel_ = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
				GameController.instance.AddCoin(coinToAdd * (long)GameController.instance.factorMoney * (long)coefLevel_, base.transform.position);
				GameController.instance.SubtractDamage((long)value, base.transform.position);
			}
			this.SubtractHealthPoint(value);
			if (this.healthPoint <= 0.0)
			{
				SoundController.instance.PlaySoundEnemyDestroy();
				base.StopCoroutine("Burn");
				if (this.IsDiamond)
				{
					this.IsDiamond = false;
					GameController.instance.Diamond += 1L;
					IEnumerator enumerator = base.transform.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Transform transform = (Transform)enumerator.Current;
							if (transform.gameObject.name == "diamond")
							{
								DynamicGrid.instance.MoveToDiamondPanel(transform.gameObject, GameController.instance.diamondImagePos.position);
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
				GameObject pooledObject = ParticleNormalBlockDestroyPooler.instance.GetPooledObject("normal_particle");
				pooledObject.SetActive(true);
				pooledObject.transform.position = base.transform.position;
				if (this.EnemyType == EnemyType.Normal)
				{
					base.gameObject.transform.SetParent(DynamicGrid.instance.enemyObjectPooler.transform);
				}
				else
				{
					base.gameObject.transform.SetParent(DynamicGrid.instance.enemyTopObjectPooler.transform);
				}
				DynamicGrid.instance.RemoveEnemyFromList(this);
				base.gameObject.SetActive(false);
			}
		}
		else if (GameController.instance.isBonus)
		{
			this.isFlash = true;
			if (projectileType == ProjectileType.Projectile)
			{
				int coefLevel_2 = BaseValue.GetCoefLevel_2(GameController.instance.CurrentLevel);
				int coefBonus = BaseValue.GetCoefBonus(GameController.instance.CurrentLevel);
				GameController.instance.AddCoinForBonus((long)(BaseValue.coin_per_hit * GameController.instance.factorMoney * coefBonus * coefLevel_2), base.transform.position);
			}
			else
			{
				int coefLevelBonus = BaseValue.GetCoefLevelBonus(GameController.instance.CurrentLevel);
				int coefBonus2 = BaseValue.GetCoefBonus(GameController.instance.CurrentLevel);
				GameController.instance.AddCoinForBonus(coinToAdd * (long)GameController.instance.factorMoney * (long)coefBonus2 * (long)coefLevelBonus, base.transform.position);
			}
		}
	}

	public void StartBurn(long damage, long coin)
	{
		base.StartCoroutine(this.Burn(damage, coin));
	}

	public IEnumerator Burn(long damage, long coin)
	{
		Enemy._Burn_c__Iterator0 _Burn_c__Iterator = new Enemy._Burn_c__Iterator0();
		_Burn_c__Iterator.damage = damage;
		_Burn_c__Iterator.coin = coin;
		_Burn_c__Iterator._this = this;
		return _Burn_c__Iterator;
	}

	public IEnumerator Burn(long damage, long coin, float time)
	{
		Enemy._Burn_c__Iterator1 _Burn_c__Iterator = new Enemy._Burn_c__Iterator1();
		_Burn_c__Iterator.damage = damage;
		_Burn_c__Iterator.coin = coin;
		_Burn_c__Iterator.time = time;
		_Burn_c__Iterator._this = this;
		return _Burn_c__Iterator;
	}

	private void OnParticleTrigger()
	{
		UnityEngine.Debug.Log("Enemy OnParticleTrigger");
	}
}
