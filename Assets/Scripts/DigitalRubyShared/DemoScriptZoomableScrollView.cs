using System;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	public class DemoScriptZoomableScrollView : MonoBehaviour
	{
		public FingersScript FingersScript;

		public ScrollRect ScrollView;

		public Canvas Canvas;

		private float scaleStart;

		private float scaleEnd;

		private float scaleTime;

		private float elapsedScaleTime;

		private Vector2 scalePosStart;

		private Vector2 scalePosEnd;

		private void Start()
		{
			ScaleGestureRecognizer scaleGestureRecognizer = new ScaleGestureRecognizer();
			scaleGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Scale_Updated);
			scaleGestureRecognizer.PlatformSpecificView = this.ScrollView.gameObject;
			this.FingersScript.AddGesture(scaleGestureRecognizer);
			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.NumberOfTapsRequired = 2;
			tapGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Tap_Updated);
			tapGestureRecognizer.PlatformSpecificView = this.ScrollView.gameObject;
			this.FingersScript.AddGesture(tapGestureRecognizer);
		}

		private void Update()
		{
			if (this.scaleEnd != 0f)
			{
				this.elapsedScaleTime += Time.deltaTime;
				float num = Mathf.Min(1f, this.elapsedScaleTime / this.scaleTime);
				float num2 = Mathf.Lerp(this.scaleStart, this.scaleEnd, num);
				this.ScrollView.content.transform.localScale = new Vector3(num2, num2, 1f);
				this.ScrollView.normalizedPosition = Vector2.Lerp(this.scalePosStart, this.scalePosEnd, num);
				if (num == 1f)
				{
					this.scaleEnd = 0f;
				}
			}
		}

		private void Tap_Updated(GestureRecognizer gesture)
		{
			if (this.scaleEnd == 0f && gesture.State == GestureRecognizerState.Ended)
			{
				this.scaleStart = this.ScrollView.content.transform.localScale.x;
				this.scaleTime = 0.5f;
				this.elapsedScaleTime = 0f;
				Vector2 screenPoint = new Vector2(gesture.FocusX, gesture.FocusY);
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.ScrollView.content, screenPoint, this.Canvas.worldCamera, out vector);
				this.scalePosStart = this.ScrollView.normalizedPosition;
				float num = this.ScrollView.content.offsetMax.x - this.ScrollView.content.offsetMin.x;
				float num2 = this.ScrollView.content.offsetMax.y - this.ScrollView.content.offsetMin.y;
				this.scalePosEnd.x = Mathf.Clamp((vector.x - this.ScrollView.content.rect.xMin) / num, 0f, 1f);
				this.scalePosEnd.y = Mathf.Clamp((vector.y - this.ScrollView.content.rect.yMin) / num2, 0f, 1f);
				if (this.ScrollView.content.transform.localScale.x >= 4f)
				{
					this.scaleEnd = 1f;
				}
				else
				{
					this.scaleEnd = 4f;
				}
			}
		}

		private void Scale_Updated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Executing)
			{
				this.ScrollView.content.transform.localScale *= (gesture as ScaleGestureRecognizer).ScaleMultiplier;
			}
		}
	}
}
