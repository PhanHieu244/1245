using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptPlatformSpecificView : MonoBehaviour
	{
		public GameObject LeftPanel;

		public GameObject Cube;

		private void Start()
		{
			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Tap_Updated_Panel);
			tapGestureRecognizer.PlatformSpecificView = this.LeftPanel;
			FingersScript.Instance.AddGesture(tapGestureRecognizer);
			TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
			tapGestureRecognizer2.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Tap_Updated_Cube);
			tapGestureRecognizer2.PlatformSpecificView = this.Cube;
			FingersScript.Instance.AddGesture(tapGestureRecognizer2);
		}

		private void Tap_Updated_Cube(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.LogFormat("Tap gesture executed on cube at {0},{1}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY
				});
			}
		}

		private void Tap_Updated_Panel(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.LogFormat("Tap gesture executed on panel at {0},{1}", new object[]
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
