using System;

namespace DigitalRubyShared
{
	public class OneTouchRotateGestureRecognizer : RotateGestureRecognizer
	{
		public float AnglePointOverrideX = -3.40282347E+38f;

		public float AnglePointOverrideY = -3.40282347E+38f;

		public OneTouchRotateGestureRecognizer()
		{
			base.MaximumNumberOfTouchesToTrack = 1;
			base.ThresholdUnits = 0.15f;
			base.AngleThreshold = 0f;
		}

		protected override float CurrentAngle()
		{
			if (this.AnglePointOverrideX != -3.40282347E+38f && this.AnglePointOverrideY != -3.40282347E+38f && base.CurrentTrackedTouches.Count != 0)
			{
				GestureTouch gestureTouch = base.CurrentTrackedTouches[0];
				return (float)Math.Atan2((double)(gestureTouch.Y - this.AnglePointOverrideY), (double)(gestureTouch.X - this.AnglePointOverrideX));
			}
			return (float)Math.Atan2((double)base.DistanceY, (double)base.DistanceX);
		}
	}
}
