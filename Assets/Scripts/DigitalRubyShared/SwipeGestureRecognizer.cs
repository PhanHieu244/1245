using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class SwipeGestureRecognizer : GestureRecognizer
	{
		private SwipeGestureRecognizerDirection _Direction_k__BackingField;

		private float _MinimumDistanceUnits_k__BackingField;

		private float _MinimumSpeedUnits_k__BackingField;

		private float _DirectionThreshold_k__BackingField;

		private bool _EndImmediately_k__BackingField;

		private bool _FailOnDirectionChange_k__BackingField;

		private SwipeGestureRecognizerDirection _EndDirection_k__BackingField;

		public SwipeGestureRecognizerDirection Direction
		{
			get;
			set;
		}

		public float MinimumDistanceUnits
		{
			get;
			set;
		}

		public float MinimumSpeedUnits
		{
			get;
			set;
		}

		public float DirectionThreshold
		{
			get;
			set;
		}

		public bool EndImmediately
		{
			get;
			set;
		}

		public bool FailOnDirectionChange
		{
			get;
			set;
		}

		public SwipeGestureRecognizerDirection EndDirection
		{
			get;
			private set;
		}

		public SwipeGestureRecognizer()
		{
			this.Direction = SwipeGestureRecognizerDirection.Any;
			this.MinimumDistanceUnits = 1f;
			this.MinimumSpeedUnits = 3f;
			this.DirectionThreshold = 1.5f;
		}

		private bool CalculateEndDirection(float x, float y)
		{
			SwipeGestureRecognizerDirection endDirection = this.EndDirection;
			float velocityX = base.VelocityX;
			float velocityY = base.VelocityY;
			float num = Math.Abs(velocityX);
			float num2 = Math.Abs(velocityY);
			if (num > num2)
			{
				if (this.DirectionThreshold > 1f && num / num2 < this.DirectionThreshold)
				{
					return false;
				}
				if (velocityX > 0f)
				{
					this.EndDirection = SwipeGestureRecognizerDirection.Right;
				}
				else
				{
					this.EndDirection = SwipeGestureRecognizerDirection.Left;
				}
			}
			else
			{
				if (this.DirectionThreshold > 1f && num2 / num < this.DirectionThreshold)
				{
					return false;
				}
				if (velocityY < 0f)
				{
					this.EndDirection = SwipeGestureRecognizerDirection.Down;
				}
				else
				{
					this.EndDirection = SwipeGestureRecognizerDirection.Up;
				}
			}
			if (this.FailOnDirectionChange && base.State != GestureRecognizerState.Possible && endDirection != SwipeGestureRecognizerDirection.Any && endDirection != this.EndDirection)
			{
				base.SetState(GestureRecognizerState.Failed);
				return false;
			}
			return true;
		}

		private void CheckForSwipeCompletion(bool end)
		{
			if (base.Speed < this.MinimumSpeedUnits * (float)DeviceInfo.PixelsPerInch || !base.TrackedTouchCountIsWithinRange)
			{
				base.CalculateFocus(base.CurrentTrackedTouches, true);
				return;
			}
			float num = base.DistanceBetweenPoints(base.StartFocusX, base.StartFocusY, base.FocusX, base.FocusY);
			if (num < this.MinimumDistanceUnits || !this.CalculateEndDirection(base.FocusX, base.FocusY))
			{
				return;
			}
			if (this.Direction == SwipeGestureRecognizerDirection.Any || this.Direction == this.EndDirection)
			{
				if (end)
				{
					base.SetState(GestureRecognizerState.Ended);
				}
				else if (base.State == GestureRecognizerState.Possible)
				{
					base.SetState(GestureRecognizerState.Began);
				}
				else if (base.State == GestureRecognizerState.Began || base.State == GestureRecognizerState.Executing)
				{
					base.SetState(GestureRecognizerState.Executing);
				}
			}
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			this.EndDirection = SwipeGestureRecognizerDirection.Any;
			base.SetState(GestureRecognizerState.Possible);
		}

		protected override void TouchesMoved()
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			this.CheckForSwipeCompletion(this.EndImmediately);
		}

		protected override void TouchesEnded()
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			if (base.State == GestureRecognizerState.Possible || base.State == GestureRecognizerState.Began || base.State == GestureRecognizerState.Executing)
			{
				this.CheckForSwipeCompletion(true);
			}
			if (base.State != GestureRecognizerState.Ended)
			{
				base.SetState(GestureRecognizerState.Failed);
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.EndDirection = SwipeGestureRecognizerDirection.Any;
		}
	}
}
