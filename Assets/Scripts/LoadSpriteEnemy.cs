using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadSpriteEnemy : MonoBehaviour
{
	public static LoadSpriteEnemy instance;

	public List<Sprite> listEnemySprite;

	public List<Sprite> listTopEnemySprite;

	private void Awake()
	{
		LoadSpriteEnemy.instance = this;
		UnityEngine.Object.DontDestroyOnLoad(this);
		this.LoadListEnemySprites();
		this.LoadListTopEnemySprites();
	}

	public void GetEnemySprites(out List<Sprite> enemySprites, out List<Sprite> topEnemySprites)
	{
		if (this.listEnemySprite.Count != 0)
		{
			enemySprites = this.listEnemySprite;
		}
		else
		{
			this.LoadListEnemySprites();
			enemySprites = this.listEnemySprite;
		}
		if (this.listTopEnemySprite.Count != 0)
		{
			topEnemySprites = this.listTopEnemySprite;
		}
		else
		{
			this.LoadListTopEnemySprites();
			topEnemySprites = this.listTopEnemySprite;
		}
	}

	private void LoadListEnemySprites()
	{
		this.listEnemySprite = new List<Sprite>();
		for (int i = 1; i <= 16; i++)
		{
			Sprite sprite = Utilities.LoadImageFrom(this.GetPath(i, false));
			if (sprite)
			{
				this.listEnemySprite.Add(sprite);
			}
		}
	}

	private void LoadListTopEnemySprites()
	{
		this.listTopEnemySprite = new List<Sprite>();
		for (int i = 1; i <= 16; i++)
		{
			Sprite sprite = Utilities.LoadImageFrom(this.GetPath(i, true));
			if (sprite)
			{
				this.listTopEnemySprite.Add(sprite);
			}
		}
	}

	private string GetPath(int number, bool isTop = false)
	{
		if (isTop)
		{
			return string.Format("Textures/TopEnemyColor/{0}a", number);
		}
		return string.Format("Textures/EnemyColor/{0}", number);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
