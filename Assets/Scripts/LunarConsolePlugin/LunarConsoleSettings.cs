using System;
using UnityEngine;

namespace LunarConsolePlugin
{
	[Serializable]
	public class LunarConsoleSettings
	{
		public bool exceptionWarning = true;

		[HideInInspector]
		public bool transparentLogOverlay;

		[HideInInspector]
		public bool sortActions = true;

		[HideInInspector]
		public bool sortVariables = true;

		[SerializeField]
		public string[] emails;
	}
}
