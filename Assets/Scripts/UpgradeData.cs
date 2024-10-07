using System;

public class UpgradeData
{
	private int level;

	private double value;

	private long currentPrice;

	public int Level
	{
		get
		{
			return this.level;
		}
		set
		{
			this.level = value;
		}
	}

	public double Value
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	public long CurrentPrice
	{
		get
		{
			return this.currentPrice;
		}
		set
		{
			this.currentPrice = value;
		}
	}

	public UpgradeData()
	{
		this.level = 0;
		this.value = 0.0;
		this.CurrentPrice = 0L;
	}

	public UpgradeData(int _level, double _value, long _currentPrice)
	{
		this.level = _level;
		this.value = _value;
		this.CurrentPrice = _currentPrice;
	}
}
