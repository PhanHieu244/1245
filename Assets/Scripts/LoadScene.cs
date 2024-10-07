using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	private sealed class _LoadingScreen_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal LoadScene _this;

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

		public _LoadingScreen_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.ao = SceneManager.LoadSceneAsync("MainGame");
				this._this.ao.allowSceneActivation = false;
				break;
			case 1u:
				break;
			case 2u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._this.ao.isDone)
			{
				this._current = new WaitForSecondsRealtime(0.5f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
			}
			else
			{
				if (this._this.ao.progress == 0.9f)
				{
					this._this.ao.allowSceneActivation = true;
				}
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
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

	private AsyncOperation ao;

	private void Start()
	{
		QualitySettings.vSyncCount = 0;
		QualitySettings.antiAliasing = 0;
		Application.targetFrameRate = 60;
		base.StartCoroutine(this.LoadingScreen());
	}

	private IEnumerator LoadingScreen()
	{
		LoadScene._LoadingScreen_c__Iterator0 _LoadingScreen_c__Iterator = new LoadScene._LoadingScreen_c__Iterator0();
		_LoadingScreen_c__Iterator._this = this;
		return _LoadingScreen_c__Iterator;
	}
}
