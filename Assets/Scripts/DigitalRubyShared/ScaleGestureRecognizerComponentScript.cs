using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Scale Gesture (Two Touches)", 7)]
	public class ScaleGestureRecognizerComponentScript : GestureRecognizerComponentScript<ScaleGestureRecognizer>
	{
		[Header("Scale gesture properties"), Range(0.01f, 10f), Tooltip("Additional multiplier for ScaleMultiplier. This will making scaling happen slower or faster.")]
		public float ZoomSpeed = 3f;

		[Range(0.01f, 1f), Tooltip("How many units the distance between the fingers must increase or decrease from the start distance to begin executing.")]
		public float ThresholdUnits = 0.15f;

		[Range(0.001f, 0.5f), Tooltip("The threshold in percent that must change to signal any listeners about a new scale.")]
		public float ScaleThresholdPercent = 0.01f;

		[Tooltip("If the focus moves more than this amount, reset the scale threshold percent. This helps avoid a wobbly zoom when panning and zooming at the same time.")]
		public float ScaleFocusMoveThresholdUnits = 0.04f;

		protected override void Start()
		{
			base.Start();
			base.Gesture.ZoomSpeed = this.ZoomSpeed;
			base.Gesture.ThresholdUnits = this.ThresholdUnits;
			base.Gesture.ScaleThresholdPercent = this.ScaleThresholdPercent;
			base.Gesture.ScaleFocusMoveThresholdUnits = this.ScaleFocusMoveThresholdUnits;
			GestureRecognizer arg_71_0 = base.Gesture;
			int num = this.MaximumNumberOfTouchesToTrack = 2;
			base.Gesture.MaximumNumberOfTouchesToTrack = num;
			arg_71_0.MinimumNumberOfTouchesToTrack = (this.MinimumNumberOfTouchesToTrack = num);
		}
	}
}
