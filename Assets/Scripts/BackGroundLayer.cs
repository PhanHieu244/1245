using System;
using UnityEngine;

[Serializable]
public class BackGroundLayer
{
	[Header("Sprite Settings")]
	public Sprite mySprite;

	public string layerName;

	public int orderInLayer;

	[Header("Parallax Settings")]
	public float parallaxSpeedX;

	public float parallaxSpeedY;

	[Header("Constant move Settings")]
	public float backGroundSpeedX;

	public float backGroundSpeedY;
}
