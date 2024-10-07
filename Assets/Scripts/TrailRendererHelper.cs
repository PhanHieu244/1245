using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TrailRendererHelper : MonoBehaviour
{
	private sealed class _ResetTrails_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal TrailRendererHelper _this;

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

		public _ResetTrails_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("ResetTrails");
				this._this.mTrail.time = 0f;
				this._current = new WaitForEndOfFrame();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.mTrail.time = this._this.mTime;
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

	protected TrailRenderer mTrail;

	protected float mTime;

	private void Awake()
	{
		this.mTrail = base.gameObject.GetComponent<TrailRenderer>();
		if (null == this.mTrail)
		{
			UnityEngine.Debug.LogError("[TrailRendererHelper.Awake] invalid TrailRenderer.");
			return;
		}
		this.mTime = this.mTrail.time;
	}

	private void OnEnable()
	{
		if (null == this.mTrail)
		{
			return;
		}
		base.StartCoroutine(this.ResetTrails());
	}

	private IEnumerator ResetTrails()
	{
		TrailRendererHelper._ResetTrails_c__Iterator0 _ResetTrails_c__Iterator = new TrailRendererHelper._ResetTrails_c__Iterator0();
		_ResetTrails_c__Iterator._this = this;
		return _ResetTrails_c__Iterator;
	}
}
