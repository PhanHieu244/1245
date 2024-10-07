using System;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
	public static BackGround instance;

	public GameObject player;

	public bool nineImage;

	public List<BackGroundLayer> paramBackGround;

	[HideInInspector]
	public List<GameObject> parallaxBackgroundLayer;

	[HideInInspector]
	public List<GameObject> moveBackGroundLayer;

	public List<GameObject> allCreateSprite;

	[HideInInspector]
	public List<Vector2> bounds;

	[HideInInspector]
	public List<Vector2> starPositionBackGroundLayer;

	[HideInInspector]
	public bool haveBackGround;

	[HideInInspector]
	private int countBounds;

	public bool isFollowTank;

	private void Awake()
	{
		BackGround.instance = this;
	}

	private void Start()
	{
		this.FindTank();
		if (this.haveBackGround)
		{
			for (int i = 0; i < this.parallaxBackgroundLayer.Count; i++)
			{
				this.starPositionBackGroundLayer.Add(this.parallaxBackgroundLayer[i].transform.position);
			}
		}
	}

	private void FindTank()
	{
		if (Tank.instance)
		{
			this.player = Tank.instance.gameObject;
			this.isFollowTank = true;
		}
	}

	public void SetTank(Tank _tank)
	{
		this.player = _tank.gameObject;
		this.isFollowTank = true;
	}

	public void CreateBackGround()
	{
		if (!this.haveBackGround)
		{
			for (int i = 0; i < this.paramBackGround.Count; i++)
			{
				this.CreateParallaxBackGround(i);
				this.CreateMoveBackGround(i, this.parallaxBackgroundLayer[i]);
				for (int j = 0; j < 3; j++)
				{
					if (this.nineImage)
					{
						GameObject gameObject = this.CreateHorizontalParent(j);
						gameObject.transform.parent = this.moveBackGroundLayer[i].transform;
						for (int k = 0; k < 3; k++)
						{
							this.Init(this.moveBackGroundLayer[i], i, k, j).transform.parent = gameObject.transform;
						}
					}
					else
					{
						this.Init(this.moveBackGroundLayer[i], i, j, 0);
					}
				}
			}
			this.haveBackGround = true;
		}
	}

	private void LateUpdate()
	{
		if (this.isFollowTank && this.haveBackGround)
		{
			for (int i = 0; i < this.paramBackGround.Count; i++)
			{
				this.parallaxBackgroundLayer[i].transform.position = new Vector2(this.starPositionBackGroundLayer[i].x + this.player.transform.position.x * this.paramBackGround[i].parallaxSpeedX, this.starPositionBackGroundLayer[i].y + this.player.transform.position.y * this.paramBackGround[i].parallaxSpeedY);
				this.moveBackGroundLayer[i].transform.position = new Vector2(this.moveBackGroundLayer[i].transform.position.x + Time.deltaTime * this.paramBackGround[i].backGroundSpeedX, this.moveBackGroundLayer[i].transform.position.y + Time.deltaTime * this.paramBackGround[i].backGroundSpeedY);
			}
			for (int j = 0; j < this.allCreateSprite.Count; j++)
			{
				this.CheckPosition(this.allCreateSprite[j], j);
			}
		}
	}

	private void CheckPosition(GameObject myObject, int j)
	{
		if (myObject.transform.position.x < this.player.transform.position.x - 1.5f * this.bounds[j].x)
		{
			myObject.transform.position = new Vector2(myObject.transform.position.x + this.bounds[j].x * 3f, myObject.transform.position.y);
		}
		else if (myObject.transform.position.x > this.player.transform.position.x + this.bounds[j].x * 1.5f)
		{
			myObject.transform.position = new Vector2(myObject.transform.position.x - this.bounds[j].x * 3f, myObject.transform.position.y);
		}
		if (this.nineImage)
		{
			if (myObject.transform.position.y < this.player.transform.position.y - 1.5f * this.bounds[j].y)
			{
				myObject.transform.position = new Vector2(myObject.transform.position.x, myObject.transform.position.y + this.bounds[j].y * 3f);
			}
			else if (myObject.transform.position.y > this.player.transform.position.y + this.bounds[j].y * 1.5f)
			{
				myObject.transform.position = new Vector2(myObject.transform.position.x, myObject.transform.position.y - this.bounds[j].y * 3f);
			}
		}
	}

	private GameObject CreateHorizontalParent(int count)
	{
		GameObject gameObject = new GameObject();
		if (count != 0)
		{
			if (count != 1)
			{
				if (count == 2)
				{
					gameObject.name = "Top Horizontal Parent";
				}
			}
			else
			{
				gameObject.name = "Middle Horizontal Parent";
			}
		}
		else
		{
			gameObject.name = "Bottom Horizontal Parent";
		}
		return gameObject;
	}

	private void CreateParallaxBackGround(int i)
	{
		GameObject gameObject = new GameObject();
		this.parallaxBackgroundLayer.Add(gameObject);
		gameObject.transform.parent = base.gameObject.transform;
		gameObject.name = "Parallax BackGround Layer_" + i;
	}

	private void CreateMoveBackGround(int i, GameObject parallaxParent)
	{
		GameObject gameObject = new GameObject();
		this.moveBackGroundLayer.Add(gameObject);
		gameObject.transform.parent = parallaxParent.transform;
		gameObject.name = "Move BackGround Layer_" + i;
	}

	private GameObject Init(GameObject newObject, int i, int j, int y)
	{
		GameObject gameObject = new GameObject();
		this.allCreateSprite.Add(gameObject);
		gameObject.transform.parent = newObject.transform;
		gameObject.AddComponent<SpriteRenderer>().sprite = this.paramBackGround[i].mySprite;
		gameObject.GetComponent<SpriteRenderer>().sortingLayerName = this.paramBackGround[i].layerName;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.paramBackGround[i].orderInLayer;
		Vector2 item = new Vector2(gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x, gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
		this.bounds.Add(item);
		int num = 0;
		if (this.nineImage)
		{
			num = 1;
		}
		this.countBounds++;
		gameObject.transform.position = new Vector2(-this.bounds[this.countBounds - 1].x + this.bounds[this.countBounds - 1].x * (float)j, -this.bounds[this.countBounds - 1].y * (float)num + this.bounds[this.countBounds - 1].y * (float)y);
		gameObject.name = "BackGroundCount";
		return gameObject;
	}

	public void DeleteBackGround()
	{
		for (int i = 0; i < this.parallaxBackgroundLayer.Count; i++)
		{
			UnityEngine.Object.DestroyImmediate(this.parallaxBackgroundLayer[i]);
		}
		this.parallaxBackgroundLayer.Clear();
		this.moveBackGroundLayer.Clear();
		this.allCreateSprite.Clear();
		this.bounds.Clear();
		this.countBounds = 0;
		this.haveBackGround = false;
	}
}
