using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRubyShared
{
	public static class DeviceInfo
	{
		private static int _PixelsPerInch_k__BackingField;

		private static int _UnitMultiplier_k__BackingField;

		public static int PixelsPerInch
		{
			get;
			set;
		}

		public static int UnitMultiplier
		{
			get;
			set;
		}

		public static float CentimetersToInches(float centimeters)
		{
			return centimeters * 0.393701f;
		}
	}
}
