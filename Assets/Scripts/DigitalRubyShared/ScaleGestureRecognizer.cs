using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class ScaleGestureRecognizer : GestureRecognizer
	{
		private float previousDistance;

		private float previousDistanceX;

		private float previousDistanceY;

		private float centerX;

		private float centerY;

		private float _ScaleMultiplier_k__BackingField;

		private float _ScaleMultiplierX_k__BackingField;

		private float _ScaleMultiplierY_k__BackingField;

		private float _ZoomSpeed_k__BackingField;

		private float _ThresholdUnits_k__BackingField;

		private float _ScaleThresholdPercent_k__BackingField;

		private float _ScaleFocusMoveThresholdUnits_k__BackingField;

		public float ScaleMultiplier
		{
			get;
			private set;
		}

		public float ScaleMultiplierX
		{
			get;
			private set;
		}

		public float ScaleMultiplierY
		{
			get;
			private set;
		}

		public float ZoomSpeed
		{
			get;
			set;
		}

		public float ThresholdUnits
		{
			get;
			set;
		}

		public float ScaleThresholdPercent
		{
			get;
			set;
		}

		public float ScaleFocusMoveThresholdUnits
		{
			get;
			set;
		}

		public ScaleGestureRecognizer()
		{
			float num = 1f;
			this.ScaleMultiplierY = num;
			num = num;
			this.ScaleMultiplierX = num;
			this.ScaleMultiplier = num;
			this.ZoomSpeed = 3f;
			this.ThresholdUnits = 0.15f;
			this.ScaleThresholdPercent = 0.01f;
			this.ScaleFocusMoveThresholdUnits = 0.04f;
			int num2 = 2;
			base.MaximumNumberOfTouchesToTrack = num2;
			base.MinimumNumberOfTouchesToTrack = num2;
		}

		private void UpdateCenter(float distance, float distanceX, float distanceY)
		{
			this.previousDistance = distance;
			this.previousDistanceX = distanceX;
			this.previousDistanceY = distanceY;
			this.centerX = base.FocusX;
			this.centerY = base.FocusY;
		}

		private void ProcessTouches()
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			if (!base.TrackedTouchCountIsWithinRange)
			{
				return;
			}
			float num = base.DistanceBetweenPoints(base.CurrentTrackedTouches[0].X, base.CurrentTrackedTouches[0].Y, base.CurrentTrackedTouches[1].X, base.CurrentTrackedTouches[1].Y);
			float num2 = base.Distance(base.CurrentTrackedTouches[0].X - base.CurrentTrackedTouches[1].X);
			float num3 = base.Distance(base.CurrentTrackedTouches[0].Y - base.CurrentTrackedTouches[1].Y);
			if (base.State == GestureRecognizerState.Possible)
			{
				if (this.previousDistance == 0f)
				{
					this.previousDistance = num;
					this.previousDistanceX = num2;
					this.previousDistanceY = num3;
				}
				else
				{
					float num4 = Math.Abs(this.previousDistance - num);
					if (num4 >= this.ThresholdUnits)
					{
						this.UpdateCenter(num, num2, num3);
						base.SetState(GestureRecognizerState.Began);
					}
				}
			}
			else if (base.State == GestureRecognizerState.Executing)
			{
				float num5 = base.DistanceBetweenPoints(base.FocusX, base.FocusY, this.centerX, this.centerY);
				if (num5 >= this.ScaleFocusMoveThresholdUnits)
				{
					this.UpdateCenter(num, num2, num3);
				}
				else
				{
					this.ScaleMultiplier = ((this.previousDistance > 0f) ? (num / this.previousDistance) : 1f);
					this.ScaleMultiplierX = ((this.previousDistanceX > 0f && Math.Abs(this.previousDistanceX - num2) >= this.ThresholdUnits) ? (num2 / this.previousDistanceX) : 1f);
					this.ScaleMultiplierY = ((this.previousDistanceY > 0f && Math.Abs(this.previousDistanceY - num3) >= this.ThresholdUnits) ? (num3 / this.previousDistanceY) : 1f);
					if (this.ScaleMultiplier < 1f - this.ScaleThresholdPercent || this.ScaleMultiplier > 1f + this.ScaleThresholdPercent)
					{
						this.ScaleMultiplier = 1f + (this.ScaleMultiplier - 1f) * this.ZoomSpeed;
						this.previousDistance = num;
						if (this.ScaleMultiplierX < 1f - this.ScaleThresholdPercent || this.ScaleMultiplierX > 1f + this.ScaleThresholdPercent)
						{
							this.ScaleMultiplierX = 1f + (this.ScaleMultiplierX - 1f) * this.ZoomSpeed;
							this.previousDistanceX = num2;
						}
						else
						{
							this.ScaleMultiplierX = 1f;
						}
						if (this.ScaleMultiplierY < 1f - this.ScaleThresholdPercent || this.ScaleMultiplierY > 1f + this.ScaleThresholdPercent)
						{
							this.ScaleMultiplierY = 1f + (this.ScaleMultiplierY - 1f) * this.ZoomSpeed;
							this.previousDistanceY = num3;
						}
						else
						{
							this.ScaleMultiplierY = 1f;
						}
						base.SetState(GestureRecognizerState.Executing);
					}
				}
			}
			else if (base.State == GestureRecognizerState.Began)
			{
				this.centerX = (base.CurrentTrackedTouches[0].X + base.CurrentTrackedTouches[1].X) * 0.5f;
				this.centerY = (base.CurrentTrackedTouches[0].Y + base.CurrentTrackedTouches[1].Y) * 0.5f;
				base.SetState(GestureRecognizerState.Executing);
			}
			else
			{
				base.SetState(GestureRecognizerState.Possible);
			}
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			this.previousDistance = 0f;
		}

		protected override void TouchesMoved()
		{
			this.ProcessTouches();
		}

		protected override void TouchesEnded()
		{
			if (base.State == GestureRecognizerState.Executing)
			{
				base.CalculateFocus(base.CurrentTrackedTouches);
				base.SetState(GestureRecognizerState.Ended);
			}
			else
			{
				base.SetState(GestureRecognizerState.Failed);
			}
		}
	}
}
