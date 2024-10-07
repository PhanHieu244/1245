
using System;
using UnityEngine;

public class Tracking
{
	public static void LogEvent(string eventName)
	{
		//UnityEngine.Debug.Log(eventName);
		//FirebaseAnalytics.LogEvent(eventName);
	}

	public static void LogEvent(string eventName, int value)
	{
		//UnityEngine.Debug.Log(string.Format("{0}_{1}", eventName, value));
		//FirebaseAnalytics.LogEvent(string.Format("{0}_{1}", eventName, value));
	}
}
