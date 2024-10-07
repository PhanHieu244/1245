using System;

public class CoinObjectPooler : ObjectPooler
{
	public static CoinObjectPooler instance;

	public override void Awake()
	{
		base.Awake();
		CoinObjectPooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
