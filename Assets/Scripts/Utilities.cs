using System;
using System.Globalization;
using UnityEngine;

public class Utilities
{
	private static string[] levelColors = new string[]
	{
		"#15b2ff",
		"#8a78f0",
		"#55ff82",
		"#30deff",
		"#f82c5d",
		"#f7c100",
		"#94e0fd",
		"#84c66e",
		"#e25ba0",
		"#0de67b",
		"#fd9139"
	};

	public static string getStringFromNumber(double number, bool nonFormat = true)
	{
		long num = 1000000000000000000L;
		int i = 0;
		double num2 = 0.0;
		while (i < 7)
		{
			num2 = number * 1.0 / (double)num;
			if (num2 >= 1.0)
			{
				break;
			}
			num /= 1000L;
			i++;
		}
		string str = string.Empty;
		if (i == 0)
		{
			str = "Q";
		}
		else if (i == 1)
		{
			str = "q";
		}
		else if (i == 2)
		{
			str = "T";
		}
		else if (i == 3)
		{
			str = "B";
		}
		else if (i == 4)
		{
			str = "M";
		}
		else if (i == 5)
		{
			if (number < 10000.0)
			{
				return number.ToString();
			}
			str = "K";
		}
		else
		{
			if (nonFormat)
			{
				return num2.ToString("F").ToString();
			}
			return num2.ToString("0.##").ToString();
		}
		return num2.ToString("F") + str;
	}

	public static string getStringTimeFromSeconds(float seconds)
	{
		double num = (double)(seconds / 60f);
		if (num > 1.0)
		{
			return num.ToString("F") + " MINS";
		}
		return seconds.ToString("F") + " SECONDS";
	}

	public static Color getLevelColor(int index)
	{
		int num = index % Utilities.levelColors.Length;
		return Utilities.hexToColor(Utilities.levelColors[num]);
	}

	public static Color hexToColor(string hex)
	{
		hex = hex.Replace("0x", string.Empty);
		hex = hex.Replace("#", string.Empty);
		byte a = 255;
		byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		if (hex.Length == 8)
		{
			a = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		}
		return new Color32(r, g, b, a);
	}

	public static string GetFormattedTime(double totalSeconds)
	{
		int num = (int)totalSeconds % 60;
		int num2 = (int)totalSeconds / 60;
		return ((num2 >= 10) ? num2.ToString() : ("0" + num2.ToString())) + ":" + ((num >= 10) ? num.ToString() : ("0" + num.ToString()));
	}

	public static string LoadResourceTextfile(string path)
	{
		UnityEngine.Debug.Log("Utilities.LoadResourceTextfile()");
		string path2 = path.Replace(".json", string.Empty);
		TextAsset textAsset = Resources.Load<TextAsset>(path2);
		UnityEngine.Debug.Log(textAsset.text);
		return textAsset.text;
	}

	public static Sprite LoadImageFrom(string path)
	{
		Sprite sprite = Resources.Load<Sprite>(path);
		if (sprite)
		{
			return sprite;
		}
		return null;
	}
}
