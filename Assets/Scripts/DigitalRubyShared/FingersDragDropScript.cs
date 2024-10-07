using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Drag and Drop")]
	public class FingersDragDropScript : MonoBehaviour
	{
		[Tooltip("The camera to use to convert screen coordinates to world coordinates. Defaults to Camera.main.")]
		public Camera Camera;

		[Tooltip("Whether to bring the object to the front when a gesture executes on it")]
		public bool BringToFront = true;

		private LongPressGestureRecognizer longPressGesture;

		private Rigidbody2D rigidBody;

		private SpriteRenderer spriteRenderer;

		private int startSortOrder;

		private Vector2 panStart;

		private void LongPressGestureUpdated(GestureRecognizer r)
		{
			FingersPanRotateScaleScript.StartOrResetGesture(r, this.BringToFront, this.Camera, base.gameObject, this.spriteRenderer);
			if (r.State == GestureRecognizerState.Began)
			{
				this.panStart = ((!(this.rigidBody == null)) ? (Vector2)this.rigidBody.position : (Vector2)base.gameObject.transform.position);
				UnityEngine.Debug.Log("Drag/drop began");
			}
			else if (r.State == GestureRecognizerState.Executing)
			{
				Vector2 v = new Vector2(this.longPressGesture.DistanceX, this.longPressGesture.DistanceY);
				Vector2 b = this.Camera.ScreenToWorldPoint(v) - this.Camera.ScreenToWorldPoint(Vector2.zero);
				if (this.rigidBody == null)
				{
					base.transform.position = this.panStart + b;
				}
				else
				{
					this.rigidBody.MovePosition(this.panStart + b);
				}
			}
			else if (r.State == GestureRecognizerState.Ended)
			{
				if (this.spriteRenderer != null && this.BringToFront)
				{
					this.spriteRenderer.sortingOrder = this.startSortOrder;
				}
				UnityEngine.Debug.Log("Drag/drop ended");
			}
		}

		private void Start()
		{
			this.Camera = ((!(this.Camera == null)) ? this.Camera : Camera.main);
			this.longPressGesture = new LongPressGestureRecognizer();
			this.longPressGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.LongPressGestureUpdated);
			this.rigidBody = base.GetComponent<Rigidbody2D>();
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			if (this.spriteRenderer != null)
			{
				this.startSortOrder = this.spriteRenderer.sortingOrder;
			}
			FingersScript.Instance.AddGesture(this.longPressGesture);
		}

		private void Update()
		{
		}
	}
}
