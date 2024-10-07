using System;

namespace DigitalRubyShared
{
	public struct GestureTouch : IDisposable, IComparable<GestureTouch>
	{
		public const int PlatformSpecificIdInvalid = -1;

		private int id;

		private float x;

		private float y;

		private float previousX;

		private float previousY;

		private float pressure;

		private float screenX;

		private float screenY;

		private object platformSpecificTouch;

		public int Id
		{
			get
			{
				return this.id;
			}
		}

		public float X
		{
			get
			{
				return this.x;
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
		}

		public float PreviousX
		{
			get
			{
				return this.previousX;
			}
		}

		public float PreviousY
		{
			get
			{
				return this.previousY;
			}
		}

		public float ScreenX
		{
			get
			{
				return this.screenX;
			}
		}

		public float ScreenY
		{
			get
			{
				return this.screenY;
			}
		}

		public float Pressure
		{
			get
			{
				return this.pressure;
			}
		}

		public float DeltaX
		{
			get
			{
				return this.x - this.previousX;
			}
		}

		public float DeltaY
		{
			get
			{
				return this.y - this.previousY;
			}
		}

		public object PlatformSpecificTouch
		{
			get
			{
				return this.platformSpecificTouch;
			}
		}

		public GestureTouch(int platformSpecificId, float x, float y, float previousX, float previousY, float pressure)
		{
			this.id = platformSpecificId;
			this.x = x;
			this.y = y;
			this.previousX = previousX;
			this.previousY = previousY;
			this.pressure = pressure;
			this.screenX = float.NaN;
			this.screenY = float.NaN;
			this.platformSpecificTouch = null;
		}

		public GestureTouch(int platformSpecificId, float x, float y, float previousX, float previousY, float pressure, float screenX, float screenY)
		{
			this.id = platformSpecificId;
			this.x = x;
			this.y = y;
			this.previousX = previousX;
			this.previousY = previousY;
			this.pressure = pressure;
			this.screenX = screenX;
			this.screenY = screenY;
			this.platformSpecificTouch = null;
		}

		public GestureTouch(int platformSpecificId, float x, float y, float previousX, float previousY, float pressure, float screenX, float screenY, object platformSpecificTouch)
		{
			this.id = platformSpecificId;
			this.x = x;
			this.y = y;
			this.previousX = previousX;
			this.previousY = previousY;
			this.pressure = pressure;
			this.screenX = screenX;
			this.screenY = screenY;
			this.platformSpecificTouch = platformSpecificTouch;
		}

		public void Invalidate()
		{
			this.id = -1;
		}

		public bool IsValid()
		{
			return this.Id != -1;
		}

		public int CompareTo(GestureTouch other)
		{
			return this.id.CompareTo(other.id);
		}

		public override int GetHashCode()
		{
			return this.Id;
		}

		public override bool Equals(object obj)
		{
			return obj is GestureTouch && ((GestureTouch)obj).Id == this.Id;
		}

		public void Dispose()
		{
			this.Invalidate();
		}
	}
}
