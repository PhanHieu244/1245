using System;
using System.Reflection;

namespace LunarConsolePluginInternal
{
	public class CAction : IComparable<CAction>
	{
		private static readonly string[] kEmptyArgs = new string[0];

		private static int s_nextActionId;

		private readonly int m_id;

		private readonly string m_name;

		private Delegate m_actionDelegate;

		public int Id
		{
			get
			{
				return this.m_id;
			}
		}

		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		public Delegate ActionDelegate
		{
			get
			{
				return this.m_actionDelegate;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("actionDelegate");
				}
				this.m_actionDelegate = value;
			}
		}

		public CAction(string name, Delegate actionDelegate)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Action name is empty");
			}
			if (actionDelegate == null)
			{
				throw new ArgumentNullException("actionDelegate");
			}
			this.m_id = CAction.s_nextActionId++;
			this.m_name = name;
			this.m_actionDelegate = actionDelegate;
		}

		public bool Execute()
		{
			try
			{
				return ReflectionUtils.Invoke(this.ActionDelegate, CAction.kEmptyArgs);
			}
			catch (TargetInvocationException ex)
			{
				Log.e(ex.InnerException, "Exception while invoking action '{0}'", new object[]
				{
					this.m_name
				});
			}
			catch (Exception exception)
			{
				Log.e(exception, "Exception while invoking action '{0}'", new object[]
				{
					this.m_name
				});
			}
			return false;
		}

		internal bool StartsWith(string prefix)
		{
			return StringUtils.StartsWithIgnoreCase(this.Name, prefix);
		}

		public int CompareTo(CAction other)
		{
			return this.Name.CompareTo(other.Name);
		}

		public override string ToString()
		{
			return string.Format("{0} ({1})", this.Name, this.ActionDelegate);
		}
	}
}
