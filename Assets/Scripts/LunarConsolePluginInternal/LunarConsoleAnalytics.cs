using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	public static class LunarConsoleAnalytics
	{
		private sealed class _TrackEvent_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal string category;

			internal string action;

			internal int value;

			internal string _payload___0;

			internal WWW _www___0;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _TrackEvent_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._payload___0 = LunarConsoleAnalytics.CreatePayload(this.category, this.action, this.value);
					this._www___0 = new WWW(LunarConsoleAnalytics.TrackingURL, Encoding.UTF8.GetBytes(this._payload___0));
					this._current = this._www___0;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		public static readonly string TrackingURL;

		public const int kUndefinedValue = -2147483648;

		private static readonly string DefaultPayload;

		static LunarConsoleAnalytics()
		{
			LunarConsoleAnalytics.TrackingURL = "https://www.google-analytics.com/collect";
			string arg = "UA-91747018-1";
			StringBuilder stringBuilder = new StringBuilder("v=1&t=event");
			stringBuilder.AppendFormat("&tid={0}", arg);
			stringBuilder.AppendFormat("&cid={0}", WWW.EscapeURL(SystemInfo.deviceUniqueIdentifier));
			stringBuilder.AppendFormat("&ua={0}", WWW.EscapeURL(SystemInfo.operatingSystem));
			stringBuilder.AppendFormat("&av={0}", WWW.EscapeURL(Constants.Version));
			stringBuilder.AppendFormat("&ds={0}", "player");
			if (!string.IsNullOrEmpty(Application.productName))
			{
				string text = WWW.EscapeURL(Application.productName);
				if (text.Length <= 100)
				{
					stringBuilder.AppendFormat("&an={0}", text);
				}
			}
			string identifier = Application.identifier;
			if (!string.IsNullOrEmpty(identifier))
			{
				string text2 = WWW.EscapeURL(identifier);
				if (text2.Length <= 150)
				{
					stringBuilder.AppendFormat("&aid={0}", text2);
				}
			}
			if (!string.IsNullOrEmpty(Application.companyName))
			{
				string text3 = WWW.EscapeURL(Application.companyName);
				if (text3.Length <= 150)
				{
					stringBuilder.AppendFormat("&aiid={0}", text3);
				}
			}
			LunarConsoleAnalytics.DefaultPayload = stringBuilder.ToString();
		}

		internal static IEnumerator TrackEvent(string category, string action, int value = -2147483648)
		{
			LunarConsoleAnalytics._TrackEvent_c__Iterator0 _TrackEvent_c__Iterator = new LunarConsoleAnalytics._TrackEvent_c__Iterator0();
			_TrackEvent_c__Iterator.category = category;
			_TrackEvent_c__Iterator.action = action;
			_TrackEvent_c__Iterator.value = value;
			return _TrackEvent_c__Iterator;
		}

		public static string CreatePayload(string category, string action, int value)
		{
			StringBuilder stringBuilder = new StringBuilder(LunarConsoleAnalytics.DefaultPayload);
			stringBuilder.AppendFormat("&ec={0}", WWW.EscapeURL(category));
			stringBuilder.AppendFormat("&ea={0}", WWW.EscapeURL(action));
			if (value != -2147483648)
			{
				stringBuilder.AppendFormat("&ev={0}", value.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
