using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	public class FingersDPadScript : MonoBehaviour
	{
		[Tooltip("The background image to use for the DPad. This should contain up, right, down, left and center in unselected state.")]
		public Image DPadBackgroundImage;

		[Tooltip("The up image to use for the DPad for selected state. Alpha pixel of less than MinAlphaForTouch will not be selectable.")]
		public Image DPadUpImageSelected;

		[Tooltip("The right image to use for the DPad for selected state. Alpha pixel of less than MinAlphaForTouch will not be selectable.")]
		public Image DPadRightImageSelected;

		[Tooltip("The down image to use for the DPad for selected state. Alpha pixel of less than MinAlphaForTouch will not be selectable.")]
		public Image DPadDownImageSelected;

		[Tooltip("The left image to use for the DPad for selected state. Alpha pixel of less than MinAlphaForTouch will not be selectable.")]
		public Image DPadLeftImageSelected;

		[Tooltip("The center image to use for the DPad for selected state. Alpha pixel of less than MinAlphaForTouch will not be selectable.")]
		public Image DPadCenterImageSelected;

		[Range(0.01f, 1f), Tooltip("Touch radius in units (usually inches). Set to lowest for single pixel accuracy, or larger if you want more than one dpad button interactable at once. You'll need to test this to make sure the DPad works how you expect for an average finger size and your screen size.")]
		public float TouchRadiusInUnits = 0.125f;

		private readonly Collider2D[] overlap = new Collider2D[32];

		public Action<FingersDPadScript, FingersDPadItem, TapGestureRecognizer> DPadItemTapped;

		public Action<FingersDPadScript, FingersDPadItem, PanGestureRecognizer> DPadItemPanned;

		private PanGestureRecognizer _PanGesture_k__BackingField;

		private TapGestureRecognizer _TapGesture_k__BackingField;

		private bool _MoveDPadToGestureStartLocation_k__BackingField;

		public PanGestureRecognizer PanGesture
		{
			get;
			private set;
		}

		public TapGestureRecognizer TapGesture
		{
			get;
			private set;
		}

		public bool MoveDPadToGestureStartLocation
		{
			get;
			set;
		}

		private void CheckForOverlap<T>(Vector2 point, T gesture, Action<FingersDPadScript, FingersDPadItem, T> action) where T : GestureRecognizer
		{
			if (action == null)
			{
				return;
			}
			int num = Physics2D.OverlapCircleNonAlloc(point, (float)DeviceInfo.PixelsPerInch * this.TouchRadiusInUnits, this.overlap);
			for (int i = 0; i < num; i++)
			{
				if (this.overlap[i].gameObject == this.DPadCenterImageSelected.gameObject)
				{
					this.DPadCenterImageSelected.enabled = true;
					action(this, FingersDPadItem.Center, gesture);
				}
				else if (this.overlap[i].gameObject == this.DPadRightImageSelected.gameObject)
				{
					this.DPadRightImageSelected.enabled = true;
					action(this, FingersDPadItem.Right, gesture);
				}
				else if (this.overlap[i].gameObject == this.DPadDownImageSelected.gameObject)
				{
					this.DPadDownImageSelected.enabled = true;
					action(this, FingersDPadItem.Down, gesture);
				}
				else if (this.overlap[i].gameObject == this.DPadLeftImageSelected.gameObject)
				{
					this.DPadLeftImageSelected.enabled = true;
					action(this, FingersDPadItem.Left, gesture);
				}
				else if (this.overlap[i].gameObject == this.DPadUpImageSelected.gameObject)
				{
					this.DPadUpImageSelected.enabled = true;
					action(this, FingersDPadItem.Up, gesture);
				}
			}
		}

		private void DisableButtons()
		{
			this.DPadUpImageSelected.enabled = false;
			this.DPadRightImageSelected.enabled = false;
			this.DPadDownImageSelected.enabled = false;
			this.DPadLeftImageSelected.enabled = false;
			this.DPadCenterImageSelected.enabled = false;
		}

		private void PanGestureUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Began || gesture.State == GestureRecognizerState.Executing)
			{
				if (gesture.State == GestureRecognizerState.Began && this.MoveDPadToGestureStartLocation)
				{
					base.transform.position = new Vector3(gesture.FocusX, gesture.FocusY, base.transform.position.z);
				}
				this.DisableButtons();
				this.CheckForOverlap<PanGestureRecognizer>(new Vector2(gesture.FocusX, gesture.FocusY), this.PanGesture, this.DPadItemPanned);
			}
			else if (gesture.State == GestureRecognizerState.Ended || gesture.State == GestureRecognizerState.Failed)
			{
				this.DisableButtons();
			}
		}

		private void TapGestureUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				this.CheckForOverlap<TapGestureRecognizer>(new Vector2(gesture.FocusX, gesture.FocusY), this.TapGesture, this.DPadItemTapped);
				this.DisableButtons();
			}
		}

		private void Start()
		{
			this.PanGesture = new PanGestureRecognizer
			{
				PlatformSpecificView = ((!this.MoveDPadToGestureStartLocation) ? this.DPadBackgroundImage.canvas.gameObject : null),
				ThresholdUnits = 0f
			};
			this.PanGesture.AllowSimultaneousExecutionWithAllGestures();
			this.PanGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.PanGestureUpdated);
			FingersScript.Instance.AddGesture(this.PanGesture);
			this.TapGesture = new TapGestureRecognizer
			{
				PlatformSpecificView = this.DPadBackgroundImage.gameObject
			};
			this.TapGesture.AllowSimultaneousExecutionWithAllGestures();
			this.TapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapGestureUpdated);
			FingersScript.Instance.AddGesture(this.TapGesture);
		}
	}
}
