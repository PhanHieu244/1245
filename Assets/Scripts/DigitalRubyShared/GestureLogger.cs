using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public static class GestureLogger
	{
		public static void Log(this GestureRecognizer gesture, string text, params object[] args)
		{
			UnityEngine.Debug.LogFormat(string.Concat(new object[]
			{
				DateTime.UtcNow,
				" (",
				gesture.ToString(),
				"): ",
				text
			}), args);
		}
	}
}
