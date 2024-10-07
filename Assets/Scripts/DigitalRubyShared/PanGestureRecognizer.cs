using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class PanGestureRecognizer : GestureRecognizer
	{
		private float _ThresholdUnits_k__BackingField;

		public float ThresholdUnits
		{
			get;
			set;
		}

		public PanGestureRecognizer()
		{
			this.ThresholdUnits = 0.2f;
		}

		private void ProcessTouches(bool resetFocus)
		{
			bool flag = base.CalculateFocus(base.CurrentTrackedTouches, resetFocus);
			if (base.State == GestureRecognizerState.Began || base.State == GestureRecognizerState.Executing)
			{
				base.SetState(GestureRecognizerState.Executing);
			}
			else if (flag)
			{
				base.SetState(GestureRecognizerState.Possible);
			}
			else if (base.State == GestureRecognizerState.Possible && base.TrackedTouchCountIsWithinRange)
			{
				float num = base.Distance(base.DistanceX, base.DistanceY);
				if (num >= this.ThresholdUnits)
				{
					base.SetState(GestureRecognizerState.Began);
				}
				else
				{
					base.SetState(GestureRecognizerState.Possible);
				}
			}
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			this.ProcessTouches(true);
		}

		protected override void TouchesMoved()
		{
			this.ProcessTouches(false);
		}

		protected override void TouchesEnded()
		{
			if (base.State == GestureRecognizerState.Possible)
			{
				base.SetState(GestureRecognizerState.Failed);
			}
			else
			{
				this.ProcessTouches(false);
				base.SetState(GestureRecognizerState.Ended);
			}
		}
	}
}
