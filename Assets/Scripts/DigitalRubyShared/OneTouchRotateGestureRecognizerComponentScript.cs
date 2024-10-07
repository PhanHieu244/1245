using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Rotate Gesture (One Touch)", 4)]
	public class OneTouchRotateGestureRecognizerComponentScript : GestureRecognizerComponentScript<OneTouchRotateGestureRecognizer>
	{
		[Header("One touch rotate gesture properties"), Range(0.01f, 0.5f), Tooltip("Angle threshold in radians that must be met before rotation starts - this is the amount of rotation that must happen to start the gesture.")]
		public float AngleThreshold = 0.05f;

		[Range(0f, 1f), Tooltip("The gesture focus must change distance by this number of units from the start focus in order to start.")]
		public float ThresholdUnits = 0.15f;

		protected override void Start()
		{
			base.Start();
			base.Gesture.AngleThreshold = this.AngleThreshold;
			base.Gesture.ThresholdUnits = this.ThresholdUnits;
			GestureRecognizer arg_4F_0 = base.Gesture;
			int num = this.MaximumNumberOfTouchesToTrack = 1;
			base.Gesture.MaximumNumberOfTouchesToTrack = num;
			arg_4F_0.MinimumNumberOfTouchesToTrack = (this.MinimumNumberOfTouchesToTrack = num);
		}
	}
}
