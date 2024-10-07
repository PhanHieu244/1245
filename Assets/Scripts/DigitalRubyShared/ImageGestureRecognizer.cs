using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DigitalRubyShared
{
	public class ImageGestureRecognizer : GestureRecognizer
	{
		private struct Point
		{
			public int X;

			public int Y;

			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"X: ",
					this.X,
					", Y: ",
					this.Y
				});
			}
		}

		public static readonly ulong[] RowBitMasks = new ulong[]
		{
			0uL,
			1uL,
			3uL,
			7uL,
			15uL,
			31uL,
			63uL,
			127uL,
			255uL,
			511uL,
			1023uL,
			2047uL,
			4095uL,
			8191uL,
			16383uL,
			32767uL,
			65535uL,
			131071uL,
			262143uL,
			524287uL,
			1048575uL,
			2097151uL,
			4194303uL,
			8388607uL,
			16777215uL,
			33554431uL,
			67108863uL,
			134217727uL,
			268435455uL,
			536870911uL,
			1073741823uL,
			2147483647uL,
			4294967295uL,
			8589934591uL,
			17179869183uL,
			34359738367uL,
			68719476735uL,
			137438953471uL,
			274877906943uL,
			549755813887uL,
			1099511627775uL,
			2199023255551uL,
			4398046511103uL,
			8796093022207uL,
			17592186044415uL,
			35184372088831uL,
			70368744177663uL,
			140737488355327uL,
			281474976710655uL,
			562949953421311uL,
			1125899906842623uL,
			2251799813685247uL,
			4503599627370495uL,
			9007199254740991uL,
			18014398509481983uL,
			36028797018963967uL,
			72057594037927935uL,
			144115188075855871uL,
			288230376151711743uL,
			576460752303423487uL,
			1152921504606846975uL,
			2305843009213693951uL,
			4611686018427387903uL,
			9223372036854775807uL,
			18446744073709551615uL
		};

		public static readonly ulong RowBitmask = ImageGestureRecognizer.RowBitMasks[16];

		public const int ImageRows = 16;

		public const int ImageColumns = 16;

		public const int ImageSize = 256;

		public const int LinePadding = 2;

		private readonly List<List<ImageGestureRecognizer.Point>> points = new List<List<ImageGestureRecognizer.Point>>();

		private int numberOfPaths;

		private List<ImageGestureRecognizer.Point> currentList;

		private int minX;

		private int minY;

		private int maxX;

		private int maxY;

		private int _MaximumPathCount_k__BackingField;

		private float _DirectionTolerance_k__BackingField;

		private float _ThresholdUnits_k__BackingField;

		private float _MinimumDistanceBetweenPointsUnits_k__BackingField;

		private float _SimilarityMinimum_k__BackingField;

		private int _MinimumPointsToRecognize_k__BackingField;

		private List<ImageGestureImage> _GestureImages_k__BackingField;



		private ImageGestureImage _Image_k__BackingField;

		private ImageGestureImage _MatchedGestureImage_k__BackingField;

		private float _MatchedGestureCalculationTimeMilliseconds_k__BackingField;

		public event EventHandler MaximumPathCountExceeded;

		public int MaximumPathCount
		{
			get;
			set;
		}

		public float DirectionTolerance
		{
			get;
			set;
		}

		public float ThresholdUnits
		{
			get;
			set;
		}

		public float MinimumDistanceBetweenPointsUnits
		{
			get;
			set;
		}

		public float SimilarityMinimum
		{
			get;
			set;
		}

		public int MinimumPointsToRecognize
		{
			get;
			set;
		}

		public List<ImageGestureImage> GestureImages
		{
			get;
			set;
		}

		public ImageGestureImage Image
		{
			get;
			private set;
		}

		public ImageGestureImage MatchedGestureImage
		{
			get;
			private set;
		}

		public float MatchedGestureCalculationTimeMilliseconds
		{
			get;
			private set;
		}

		public override bool ResetOnEnd
		{
			get
			{
				return false;
			}
		}

		public ImageGestureRecognizer()
		{
			this.MaximumPathCount = 1;
			this.ThresholdUnits = 0.4f;
			this.DirectionTolerance = 0.3f;
			this.SimilarityMinimum = 0.8f;
			this.MinimumDistanceBetweenPointsUnits = 0.1f;
			this.MinimumPointsToRecognize = 2;
			this.Image = new ImageGestureImage();
			this.Image.Initialize(new byte[256], 16);
			this.Reset();
		}

		private void AddPoint(float x, float y)
		{
			if (this.currentList == null)
			{
				return;
			}
			ImageGestureRecognizer.Point point = new ImageGestureRecognizer.Point
			{
				X = (int)x,
				Y = (int)y
			};
			if (this.currentList.Count < 2)
			{
				this.currentList.Add(point);
			}
			else
			{
				ImageGestureRecognizer.Point point2 = this.currentList[this.currentList.Count - 1];
				ImageGestureRecognizer.Point point3 = this.currentList[this.currentList.Count - 2];
				float num = (float)(point2.X - point3.X);
				float num2 = (float)(point2.Y - point3.Y);
				float num3 = (float)(point.X - point2.X);
				float num4 = (float)(point.Y - point2.Y);
				float v = (float)Math.Atan2((double)num4, (double)num3);
				float v2 = (float)Math.Atan2((double)num2, (double)num);
				float num5 = base.DistanceBetweenPoints((float)point2.X, (float)point2.Y, (float)point3.X, (float)point3.Y);
				if (num5 < this.MinimumDistanceBetweenPointsUnits || this.CompareFloat(v, v2, this.DirectionTolerance))
				{
					this.currentList[this.currentList.Count - 1] = point;
				}
				else
				{
					this.currentList.Add(point);
				}
			}
			this.minX = Math.Min(point.X, this.minX);
			this.minY = Math.Min(point.Y, this.minY);
			this.maxX = Math.Max(point.X, this.maxX);
			this.maxY = Math.Max(point.Y, this.maxY);
		}

		private void ProcessTouches()
		{
			if (base.CurrentTrackedTouches.Count != 0)
			{
				GestureTouch gestureTouch = base.CurrentTrackedTouches[0];
				this.AddPoint(gestureTouch.X, gestureTouch.Y);
			}
		}

		private bool CompareFloat(float v1, float v2, float tolerance)
		{
			return Math.Abs(v1 - v2) < tolerance;
		}

		private void AddLineToGesturedImage(ImageGestureRecognizer.Point point1, ImageGestureRecognizer.Point point2, float s)
		{
			float num = (float)((int)((float)(point1.X - this.minX) / s * 16f));
			float num2 = (float)((int)((float)(point1.Y - this.minY) / s * 16f));
			float num3 = (float)((int)((float)(point2.X - this.minX) / s * 16f));
			float num4 = (float)((int)((float)(point2.Y - this.minY) / s * 16f));
			float num5 = num3 - num;
			float num6 = num4 - num2;
			float num7 = (float)Math.Sign(num5);
			float num8 = (float)Math.Sign(num6);
			float num9;
			float num10;
			if (num5 * num7 > num6 * num8)
			{
				num9 = 1f * num7;
				num10 = num6 / (num5 * num7);
			}
			else
			{
				num9 = num5 / (num6 * num8);
				num10 = 1f * num8;
			}
			while (true)
			{
				this.Image.SetPixelWithPadding((int)num, (int)num2, 2);
				if (this.CompareFloat(num, num3, 0.1f) && this.CompareFloat(num2, num4, 0.1f))
				{
					break;
				}
				num += num9;
				num2 += num10;
			}
		}

		private void CalculateScores()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.MatchedGestureImage = null;
			if (this.GestureImages == null || this.GestureImages.Count == 0 || this.currentList.Count < this.MinimumPointsToRecognize)
			{
				return;
			}
			float num = 0f;
			foreach (ImageGestureImage current in this.GestureImages)
			{
				float num2 = current.Similarity(this.Image);
				current.Score = num2;
				if (num2 > num && num2 >= this.SimilarityMinimum)
				{
					num = num2;
					this.MatchedGestureImage = current;
					this.Image.Score = num2;
				}
			}
			stopwatch.Stop();
			this.MatchedGestureCalculationTimeMilliseconds = (float)((int)stopwatch.Elapsed.TotalMilliseconds);
		}

		private void CheckImages()
		{
			this.Image.Clear();
			float val = (float)(this.maxX - this.minX) + 0.05f;
			float val2 = (float)(this.maxY - this.minY) + 0.05f;
			float s = Math.Max(val, val2);
			foreach (List<ImageGestureRecognizer.Point> current in this.points)
			{
				for (int i = 1; i < current.Count; i++)
				{
					this.AddLineToGesturedImage(current[i - 1], current[i], s);
				}
			}
			this.Image.Initialize(this.Image.Pixels, this.Image.Width);
			this.CalculateScores();
		}

		private void ResetImage()
		{
			this.Image.Clear();
			this.numberOfPaths = 0;
			this.points.Clear();
			this.currentList = null;
			this.MatchedGestureImage = null;
			this.minX = 2147483647;
			this.minY = 2147483647;
			this.maxX = -2147483648;
			this.maxY = -2147483648;
		}

		private bool CanExecute()
		{
			bool flag = base.State == GestureRecognizerState.Began || base.State == GestureRecognizerState.Executing;
			base.CalculateFocus(base.CurrentTrackedTouches);
			if (flag)
			{
				this.ProcessTouches();
				return true;
			}
			if (base.State != GestureRecognizerState.Possible)
			{
				return false;
			}
			float num = base.Distance(base.DistanceX, base.DistanceY);
			if (num >= this.ThresholdUnits)
			{
				if (this.numberOfPaths++ >= this.MaximumPathCount)
				{
					this.numberOfPaths = 1;
					if (this.MaximumPathCountExceeded != null)
					{
						this.MaximumPathCountExceeded(this, EventArgs.Empty);
					}
				}
				this.currentList = new List<ImageGestureRecognizer.Point>();
				this.points.Add(this.currentList);
				base.SetState(GestureRecognizerState.Began);
				this.AddPoint(base.StartFocusX, base.StartFocusY);
				return true;
			}
			base.SetState(GestureRecognizerState.Possible);
			return false;
		}

		protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
		{
			base.CalculateFocus(base.CurrentTrackedTouches, true);
		}

		protected override void TouchesMoved()
		{
			if (!this.CanExecute())
			{
				return;
			}
			base.SetState(GestureRecognizerState.Executing);
		}

		protected override void TouchesEnded()
		{
			if (!this.CanExecute())
			{
				return;
			}
			this.CheckImages();
			base.SetState(GestureRecognizerState.Ended);
			if (this.numberOfPaths == this.MaximumPathCount)
			{
				int num = this.numberOfPaths;
				this.Reset();
				this.numberOfPaths = num;
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.ResetImage();
		}
	}
}
