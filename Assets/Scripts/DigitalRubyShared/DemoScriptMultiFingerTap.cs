using System;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	public class DemoScriptMultiFingerTap : MonoBehaviour
	{
		public Text statusText;

		private void Start()
		{
			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapCallback);
			TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
			GestureRecognizer arg_29_0 = tapGestureRecognizer2;
			int num = 2;
			tapGestureRecognizer2.MaximumNumberOfTouchesToTrack = num;
			arg_29_0.MinimumNumberOfTouchesToTrack = num;
			tapGestureRecognizer2.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapCallback);
			TapGestureRecognizer tapGestureRecognizer3 = new TapGestureRecognizer();
			GestureRecognizer arg_51_0 = tapGestureRecognizer3;
			num = 3;
			tapGestureRecognizer3.MaximumNumberOfTouchesToTrack = num;
			arg_51_0.MinimumNumberOfTouchesToTrack = num;
			tapGestureRecognizer3.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapCallback);
			FingersScript.Instance.AddGesture(tapGestureRecognizer);
			FingersScript.Instance.AddGesture(tapGestureRecognizer2);
			FingersScript.Instance.AddGesture(tapGestureRecognizer3);
			tapGestureRecognizer.RequireGestureRecognizerToFail = tapGestureRecognizer2;
			tapGestureRecognizer2.RequireGestureRecognizerToFail = tapGestureRecognizer3;
			FingersScript.Instance.ShowTouches = true;
		}

		private void TapCallback(GestureRecognizer tapGesture)
		{
			if (tapGesture.State == GestureRecognizerState.Ended)
			{
				this.statusText.text = string.Format("Tap gesture finished, touch count: {0}", (tapGesture as TapGestureRecognizer).TapTouches.Count);
				UnityEngine.Debug.Log(this.statusText.text);
			}
		}
	}
}
