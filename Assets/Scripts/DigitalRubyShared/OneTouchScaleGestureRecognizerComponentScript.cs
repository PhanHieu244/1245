using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Scale Gesture (One Touch)", 6)]
	public class OneTouchScaleGestureRecognizerComponentScript : GestureRecognizerComponentScript<OneTouchScaleGestureRecognizer>
	{
		[Header("One touch scale gesture properties"), Range(-10f, 10f), Tooltip("Additional multiplier for ScaleMultiplier. This will making scaling happen slower or faster.")]
		public float ZoomSpeed = -0.2f;

		[Range(0.01f, 1f), Tooltip("The threshold in units that the touch must move to start the gesture.")]
		public float ThresholdUnits = 0.15f;

		protected override void Start()
		{
			base.Start();
			base.Gesture.ZoomSpeed = this.ZoomSpeed;
			base.Gesture.ThresholdUnits = this.ThresholdUnits;
			GestureRecognizer arg_4F_0 = base.Gesture;
			int num = this.MaximumNumberOfTouchesToTrack = 1;
			base.Gesture.MaximumNumberOfTouchesToTrack = num;
			arg_4F_0.MinimumNumberOfTouchesToTrack = (this.MinimumNumberOfTouchesToTrack = num);
		}
	}
}
