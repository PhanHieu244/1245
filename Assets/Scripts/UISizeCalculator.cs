using System;
using System.Collections.Generic;
using UnityEngine;

public class UISizeCalculator
{
	public static bool IsClickOnUI(GameObject panel, Vector3 pos)
	{
		List<Vector2> size = UISizeCalculator.GetSize(panel);
		Vector2 vector = size[0];
		Vector2 vector2 = size[1];
		return pos.x < vector.x || pos.x > vector2.x || pos.y < vector.y || pos.y > vector2.y;
	}

	public static List<Vector2> GetSize(GameObject panel)
	{
		RectTransform component = panel.GetComponent<RectTransform>();
		Vector2 pivot = component.pivot;
		Vector2 sizeDelta = component.sizeDelta;
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		if (pivot.x == 0f)
		{
			zero.x = component.position.x;
			zero2.x = component.position.x + sizeDelta.x;
		}
		else if (pivot.x == 0.5f)
		{
			zero.x = component.position.x - sizeDelta.x / 2f;
			zero2.x = component.position.x + sizeDelta.x / 2f;
		}
		else
		{
			zero.x = component.position.x - sizeDelta.x;
			zero2.x = component.position.x;
		}
		if (pivot.y == 0f)
		{
			zero.y = component.position.y;
			zero2.y = component.position.y + sizeDelta.y;
		}
		else if (pivot.y == 0.5f)
		{
			UnityEngine.Debug.Log("a");
			zero.y = component.position.y - sizeDelta.y / 2f;
			zero2.y = component.position.y + sizeDelta.y / 2f;
		}
		else
		{
			zero.y = component.position.y - sizeDelta.y;
			zero2.y = component.position.y;
		}
		UnityEngine.Debug.Log(component.localPosition);
		UnityEngine.Debug.Log(component.sizeDelta);
		UnityEngine.Debug.Log(zero);
		UnityEngine.Debug.Log(zero2);
		return new List<Vector2>
		{
			zero,
			zero2
		};
	}

	public static Vector2 GetMiddleRightPos()
	{
		return new Vector2(Camera.main.transform.position.x, 0f) + UISizeCalculator.ScreenToWorldPoint(new Vector2((float)Screen.width * 0.5f, 0f));
	}

	public static Vector2 ScreenToWorldPoint(Vector2 screenPoint)
	{
		return Camera.main.ScreenToWorldPoint(screenPoint);
	}
}
