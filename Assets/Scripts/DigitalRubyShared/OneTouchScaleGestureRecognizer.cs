using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class OneTouchScaleGestureRecognizer : GestureRecognizer
	{
		private float _ScaleMultiplier_k__BackingField;

		private float _ScaleMultiplierX_k__BackingField;

		private float _ScaleMultiplierY_k__BackingField;

		private float _ZoomSpeed_k__BackingField;

		private float _ThresholdUnits_k__BackingField;

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

		public OneTouchScaleGestureRecognizer()
		{
			float num = 1f;
			this.ScaleMultiplierY = num;
			num = num;
			this.ScaleMultiplierX = num;
			this.ScaleMultiplier = num;
			this.ThresholdUnits = 0.15f;
			this.ZoomSpeed = -0.2f;
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			base.SetState(GestureRecognizerState.Possible);
		}

		protected override void TouchesMoved()
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			if (!base.TrackedTouchCountIsWithinRange)
			{
				return;
			}
			if (base.State == GestureRecognizerState.Possible)
			{
				if (base.Distance(base.DistanceX, base.DistanceY) < this.ThresholdUnits)
				{
					return;
				}
				float num = 1f;
				this.ScaleMultiplierY = num;
				num = num;
				this.ScaleMultiplierX = num;
				this.ScaleMultiplier = num;
				base.SetState(GestureRecognizerState.Began);
			}
			else if (base.DeltaX != 0f || base.DeltaY != 0f)
			{
				this.ScaleMultiplier = 1f + base.Distance(base.DeltaX, base.DeltaY) * (float)Math.Sign(base.DeltaY) * this.ZoomSpeed;
				this.ScaleMultiplierX = 1f + base.Distance(base.DeltaX) * (float)(-(float)Math.Sign(base.DeltaX)) * this.ZoomSpeed;
				this.ScaleMultiplierY = 1f + base.Distance(base.DeltaY) * (float)Math.Sign(base.DeltaY) * this.ZoomSpeed;
				base.SetState(GestureRecognizerState.Executing);
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
	}
}
