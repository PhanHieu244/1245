using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Pan Gesture", 1)]
	public class PanGestureRecognizerComponentScript : GestureRecognizerComponentScript<PanGestureRecognizer>
	{
		[Header("Pan gesture properties"), Range(0f, 1f), Tooltip("How many units away the pan must move to execute.")]
		public float ThresholdUnits = 0.2f;

		protected override void Start()
		{
			base.Start();
			base.Gesture.ThresholdUnits = this.ThresholdUnits;
		}
	}
}
