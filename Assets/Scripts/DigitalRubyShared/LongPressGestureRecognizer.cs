using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class LongPressGestureRecognizer : GestureRecognizer
	{
		private readonly Stopwatch stopWatch = new Stopwatch();

		private float _MinimumDurationSeconds_k__BackingField;

		private float _ThresholdUnits_k__BackingField;

		public float MinimumDurationSeconds
		{
			get;
			set;
		}

		public float ThresholdUnits
		{
			get;
			set;
		}

		public LongPressGestureRecognizer()
		{
			this.MinimumDurationSeconds = 0.6f;
			this.ThresholdUnits = 0.35f;
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			this.stopWatch.Reset();
			this.stopWatch.Start();
		}

		protected override void TouchesMoved()
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			if (base.State == GestureRecognizerState.Began || base.State == GestureRecognizerState.Executing)
			{
				base.SetState(GestureRecognizerState.Executing);
			}
			else if (base.State == GestureRecognizerState.Possible && base.TrackedTouchCountIsWithinRange)
			{
				float num = base.Distance(base.DistanceX, base.DistanceY);
				if (num > this.ThresholdUnits)
				{
					base.SetState(GestureRecognizerState.Failed);
				}
				else if (this.stopWatch.Elapsed.TotalSeconds >= (double)this.MinimumDurationSeconds)
				{
					base.SetState(GestureRecognizerState.Began);
				}
				else
				{
					base.SetState(GestureRecognizerState.Possible);
				}
			}
		}

		protected override void TouchesEnded()
		{
			if (base.State == GestureRecognizerState.Began || base.State == GestureRecognizerState.Executing)
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
