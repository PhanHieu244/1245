using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
	private sealed class _Split_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		private sealed class _Split_c__AnonStorey6
		{
			internal GameObject splitEffect;

			internal MissileLauncher._Split_c__Iterator0 __f__ref_0;

			internal void __m__0()
			{
				this.splitEffect.SetActive(false);
			}
		}

		internal GameObject currentItem;

		internal float timeToDelay;

		internal Tweener _anim___0;

		internal GameObject _splitProj___0;

		internal TrailRenderer _trail___0;

		internal MissileLauncher _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private MissileLauncher._Split_c__Iterator0._Split_c__AnonStorey6 _locvar0;

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

		public _Split_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._locvar0 = new MissileLauncher._Split_c__Iterator0._Split_c__AnonStorey6();
				this._locvar0.__f__ref_0 = this;
				this._anim___0 = this.currentItem.transform.DOScale(this.currentItem.transform.localScale, this.timeToDelay);
				this._current = this._anim___0.WaitForCompletion();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				SoundController.instance.PlaySoundTankFire();
				this.currentItem.transform.localScale = Vector3.one * 0.7f;
				this._locvar0.splitEffect = ParticleObjectPooler.instance.GetPooledObject("item2_particle_split");
				this._locvar0.splitEffect.SetActive(true);
				this._locvar0.splitEffect.transform.position = this.currentItem.transform.position;
				this._locvar0.splitEffect.transform.DOScale(this._locvar0.splitEffect.transform.localScale, 0.8f).OnComplete(new TweenCallback(this._locvar0.__m__0));
				this._splitProj___0 = this._this.poolerManager.item2Pooler.GetPooledObject();
				this._splitProj___0.SetActive(true);
				this._trail___0 = this._splitProj___0.GetComponent<TrailRenderer>();
				if (this._trail___0)
				{
					this._trail___0.Clear();
				}
				this._splitProj___0.transform.localScale = Vector3.one * 0.7f;
				this._this.FireItem(this.currentItem, this._splitProj___0, this.currentItem.transform.position, 0.1f, this.timeToDelay * 0.7f);
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

	private sealed class __Split_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		private sealed class __Split_c__AnonStorey7
		{
			internal GameObject splitEffect;

			internal MissileLauncher.__Split_c__Iterator1 __f__ref_1;

			internal void __m__0()
			{
				this.splitEffect.SetActive(false);
			}
		}

		internal GameObject currentItem;

		internal float timeToDelay;

		internal Tweener _anim___0;

		internal MissileLauncher _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private MissileLauncher.__Split_c__Iterator1.__Split_c__AnonStorey7 _locvar0;

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

		public __Split_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._locvar0 = new MissileLauncher.__Split_c__Iterator1.__Split_c__AnonStorey7();
				this._locvar0.__f__ref_1 = this;
				this._anim___0 = this.currentItem.transform.DOScale(this.currentItem.transform.localScale, this.timeToDelay);
				this._current = this._anim___0.WaitForCompletion();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				SoundController.instance.PlaySoundTankFire();
				this.currentItem.transform.localScale = Vector3.one * 0.7f;
				this._locvar0.splitEffect = ParticleObjectPooler.instance.GetPooledObject("item2_particle_split");
				this._locvar0.splitEffect.SetActive(true);
				this._locvar0.splitEffect.transform.position = this.currentItem.transform.position;
				this._locvar0.splitEffect.transform.DOScale(this._locvar0.splitEffect.transform.localScale, 0.8f).OnComplete(new TweenCallback(this._locvar0.__m__0));
				for (int i = 1; i <= 4; i++)
				{
					if (i != 2)
					{
						this._this.DrawItem2Bullet(this.currentItem, this.timeToDelay, (float)i);
					}
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

	private sealed class _Split2_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		private sealed class _Split2_c__AnonStorey8
		{
			internal GameObject splitEffect;

			internal MissileLauncher._Split2_c__Iterator2 __f__ref_2;

			internal void __m__0()
			{
				this.splitEffect.SetActive(false);
			}
		}

		internal GameObject currentItem;

		internal float timeToDelay;

		internal Tweener _anim___0;

		internal MissileLauncher _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private MissileLauncher._Split2_c__Iterator2._Split2_c__AnonStorey8 _locvar0;

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

		public _Split2_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._locvar0 = new MissileLauncher._Split2_c__Iterator2._Split2_c__AnonStorey8();
				this._locvar0.__f__ref_2 = this;
				this._anim___0 = this.currentItem.transform.DOScale(this.currentItem.transform.localScale, this.timeToDelay);
				this._current = this._anim___0.WaitForCompletion();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				SoundController.instance.PlaySoundTankFire();
				this._locvar0.splitEffect = ParticleObjectPooler.instance.GetPooledObject("item2_particle_split");
				this._locvar0.splitEffect.SetActive(true);
				this._locvar0.splitEffect.transform.position = this.currentItem.transform.position;
				this._locvar0.splitEffect.transform.DOScale(this._locvar0.splitEffect.transform.localScale, 0.8f).OnComplete(new TweenCallback(this._locvar0.__m__0));
				this._this.SpawnNewProjectile(this.currentItem, 1.5f);
				this._this.SpawnNewProjectile(this.currentItem, -1.5f);
				this.currentItem.SetActive(false);
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

	private sealed class _DropBomb_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal GameObject _newBullet___2;

		internal TrailRenderer _trail___2;

		internal Vector3 position;

		internal Rigidbody2D _rigid___2;

		internal MissileLauncher _this;

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

		public _DropBomb_c__Iterator3()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 5)
			{
				SoundController.instance.PlaySoundUseItem3();
				this._newBullet___2 = this._this.poolerManager.item3Pooler.GetPooledObject();
				this._newBullet___2.SetActive(true);
				this._trail___2 = this._newBullet___2.GetComponent<TrailRenderer>();
				if (this._trail___2)
				{
					this._trail___2.Clear();
				}
				this._newBullet___2.transform.position = new Vector3(this.position.x + DynamicGrid.instance.cellSize.x * (float)this._i___1 - DynamicGrid.instance.cellSize.x * 2f, this._this.topPosition.transform.position.y + 0.6f * (float)this._i___1);
				this._newBullet___2.transform.eulerAngles = Vector3.forward * -90f;
				this._rigid___2 = this._newBullet___2.GetComponent<Rigidbody2D>();
				this._rigid___2.gravityScale = 2f;
				this._current = new WaitForSeconds(0.1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
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

	private sealed class _Napalm_c__Iterator4 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _randomValue___0;

		internal Vector3 muzzlePos;

		internal int _i___1;

		internal Vector3 curTargetPos;

		internal float _xdistance___2;

		internal float _ydistance___2;

		internal float _throwAngle___2;

		internal float _totalVelo___2;

		internal float _xVelo___2;

		internal float _yVelo___2;

		internal GameObject _newBullet___2;

		internal Rigidbody2D _rigid___2;

		internal MissileLauncher _this;

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

		public _Napalm_c__Iterator4()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._randomValue___0 = UnityEngine.Random.Range(this._this.randomDistance / 3f, this._this.randomDistance);
				UnityEngine.Debug.Log(this._randomValue___0);
				UnityEngine.Debug.Log(this._this.randomDistance);
				this.muzzlePos.y = this.muzzlePos.y + this._randomValue___0;
				UnityEngine.Debug.Log("Item 6 Fired");
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 7)
			{
				SoundController.instance.PlaySoundUseItem123();
				this._xdistance___2 = this.curTargetPos.x - this.muzzlePos.x;
				this._ydistance___2 = this.curTargetPos.y - this.muzzlePos.y;
				this._throwAngle___2 = Mathf.Atan((this._ydistance___2 + 4.905f) / this._xdistance___2);
				this._this.transform.eulerAngles = new Vector3(0f, 0f, this._throwAngle___2 * 57.2957764f);
				this._totalVelo___2 = this._xdistance___2 / (Mathf.Cos(this._throwAngle___2) * 1f);
				this._xVelo___2 = this._totalVelo___2 * Mathf.Cos(this._throwAngle___2);
				this._yVelo___2 = this._totalVelo___2 * Mathf.Sin(this._throwAngle___2);
				this._newBullet___2 = this._this.poolerManager.item4Pooler.GetPooledObject();
				this._newBullet___2.SetActive(true);
				this._newBullet___2.transform.position = this.muzzlePos;
				this._newBullet___2.transform.eulerAngles = Vector3.zero;
				this._rigid___2 = this._newBullet___2.GetComponent<Rigidbody2D>();
				this._rigid___2.velocity = new Vector2(this._xVelo___2, this._yVelo___2) + new Vector2(0f, 4f * UnityEngine.Random.Range(-0.5f, 0.5f));
				this._current = new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.2f));
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
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

	private sealed class _LaunchItem5_c__Iterator5 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ObjectPooler _item5Pooler___0;

		internal int _i___1;

		internal Vector3 curTargetPos;

		internal Vector2 _direction___2;

		internal float _angle___2;

		internal Vector2 _velocity___2;

		internal MissileLauncher _this;

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

		public _LaunchItem5_c__Iterator5()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._item5Pooler___0 = this._this.poolerManager.item5Pooler;
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this.listItem5Pos.Count)
			{
				SoundController.instance.PlaySoundItem5Launch();
				this._direction___2 = this.curTargetPos - this._this.listItem5Pos[this._i___1].transform.position;
				this._angle___2 = Mathf.Atan2(this._direction___2.y, this._direction___2.x) * 57.29578f;
				this._velocity___2 = this._this.CalculateVeloCity(this.curTargetPos, this._this.listItem5Pos[this._i___1].transform.position, 20f);
				this._this.DrawItem5(this._item5Pooler___0, this.curTargetPos, this._velocity___2, new Vector3(Mathf.Tan(this._angle___2) * 0.3f, 0.3f), this._i___1);
				this._this.DrawItem5(this._item5Pooler___0, this.curTargetPos, this._velocity___2, new Vector3(Mathf.Tan(this._angle___2) * -0.3f, -0.3f), this._i___1);
				this._current = new WaitForSeconds(0.2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
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

	private sealed class _FireItem7_c__AnonStorey9
	{
		internal Item7Projectile proj;

		internal GameObject item7;

		internal void __m__0()
		{
			this.proj.StartShot();
			SoundController.instance.PlaySoundItem7Firing();
		}

		internal void __m__1()
		{
			this.proj.StopShot();
			this.item7.SetActive(false);
			SoundController.instance.StopInvokeSoundItem7Firing();
		}
	}

	[SerializeField]
	private Sprite spriteItem5;

	public static MissileLauncher instance;

	public PoolerManager poolerManager;

	protected float speed;

	protected int damage;

	public GameObject originPos;

	public GameObject topPosition;

	public GameObject lowerPosition;

	public List<Transform> listItem5Pos;

	private float randomDistance;

	private void Awake()
	{
		MissileLauncher.instance = this;
		this.randomDistance = Mathf.Abs(this.lowerPosition.transform.position.y - base.transform.position.y);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void FireItem1(Vector3 curTargetPos, Vector3 muzzlePos, Vector3 mouseposition, float projSpeed, float gravity, float _angle = 30f)
	{
		float num = UnityEngine.Random.Range(0f, this.randomDistance);
		muzzlePos.y += num;
		float num2 = 2f;
		Vector3 v = this.CalculateVelocity(curTargetPos, muzzlePos, num2 - num2 / 2f * num / this.randomDistance);
		float angle = Mathf.Atan2(v.y, v.x) * 57.29578f;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		base.transform.rotation = rotation;
		GameObject pooledObject = this.poolerManager.item1Pooler.GetPooledObject();
		pooledObject.SetActive(true);
		TrailRenderer component = pooledObject.GetComponent<TrailRenderer>();
		if (component)
		{
			component.Clear();
		}
		pooledObject.transform.position = muzzlePos;
		pooledObject.transform.rotation = base.transform.rotation;
		Rigidbody2D component2 = pooledObject.GetComponent<Rigidbody2D>();
		component2.velocity = v;
	}

	public void FireItem2(Vector3 curTargetPos, Vector3 muzzlePos, Vector3 mouseposition, float projSpeed, float gravity, float _angle = 60f)
	{
		curTargetPos.y -= 2f;
		float num = UnityEngine.Random.Range(0f, this.randomDistance / 2f);
		muzzlePos.y += num;
		float time = 2f;
		Vector2 a = this.CalculateVelocity(curTargetPos, muzzlePos, time);
		float angle = Mathf.Atan2(a.y, a.x) * 57.29578f;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		base.transform.rotation = rotation;
		GameObject pooledObject = this.poolerManager.item2Pooler.GetPooledObject();
		pooledObject.SetActive(true);
		TrailRenderer component = pooledObject.GetComponent<TrailRenderer>();
		if (component)
		{
			component.Clear();
		}
		pooledObject.transform.position = muzzlePos;
		pooledObject.transform.rotation = base.transform.rotation;
		Rigidbody2D component2 = pooledObject.GetComponent<Rigidbody2D>();
		component2.velocity = a + Vector2.up * 2.3f;
		base.StartCoroutine(this.Split(pooledObject, curTargetPos, 0.7f));
	}

	public void FireItem3(Vector3 curTargetPos, Vector3 muzzlePos, Vector3 mouseposition, float projSpeed, float gravity, float _angle = 30f)
	{
		base.StartCoroutine(this.DropBomb(DynamicGrid.instance.transform.position));
	}

	public void FireItem4(Vector3 curTargetPos, Vector3 muzzlePos, Vector3 mouseposition, float projSpeed, float gravity, float _angle = 30f)
	{
		base.StartCoroutine(this.Napalm(curTargetPos, muzzlePos));
	}

	public IEnumerator Split(GameObject currentItem, Vector3 targetPos, float timeToDelay)
	{
		MissileLauncher._Split_c__Iterator0 _Split_c__Iterator = new MissileLauncher._Split_c__Iterator0();
		_Split_c__Iterator.currentItem = currentItem;
		_Split_c__Iterator.timeToDelay = timeToDelay;
		_Split_c__Iterator._this = this;
		return _Split_c__Iterator;
	}

	public IEnumerator _Split(GameObject currentItem, Vector3 targetPos, float timeToDelay)
	{
		MissileLauncher.__Split_c__Iterator1 __Split_c__Iterator = new MissileLauncher.__Split_c__Iterator1();
		__Split_c__Iterator.currentItem = currentItem;
		__Split_c__Iterator.timeToDelay = timeToDelay;
		__Split_c__Iterator._this = this;
		return __Split_c__Iterator;
	}

	public void DrawItem2Bullet(GameObject currentItem, float timeToDelay, float index)
	{
		GameObject pooledObject = this.poolerManager.item2Pooler.GetPooledObject();
		pooledObject.SetActive(true);
		TrailRenderer component = pooledObject.GetComponent<TrailRenderer>();
		if (component)
		{
			component.Clear();
		}
		pooledObject.transform.localScale = Vector3.one * 0.7f;
		this.FireItem(currentItem, pooledObject, currentItem.transform.position, index, timeToDelay * 0.7f);
	}

	public IEnumerator Split2(GameObject currentItem, float index, float timeToDelay)
	{
		MissileLauncher._Split2_c__Iterator2 _Split2_c__Iterator = new MissileLauncher._Split2_c__Iterator2();
		_Split2_c__Iterator.currentItem = currentItem;
		_Split2_c__Iterator.timeToDelay = timeToDelay;
		_Split2_c__Iterator._this = this;
		return _Split2_c__Iterator;
	}

	public void SpawnNewProjectile(GameObject currentItem, float index)
	{
		GameObject pooledObject = this.poolerManager.item2SmallPooler.GetPooledObject();
		pooledObject.SetActive(true);
		TrailRenderer component = pooledObject.GetComponent<TrailRenderer>();
		if (component)
		{
			component.Clear();
		}
		pooledObject.transform.position = currentItem.transform.position;
		Rigidbody2D component2 = pooledObject.GetComponent<Rigidbody2D>();
		component2.velocity = currentItem.GetComponent<Rigidbody2D>().velocity + Vector2.up * index;
	}

	public void FireItem(GameObject currentItem, GameObject newBullet, Vector3 muzzlePos, float index, float timeToDelay)
	{
		newBullet.transform.position = muzzlePos;
		newBullet.transform.eulerAngles = currentItem.transform.eulerAngles + new Vector3(0f, 0f, index * 5f - 10f);
		Rigidbody2D component = newBullet.GetComponent<Rigidbody2D>();
		component.velocity = currentItem.GetComponent<Rigidbody2D>().velocity + Vector2.up * -2.3f;
		base.StartCoroutine(this.Split2(newBullet, 0f, timeToDelay));
		base.StartCoroutine(this.Split2(currentItem, 0.2f, timeToDelay));
	}

	private IEnumerator DropBomb(Vector3 position)
	{
		MissileLauncher._DropBomb_c__Iterator3 _DropBomb_c__Iterator = new MissileLauncher._DropBomb_c__Iterator3();
		_DropBomb_c__Iterator.position = position;
		_DropBomb_c__Iterator._this = this;
		return _DropBomb_c__Iterator;
	}

	private IEnumerator Napalm(Vector3 curTargetPos, Vector3 muzzlePos)
	{
		MissileLauncher._Napalm_c__Iterator4 _Napalm_c__Iterator = new MissileLauncher._Napalm_c__Iterator4();
		_Napalm_c__Iterator.muzzlePos = muzzlePos;
		_Napalm_c__Iterator.curTargetPos = curTargetPos;
		_Napalm_c__Iterator._this = this;
		return _Napalm_c__Iterator;
	}

	public Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
	{
		Vector3 vector = target - origin;
		Vector3 vector2 = vector;
		vector2.y = 0f;
		float y = vector.y;
		float magnitude = vector2.magnitude;
		float d = magnitude / time;
		float y2 = y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
		Vector3 vector3 = vector2.normalized;
		vector3 *= d;
		vector3.y = y2;
		return vector3;
	}

	public Vector2 CalculateVeloCity(Vector3 target, Vector3 origin, float speed)
	{
		Vector2 a = target - origin;
		a.Normalize();
		return a * speed;
	}

	public void FireItem5(Vector3 curTargetPos)
	{
		base.StartCoroutine(this.LaunchItem5(curTargetPos));
	}

	private IEnumerator LaunchItem5(Vector3 curTargetPos)
	{
		MissileLauncher._LaunchItem5_c__Iterator5 _LaunchItem5_c__Iterator = new MissileLauncher._LaunchItem5_c__Iterator5();
		_LaunchItem5_c__Iterator.curTargetPos = curTargetPos;
		_LaunchItem5_c__Iterator._this = this;
		return _LaunchItem5_c__Iterator;
	}

	public void DrawItem5(ObjectPooler item5Pooler, Vector3 curTargetPos, Vector2 velocity, Vector3 offset, int i)
	{
		GameObject pooledObject = item5Pooler.GetPooledObject();
		pooledObject.SetActive(true);
		Vector3 position = this.listItem5Pos[i].transform.position + offset;
		position.z = -6f;
		pooledObject.transform.position = position;
		Item5Projectile component = pooledObject.GetComponent<Item5Projectile>();
		SpriteRenderer renderer = component.GetRenderer();
		if (renderer)
		{
			renderer.sprite = this.spriteItem5;
		}
		Rigidbody2D rigidbody = component.GetRigidbody();
		if (rigidbody)
		{
			rigidbody.velocity = velocity;
		}
	}

	public void FireItem5()
	{
		this.FireItem5(DynamicGrid.instance.transform.position);
	}

	public void FireItem6(Vector3 curTargetPos)
	{
		SoundController.instance.PlaySoundItem6Launch();
		GameObject pooledObject = this.poolerManager.item6Pooler.GetPooledObject();
		pooledObject.SetActive(true);
		Vector3 vector = this.topPosition.transform.position + Vector3.right * (float)UnityEngine.Random.Range(-10, 0);
		vector.z = -6f;
		pooledObject.transform.position = vector;
		Vector3 v = this.CalculateVelocity(curTargetPos, vector, 2f);
		pooledObject.GetComponent<BallisticMotion>().Initialize(vector, Physics2D.gravity.y);
		pooledObject.GetComponent<Rigidbody2D>().velocity = v;
	}

	public void FireItem6()
	{
		this.FireItem6(DynamicGrid.instance.transform.position);
	}

	public void FireItem7(Vector3 target)
	{
		GameObject item7 = this.poolerManager.item7Pooler.GetPooledObject();
		item7.SetActive(true);
		Item7Projectile proj = item7.GetComponent<Item7Projectile>();
		Vector3 position = this.topPosition.transform.position;
		position.z = -6f;
		position.x = DynamicGrid.instance.transform.position.x - 8f;
		item7.transform.position = position;
		Sequence s = DOTween.Sequence();
		Tweener t = item7.transform.DOMoveY(position.y - 10f, 1f, false).SetEase(Ease.OutCirc).OnComplete(delegate
		{
			proj.StartShot();
			SoundController.instance.PlaySoundItem7Firing();
		});
		s.Append(t);
		Tweener t2 = item7.transform.DOMoveX(item7.transform.position.x + 16f, 3f, false).OnComplete(delegate
		{
			proj.StopShot();
			item7.SetActive(false);
			SoundController.instance.StopInvokeSoundItem7Firing();
		});
		s.Append(t2);
	}

	public void FireItem7()
	{
		this.FireItem7(base.transform.position);
	}

	public void FireItem8(Vector3 curTargetPos)
	{
		SoundController.instance.PlaySoundItem6Launch();
		GameObject pooledObject = this.poolerManager.item8Pooler.GetPooledObject();
		pooledObject.SetActive(true);
		Vector3 vector = this.topPosition.transform.position + Vector3.right * (float)UnityEngine.Random.Range(-10, 0);
		vector.z = -6f;
		pooledObject.transform.position = vector;
		Vector3 v = this.CalculateVelocity(curTargetPos, vector, 2f);
		pooledObject.GetComponent<BallisticMotion>().Initialize(vector, Physics2D.gravity.y);
		pooledObject.GetComponent<Rigidbody2D>().velocity = v;
	}

	public void FireItem8()
	{
		this.FireItem8(DynamicGrid.instance.transform.position);
	}
}
