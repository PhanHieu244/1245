using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRubyShared
{
	public abstract class GestureRecognizerComponentScript<T> : GestureRecognizerComponentScriptBase where T : GestureRecognizer, new()
	{
		[Header("Gesture properties"), Tooltip("Gesture state updated callback")]
		public GestureRecognizerComponentStateUpdatedEvent GestureStateUpdated;

		[Tooltip("The game object the gesture must execute over, null to allow the gesture to execute anywhere.")]
		public GameObject GestureView;

		[Range(1f, 10f), Tooltip("The minimum number of touches to track. This gesture will not start unless this many touches are tracked. Default is usually 1 or 2. Not all gestures will honor values higher than 1.")]
		public int MinimumNumberOfTouchesToTrack = 1;

		[Range(1f, 10f), Tooltip("The maximum number of touches to track. This gesture will never track more touches than this. Default is usually 1 or 2. Not all gestures will honor values higher than 1.")]
		public int MaximumNumberOfTouchesToTrack = 1;

		[Tooltip("Gesture components to allow simultaneous execution with. By default, gestures cannot execute together.")]
		public List<GestureRecognizerComponentScriptBase> AllowSimultaneousExecutionWith;

		[Tooltip("Whether to allow the gesture to execute simultaneously with all other gestures.")]
		public bool AllowSimultaneousExecutionWithAllGestures;

		[Tooltip("Whether tracked touches are cleared when the gesture ends or fails, default is false. By setting to true, you allow the gesture to possibly execute again with a different touch even if the original touch it failed on is still on-going. This is a special case, so be sure to watch for problems if you set this to true, as leaving it false ensures the most correct behavior, especially with lots of gestures at once.")]
		public bool ClearTrackedTouchesOnEndOrFail;

		private T _Gesture_k__BackingField;

		public T Gesture
		{
			get;
			private set;
		}

		protected virtual void GestureStateUpdatedCallback(GestureRecognizer gesture)
		{
			if (this.GestureStateUpdated != null)
			{
				this.GestureStateUpdated.Invoke(gesture);
			}
		}

		protected virtual void Awake()
		{
			this.Gesture = Activator.CreateInstance<T>();
			base.GestureBase = this.Gesture;
		}

		protected virtual void Start()
		{
			T gesture = this.Gesture;
			gesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.GestureStateUpdatedCallback);
			T gesture2 = this.Gesture;
			gesture2.PlatformSpecificView = this.GestureView;
			T gesture3 = this.Gesture;
			gesture3.MinimumNumberOfTouchesToTrack = this.MinimumNumberOfTouchesToTrack;
			T gesture4 = this.Gesture;
			gesture4.MaximumNumberOfTouchesToTrack = this.MaximumNumberOfTouchesToTrack;
			T gesture5 = this.Gesture;
			gesture5.ClearTrackedTouchesOnEndOrFail = this.ClearTrackedTouchesOnEndOrFail;
			if (this.AllowSimultaneousExecutionWithAllGestures)
			{
				T gesture6 = this.Gesture;
				gesture6.AllowSimultaneousExecutionWithAllGestures();
			}
			else if (this.AllowSimultaneousExecutionWith != null)
			{
				foreach (GestureRecognizerComponentScriptBase current in this.AllowSimultaneousExecutionWith)
				{
					T gesture7 = this.Gesture;
					gesture7.AllowSimultaneousExecution(current.GestureBase);
				}
			}
			FingersScript.Instance.AddGesture(this.Gesture);
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void OnEnable()
		{
			if (FingersScript.HasInstance)
			{
				FingersScript.Instance.AddGesture(this.Gesture);
			}
		}

		protected virtual void OnDisable()
		{
			if (FingersScript.HasInstance)
			{
				FingersScript.Instance.RemoveGesture(this.Gesture);
			}
		}

		protected virtual void OnDestroy()
		{
		}
	}
}
