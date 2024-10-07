using DG.Tweening;
using DigitalRubyShared;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
	public enum State
	{
		Searching,
		Aiming,
		AimingForTouch,
		Firing,
		Clicking,
		Waiting,
		WaitingForNextTouch
	}

	public static Gun instance;

	public Tweener animFire;

	[SerializeField]
	public Transform muzzle;

	private long power;

	private long bulletSpeed;

	private double reloadSpeed;

	private bool enableMultiTap = true;

	public Gun.State state;

	public const int MAX_ANGLE = 70;

	public const int MIN_ANGLE = 10;

	private Enemy curTarget;

	private float cooldownTime;

	public Vector2 targetPos;

	public Vector2 _touchPos = new Vector2(-100f, -100f);

	public float waitAfterClick;

	private float timeToWaitAfterClick = 0.4f;

	public bool isWaiting = true;

	private int moreAccurateBeat;

	private float timePerPanning = 0.2f;

	public long Power
	{
		get
		{
			return this.power;
		}
		set
		{
			this.power = value;
		}
	}

	public long BulletSpeed
	{
		get
		{
			return this.bulletSpeed;
		}
		set
		{
			this.bulletSpeed = value;
		}
	}

	public double ReloadSpeed
	{
		get
		{
			return this.reloadSpeed;
		}
		set
		{
			this.reloadSpeed = value;
		}
	}

	public void AddPower(long value)
	{
		this.Power += value;
	}

	public void AddBulletSpeed(long value)
	{
		this.BulletSpeed += value;
	}

	public void AddReloadSpeed(double value)
	{
		this.ReloadSpeed += value;
	}

	public void UpdatePower(long value)
	{
		this.Power = value;
	}

	public void UpdateBulletSpeed(long value)
	{
		this.BulletSpeed = value;
	}

	public void UpdateReloadSpeed(double value)
	{
		this.ReloadSpeed = value;
	}

	public void UpdateStat(double value, UpgradeType type)
	{
		switch (type)
		{
		case UpgradeType.Power:
			this.UpdatePower((long)value);
			break;
		case UpgradeType.BulletSpeed:
			this.UpdateBulletSpeed((long)value);
			break;
		case UpgradeType.ReloadSpeed:
			this.UpdateReloadSpeed(value);
			break;
		}
	}

	public void LoadStats()
	{
		if (GameController.instance)
		{
			this.Power = GameController.instance.game.Gun_power;
			this.BulletSpeed = GameController.instance.game.Gun_bulletspeed;
			this.ReloadSpeed = GameController.instance.game.Gun_reloadspeed;
		}
		else
		{
			UnityEngine.Debug.LogError("GameController not found!!!");
		}
	}

	public void SaveStats()
	{
		if (GameController.instance)
		{
			GameController.instance.game.Gun_power = this.Power;
			GameController.instance.game.Gun_bulletspeed = this.BulletSpeed;
			GameController.instance.game.Gun_reloadspeed = this.ReloadSpeed;
		}
		else
		{
			UnityEngine.Debug.LogError("GameController not found!!!");
		}
	}

	public void ResetStats(int numOfTimeReset)
	{
		if (numOfTimeReset != 2)
		{
			this.Power = BaseValue.basevalue_power;
			this.BulletSpeed = BaseValue.basevalue_bulletspeed;
			this.ReloadSpeed = (double)BaseValue.basevalue_reloadspeed;
		}
		else
		{
			this.Power = BaseValue.basevalue_power_tank2;
			this.BulletSpeed = BaseValue.basevalue_bulletspeed_tank2;
			this.ReloadSpeed = (double)BaseValue.basevalue_reloadspeed_tank2;
		}
		this.SaveStats();
	}

	public float GetDistanceToMuzzle()
	{
		return Vector2.Distance(base.transform.position, this.muzzle.transform.position);
	}

	public virtual void Start()
	{
	}

	private void ShowInterstitial()
	{
		GameController.instance.ShowInterstitial();
	}

	public virtual void Update()
	{
		if (GameController.instance.isDisableTouch)
		{
			return;
		}
		if (this.isWaiting)
		{
			this.waitAfterClick += Time.deltaTime;
		}
		if (this.waitAfterClick >= this.timeToWaitAfterClick)
		{
			this.waitAfterClick = 0f;
			this.isWaiting = false;
			this._touchPos = new Vector2(-100f, -100f);
		}
		Vector2 touchPos = Vector2.zero;
		Touch[] touches = Input.touches;
		for (int i = 0; i < touches.Length; i++)
		{
			Touch touch = touches[i];
			int fingerId = touch.fingerId;
			touchPos = Camera.main.ScreenToWorldPoint(touch.position);
			if (EventSystem.current.IsPointerOverGameObject(fingerId))
			{
				UnityEngine.Debug.Log("UITouched");
			}
			else
			{
				switch (touch.phase)
				{
				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if (touchPos.x >= base.transform.position.x + 7f && touchPos.y >= Tank.instance.transform.position.y - 2f && touchPos.y < CanvasTop.instance.GetTextDiamondPos().y - 1f)
					{
						this._touchPos = touchPos;
						this.waitAfterClick = 0f;
						this.isWaiting = true;
						this.timePerPanning -= Time.deltaTime;
						if (this.timePerPanning <= 0f)
						{
							this.timePerPanning = 0.2f;
							GameObject pooledObject = ParticleTouchObjectPooler.instance.GetPooledObject();
							pooledObject.SetActive(true);
							pooledObject.transform.position = new Vector3(touchPos.x, touchPos.y, 0f);
						}
						this.FireBullet(touchPos, true);
						return;
					}
					break;
				case TouchPhase.Ended:
					UnityEngine.Debug.Log(touch.position);
					UnityEngine.Debug.Log(Screen.height - 67);
					if (touchPos.x >= base.transform.position.x + 7f && touchPos.y >= Tank.instance.transform.position.y - 2f && touchPos.y < CanvasTop.instance.GetTextDiamondPos().y - 1f)
					{
						this._touchPos = touchPos;
						GameObject pooledObject2 = ParticleTouchObjectPooler.instance.GetPooledObject();
						pooledObject2.SetActive(true);
						pooledObject2.transform.position = new Vector3(touchPos.x, touchPos.y, 0f);
						this.waitAfterClick = 0f;
						this.isWaiting = true;
					}
					break;
				}
			}
		}
		if (!this.isWaiting)
		{
			if (this._touchPos.x == -100f)
			{
				this.FireBullet(false);
			}
			else
			{
				this.FireBullet(this._touchPos, true);
			}
		}
		else if (touchPos.x < base.transform.position.x + 7f || touchPos.y < Tank.instance.transform.position.y - 2f || touchPos.y >= CanvasTop.instance.GetTextDiamondPos().y - 1f)
		{
			if (this._touchPos.x == -100f)
			{
				this.FireBullet(false);
			}
			else
			{
				this.FireBullet(this._touchPos, true);
			}
		}
		else
		{
			this.FireBullet(this._touchPos, true);
		}
	}

	public void ClearAllTouch()
	{
		this.isWaiting = false;
		this.waitAfterClick = 0f;
	}

	private void GoToNextLevel()
	{
		GameController.instance.GoToNextLevel();
	}

	private void FireBullet(Vector2 touchPos, bool isWaitingAfterTap = false)
	{
		if (this.state == Gun.State.Searching)
		{
			Enemy enemy = DynamicGrid.instance.FindEnemy();
			if (enemy == null)
			{
				return;
			}
			Enemy component = enemy.GetComponent<Enemy>();
			if (component)
			{
				this.curTarget = component;
				this.state = Gun.State.Aiming;
			}
		}
		if (isWaitingAfterTap)
		{
			if (this.state == Gun.State.Aiming)
			{
				this.FirePerRateWhenTap(touchPos);
			}
			if (this.state == Gun.State.Firing)
			{
				float num = 1f / ((float)this.ReloadSpeed * (float)GameController.instance.factorSpeed + BaseValue.adding_reloadspeed);
				this.cooldownTime = Time.time + num;
				this.state = Gun.State.Waiting;
			}
		}
		else
		{
			if (this.state == Gun.State.Aiming)
			{
				this.FirePerRate(this.curTarget.transform.position);
			}
			if (this.state == Gun.State.Firing)
			{
				float num2 = 1f / ((float)this.ReloadSpeed * (float)GameController.instance.factorSpeed);
				this.cooldownTime = Time.time + num2;
				this.state = Gun.State.Waiting;
			}
		}
		if (this.state == Gun.State.Waiting && Time.time > this.cooldownTime)
		{
			this.state = Gun.State.Searching;
		}
	}

	private void FireBullet(bool isWaitingAfterTap = false)
	{
		if (this.state == Gun.State.Searching)
		{
			Enemy enemy = DynamicGrid.instance.FindEnemy();
			if (enemy == null)
			{
				return;
			}
			Enemy component = enemy.GetComponent<Enemy>();
			if (component)
			{
				this.curTarget = component;
				this.state = Gun.State.Aiming;
			}
		}
		if (isWaitingAfterTap)
		{
			if (this.state == Gun.State.Aiming)
			{
			}
			if (this.state == Gun.State.Firing)
			{
			}
		}
		else
		{
			if (this.state == Gun.State.Aiming)
			{
				float d = DynamicGrid.instance.cellSize.x / 4f;
				this.FirePerRate(this.curTarget.transform.position + Vector3.down * d);
			}
			if (this.state == Gun.State.Firing)
			{
				float num = 1f / ((float)this.ReloadSpeed * (float)GameController.instance.factorSpeed);
				this.cooldownTime = Time.time + num;
				this.state = Gun.State.Waiting;
			}
		}
		if (this.state == Gun.State.Waiting && Time.time > this.cooldownTime)
		{
			this.state = Gun.State.Searching;
		}
	}

	private void TapGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Ended)
		{
			if (Tank.instance.isMoving)
			{
				return;
			}
			if (GameController.instance.isDisableTouch)
			{
				return;
			}
			Vector2 vector = Vector2.zero;
			List<GestureTouch> list = FingersScript.Instance.Touches.ToList<GestureTouch>();
			if (list.Count > 0)
			{
				GestureTouch gestureTouch = list[UnityEngine.Random.Range(0, list.Count)];
				vector = this.GetWorldPoint(gestureTouch.X, gestureTouch.Y);
			}
			if (vector.x < base.transform.position.x + 7f || vector.y < Tank.instance.transform.position.y - 2f)
			{
				return;
			}
			GameObject pooledObject = ParticleTouchObjectPooler.instance.GetPooledObject();
			pooledObject.SetActive(true);
			pooledObject.transform.position = new Vector3(vector.x, vector.y, 0f);
			this.waitAfterClick = 0f;
			this.isWaiting = true;
		}
	}

	public Vector2 GetAveragePos(List<GestureTouch> touches)
	{
		Vector2 zero = Vector2.zero;
		foreach (GestureTouch current in touches)
		{
			Vector2 worldPoint = this.GetWorldPoint(current.X, current.Y);
			zero.x += worldPoint.x;
			zero.y += worldPoint.y;
		}
		if (touches.Count > 0)
		{
			zero.x /= (float)touches.Count<GestureTouch>();
			zero.y /= (float)touches.Count<GestureTouch>();
			return zero;
		}
		return Vector2.zero;
	}

	public Vector2 GetWorldPoint(float x, float y)
	{
		return Camera.main.ScreenToWorldPoint(new Vector2(x, y));
	}

	public GestureTouch GetNearestTouch()
	{
		if (FingersScript.Instance.Touches.Count != 0)
		{
			float num = 100f;
			GestureTouch result = FingersScript.Instance.Touches.ToList<GestureTouch>()[0];
			foreach (GestureTouch current in FingersScript.Instance.Touches)
			{
				float num2 = Vector2.Distance(DynamicGrid.instance.transform.position, Camera.main.ScreenToWorldPoint(new Vector2(current.X, current.Y)));
				if (num2 < num)
				{
					num = num2;
					result = current;
				}
			}
			return result;
		}
		return default(GestureTouch);
	}

	private void CreatePanGesture()
	{
		PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
		panGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Pan_Updated);
		FingersScript.Instance.AddGesture(panGestureRecognizer);
		panGestureRecognizer.MinimumNumberOfTouchesToTrack = 1;
		panGestureRecognizer.MaximumNumberOfTouchesToTrack = 10;
	}

	private void Pan_Updated(GestureRecognizer gesture)
	{
		Vector2 vector = Vector2.zero;
		vector = this.GetWorldPoint(gesture.FocusX, gesture.FocusY);
		if (vector.x < base.transform.position.x + 7f || vector.y < Tank.instance.transform.position.y - 2f)
		{
			return;
		}
		this.waitAfterClick = 0f;
		this.isWaiting = true;
		this.timePerPanning -= Time.deltaTime;
		if (this.timePerPanning <= 0f)
		{
			this.timePerPanning = 0.2f;
			GameObject pooledObject = ParticleTouchObjectPooler.instance.GetPooledObject();
			pooledObject.SetActive(true);
			pooledObject.transform.position = new Vector3(vector.x, vector.y, 0f);
		}
	}

	private void CreateTapGesture()
	{
		TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
		tapGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapGestureCallback);
		tapGestureRecognizer.MinimumNumberOfTouchesToTrack = 1;
		tapGestureRecognizer.MaximumNumberOfTouchesToTrack = 10;
		FingersScript.Instance.AddGesture(tapGestureRecognizer);
	}

	public virtual void Fire(Vector2 touchPos)
	{
		UnityEngine.Debug.Log("base.Fire()");
	}

	public virtual void FirePerRate(Vector3 target)
	{
		UnityEngine.Debug.Log("base.FirePerRate()");
	}

	public virtual void FirePerRateWhenTap(Vector2 touchPos)
	{
		UnityEngine.Debug.Log("base.FirePerRateWhenTap()");
	}

	public virtual void PlayFireAnimation()
	{
		Tank.instance.GetComponent<Animation>().Stop();
		Tank.instance.GetComponent<Animation>().Play("FireAnim");
	}
}
