using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Long Press Gesture", 3)]
	public class LongPressGestureRecognizerComponentScript : GestureRecognizerComponentScript<LongPressGestureRecognizer>
	{
		[Header("Long press gesture properties"), Range(0.01f, 1f), Tooltip("The number of seconds that the touch must stay down to begin executing.")]
		public float MinimumDurationSeconds = 0.6f;

		[Range(0.01f, 1f), Tooltip("How many units away the long press can move before failing. After the long press begins, it is allowed to move any distance and stay executing.")]
		public float ThresholdUnits = 0.35f;

		protected override void Start()
		{
			base.Start();
			base.Gesture.MinimumDurationSeconds = this.MinimumDurationSeconds;
			base.Gesture.ThresholdUnits = this.ThresholdUnits;
		}
	}
}
