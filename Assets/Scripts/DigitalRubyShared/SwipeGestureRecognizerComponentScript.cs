using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Swipe Gesture", 2)]
	public class SwipeGestureRecognizerComponentScript : GestureRecognizerComponentScript<SwipeGestureRecognizer>
	{
		[Header("Swipe gesture properties"), Tooltip("The swipe direction required to recognize the gesture (default is any)")]
		public SwipeGestureRecognizerDirection Direction = SwipeGestureRecognizerDirection.Any;

		[Range(0.01f, 10f), Tooltip("The minimum distance the swipe must travel to be recognized.")]
		public float MinimumDistanceUnits = 1f;

		[Range(0.01f, 10f), Tooltip("The minimum units per second the swipe must travel to be recognized.")]
		public float MinimumSpeedUnits = 3f;

		[Range(0f, 5f), Tooltip("For set directions, this is the amount that the swipe must be proportionally in that direction vs the other direction. For example, a swipe down gesture will need to move in the y axis by this multiplier more versus moving along the x axis.")]
		public float DirectionThreshold = 1.5f;

		[Tooltip("End the swipe gesture immediately if recognized, reglardless of whether the touch is lifted. Default is false.")]
		public bool EndImmediately;

		[Tooltip("Whether to fail if the gesture changes direction mid swipe")]
		public bool FailOnDirectionChange;

		protected override void Start()
		{
			base.Start();
			base.Gesture.Direction = this.Direction;
			base.Gesture.MinimumDistanceUnits = this.MinimumDistanceUnits;
			base.Gesture.MinimumSpeedUnits = this.MinimumSpeedUnits;
			base.Gesture.DirectionThreshold = this.DirectionThreshold;
			base.Gesture.EndImmediately = this.EndImmediately;
			base.Gesture.FailOnDirectionChange = this.FailOnDirectionChange;
		}
	}
}
