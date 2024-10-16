using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptComponent : MonoBehaviour
	{
		private float scale = 1f;

		private float oneTouchScale = 1f;

		private void Start()
		{
			FingersScript.Instance.ShowTouches = true;
		}

		public void TapGestureExecuted(GestureRecognizer gesture)
		{
			UnityEngine.Debug.LogFormat("Tap gesture executing, state: {0}, pos: {1},{2}", new object[]
			{
				gesture.State,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		public void SwipeGestureExecuted(GestureRecognizer gesture)
		{
			UnityEngine.Debug.LogFormat("Swipe gesture executing, state: {0}, dir: {1} pos: {2},{3}", new object[]
			{
				gesture.State,
				(gesture as SwipeGestureRecognizer).EndDirection,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		public void ScaleGestureExecuted(GestureRecognizer gesture)
		{
			this.scale *= (gesture as ScaleGestureRecognizer).ScaleMultiplier;
			UnityEngine.Debug.LogFormat("Scale gesture executing, state: {0}, scale: {1} pos: {2},{3}", new object[]
			{
				gesture.State,
				this.scale,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		public void OneTouchScaleGestureExecuted(GestureRecognizer gesture)
		{
			this.oneTouchScale *= (gesture as OneTouchScaleGestureRecognizer).ScaleMultiplier;
			UnityEngine.Debug.LogFormat("Scale gesture executing, state: {0}, scale: {1} pos: {2},{3}", new object[]
			{
				gesture.State,
				this.oneTouchScale,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		public void RotateGestureExecuted(GestureRecognizer gesture)
		{
			UnityEngine.Debug.LogFormat("Rotate gesture executing, state: {0}, degrees: {1} pos: {2},{3}", new object[]
			{
				gesture.State,
				(gesture as RotateGestureRecognizer).RotationDegrees,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		public void PanGestureExecuted(GestureRecognizer gesture)
		{
			UnityEngine.Debug.LogFormat("Pan gesture executing, state: {0}, pos: {1},{2}", new object[]
			{
				gesture.State,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		public void LongPressGestureExecuted(GestureRecognizer gesture)
		{
			UnityEngine.Debug.LogFormat("Long press gesture executing, state: {0}, pos: {1},{2}", new object[]
			{
				gesture.State,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		public void ImageGestureExecuted(GestureRecognizer gesture)
		{
			ImageGestureRecognizer imageGestureRecognizer = gesture as ImageGestureRecognizer;
			if (gesture.State == GestureRecognizerState.Ended)
			{
				if (imageGestureRecognizer.MatchedGestureImage == null)
				{
					UnityEngine.Debug.Log("Image gesture failed to match.");
				}
				else
				{
					UnityEngine.Debug.Log("Image gesture matched!");
				}
				gesture.Reset();
			}
		}
	}
}
