using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptOneFinger : MonoBehaviour
	{
		public GameObject RotateIcon;

		public GameObject ScaleIcon;

		public GameObject Earth;

		private OneTouchRotateGestureRecognizer rotationGesture = new OneTouchRotateGestureRecognizer();

		private OneTouchScaleGestureRecognizer scaleGesture = new OneTouchScaleGestureRecognizer();

		private bool GestureIntersectsSprite(GestureRecognizer g, GameObject obj)
		{
			Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(g.StartFocusX, g.StartFocusY, -Camera.main.transform.position.z));
			Collider2D collider2D = Physics2D.OverlapPoint(v);
			return collider2D != null && collider2D.gameObject != null && collider2D.gameObject == obj;
		}

		private void RotationGestureUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Began && !this.GestureIntersectsSprite(gesture, this.RotateIcon))
			{
				gesture.Reset();
			}
			else if (gesture.State == GestureRecognizerState.Executing)
			{
				this.Earth.transform.Rotate(0f, 0f, this.rotationGesture.RotationDegreesDelta);
			}
		}

		private void ScaleGestureUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Began && !this.GestureIntersectsSprite(gesture, this.ScaleIcon))
			{
				gesture.Reset();
			}
			else if (gesture.State == GestureRecognizerState.Executing)
			{
				this.Earth.transform.localScale *= this.scaleGesture.ScaleMultiplier;
			}
		}

		private void Start()
		{
			FingersScript.Instance.AddGesture(this.rotationGesture);
			this.rotationGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.RotationGestureUpdated);
			FingersScript.Instance.AddGesture(this.scaleGesture);
			this.scaleGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.ScaleGestureUpdated);
		}

		private void Update()
		{
		}
	}
}
