using System;
using System.Collections.Generic;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	[Obsolete("Use 'Lunar Console Action' instead")]
	public class LunarConsoleLegacyActions : MonoBehaviour
	{
		[SerializeField]
		private bool m_dontDestroyOnLoad;

		[HideInInspector, SerializeField]
		private List<LunarConsoleLegacyAction> m_actions;

		public List<LunarConsoleLegacyAction> actions
		{
			get
			{
				return this.m_actions;
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
			if (this.m_dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		private void Start()
		{
			if (this.actionsEnabled)
			{
				foreach (LunarConsoleLegacyAction current in this.m_actions)
				{
					current.Register();
				}
			}
			else
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		private void OnDestroy()
		{
			if (this.actionsEnabled)
			{
				foreach (LunarConsoleLegacyAction current in this.m_actions)
				{
					current.Unregister();
				}
			}
		}

		public void AddAction(LunarConsoleLegacyAction action)
		{
			this.m_actions.Add(action);
		}
	}
}
