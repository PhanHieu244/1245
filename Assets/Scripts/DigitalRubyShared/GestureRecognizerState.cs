using System;

namespace DigitalRubyShared
{
	public enum GestureRecognizerState
	{
		Possible = 1,
		Began,
		Executing = 4,
		Ended = 8,
		EndPending = 16,
		Failed = 32
	}
}
