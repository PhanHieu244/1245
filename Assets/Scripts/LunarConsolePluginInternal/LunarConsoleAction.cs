using LunarConsolePlugin;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	public class LunarConsoleAction : MonoBehaviour
	{
		[SerializeField]
		private string m_title = "Untitled Action";

		[HideInInspector, SerializeField]
		private List<LunarConsoleActionCall> m_calls;

		public List<LunarConsoleActionCall> calls
		{
			get
			{
				return this.m_calls;
			}
		}

		private bool actionsEnabled
		{
			get
			{
				return LunarConsoleConfig.actionsEnabled;
			}
		}

		private void Awake()
		{
			if (!this.actionsEnabled)
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		private void Start()
		{
			if (this.actionsEnabled)
			{
				this.RegisterAction();
			}
			else
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		private void OnValidate()
		{
			if (this.m_calls.Count > 0)
			{
				foreach (LunarConsoleActionCall current in this.m_calls)
				{
					this.Validate(current);
				}
			}
		}

		private void Validate(LunarConsoleActionCall call)
		{
			if (call.target == null)
			{
				UnityEngine.Debug.LogWarning(string.Format("Action '{0}' ({1}) is missing a target object", this.m_title, base.gameObject.name), base.gameObject);
			}
			else if (!LunarConsoleActionCall.IsPersistantListenerValid(call.target, call.methodName, call.mode))
			{
				UnityEngine.Debug.LogWarning(string.Format("Action '{0}' ({1}) is missing a handler <{2}.{3} ({4})>", new object[]
				{
					this.m_title,
					base.gameObject.name,
					call.target.GetType(),
					call.methodName,
					LunarConsoleAction.ModeParamTypeName(call.mode)
				}), base.gameObject);
			}
		}

		private void OnDestroy()
		{
			if (this.actionsEnabled)
			{
				this.UnregisterAction();
			}
		}

		private void RegisterAction()
		{
			LunarConsole.RegisterAction(this.m_title, new Action(this.InvokeAction));
		}

		private void UnregisterAction()
		{
			LunarConsole.UnregisterAction(new Action(this.InvokeAction));
		}

		private void InvokeAction()
		{
			if (this.m_calls != null && this.m_calls.Count > 0)
			{
				foreach (LunarConsoleActionCall current in this.m_calls)
				{
					current.Invoke();
				}
			}
			else
			{
				UnityEngine.Debug.LogWarningFormat("Action '{0}' has 0 calls", new object[]
				{
					this.m_title
				});
			}
		}

		private static string ModeParamTypeName(LunarPersistentListenerMode mode)
		{
			switch (mode)
			{
			case LunarPersistentListenerMode.Void:
				return string.Empty;
			case LunarPersistentListenerMode.Bool:
				return "bool";
			case LunarPersistentListenerMode.Int:
				return "int";
			case LunarPersistentListenerMode.Float:
				return "float";
			case LunarPersistentListenerMode.String:
				return "string";
			case LunarPersistentListenerMode.Object:
				return "UnityEngine.Object";
			default:
				return "???";
			}
		}
	}
}
