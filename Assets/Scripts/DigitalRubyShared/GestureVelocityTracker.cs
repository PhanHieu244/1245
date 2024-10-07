using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public class GestureVelocityTracker
	{
		private struct VelocityHistory
		{
			public float VelocityX;

			public float VelocityY;

			public float Seconds;
		}

		private const int maxHistory = 8;

		private readonly Queue<GestureVelocityTracker.VelocityHistory> history = new Queue<GestureVelocityTracker.VelocityHistory>();

		private readonly Stopwatch timer = new Stopwatch();

		private float previousX;

		private float previousY;

		private float _VelocityX_k__BackingField;

		private float _VelocityY_k__BackingField;

		public float ElapsedSeconds
		{
			get
			{
				return (float)this.timer.Elapsed.TotalSeconds;
			}
		}

		public float VelocityX
		{
			get;
			private set;
		}

		public float VelocityY
		{
			get;
			private set;
		}

		public float Speed
		{
			get
			{
				return (float)Math.Sqrt((double)(this.VelocityX * this.VelocityX + this.VelocityY * this.VelocityY));
			}
		}

		private void AddItem(float velocityX, float velocityY, float elapsed)
		{
			GestureVelocityTracker.VelocityHistory item = new GestureVelocityTracker.VelocityHistory
			{
				VelocityX = velocityX,
				VelocityY = velocityY,
				Seconds = elapsed
			};
			this.history.Enqueue(item);
			if (this.history.Count > 8)
			{
				this.history.Dequeue();
			}
			float num = 0f;
			float num2 = 0f;
			this.VelocityY = num2;
			this.VelocityX = num2;
			foreach (GestureVelocityTracker.VelocityHistory current in this.history)
			{
				num += current.Seconds;
			}
			foreach (GestureVelocityTracker.VelocityHistory current2 in this.history)
			{
				float num3 = current2.Seconds / num;
				this.VelocityX += current2.VelocityX * num3;
				this.VelocityY += current2.VelocityY * num3;
			}
			this.timer.Reset();
			this.timer.Start();
		}

		public void Reset()
		{
			this.timer.Reset();
			float num = 0f;
			this.VelocityY = num;
			this.VelocityX = num;
			this.history.Clear();
		}

		public void Restart()
		{
			this.Restart(-3.40282347E+38f, -3.40282347E+38f);
		}

		public void Restart(float previousX, float previousY)
		{
			this.previousX = previousX;
			this.previousY = previousY;
			this.Reset();
			this.timer.Start();
		}

		public void Update(float x, float y)
		{
			float elapsedSeconds = this.ElapsedSeconds;
			if (this.previousX != -3.40282347E+38f)
			{
				float num = this.previousX;
				float num2 = this.previousY;
				float velocityX = (x - num) / elapsedSeconds;
				float velocityY = (y - num2) / elapsedSeconds;
				this.AddItem(velocityX, velocityY, elapsedSeconds);
			}
			this.previousX = x;
			this.previousY = y;
		}
	}
}
