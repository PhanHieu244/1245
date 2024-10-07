using System;
using System.Collections;
using System.Collections.Generic;

namespace LunarConsolePluginInternal
{
	public class CActionList : IEnumerable<CAction>, IEnumerable
	{
		private readonly List<CAction> m_actions;

		private readonly Dictionary<int, CAction> m_actionLookupById;

		private readonly Dictionary<string, CAction> m_actionLookupByName;

		public CActionList()
		{
			this.m_actions = new List<CAction>();
			this.m_actionLookupById = new Dictionary<int, CAction>();
			this.m_actionLookupByName = new Dictionary<string, CAction>();
		}

		public void Add(CAction action)
		{
			this.m_actions.Add(action);
			this.m_actionLookupById.Add(action.Id, action);
			this.m_actionLookupByName.Add(action.Name, action);
		}

		public bool Remove(int id)
		{
			CAction cAction;
			if (this.m_actionLookupById.TryGetValue(id, out cAction))
			{
				this.m_actionLookupById.Remove(id);
				this.m_actionLookupByName.Remove(cAction.Name);
				this.m_actions.Remove(cAction);
				return true;
			}
			return false;
		}

		public CAction Find(string name)
		{
			CAction cAction;
			return (!this.m_actionLookupByName.TryGetValue(name, out cAction)) ? null : cAction;
		}

		public CAction Find(int id)
		{
			CAction cAction;
			return (!this.m_actionLookupById.TryGetValue(id, out cAction)) ? null : cAction;
		}

		public void Clear()
		{
			this.m_actions.Clear();
			this.m_actionLookupById.Clear();
			this.m_actionLookupByName.Clear();
		}

		public IEnumerator<CAction> GetEnumerator()
		{
			return this.m_actions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_actions.GetEnumerator();
		}
	}
}
