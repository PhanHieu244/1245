using System;

public class UpgradeLevelArrowPooler : ObjectPooler
{
	public static UpgradeLevelArrowPooler instance;

	public override void Awake()
	{
		base.Awake();
		UpgradeLevelArrowPooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
