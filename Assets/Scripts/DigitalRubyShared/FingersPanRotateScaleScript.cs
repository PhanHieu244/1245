using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Pan, Rotate and Scale")]
	public class FingersPanRotateScaleScript : MonoBehaviour
	{
		[Tooltip("The camera to use to convert screen coordinates to world coordinates. Defaults to Camera.main.")]
		public Camera Camera;

		[Tooltip("Whether to bring the object to the front when a gesture executes on it")]
		public bool BringToFront = true;

		[Range(1f, 2f), Tooltip("Minimum touch count to start panning. Rotating and scaling always requires two fingers. This should be 1 or 2.")]
		public int PanMinimumTouchCount = 2;

		[Tooltip("Whether the gestures in this script can execute simultaneously with all other gestures.")]
		public bool AllowExecutionWithAllGestures;

		[Tooltip("Whether to set the platform specific view for each gesture to the game object. This means the gestures will not start unless they intersect the game object some how.")]
		public bool SetPlatformSpecificView;

		private PanGestureRecognizer _PanGesture_k__BackingField;

		private ScaleGestureRecognizer _ScaleGesture_k__BackingField;

		private RotateGestureRecognizer _RotateGesture_k__BackingField;

		private Rigidbody2D rigidBody2D;

		private Rigidbody rigidBody;

		private SpriteRenderer spriteRenderer;

		private CanvasRenderer canvasRenderer;

		private int startSortOrder;

		private Vector3 panStart;

		private static readonly List<RaycastResult> captureRaycastResults = new List<RaycastResult>();

		private static Comparison<RaycastResult> __f__mg_cache0;

		public PanGestureRecognizer PanGesture
		{
			get;
			private set;
		}

		public ScaleGestureRecognizer ScaleGesture
		{
			get;
			private set;
		}

		public RotateGestureRecognizer RotateGesture
		{
			get;
			private set;
		}

		public static void StartOrResetGesture(GestureRecognizer r, bool bringToFront, Camera camera, GameObject obj, SpriteRenderer spriteRenderer)
		{
			if (r.State == GestureRecognizerState.Began)
			{
				if (FingersPanRotateScaleScript.GestureIntersectsObject(r, camera, obj))
				{
					if (bringToFront && spriteRenderer != null)
					{
						spriteRenderer.sortingOrder = 1000;
					}
				}
				else
				{
					r.Reset();
				}
			}
		}

		private static int RaycastResultCompare(RaycastResult r1, RaycastResult r2)
		{
			SpriteRenderer component = r1.gameObject.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				SpriteRenderer component2 = r2.gameObject.GetComponent<SpriteRenderer>();
				if (component2 != null)
				{
					int num = component2.sortingLayerID.CompareTo(component.sortingLayerID);
					if (num == 0)
					{
						num = component2.sortingOrder.CompareTo(component.sortingOrder);
					}
					return num;
				}
			}
			return 0;
		}

		private static bool GestureIntersectsObject(GestureRecognizer r, Camera camera, GameObject obj)
		{
			FingersPanRotateScaleScript.captureRaycastResults.Clear();
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			pointerEventData.Reset();
			pointerEventData.position = new Vector2(r.FocusX, r.FocusY);
			pointerEventData.clickCount = 1;
			EventSystem.current.RaycastAll(pointerEventData, FingersPanRotateScaleScript.captureRaycastResults);
			List<RaycastResult> arg_6B_0 = FingersPanRotateScaleScript.captureRaycastResults;
			if (FingersPanRotateScaleScript.__f__mg_cache0 == null)
			{
				FingersPanRotateScaleScript.__f__mg_cache0 = new Comparison<RaycastResult>(FingersPanRotateScaleScript.RaycastResultCompare);
			}
			arg_6B_0.Sort(FingersPanRotateScaleScript.__f__mg_cache0);
			foreach (RaycastResult current in FingersPanRotateScaleScript.captureRaycastResults)
			{
				if (current.gameObject == obj)
				{
					return true;
				}
				if (current.gameObject.GetComponent<Collider>() != null || current.gameObject.GetComponent<Collider2D>() != null || current.gameObject.GetComponent<FingersPanRotateScaleScript>() != null)
				{
					break;
				}
			}
			return false;
		}

		private void PanGestureUpdated(GestureRecognizer r)
		{
			FingersPanRotateScaleScript.StartOrResetGesture(r, this.BringToFront, this.Camera, base.gameObject, this.spriteRenderer);
			if (r.State == GestureRecognizerState.Began)
			{
				if (this.rigidBody != null)
				{
					this.panStart = this.rigidBody.position;
				}
				else if (this.rigidBody2D != null)
				{
					this.panStart = this.rigidBody2D.position;
				}
				else
				{
					this.panStart = base.gameObject.transform.position;
				}
			}
			else if (r.State == GestureRecognizerState.Executing)
			{
				Vector3 vector = new Vector2(this.PanGesture.DistanceX, this.PanGesture.DistanceY);
				Plane plane = new Plane(-this.Camera.transform.forward, base.transform.position);
				float distanceToPoint = plane.GetDistanceToPoint(this.Camera.transform.position);
				vector.z = distanceToPoint;
				Vector3 a = this.Camera.ScreenToWorldPoint(vector);
				Vector3 b = a - this.Camera.ScreenToWorldPoint(new Vector3(0f, 0f, vector.z));
				if (this.rigidBody != null)
				{
					this.rigidBody.MovePosition(this.panStart + b);
				}
				else if (this.rigidBody2D != null)
				{
					b.z = 0f;
					this.rigidBody2D.MovePosition(this.panStart + b);
				}
				else if (this.canvasRenderer != null)
				{
					vector.z = 0f;
					base.transform.position = this.panStart + vector;
				}
				else
				{
					base.transform.position = this.panStart + b;
				}
			}
			else if (r.State == GestureRecognizerState.Ended && this.spriteRenderer != null && this.BringToFront)
			{
				this.spriteRenderer.sortingOrder = this.startSortOrder;
			}
		}

		private void ScaleGestureUpdated(GestureRecognizer r)
		{
			FingersPanRotateScaleScript.StartOrResetGesture(r, this.BringToFront, this.Camera, base.gameObject, this.spriteRenderer);
			if (r.State == GestureRecognizerState.Executing)
			{
				base.transform.localScale *= this.ScaleGesture.ScaleMultiplier;
			}
		}

		private void RotateGestureUpdated(GestureRecognizer r)
		{
			FingersPanRotateScaleScript.StartOrResetGesture(r, this.BringToFront, this.Camera, base.gameObject, this.spriteRenderer);
			if (r.State == GestureRecognizerState.Executing)
			{
				if (this.rigidBody != null)
				{
					float rotationDegreesDelta = this.RotateGesture.RotationDegreesDelta;
					Quaternion rhs = Quaternion.AngleAxis(rotationDegreesDelta, this.Camera.transform.forward);
					this.rigidBody.MoveRotation(this.rigidBody.rotation * rhs);
				}
				else if (this.rigidBody2D != null)
				{
					this.rigidBody2D.MoveRotation(this.rigidBody2D.rotation + this.RotateGesture.RotationDegreesDelta);
				}
				else if (this.canvasRenderer != null)
				{
					base.transform.Rotate(Vector3.forward, this.RotateGesture.RotationDegreesDelta, Space.Self);
				}
				else
				{
					base.transform.Rotate(this.Camera.transform.forward, this.RotateGesture.RotationDegreesDelta, Space.Self);
				}
			}
		}

		private void Start()
		{
			this.Camera = ((!(this.Camera == null)) ? this.Camera : Camera.main);
			this.PanGesture = new PanGestureRecognizer();
			this.PanGesture.MinimumNumberOfTouchesToTrack = this.PanMinimumTouchCount;
			this.PanGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.PanGestureUpdated);
			this.ScaleGesture = new ScaleGestureRecognizer();
			this.ScaleGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.ScaleGestureUpdated);
			this.RotateGesture = new RotateGestureRecognizer();
			this.RotateGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.RotateGestureUpdated);
			this.rigidBody2D = base.GetComponent<Rigidbody2D>();
			this.rigidBody = base.GetComponent<Rigidbody>();
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.canvasRenderer = base.GetComponent<CanvasRenderer>();
			if (this.spriteRenderer != null)
			{
				this.startSortOrder = this.spriteRenderer.sortingOrder;
			}
			if (this.AllowExecutionWithAllGestures)
			{
				this.PanGesture.AllowSimultaneousExecutionWithAllGestures();
				this.PanGesture.AllowSimultaneousExecutionWithAllGestures();
				this.ScaleGesture.AllowSimultaneousExecutionWithAllGestures();
			}
			else
			{
				this.PanGesture.AllowSimultaneousExecution(this.ScaleGesture);
				this.PanGesture.AllowSimultaneousExecution(this.RotateGesture);
				this.ScaleGesture.AllowSimultaneousExecution(this.RotateGesture);
			}
			if (this.SetPlatformSpecificView)
			{
				this.RotateGesture.PlatformSpecificView = base.gameObject;
				this.PanGesture.PlatformSpecificView = base.gameObject;
				this.ScaleGesture.PlatformSpecificView = base.gameObject;
			}
			FingersScript.Instance.AddGesture(this.PanGesture);
			FingersScript.Instance.AddGesture(this.ScaleGesture);
			FingersScript.Instance.AddGesture(this.RotateGesture);
		}
	}
}
