using System;

namespace LunarConsolePlugin
{
	internal struct CValue
	{
		public string stringValue;

		public int intValue;

		public float floatValue;

		public bool Equals(ref CValue other)
		{
			return other.intValue == this.intValue && other.floatValue == this.floatValue && other.stringValue == this.stringValue;
		}
	}
}
