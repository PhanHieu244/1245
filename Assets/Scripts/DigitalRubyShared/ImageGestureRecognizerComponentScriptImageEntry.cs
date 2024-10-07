using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[Serializable]
	public struct ImageGestureRecognizerComponentScriptImageEntry
	{
		[Tooltip("Key")]
		public string Key;

		[TextArea(1, 8), Tooltip("Comma separated list of hex format ulong for each row, separated by newlines.")]
		public string Images;

		[Range(0f, 0.5f), Tooltip("Score padding, makes it easier to match")]
		public float ScorePadding;
	}
}
