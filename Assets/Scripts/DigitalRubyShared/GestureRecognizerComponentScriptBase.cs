using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRubyShared
{
	public abstract class GestureRecognizerComponentScriptBase : MonoBehaviour
	{
		private GestureRecognizer _GestureBase_k__BackingField;

		public GestureRecognizer GestureBase
		{
			get;
			protected set;
		}
	}
}
