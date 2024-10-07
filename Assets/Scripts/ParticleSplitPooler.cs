using System;

public class ParticleSplitPooler : ObjectPooler
{
	public static ParticleSplitPooler instance;

	public override void Awake()
	{
		base.Awake();
		ParticleSplitPooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
