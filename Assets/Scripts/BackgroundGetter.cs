using System;
using System.Collections.Generic;
using UnityEngine;

public static class BackgroundGetter
{
	public static List<Sprite> GetBackground(BackgroundType backgroundType)
	{
		List<Sprite> list = new List<Sprite>();
		switch (backgroundType)
		{
		case BackgroundType.Plain:
		{
			Sprite sprite = Resources.Load<Sprite>("Textures/Background/DongBang/color");
			Sprite sprite2 = Resources.Load<Sprite>("Textures/Background/DongBang/moutain");
			Sprite sprite3 = Resources.Load<Sprite>("Textures/Background/DongBang/river");
			Sprite sprite4 = Resources.Load<Sprite>("Textures/Background/DongBang/field");
			Sprite sprite5 = Resources.Load<Sprite>("Textures/Background/DongBang/ground");
			if (sprite)
			{
				list.Add(sprite);
			}
			if (sprite2)
			{
				list.Add(sprite2);
			}
			if (sprite3)
			{
				list.Add(sprite3);
			}
			if (sprite4)
			{
				list.Add(sprite4);
			}
			if (sprite5)
			{
				list.Add(sprite5);
			}
			break;
		}
		case BackgroundType.Desert:
		{
			Sprite sprite6 = Resources.Load<Sprite>("Textures/Background/SaMac/color");
			Sprite sprite7 = Resources.Load<Sprite>("Textures/Background/SaMac/kimtuthap");
			Sprite sprite8 = Resources.Load<Sprite>("Textures/Background/SaMac/river");
			Sprite sprite9 = Resources.Load<Sprite>("Textures/Background/SaMac/sand");
			Sprite sprite10 = Resources.Load<Sprite>("Textures/Background/SaMac/ground");
			if (sprite6)
			{
				list.Add(sprite6);
			}
			if (sprite7)
			{
				list.Add(sprite7);
			}
			if (sprite8)
			{
				list.Add(sprite8);
			}
			if (sprite9)
			{
				list.Add(sprite9);
			}
			if (sprite10)
			{
				list.Add(sprite10);
			}
			break;
		}
		case BackgroundType.City:
		{
			Sprite sprite11 = Resources.Load<Sprite>("Textures/Background/ThanhPho/color");
			Sprite sprite12 = Resources.Load<Sprite>("Textures/Background/ThanhPho/city");
			Sprite sprite13 = Resources.Load<Sprite>("Textures/Background/ThanhPho/river");
			Sprite sprite14 = Resources.Load<Sprite>("Textures/Background/ThanhPho/ice");
			Sprite sprite15 = Resources.Load<Sprite>("Textures/Background/ThanhPho/ground");
			if (sprite11)
			{
				list.Add(sprite11);
			}
			if (sprite12)
			{
				list.Add(sprite12);
			}
			if (sprite13)
			{
				list.Add(sprite13);
			}
			if (sprite14)
			{
				list.Add(sprite14);
			}
			if (sprite15)
			{
				list.Add(sprite15);
			}
			break;
		}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list;
	}
}
