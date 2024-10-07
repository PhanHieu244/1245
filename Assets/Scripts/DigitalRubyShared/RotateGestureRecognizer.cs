using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class RotateGestureRecognizer : GestureRecognizer
	{
		private float startAngle = -3.40282347E+38f;

		private float previousAngle;

		private float _AngleThreshold_k__BackingField;

		private float _ThresholdUnits_k__BackingField;

		private float _RotationRadians_k__BackingField;

		private float _RotationRadiansDelta_k__BackingField;

		public float AngleThreshold
		{
			get;
			set;
		}

		public float ThresholdUnits
		{
			get;
			set;
		}

		public float RotationRadians
		{
			get;
			private set;
		}

		public float RotationRadiansDelta
		{
			get;
			private set;
		}

		public float RotationDegrees
		{
			get
			{
				return this.RotationRadians * 57.2957764f;
			}
		}

		public float RotationDegreesDelta
		{
			get
			{
				return this.RotationRadiansDelta * 57.2957764f;
			}
		}

		public RotateGestureRecognizer()
		{
			base.MaximumNumberOfTouchesToTrack = 2;
			this.AngleThreshold = 0.05f;
		}

		private float DifferenceBetweenAngles(float angle1, float angle2)
		{
			float num = angle1 - angle2;
			return (float)Math.Atan2(Math.Sin((double)num), Math.Cos((double)num));
		}

		private void UpdateAngle()
		{
			float angle = this.CurrentAngle();
			this.RotationRadians = this.DifferenceBetweenAngles(angle, this.startAngle);
			this.RotationRadiansDelta = this.DifferenceBetweenAngles(angle, this.previousAngle);
			this.previousAngle = angle;
			base.CalculateFocus(base.CurrentTrackedTouches);
			base.SetState(GestureRecognizerState.Executing);
		}

		private void CheckForStart()
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			if (!base.TrackedTouchCountIsWithinRange || base.Distance(base.DistanceX, base.DistanceY) < this.ThresholdUnits)
			{
				return;
			}
			float angle = this.CurrentAngle();
			if (this.startAngle == -3.40282347E+38f)
			{
				this.startAngle = (this.previousAngle = angle);
			}
			else
			{
				float num = Math.Abs(this.DifferenceBetweenAngles(angle, this.startAngle));
				if (num >= this.AngleThreshold)
				{
					this.startAngle = (this.previousAngle = angle);
					base.SetState(GestureRecognizerState.Began);
				}
			}
		}

		protected override void StateChanged()
		{
			base.StateChanged();
			if (base.State == GestureRecognizerState.Ended || base.State == GestureRecognizerState.Failed)
			{
				this.startAngle = -3.40282347E+38f;
				this.RotationRadians = 0f;
			}
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
		}

		protected override void TouchesMoved()
		{
			if (base.CurrentTrackedTouches.Count == base.MaximumNumberOfTouchesToTrack)
			{
				if (base.State == GestureRecognizerState.Possible)
				{
					this.CheckForStart();
				}
				else
				{
					this.UpdateAngle();
				}
			}
		}

		protected override void TouchesEnded()
		{
			if (base.State == GestureRecognizerState.Possible)
			{
				base.SetState(GestureRecognizerState.Failed);
			}
			else
			{
				base.CalculateFocus(base.CurrentTrackedTouches);
				base.SetState(GestureRecognizerState.Ended);
			}
		}

		protected virtual float CurrentAngle()
		{
			return (float)Math.Atan2((double)(base.CurrentTrackedTouches[0].Y - base.CurrentTrackedTouches[1].Y), (double)(base.CurrentTrackedTouches[0].X - base.CurrentTrackedTouches[1].X));
		}
	}
}
