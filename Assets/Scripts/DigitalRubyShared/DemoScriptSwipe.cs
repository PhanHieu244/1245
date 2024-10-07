using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptSwipe : MonoBehaviour
	{
		[Tooltip("Emit this particle system in the swipe direction.")]
		public ParticleSystem SwipeParticleSystem;

		[Range(1f, 10f), Tooltip("Set the required touches for the swipe.")]
		public int SwipeTouchCount = 1;

		[Tooltip("Whether to reset all touch states on swipe end, this allows multiple swipes without lifting the finger.")]
		public bool ResetStateOnEnd = true;

		private SwipeGestureRecognizer swipe;

		private void Start()
		{
			this.swipe = new SwipeGestureRecognizer();
			this.swipe.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Swipe_Updated);
			this.swipe.DirectionThreshold = 0f;
			this.swipe.EndImmediately = true;
			GestureRecognizer arg_58_0 = this.swipe;
			int swipeTouchCount = this.SwipeTouchCount;
			this.swipe.MaximumNumberOfTouchesToTrack = swipeTouchCount;
			arg_58_0.MinimumNumberOfTouchesToTrack = swipeTouchCount;
			FingersScript.Instance.AddGesture(this.swipe);
			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Tap_Updated);
			FingersScript.Instance.AddGesture(tapGestureRecognizer);
		}

		private void Update()
		{
			GestureRecognizer arg_1A_0 = this.swipe;
			int swipeTouchCount = this.SwipeTouchCount;
			this.swipe.MaximumNumberOfTouchesToTrack = swipeTouchCount;
			arg_1A_0.MinimumNumberOfTouchesToTrack = swipeTouchCount;
		}

		private void Tap_Updated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.Log("Tap");
			}
		}

		private void Swipe_Updated(GestureRecognizer gesture)
		{
			UnityEngine.Debug.LogFormat("Swipe state: {0}", new object[]
			{
				gesture.State
			});
			SwipeGestureRecognizer swipeGestureRecognizer = gesture as SwipeGestureRecognizer;
			if (swipeGestureRecognizer.State == GestureRecognizerState.Ended)
			{
				float x = Mathf.Atan2(-swipeGestureRecognizer.DistanceY, swipeGestureRecognizer.DistanceX) * 57.29578f;
				this.SwipeParticleSystem.transform.rotation = Quaternion.Euler(x, 90f, 0f);
				Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(gesture.StartFocusX, gesture.StartFocusY, 0f));
				position.z = 0f;
				this.SwipeParticleSystem.transform.position = position;
				this.SwipeParticleSystem.Play();
				if (this.ResetStateOnEnd)
				{
					gesture.BeginGestureRestart();
				}
			}
		}
	}
}
