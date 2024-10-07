using System;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	public static class LunarConsoleConfig
	{
		public static readonly bool consoleEnabled;

		public static readonly bool consoleSupported;

		public static readonly bool freeVersion;

		public static readonly bool fullVersion;

		public static bool actionsEnabled
		{
			get
			{
				return LunarConsoleConfig.consoleSupported && LunarConsoleConfig.consoleEnabled && Application.platform == RuntimePlatform.Android;
			}
		}

		static LunarConsoleConfig()
		{
			LunarConsoleConfig.consoleEnabled = true;
			LunarConsoleConfig.consoleSupported = true;
			LunarConsoleConfig.freeVersion = true;
			LunarConsoleConfig.fullVersion = false;
		}
	}
}
