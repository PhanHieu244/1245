using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	public class FingersJoystickScript : MonoBehaviour
	{
		[Tooltip("The image to move around like a joystick")]
		public Image JoystickImage;

		[Range(0.01f, 10f), Tooltip("Reduces the amount the joystick moves the closer it is to the center. As the joystick moves to it's max extents, the movement amount approaches 1. For example, a power of 1 would be a linear equation, 2 would be squared, 3 cubed, etc.")]
		public float JoystickPower = 2f;

		[Range(0.001f, 0.2f), Tooltip("The max exten the joystick can move as a percentage of Screen.width + Screen.height")]
		public float MaxExtentPercent = 0.02f;

		[Tooltip("In eight axis mode, the joystick can only move up, down, left, right or diagonally. No in between.")]
		public bool EightAxisMode;

		private Vector2 startCenter;

		private PanGestureRecognizer _PanGesture_k__BackingField;

		public Action<FingersJoystickScript, Vector2> JoystickExecuted;

		private bool _MoveJoystickToGestureStartLocation_k__BackingField;

		public PanGestureRecognizer PanGesture
		{
			get;
			private set;
		}

		public bool MoveJoystickToGestureStartLocation
		{
			get;
			set;
		}

		private void Start()
		{
			this.PanGesture = new PanGestureRecognizer
			{
				PlatformSpecificView = ((!this.MoveJoystickToGestureStartLocation) ? this.JoystickImage.gameObject : null),
				ThresholdUnits = 0f
			};
			this.PanGesture.AllowSimultaneousExecutionWithAllGestures();
			this.PanGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.PanGestureUpdated);
			FingersScript.Instance.AddGesture(this.PanGesture);
		}

		private void SetImagePosition(Vector2 pos)
		{
			this.JoystickImage.rectTransform.anchoredPosition = pos;
		}

		private Vector2 UpdateForEightAxisMode(Vector2 amount, float maxOffset)
		{
			if (this.EightAxisMode)
			{
				float num = Mathf.Abs(amount.x);
				float num2 = Mathf.Abs(amount.y);
				if (num > num2 * 1.5f)
				{
					amount.y = 0f;
					amount.x = Mathf.Sign(amount.x) * maxOffset;
				}
				else if ((double)num2 > (double)num * 1.5)
				{
					amount.x = 0f;
					amount.y = Mathf.Sign(amount.y) * maxOffset;
				}
				else
				{
					amount.x = Mathf.Sign(amount.x) * maxOffset * 0.7f;
					amount.y = Mathf.Sign(amount.y) * maxOffset * 0.7f;
				}
			}
			return amount;
		}

		private void ExecuteCallback(Vector2 amount)
		{
			if (this.JoystickExecuted != null)
			{
				this.JoystickExecuted(this, amount);
			}
		}

		private void PanGestureUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Executing)
			{
				float num = (float)(Screen.width + Screen.height) * this.MaxExtentPercent;
				Vector2 vector = new Vector2(gesture.FocusX - gesture.StartFocusX, gesture.FocusY - gesture.StartFocusY);
				vector = Vector2.ClampMagnitude(vector, num);
				if (vector == Vector2.zero)
				{
					return;
				}
				vector = this.UpdateForEightAxisMode(vector, num);
				this.SetImagePosition(this.startCenter + vector);
				if (this.JoystickPower >= 1f)
				{
					vector.x = Mathf.Sign(vector.x) * Mathf.Pow(Mathf.Abs(vector.x) / num, this.JoystickPower);
					vector.y = Mathf.Sign(vector.y) * Mathf.Pow(Mathf.Abs(vector.y) / num, this.JoystickPower);
				}
				else
				{
					Vector2 vector2 = new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
					float num2 = vector2.x + vector2.y;
					float num3 = vector2.x / num2;
					float num4 = vector2.y / num2;
					vector.x = num3 * Mathf.Sign(vector.x) * Mathf.Pow(vector2.x / num, this.JoystickPower);
					vector.y = num4 * Mathf.Sign(vector.y) * Mathf.Pow(vector2.y / num, this.JoystickPower);
					vector = Vector2.ClampMagnitude(vector, num);
				}
				this.ExecuteCallback(vector);
			}
			else if (gesture.State == GestureRecognizerState.Began)
			{
				if (this.MoveJoystickToGestureStartLocation)
				{
					this.JoystickImage.transform.parent.position = new Vector3(gesture.FocusX, gesture.FocusY, this.JoystickImage.transform.parent.position.z);
				}
				this.startCenter = this.JoystickImage.rectTransform.anchoredPosition;
			}
			else if (gesture.State == GestureRecognizerState.Ended)
			{
				this.SetImagePosition(this.startCenter);
				this.ExecuteCallback(Vector2.zero);
			}
		}

		private void Update()
		{
		}
	}
}
