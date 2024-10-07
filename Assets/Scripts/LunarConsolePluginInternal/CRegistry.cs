using LunarConsolePlugin;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LunarConsolePluginInternal
{
	public class CRegistry
	{
		private sealed class _Unregister_c__AnonStorey0
		{
			internal string name;

			internal bool __m__0(CAction action)
			{
				return action.Name == this.name;
			}
		}

		private sealed class _Unregister_c__AnonStorey1
		{
			internal int id;

			internal bool __m__0(CAction action)
			{
				return action.Id == this.id;
			}
		}

		private sealed class _Unregister_c__AnonStorey2
		{
			internal Delegate del;

			internal bool __m__0(CAction action)
			{
				return action.ActionDelegate == this.del;
			}
		}

		private sealed class _UnregisterAll_c__AnonStorey3
		{
			internal object target;

			internal bool __m__0(CAction action)
			{
				return action.ActionDelegate.Target == this.target;
			}
		}

		private readonly CActionList m_actions = new CActionList();

		private readonly CVarList m_vars = new CVarList();

		private ICRegistryDelegate m_delegate;

		public ICRegistryDelegate registryDelegate
		{
			get
			{
				return this.m_delegate;
			}
			set
			{
				this.m_delegate = value;
			}
		}

		public CActionList actions
		{
			get
			{
				return this.m_actions;
			}
		}

		public CVarList cvars
		{
			get
			{
				return this.m_vars;
			}
		}

		public CAction RegisterAction(string name, Delegate actionDelegate)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Action's name is empty");
			}
			if (actionDelegate == null)
			{
				throw new ArgumentNullException("actionDelegate");
			}
			CAction cAction = this.m_actions.Find(name);
			if (cAction != null)
			{
				cAction.ActionDelegate = actionDelegate;
			}
			else
			{
				cAction = new CAction(name, actionDelegate);
				this.m_actions.Add(cAction);
				if (this.m_delegate != null)
				{
					this.m_delegate.OnActionRegistered(this, cAction);
				}
			}
			return cAction;
		}

		public bool Unregister(string name)
		{
			return this.Unregister((CAction action) => action.Name == name);
		}

		public bool Unregister(int id)
		{
			return this.Unregister((CAction action) => action.Id == id);
		}

		public bool Unregister(Delegate del)
		{
			return this.Unregister((CAction action) => action.ActionDelegate == del);
		}

		public bool UnregisterAll(object target)
		{
			return target != null && this.Unregister((CAction action) => action.ActionDelegate.Target == target);
		}

		private bool Unregister(CActionFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			IList<CAction> list = new List<CAction>();
			foreach (CAction current in this.m_actions)
			{
				if (filter(current))
				{
					list.Add(current);
				}
			}
			foreach (CAction current2 in list)
			{
				this.RemoveAction(current2);
			}
			return list.Count > 0;
		}

		private bool RemoveAction(CAction action)
		{
			if (this.m_actions.Remove(action.Id))
			{
				if (this.m_delegate != null)
				{
					this.m_delegate.OnActionUnregistered(this, action);
				}
				return true;
			}
			return false;
		}

		public CAction FindAction(int id)
		{
			return this.m_actions.Find(id);
		}

		public void Register(CVar cvar)
		{
			this.m_vars.Add(cvar);
			if (this.m_delegate != null)
			{
				this.m_delegate.OnVariableRegistered(this, cvar);
			}
		}

		public CVar FindVariable(int variableId)
		{
			return this.m_vars.Find(variableId);
		}

		public CVar FindVariable(string variableName)
		{
			return this.m_vars.Find(variableName);
		}

		public void Destroy()
		{
			this.m_actions.Clear();
			this.m_vars.Clear();
			this.m_delegate = null;
		}
	}
}
