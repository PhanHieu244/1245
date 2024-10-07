using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class TapGestureRecognizer : GestureRecognizer
	{
		private int tapCount;

		private readonly Stopwatch timer = new Stopwatch();

		private readonly List<GestureTouch> tapTouches = new List<GestureTouch>();

		private int _NumberOfTapsRequired_k__BackingField;

		private float _ThresholdSeconds_k__BackingField;

		private float _ThresholdUnits_k__BackingField;

		private bool _SendBeginState_k__BackingField;

		private ReadOnlyCollection<GestureTouch> _TapTouches_k__BackingField;

		public int NumberOfTapsRequired
		{
			get;
			set;
		}

		public float ThresholdSeconds
		{
			get;
			set;
		}

		public float ThresholdUnits
		{
			get;
			set;
		}

		public bool SendBeginState
		{
			get;
			set;
		}

		public ReadOnlyCollection<GestureTouch> TapTouches
		{
			get;
			private set;
		}

		public TapGestureRecognizer()
		{
			this.NumberOfTapsRequired = 1;
			this.ThresholdSeconds = 0.4f;
			this.ThresholdUnits = 0.3f;
			this.TapTouches = this.tapTouches.AsReadOnly();
		}

		private void VerifyFailGestureAfterDelay()
		{
			float num = (float)this.timer.Elapsed.TotalSeconds;
			if (base.State == GestureRecognizerState.Possible && num >= this.ThresholdSeconds)
			{
				base.SetState(GestureRecognizerState.Failed);
			}
		}

		private void FailGestureAfterDelayIfNoTap()
		{
			GestureRecognizer.RunActionAfterDelay(this.ThresholdSeconds, new Action(this.VerifyFailGestureAfterDelay));
		}

		protected override void StateChanged()
		{
			base.StateChanged();
			if (base.State == GestureRecognizerState.Failed || base.State == GestureRecognizerState.Ended)
			{
				this.tapCount = 0;
				this.timer.Reset();
				this.tapTouches.Clear();
			}
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			foreach (GestureTouch current in touches)
			{
				if (!base.IgnoreTouch(current.Id))
				{
					base.SetState(GestureRecognizerState.Failed);
					return;
				}
			}
			base.CalculateFocus(base.CurrentTrackedTouches);
			this.timer.Reset();
			this.timer.Start();
			if (this.SendBeginState && base.TrackedTouchCountIsWithinRange)
			{
				base.SetState(GestureRecognizerState.Began);
			}
			else
			{
				base.SetState(GestureRecognizerState.Possible);
			}
			if (this.tapCount == 0)
			{
				base.TrackCurrentTrackedTouchesStartLocations();
			}
			foreach (GestureTouch current2 in touches)
			{
				this.tapTouches.Add(current2);
			}
		}

		protected override void TouchesMoved()
		{
			base.CalculateFocus(base.CurrentTrackedTouches);
			if (this.timer.Elapsed.TotalSeconds >= (double)this.ThresholdSeconds)
			{
				base.SetState(GestureRecognizerState.Failed);
			}
		}

		protected override void TouchesEnded()
		{
			if ((float)this.timer.Elapsed.TotalSeconds <= this.ThresholdSeconds)
			{
				base.CalculateFocus(base.CurrentTrackedTouches);
				bool flag = base.AreTrackedTouchesWithinDistance(this.ThresholdUnits);
				if (flag)
				{
					if (++this.tapCount == this.NumberOfTapsRequired)
					{
						base.SetState(GestureRecognizerState.Ended);
					}
					else
					{
						this.timer.Reset();
						this.timer.Start();
						this.FailGestureAfterDelayIfNoTap();
					}
				}
				else
				{
					base.SetState(GestureRecognizerState.Failed);
				}
			}
			else
			{
				base.SetState(GestureRecognizerState.Failed);
			}
		}

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				"; ",
				base.MinimumNumberOfTouchesToTrack,
				",",
				base.MaximumNumberOfTouchesToTrack,
				",",
				this.NumberOfTapsRequired,
				","
			});
		}
	}
}
