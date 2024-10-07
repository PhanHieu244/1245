using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarController : MonoBehaviour
{
	private sealed class _AnimateProgressbar_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal double value;

		internal ProgressbarController _this;

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

		public _AnimateProgressbar_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if ((double)this._this.foregroundProgressbar.fillAmount < this.value)
			{
				this._this.foregroundProgressbar.fillAmount += Time.deltaTime;
				this._current = new WaitForSeconds(Time.deltaTime);
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

	public static ProgressbarController instance;

	public Image foregroundProgressbar;

	public void SetProgressBarValue(double value)
	{
		this.foregroundProgressbar.fillAmount = (float)value;
	}

	public void InitValue()
	{
		this.SetProgressBarValue(0.0);
	}

	private void Awake()
	{
		ProgressbarController.instance = this;
		this.InitValue();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public IEnumerator AnimateProgressbar(double value)
	{
		ProgressbarController._AnimateProgressbar_c__Iterator0 _AnimateProgressbar_c__Iterator = new ProgressbarController._AnimateProgressbar_c__Iterator0();
		_AnimateProgressbar_c__Iterator.value = value;
		_AnimateProgressbar_c__Iterator._this = this;
		return _AnimateProgressbar_c__Iterator;
	}
}
