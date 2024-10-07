using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CanvasItem : MonoBehaviour
{
	private sealed class _CallUseItem_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal List<ButtonItem>.Enumerator _locvar0;

		internal ButtonItem _btnItem___1;

		internal CanvasItem _this;

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

		public _CallUseItem_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			bool flag = false;
			switch (num)
			{
			case 0u:
				this._locvar0 = this._this.listBtnItem.GetEnumerator();
				num = 4294967293u;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			try
			{
				switch (num)
				{
				}
				if (this._locvar0.MoveNext())
				{
					this._btnItem___1 = this._locvar0.Current;
					this._btnItem___1.autoUse = true;
					this._current = new WaitForSeconds(1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					flag = true;
					return true;
				}
			}
			finally
			{
				if (!flag)
				{
					((IDisposable)this._locvar0).Dispose();
				}
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			uint num = (uint)this._PC;
			this._disposing = true;
			this._PC = -1;
			switch (num)
			{
			case 1u:
				try
				{
				}
				finally
				{
					((IDisposable)this._locvar0).Dispose();
				}
				break;
			}
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static CanvasItem instance;

	public MissileLauncher missile;

	[SerializeField]
	public GameObject mainPanel;

	[SerializeField]
	private List<ButtonItem> listBtnItem;

	private List<int> levelUnlockData;

	public bool isAutoUse;

	private void Awake()
	{
		CanvasItem.instance = this;
	}

	private IEnumerator CallUseItem()
	{
		CanvasItem._CallUseItem_c__Iterator0 _CallUseItem_c__Iterator = new CanvasItem._CallUseItem_c__Iterator0();
		_CallUseItem_c__Iterator._this = this;
		return _CallUseItem_c__Iterator;
	}

	public void StartAutoUseItem()
	{
		base.StartCoroutine(this.CallUseItem());
	}

	public void ButtonItem1()
	{
		this.listBtnItem[0].ItemPressed(this.missile);
	}

	public void ButtonItem2()
	{
		this.listBtnItem[1].ItemPressed(this.missile);
	}

	public void ButtonItem3()
	{
		this.listBtnItem[2].ItemPressed(this.missile);
	}

	public void ButtonItem4()
	{
		this.listBtnItem[5].ItemPressed(this.missile);
	}

	public void ButtonItem5()
	{
		this.listBtnItem[4].ItemPressed(this.missile);
	}

	public void ButtonItem6()
	{
		this.listBtnItem[3].ItemPressed(this.missile);
	}

	public void ButtonItem7()
	{
		this.listBtnItem[6].ItemPressed(this.missile);
	}

	internal void ReduceRemainingTime()
	{
		foreach (ButtonItem current in this.listBtnItem)
		{
			if (current.remainingTime > 0.0)
			{
				current.remainingTime /= 2.0;
			}
		}
	}

	public void LoadItemData()
	{
		foreach (ButtonItem current in this.listBtnItem)
		{
			current.LoadItemData();
		}
	}

	public void SaveItemData()
	{
		foreach (ButtonItem current in this.listBtnItem)
		{
			current.SaveRemainingTime();
		}
	}

	public void ResetItemDate()
	{
		foreach (ButtonItem current in this.listBtnItem)
		{
			current.ResetItemData();
		}
	}

	public void LoadLevelUnlockData()
	{
		this.levelUnlockData = BaseValue.level_unlock.ToList<int>();
	}

	public void CheckUnlockItem(int level)
	{
		this.LoadLevelUnlockData();
		int num = 0;
		UnityEngine.Debug.Log(this.listBtnItem.Count);
		if (level >= this.levelUnlockData[0] && level < this.levelUnlockData[1])
		{
			num = 1;
		}
		if (level >= this.levelUnlockData[1] && level < this.levelUnlockData[2])
		{
			num = 2;
		}
		if (level >= this.levelUnlockData[2] && level < this.levelUnlockData[3])
		{
			num = 3;
		}
		if (level >= this.levelUnlockData[3] && level < this.levelUnlockData[4])
		{
			num = 4;
		}
		if (level >= this.levelUnlockData[4] && level < this.levelUnlockData[5])
		{
			num = 5;
		}
		if (level >= this.levelUnlockData[5] && level < this.levelUnlockData[6])
		{
			num = 6;
		}
		if (level >= this.levelUnlockData[6])
		{
			num = 7;
		}
		for (int i = 0; i < this.listBtnItem.Count; i++)
		{
			if (i < num)
			{
				this.listBtnItem[i].UnlockItem();
			}
			else
			{
				this.listBtnItem[i].LockItem(this.levelUnlockData[i]);
			}
		}
		if (num > 0 && CanvasUpgrade.instance && CanvasUpgrade.instance.buttonBoosterCooldown.IsBoosterUsed)
		{
			this.ShowCooldownEffect();
		}
	}

	public int GetNumItemUnlocked()
	{
		int num = 0;
		for (int i = 0; i < this.listBtnItem.Count; i++)
		{
			if (this.listBtnItem[i].isUnlocked)
			{
				num++;
			}
		}
		return num;
	}

	public void ShowCooldownEffect()
	{
		UnityEngine.Debug.Log("ShowCooldownEffect");
		foreach (ButtonItem current in this.listBtnItem)
		{
			if (current.isUnlocked)
			{
				current.ShowCooldownEffect();
			}
		}
	}

	public void HideCooldownEffect()
	{
		UnityEngine.Debug.Log("HideCooldownEffect");
		foreach (ButtonItem current in this.listBtnItem)
		{
			current.HideCooldownEffect();
		}
	}

	public List<ButtonItem> GetListBtnItem()
	{
		return this.listBtnItem;
	}
}
