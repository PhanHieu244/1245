using System;

public class FirePooler : ObjectPooler
{
	public static FirePooler instance;

	public override void Awake()
	{
		base.Awake();
		FirePooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
