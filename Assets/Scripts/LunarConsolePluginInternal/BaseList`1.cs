using System;
using System.Collections.Generic;

namespace LunarConsolePluginInternal
{
	internal abstract class BaseList<T> where T : class
	{
		protected readonly List<T> list;

		private readonly T nullElement;

		private int removedCount;

		private bool locked;

		public virtual int Count
		{
			get
			{
				return this.list.Count - this.removedCount;
			}
		}

		protected BaseList(T nullElement) : this(nullElement, 0)
		{
		}

		protected BaseList(T nullElement, int capacity) : this(new List<T>(capacity), nullElement)
		{
			if (nullElement == null)
			{
				throw new ArgumentNullException("nullElement");
			}
		}

		protected BaseList(List<T> list, T nullElement)
		{
			this.list = list;
			this.nullElement = nullElement;
		}

		public virtual bool Add(T e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			this.list.Add(e);
			return true;
		}

		public virtual bool Remove(T e)
		{
			int num = this.list.IndexOf(e);
			if (num != -1)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		public virtual T Get(int index)
		{
			return this.list[index];
		}

		public virtual int IndexOf(T e)
		{
			return this.list.IndexOf(e);
		}

		public virtual void RemoveAt(int index)
		{
			if (this.locked)
			{
				this.removedCount++;
				this.list[index] = this.nullElement;
			}
			else
			{
				this.list.RemoveAt(index);
			}
		}

		public virtual void Clear()
		{
			if (this.locked)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					this.list[i] = this.nullElement;
				}
				this.removedCount = this.list.Count;
			}
			else
			{
				this.list.Clear();
				this.removedCount = 0;
			}
		}

		public virtual bool Contains(T e)
		{
			return this.list.Contains(e);
		}

		private void ClearRemoved()
		{
			int num = this.list.Count - 1;
			while (this.removedCount > 0 && num >= 0)
			{
				if (this.list[num] == this.nullElement)
				{
					this.list.RemoveAt(num);
					this.removedCount--;
				}
				num--;
			}
		}

		protected void Lock()
		{
			this.locked = true;
		}

		protected void Unlock()
		{
			this.ClearRemoved();
			this.locked = false;
		}
	}
}
