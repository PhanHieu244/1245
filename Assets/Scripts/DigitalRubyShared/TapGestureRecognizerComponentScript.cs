using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Tap Gesture", 0)]
	public class TapGestureRecognizerComponentScript : GestureRecognizerComponentScript<TapGestureRecognizer>
	{
		[Header("Tap gesture properties"), Range(1f, 5f), Tooltip("How many taps must execute in order to end the gesture")]
		public int NumberOfTapsRequired = 1;

		[Range(0.01f, 1f), Tooltip("How many seconds can expire before the tap is released to still count as a tap")]
		public float ThresholdSeconds = 0.5f;

		[Range(0.01f, 1f), Tooltip("How many units away the tap down and up and subsequent taps can be to still be considered - must be greater than 0.")]
		public float ThresholdUnits = 0.3f;

		[Tooltip("Whether the tap gesture will immediately send a begin state when a touch is first down. Default is false.")]
		public bool SendBeginState;

		protected override void Start()
		{
			base.Start();
			base.Gesture.NumberOfTapsRequired = this.NumberOfTapsRequired;
			base.Gesture.ThresholdSeconds = this.ThresholdSeconds;
			base.Gesture.ThresholdUnits = this.ThresholdUnits;
			base.Gesture.SendBeginState = this.SendBeginState;
		}
	}
}
