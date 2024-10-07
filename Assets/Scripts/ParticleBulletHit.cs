using System;

public class ParticleBulletHit : ObjectPooler
{
	public static ParticleBulletHit instance;

	public override void Awake()
	{
		base.Awake();
		ParticleBulletHit.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
