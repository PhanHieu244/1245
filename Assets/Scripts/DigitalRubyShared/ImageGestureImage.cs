using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DigitalRubyShared
{
	public class ImageGestureImage
	{
		private const ulong m1 = 6148914691236517205uL;

		private const ulong m2 = 3689348814741910323uL;

		private const ulong m4 = 1085102592571150095uL;

		private const ulong m8 = 71777214294589695uL;

		private const ulong m16 = 281470681808895uL;

		private const ulong m32 = 4294967295uL;

		private const ulong hff = 18446744073709551615uL;

		private const ulong h01 = 72340172838076673uL;

		private int _Width_k__BackingField;

		private int _Height_k__BackingField;

		private int _Size_k__BackingField;

		private ulong[] _Rows_k__BackingField;

		private byte[] _Pixels_k__BackingField;

		private float _SimilarityPadding_k__BackingField;

		private float _Score_k__BackingField;

		public int Width
		{
			get;
			private set;
		}

		public int Height
		{
			get;
			private set;
		}

		public int Size
		{
			get;
			private set;
		}

		public ulong[] Rows
		{
			get;
			private set;
		}

		public byte[] Pixels
		{
			get;
			private set;
		}

		public float SimilarityPadding
		{
			get;
			set;
		}

		public float Score
		{
			get;
			internal set;
		}

		public ImageGestureImage()
		{
		}

		public ImageGestureImage(ulong[] rows, int width) : this(rows, width, 0f)
		{
		}

		public ImageGestureImage(ulong[] rows, int width, float scorePadding)
		{
			this.Width = width;
			this.Height = rows.Length;
			this.Size = this.Width * this.Height;
			this.Rows = rows;
			this.Pixels = null;
			this.SimilarityPadding = scorePadding;
		}

		private void ComputeRow(byte[] pixels, int row)
		{
			ulong num = 0uL;
			int num2 = row * this.Width;
			int num3 = num2 + this.Width;
			int num4 = 0;
			while (num2 != num3)
			{
				num |= ((ulong)pixels[num2++] & 1uL) << num4++;
			}
			this.Rows[row] = num;
		}

		public ImageGestureImage Clone()
		{
			return new ImageGestureImage
			{
				Height = this.Height,
				Rows = (this.Rows.Clone() as ulong[]),
				Pixels = ((this.Pixels != null) ? (this.Pixels.Clone() as byte[]) : null),
				SimilarityPadding = this.SimilarityPadding,
				Size = this.Size,
				Width = this.Width
			};
		}

		public void Initialize(byte[] pixels, int width)
		{
			this.Pixels = pixels;
			this.Width = width;
			this.Height = pixels.Length / this.Width;
			this.Size = this.Width * this.Height;
			this.Rows = new ulong[this.Height];
			int height = this.Height;
			int num = 0;
			while (height-- != 0)
			{
				this.ComputeRow(pixels, num++);
			}
		}

		public float Similarity(ImageGestureImage other)
		{
			if (this.Rows == null || other == null || other.Rows == null || other.Rows.Length != this.Rows.Length)
			{
				return 0f;
			}
			int num = 0;
			for (int i = 0; i < this.Rows.Length; i++)
			{
				ulong value = (this.Rows[i] ^ other.Rows[i]) & ImageGestureRecognizer.RowBitmask;
				num += ImageGestureImage.NumberOfBitsSet(value);
			}
			float num2 = (float)num / (float)this.Size;
			return 1f - num2 + this.SimilarityPadding;
		}

		public int Difference(ImageGestureImage other)
		{
			if (this.Rows == null || other == null || other.Rows == null || other.Rows.Length != this.Rows.Length)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < this.Rows.Length; i++)
			{
				ulong value = (this.Rows[i] ^ other.Rows[i]) & ImageGestureRecognizer.RowBitmask;
				num += ImageGestureImage.NumberOfBitsSet(value);
			}
			return num;
		}

		public bool SetPixelWithPadding(int x, int y, int padding)
		{
			if (this.Pixels == null || x < 0 || x >= this.Width || y < 0 || y >= this.Height)
			{
				return false;
			}
			int num = x + y * this.Width;
			this.Pixels[num] = 1;
			if (padding == 0)
			{
				return true;
			}
			if (padding == 1)
			{
				this.SetPixelWithPadding(x + 1, y, 0);
				this.SetPixelWithPadding(x, y + 1, 0);
			}
			else
			{
				if (padding != 2)
				{
					throw new InvalidOperationException("Padding greater than 2 is not supported right now.");
				}
				this.SetPixelWithPadding(x - 1, y - 1, 0);
				this.SetPixelWithPadding(x, y - 1, 0);
				this.SetPixelWithPadding(x + 1, y - 1, 0);
				this.SetPixelWithPadding(x - 1, y, 0);
				this.SetPixelWithPadding(x + 1, y, 0);
				this.SetPixelWithPadding(x - 1, y + 1, 0);
				this.SetPixelWithPadding(x, y + 1, 0);
				this.SetPixelWithPadding(x + 1, y + 1, 0);
			}
			return true;
		}

		public void Clear()
		{
			if (this.Rows != null)
			{
				for (int i = 0; i < this.Rows.Length; i++)
				{
					this.Rows[i] = 0uL;
				}
			}
			if (this.Pixels != null)
			{
				for (int j = 0; j < this.Pixels.Length; j++)
				{
					this.Pixels[j] = 0;
				}
			}
		}

		public string GetCodeForRowsInitialize()
		{
			if (this.Rows == null || this.Rows.Length == 0)
			{
				throw new InvalidOperationException("Cannot generate C# script with null rows");
			}
			StringBuilder stringBuilder = new StringBuilder("new ImageGestureImage(new ulong[] { ");
			stringBuilder.AppendFormat("0x{0:X16}", this.Rows[0]);
			for (int i = 1; i < this.Rows.Length; i++)
			{
				stringBuilder.AppendFormat(", 0x{0:X16}", this.Rows[i]);
			}
			stringBuilder.Append(" }, ");
			stringBuilder.Append(this.Width);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		public static int NumberOfBitsSet(ulong value)
		{
			value -= (value >> 1 & 6148914691236517205uL);
			value = (value & 3689348814741910323uL) + (value >> 2 & 3689348814741910323uL);
			value = (value + (value >> 4) & 1085102592571150095uL);
			return (int)(value * 72340172838076673uL) >> 24;
		}
	}
}
