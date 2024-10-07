using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DigitalRubyShared
{
	public class GestureRecognizer : IDisposable
	{
		public delegate void CallbackMainThreadDelegate(float delay, Action callback);

		private static readonly GestureRecognizer allGesturesReference = new GestureRecognizer();

		private GestureRecognizerState state = GestureRecognizerState.Possible;

		private readonly List<GestureTouch> currentTrackedTouches = new List<GestureTouch>();

		private readonly ReadOnlyCollection<GestureTouch> currentTrackedTouchesReadOnly;

		private GestureRecognizer requireGestureRecognizerToFail;

		private readonly HashSet<GestureRecognizer> failGestures = new HashSet<GestureRecognizer>();

		private readonly List<GestureRecognizer> simultaneousGestures = new List<GestureRecognizer>();

		private readonly GestureVelocityTracker velocityTracker = new GestureVelocityTracker();

		private readonly List<KeyValuePair<float, float>> touchStartLocations = new List<KeyValuePair<float, float>>();

		private readonly HashSet<int> ignoreTouchIds = new HashSet<int>();

		private readonly List<GestureTouch> tempTouches = new List<GestureTouch>();

		private int minimumNumberOfTouchesToTrack = 1;

		private int maximumNumberOfTouchesToTrack = 1;

		private bool justFailed;

		private bool justEnded;

		private bool isRestarting;

		private float _prevFocusX_k__BackingField;

		private float _prevFocusY_k__BackingField;

		internal static readonly HashSet<GestureRecognizer> ActiveGestures = new HashSet<GestureRecognizer>();





		private float _FocusX_k__BackingField;

		private float _FocusY_k__BackingField;

		private float _StartFocusX_k__BackingField;

		private float _StartFocusY_k__BackingField;

		private float _DeltaX_k__BackingField;

		private float _DeltaY_k__BackingField;

		private float _DistanceX_k__BackingField;

		private float _DistanceY_k__BackingField;

		private float _Pressure_k__BackingField;

		private object _PlatformSpecificView_k__BackingField;

		private float _PlatformSpecificViewScale_k__BackingField;

		private bool _ClearTrackedTouchesOnEndOrFail_k__BackingField;

		public static GestureRecognizer.CallbackMainThreadDelegate MainThreadCallback;

		[Obsolete("Please use StateChanged as this property will be removed in a future version.")]
		public event GestureRecognizerUpdated Updated;

		public event GestureRecognizerStateUpdatedDelegate StateUpdated;

		protected float prevFocusX
		{
			get;
			private set;
		}

		protected float prevFocusY
		{
			get;
			private set;
		}

		public GestureRecognizerState State
		{
			get
			{
				return this.state;
			}
		}

		public ReadOnlyCollection<GestureTouch> CurrentTrackedTouches
		{
			get
			{
				return this.currentTrackedTouchesReadOnly;
			}
		}

		public float FocusX
		{
			get;
			private set;
		}

		public float FocusY
		{
			get;
			private set;
		}

		public float StartFocusX
		{
			get;
			private set;
		}

		public float StartFocusY
		{
			get;
			private set;
		}

		public float DeltaX
		{
			get;
			private set;
		}

		public float DeltaY
		{
			get;
			private set;
		}

		public float DistanceX
		{
			get;
			private set;
		}

		public float DistanceY
		{
			get;
			private set;
		}

		public float VelocityX
		{
			get
			{
				return this.velocityTracker.VelocityX;
			}
		}

		public float VelocityY
		{
			get
			{
				return this.velocityTracker.VelocityY;
			}
		}

		public float Speed
		{
			get
			{
				return this.velocityTracker.Speed;
			}
		}

		public float Pressure
		{
			get;
			private set;
		}

		public object PlatformSpecificView
		{
			get;
			set;
		}

		public float PlatformSpecificViewScale
		{
			get;
			set;
		}

		public GestureRecognizer RequireGestureRecognizerToFail
		{
			get
			{
				return this.requireGestureRecognizerToFail;
			}
			set
			{
				if (value != this.requireGestureRecognizerToFail)
				{
					if (this.requireGestureRecognizerToFail != null)
					{
						this.requireGestureRecognizerToFail.failGestures.Remove(this);
					}
					this.requireGestureRecognizerToFail = value;
					if (this.requireGestureRecognizerToFail != null)
					{
						this.requireGestureRecognizerToFail.failGestures.Add(this);
					}
				}
			}
		}

		public int MinimumNumberOfTouchesToTrack
		{
			get
			{
				return this.minimumNumberOfTouchesToTrack;
			}
			set
			{
				this.minimumNumberOfTouchesToTrack = ((value >= 1) ? value : 1);
				if (this.minimumNumberOfTouchesToTrack > this.maximumNumberOfTouchesToTrack)
				{
					this.maximumNumberOfTouchesToTrack = this.minimumNumberOfTouchesToTrack;
				}
			}
		}

		public int MaximumNumberOfTouchesToTrack
		{
			get
			{
				return this.maximumNumberOfTouchesToTrack;
			}
			set
			{
				this.maximumNumberOfTouchesToTrack = ((value >= 1) ? value : 1);
				if (this.maximumNumberOfTouchesToTrack < this.minimumNumberOfTouchesToTrack)
				{
					this.minimumNumberOfTouchesToTrack = this.maximumNumberOfTouchesToTrack;
				}
			}
		}

		public bool TrackedTouchCountIsWithinRange
		{
			get
			{
				return this.currentTrackedTouches.Count >= this.minimumNumberOfTouchesToTrack && this.currentTrackedTouches.Count <= this.maximumNumberOfTouchesToTrack;
			}
		}

		public bool ClearTrackedTouchesOnEndOrFail
		{
			get;
			set;
		}

		public virtual bool ResetOnEnd
		{
			get
			{
				return true;
			}
		}

		public bool IsRestarting
		{
			get
			{
				return this.isRestarting;
			}
		}

		public GestureRecognizer()
		{
			this.state = GestureRecognizerState.Possible;
			this.PlatformSpecificViewScale = 1f;
			float num = -3.40282347E+38f;
			this.StartFocusY = num;
			this.StartFocusX = num;
			this.currentTrackedTouchesReadOnly = new ReadOnlyCollection<GestureTouch>(this.currentTrackedTouches);
		}

		private void EndGesture()
		{
			this.state = GestureRecognizerState.Ended;
			this.StateChanged();
			if (this.ResetOnEnd)
			{
				this.ResetInternal(this.ClearTrackedTouchesOnEndOrFail);
			}
			else
			{
				this.SetState(GestureRecognizerState.Possible);
				this.touchStartLocations.Clear();
				this.RemoveFromActiveGestures();
			}
		}

		private void RemoveFromActiveGestures()
		{
			GestureRecognizer.ActiveGestures.Remove(this);
		}

		private bool CanExecuteGestureWithOtherGesturesOrFail(GestureRecognizerState value)
		{
			if (GestureRecognizer.ActiveGestures.Count != 0 && (value == GestureRecognizerState.Began || value == GestureRecognizerState.Executing || value == GestureRecognizerState.Ended) && this.state != GestureRecognizerState.Began && this.state != GestureRecognizerState.Executing)
			{
				foreach (GestureRecognizer current in GestureRecognizer.ActiveGestures)
				{
					if (current != this && !this.simultaneousGestures.Contains(current) && !current.simultaneousGestures.Contains(this) && !this.simultaneousGestures.Contains(GestureRecognizer.allGesturesReference) && !current.simultaneousGestures.Contains(GestureRecognizer.allGesturesReference))
					{
						this.FailGestureNow();
						return false;
					}
				}
				return true;
			}
			return true;
		}

		private void FailGestureNow()
		{
			this.state = GestureRecognizerState.Failed;
			this.RemoveFromActiveGestures();
			this.StateChanged();
			foreach (GestureRecognizer current in this.failGestures)
			{
				if (current.state == GestureRecognizerState.EndPending)
				{
					current.SetState(GestureRecognizerState.Ended);
				}
			}
			this.ResetInternal(this.ClearTrackedTouchesOnEndOrFail);
			this.justFailed = true;
		}

		private bool TouchesIntersect(IEnumerable<GestureTouch> collection, List<GestureTouch> list)
		{
			foreach (GestureTouch current in collection)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Id == current.Id)
					{
						return true;
					}
				}
			}
			return false;
		}

		private void UpdateTrackedTouches(IEnumerable<GestureTouch> touches)
		{
			int num = 0;
			foreach (GestureTouch current in touches)
			{
				for (int i = 0; i < this.currentTrackedTouches.Count; i++)
				{
					if (this.currentTrackedTouches[i].Id == current.Id)
					{
						this.currentTrackedTouches[i] = current;
						num++;
						break;
					}
				}
			}
			if (num != 0)
			{
				this.currentTrackedTouches.Sort();
			}
		}

		private int TrackTouchesInternal(IEnumerable<GestureTouch> touches)
		{
			int num = 0;
			foreach (GestureTouch current in touches)
			{
				if (!this.currentTrackedTouches.Contains(current))
				{
					this.currentTrackedTouches.Add(current);
					num++;
				}
			}
			if (this.currentTrackedTouches.Count > 1)
			{
				this.currentTrackedTouches.Sort();
			}
			return num;
		}

		private int StopTrackingTouches(ICollection<GestureTouch> touches)
		{
			if (touches == null || touches.Count == 0)
			{
				return 0;
			}
			int num = 0;
			foreach (GestureTouch current in touches)
			{
				for (int i = 0; i < this.currentTrackedTouches.Count; i++)
				{
					if (this.currentTrackedTouches[i].Id == current.Id)
					{
						this.currentTrackedTouches.RemoveAt(i);
						num++;
						break;
					}
				}
			}
			return num;
		}

		private void ResetInternal(bool clearCurrentTrackedTouches)
		{
			if (clearCurrentTrackedTouches)
			{
				this.currentTrackedTouches.Clear();
			}
			this.touchStartLocations.Clear();
			float num = -3.40282347E+38f;
			this.prevFocusY = num;
			num = num;
			this.StartFocusY = num;
			num = num;
			this.prevFocusX = num;
			this.StartFocusX = num;
			num = 0f;
			this.DistanceY = num;
			num = num;
			this.DistanceX = num;
			num = num;
			this.DeltaY = num;
			num = num;
			this.DeltaX = num;
			num = num;
			this.FocusY = num;
			this.FocusX = num;
			this.Pressure = 0f;
			this.velocityTracker.Reset();
			this.RemoveFromActiveGestures();
			this.SetState(GestureRecognizerState.Possible);
		}

		private static void RunActionAfterDelayInternal(float seconds, Action action)
		{
			if (action == null)
			{
				return;
			}
			GestureRecognizer.MainThreadCallback(seconds, action);
		}

		protected bool IgnoreTouch(int id)
		{
			return this.ignoreTouchIds.Add(id);
		}

		protected void TrackCurrentTrackedTouchesStartLocations()
		{
			foreach (GestureTouch current in this.CurrentTrackedTouches)
			{
				this.touchStartLocations.Add(new KeyValuePair<float, float>(current.X, current.Y));
			}
		}

		protected bool AreTrackedTouchesWithinDistance(float thresholdUnits)
		{
			if (this.CurrentTrackedTouches.Count == 0 || this.touchStartLocations.Count == 0)
			{
				return false;
			}
			foreach (GestureTouch current in this.CurrentTrackedTouches)
			{
				bool flag = false;
				for (int i = this.touchStartLocations.Count - 1; i >= 0; i--)
				{
					if (this.PointsAreWithinDistance(current.X, current.Y, this.touchStartLocations[i].Key, this.touchStartLocations[i].Value, thresholdUnits))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		protected bool CalculateFocus(ICollection<GestureTouch> touches)
		{
			return this.CalculateFocus(touches, false);
		}

		protected bool CalculateFocus(ICollection<GestureTouch> touches, bool resetFocus)
		{
			bool flag = resetFocus || this.StartFocusX == -3.40282347E+38f || this.StartFocusY == -3.40282347E+38f;
			this.prevFocusX = this.FocusX;
			this.prevFocusY = this.FocusY;
			this.FocusX = 0f;
			this.FocusY = 0f;
			this.Pressure = 0f;
			foreach (GestureTouch current in touches)
			{
				this.FocusX += current.X;
				this.FocusY += current.Y;
				this.Pressure += current.Pressure;
			}
			float num = 1f / (float)touches.Count;
			this.FocusX *= num;
			this.FocusY *= num;
			this.Pressure *= num;
			if (flag)
			{
				this.StartFocusX = this.FocusX;
				this.StartFocusY = this.FocusY;
				this.DeltaX = 0f;
				this.DeltaY = 0f;
				this.velocityTracker.Restart();
			}
			else
			{
				this.DeltaX = this.FocusX - this.prevFocusX;
				this.DeltaY = this.FocusY - this.prevFocusY;
			}
			this.velocityTracker.Update(this.FocusX, this.FocusY);
			this.DistanceX = this.FocusX - this.StartFocusX;
			this.DistanceY = this.FocusY - this.StartFocusY;
			return flag;
		}

		protected virtual void StateChanged()
		{
			if (this.Updated != null)
			{
				this.Updated(this, this.currentTrackedTouches);
			}
			if (this.StateUpdated != null)
			{
				this.StateUpdated(this);
			}
			if (this.failGestures.Count != 0 && (this.state == GestureRecognizerState.Began || this.state == GestureRecognizerState.Executing || this.state == GestureRecognizerState.Ended))
			{
				foreach (GestureRecognizer current in this.failGestures)
				{
					current.FailGestureNow();
				}
			}
		}

		protected bool SetState(GestureRecognizerState value)
		{
			if (value == GestureRecognizerState.Failed)
			{
				this.FailGestureNow();
				return true;
			}
			if (!this.CanExecuteGestureWithOtherGesturesOrFail(value))
			{
				return false;
			}
			if (this.requireGestureRecognizerToFail != null && value == GestureRecognizerState.Ended && this.requireGestureRecognizerToFail.state == GestureRecognizerState.Possible && (this.requireGestureRecognizerToFail.CurrentTrackedTouches.Count != 0 || this.requireGestureRecognizerToFail.justEnded) && !this.requireGestureRecognizerToFail.justFailed)
			{
				this.state = GestureRecognizerState.EndPending;
				return false;
			}
			if (value == GestureRecognizerState.Began || value == GestureRecognizerState.Executing)
			{
				this.state = value;
				GestureRecognizer.ActiveGestures.Add(this);
				this.StateChanged();
			}
			else if (value == GestureRecognizerState.Ended)
			{
				this.EndGesture();
				GestureRecognizer.ActiveGestures.Add(this);
				GestureRecognizer.RunActionAfterDelay(0.001f, new Action(this.RemoveFromActiveGestures));
			}
			else
			{
				this.state = value;
				this.StateChanged();
			}
			return true;
		}

		protected virtual void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
		}

		protected virtual void TouchesMoved()
		{
		}

		protected virtual void TouchesEnded()
		{
		}

		protected int TrackTouches(IEnumerable<GestureTouch> touches)
		{
			return this.TrackTouchesInternal(touches);
		}

		public bool Simulate(params float[] xy)
		{
			if (xy == null || xy.Length < 2 || xy.Length % 2 != 0)
			{
				return false;
			}
			if (xy.Length > 3)
			{
				this.ProcessTouchesBegan(new GestureTouch[]
				{
					new GestureTouch(0, xy[2], xy[3], xy[0], xy[1], 1f)
				});
			}
			else
			{
				this.ProcessTouchesBegan(new GestureTouch[]
				{
					new GestureTouch(0, xy[0], xy[1], xy[0], xy[1], 1f)
				});
			}
			for (int i = 2; i < xy.Length - 2; i += 2)
			{
				this.ProcessTouchesMoved(new GestureTouch[]
				{
					new GestureTouch(0, xy[i - 2], xy[i - 1], xy[i], xy[i + 1], 1f)
				});
			}
			if (xy.Length > 3)
			{
				this.ProcessTouchesEnded(new GestureTouch[]
				{
					new GestureTouch(0, xy[xy.Length - 2], xy[xy.Length - 1], xy[xy.Length - 4], xy[xy.Length - 3], 1f)
				});
			}
			else
			{
				this.ProcessTouchesEnded(new GestureTouch[]
				{
					new GestureTouch(0, xy[xy.Length - 2], xy[xy.Length - 1], xy[xy.Length - 2], xy[xy.Length - 1], 1f)
				});
			}
			return true;
		}

		~GestureRecognizer()
		{
			this.Dispose();
		}

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.GetType().Name,
				": Tracking ",
				this.CurrentTrackedTouches.Count,
				" touches, state = ",
				this.State
			});
		}

		public virtual void Reset()
		{
			this.ResetInternal(true);
		}

		public bool BeginGestureRestart()
		{
			if (this.State == GestureRecognizerState.Ended)
			{
				this.isRestarting = true;
				return true;
			}
			return false;
		}

		public bool EndGestureRestart(ICollection<GestureTouch> touches)
		{
			if (this.isRestarting)
			{
				foreach (GestureTouch current in touches)
				{
					if (this.CurrentTrackedTouches.Contains(current))
					{
						this.tempTouches.Add(current);
					}
				}
				this.currentTrackedTouches.Clear();
				this.ProcessTouchesBegan(this.tempTouches);
				this.tempTouches.Clear();
				this.isRestarting = false;
				return true;
			}
			return false;
		}

		public void ProcessTouchesBegan(ICollection<GestureTouch> touches)
		{
			this.justFailed = false;
			this.justEnded = false;
			if (touches == null || touches.Count == 0)
			{
				return;
			}
			if (this.State == GestureRecognizerState.Possible && this.TrackTouches(touches) > 0)
			{
				if (this.CurrentTrackedTouches.Count > this.MaximumNumberOfTouchesToTrack)
				{
					this.SetState(GestureRecognizerState.Failed);
				}
				else
				{
					this.TouchesBegan(touches);
				}
			}
		}

		public void ProcessTouchesMoved(ICollection<GestureTouch> touches)
		{
			if (touches == null || touches.Count == 0 || !this.TouchesIntersect(touches, this.currentTrackedTouches))
			{
				return;
			}
			if (this.CurrentTrackedTouches.Count > this.MaximumNumberOfTouchesToTrack || (this.State != GestureRecognizerState.Possible && this.State != GestureRecognizerState.Began && this.State != GestureRecognizerState.Executing))
			{
				this.SetState(GestureRecognizerState.Failed);
			}
			else if (!this.EndGestureRestart(touches))
			{
				this.UpdateTrackedTouches(touches);
				this.TouchesMoved();
			}
		}

		public void ProcessTouchesEnded(ICollection<GestureTouch> touches)
		{
			if (touches == null || touches.Count == 0 || !this.TouchesIntersect(touches, this.currentTrackedTouches))
			{
				return;
			}
			try
			{
				foreach (GestureTouch current in touches)
				{
					this.ignoreTouchIds.Remove(current.Id);
				}
				if (!this.TrackedTouchCountIsWithinRange || (this.State != GestureRecognizerState.Possible && this.State != GestureRecognizerState.Began && this.State != GestureRecognizerState.Executing))
				{
					this.SetState(GestureRecognizerState.Failed);
				}
				else
				{
					this.UpdateTrackedTouches(touches);
					this.TouchesEnded();
				}
			}
			finally
			{
				this.StopTrackingTouches(touches);
				this.justEnded = true;
			}
		}

		public void ProcessTouchesCancelled(ICollection<GestureTouch> touches)
		{
			if (touches == null || touches.Count == 0 || !this.TouchesIntersect(touches, this.currentTrackedTouches))
			{
				return;
			}
			try
			{
				foreach (GestureTouch current in touches)
				{
					if (this.currentTrackedTouches.Contains(current))
					{
						this.SetState(GestureRecognizerState.Failed);
						break;
					}
				}
			}
			finally
			{
				this.StopTrackingTouches(touches);
				this.justEnded = true;
			}
		}

		public bool PointsAreWithinDistance(float x1, float y1, float x2, float y2, float d)
		{
			return this.DistanceBetweenPoints(x1, y1, x2, y2) <= d;
		}

		public float DistanceBetweenPoints(float x1, float y1, float x2, float y2)
		{
			float num = x2 - x1;
			float num2 = y2 - y1;
			return (float)Math.Sqrt((double)(num * num + num2 * num2)) * this.PlatformSpecificViewScale / (float)DeviceInfo.UnitMultiplier;
		}

		public float Distance(float xVector, float yVector)
		{
			return (float)Math.Sqrt((double)(xVector * xVector + yVector * yVector)) * this.PlatformSpecificViewScale / (float)DeviceInfo.UnitMultiplier;
		}

		public float Distance(float length)
		{
			return Math.Abs(length) * this.PlatformSpecificViewScale / (float)DeviceInfo.UnitMultiplier;
		}

		public virtual void Dispose()
		{
			this.RemoveFromActiveGestures();
			GestureRecognizer[] array = this.simultaneousGestures.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				GestureRecognizer other = array[i];
				this.DisallowSimultaneousExecution(other);
			}
			foreach (GestureRecognizer current in this.failGestures)
			{
				if (current.requireGestureRecognizerToFail == this)
				{
					current.requireGestureRecognizerToFail = null;
				}
			}
		}

		public void AllowSimultaneousExecution(GestureRecognizer other)
		{
			other = (other ?? GestureRecognizer.allGesturesReference);
			this.simultaneousGestures.Add(other);
			if (other != GestureRecognizer.allGesturesReference)
			{
				other.simultaneousGestures.Add(this);
			}
		}

		public void AllowSimultaneousExecutionWithAllGestures()
		{
			this.AllowSimultaneousExecution(null);
		}

		public void DisallowSimultaneousExecution(GestureRecognizer other)
		{
			other = (other ?? GestureRecognizer.allGesturesReference);
			this.simultaneousGestures.Remove(other);
			if (other != GestureRecognizer.allGesturesReference)
			{
				other.simultaneousGestures.Remove(this);
			}
		}

		public void DisallowSimultaneousExecutionWithAllGestures()
		{
			this.DisallowSimultaneousExecution(null);
		}

		public static void RunActionAfterDelay(float seconds, Action action)
		{
			GestureRecognizer.RunActionAfterDelayInternal(seconds, action);
		}

		public static int NumberOfGesturesInProgress()
		{
			return GestureRecognizer.ActiveGestures.Count;
		}
	}
}
