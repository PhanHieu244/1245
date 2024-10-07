using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptPan : MonoBehaviour
	{
		public FingersScript FingersScript;

		private void Start()
		{
			PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
			panGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Pan_Updated);
			this.FingersScript.AddGesture(panGestureRecognizer);
		}

		private void Pan_Updated(GestureRecognizer gesture)
		{
			UnityEngine.Debug.LogFormat("Pan gesture, state: {0}, position: {1},{2}", new object[]
			{
				gesture.State,
				gesture.FocusX,
				gesture.FocusY
			});
		}

		private void Update()
		{
		}
	}
}
