using System;

public class FireDropPooler : ObjectPooler
{
	public static FireDropPooler instance;

	public override void Awake()
	{
		base.Awake();
		FireDropPooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
