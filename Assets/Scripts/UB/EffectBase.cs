using System;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
	public class EffectBase : MonoBehaviour
	{
		public static Dictionary<string, RenderTexture> AlreadyRendered = new Dictionary<string, RenderTexture>();

		private static bool _insiderendering = false;

		public static bool InsideRendering
		{
			get
			{
				return EffectBase._insiderendering;
			}
			set
			{
				EffectBase._insiderendering = value;
			}
		}
	}
}
