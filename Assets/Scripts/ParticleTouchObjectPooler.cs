using System;

public class ParticleTouchObjectPooler : ObjectPooler
{
	public static ParticleTouchObjectPooler instance;

	public override void Awake()
	{
		base.Awake();
		ParticleTouchObjectPooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
