using System;
using System.Collections.Generic;

namespace LunarConsolePluginInternal
{
	internal class Iterator<T>
	{
		private IList<T> m_target;

		private int m_current;

		public int Count
		{
			get
			{
				return this.m_target.Count;
			}
		}

		public int Position
		{
			get
			{
				return this.m_current;
			}
			set
			{
				if (value < -1 || value >= this.Count)
				{
					throw new IndexOutOfRangeException("Invalid position: " + value);
				}
				this.m_current = value;
			}
		}

		public Iterator(IList<T> target)
		{
			this.Init(target);
		}

		public void Init(IList<T> target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.m_target = target;
			this.m_current = -1;
		}

		public bool HasNext(int items = 1)
		{
			return this.m_current + items < this.Count;
		}

		public bool HasPrev(int items = 1)
		{
			return this.m_current - items >= 0;
		}

		public void Skip(int items = 1)
		{
			this.m_current += items;
		}

		public bool TrySkip(int items = 1)
		{
			if (this.HasNext(items))
			{
				this.Skip(items);
				return true;
			}
			return false;
		}

		public T Current()
		{
			return this.m_target[this.m_current];
		}

		public T Next()
		{
			this.m_current++;
			return this.Current();
		}

		public T Prev()
		{
			this.m_current--;
			return this.Current();
		}

		public void Reset()
		{
			this.m_current = -1;
		}
	}
}
