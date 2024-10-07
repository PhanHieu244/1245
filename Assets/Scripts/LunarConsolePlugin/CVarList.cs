using System;
using System.Collections;
using System.Collections.Generic;

namespace LunarConsolePlugin
{
	public class CVarList : IEnumerable<CVar>, IEnumerable
	{
		private readonly List<CVar> m_variables;

		private readonly Dictionary<int, CVar> m_lookupById;

		public int Count
		{
			get
			{
				return this.m_variables.Count;
			}
		}

		public CVarList()
		{
			this.m_variables = new List<CVar>();
			this.m_lookupById = new Dictionary<int, CVar>();
		}

		public void Add(CVar variable)
		{
			this.m_variables.Add(variable);
			this.m_lookupById.Add(variable.Id, variable);
		}

		public bool Remove(int id)
		{
			CVar item;
			if (this.m_lookupById.TryGetValue(id, out item))
			{
				this.m_lookupById.Remove(id);
				this.m_variables.Remove(item);
				return true;
			}
			return false;
		}

		public CVar Find(int id)
		{
			CVar cVar;
			return (!this.m_lookupById.TryGetValue(id, out cVar)) ? null : cVar;
		}

		public CVar Find(string name)
		{
			foreach (CVar current in this.m_variables)
			{
				if (current.Name == name)
				{
					return current;
				}
			}
			return null;
		}

		public void Clear()
		{
			this.m_variables.Clear();
			this.m_lookupById.Clear();
		}

		public IEnumerator<CVar> GetEnumerator()
		{
			return this.m_variables.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_variables.GetEnumerator();
		}
	}
}
