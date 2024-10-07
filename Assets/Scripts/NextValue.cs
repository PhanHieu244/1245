using System;

public class NextValue
{
	public double nextCost;

	public double nextValue;

	public NextValue()
	{
		this.nextCost = 0.0;
		this.nextValue = 0.0;
	}

	public NextValue(double value, double cost)
	{
		this.nextCost = cost;
		this.nextValue = value;
	}

	public NextValue(NextValue value)
	{
		this.nextCost = value.nextCost;
		this.nextValue = value.nextValue;
	}
}
