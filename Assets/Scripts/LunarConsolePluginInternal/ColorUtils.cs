using System;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	internal static class ColorUtils
	{
		private const float kMultiplier = 0.003921569f;

		public static Color FromRGBA(uint value)
		{
			float r = (value >> 24 & 255u) * 0.003921569f;
			float g = (value >> 16 & 255u) * 0.003921569f;
			float b = (value >> 8 & 255u) * 0.003921569f;
			float a = (value & 255u) * 0.003921569f;
			return new Color(r, g, b, a);
		}

		public static Color FromRGB(uint value)
		{
			float r = (value >> 16 & 255u) * 0.003921569f;
			float g = (value >> 8 & 255u) * 0.003921569f;
			float b = (value & 255u) * 0.003921569f;
			float a = 1f;
			return new Color(r, g, b, a);
		}

		public static uint ToRGBA(ref Color value)
		{
			uint num = (uint)(value.r * 255f) & 255u;
			uint num2 = (uint)(value.g * 255f) & 255u;
			uint num3 = (uint)(value.b * 255f) & 255u;
			uint num4 = (uint)(value.a * 255f) & 255u;
			return num << 24 | num2 << 16 | num3 << 8 | num4;
		}
	}
}
