using System;

public class ParticleMuzzleFlashPooler : ObjectPooler
{
	public static ParticleMuzzleFlashPooler instance;

	public override void Awake()
	{
		base.Awake();
		ParticleMuzzleFlashPooler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
