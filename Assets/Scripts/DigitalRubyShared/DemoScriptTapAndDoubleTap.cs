using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptTapAndDoubleTap : MonoBehaviour
	{
		private TapGestureRecognizer tapGesture;

		private TapGestureRecognizer doubleTapGesture;

		private void Start()
		{
			this.tapGesture = new TapGestureRecognizer();
			this.doubleTapGesture = new TapGestureRecognizer
			{
				NumberOfTapsRequired = 2
			};
			this.tapGesture.RequireGestureRecognizerToFail = this.doubleTapGesture;
			this.tapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapGesture_StateUpdated);
			this.doubleTapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.DoubleTapGesture_StateUpdated);
			FingersScript.Instance.AddGesture(this.tapGesture);
			FingersScript.Instance.AddGesture(this.doubleTapGesture);
		}

		private void TapGesture_StateUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.LogFormat("Single tap at {0},{1}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY
				});
			}
		}

		private void DoubleTapGesture_StateUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.LogFormat("Double tap at {0},{1}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY
				});
			}
		}

		private void Update()
		{
		}
	}
}
