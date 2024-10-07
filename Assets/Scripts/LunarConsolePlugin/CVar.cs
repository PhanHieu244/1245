using LunarConsolePluginInternal;
using System;

namespace LunarConsolePlugin
{
	public class CVar : IEquatable<CVar>, IComparable<CVar>
	{
		private static int s_nextId;

		private readonly int m_id;

		private readonly string m_name;

		private readonly CVarType m_type;

		private readonly CFlags m_flags;

		private CValue m_value;

		private CValue m_defaultValue;

		private CVarValueRange m_range = CVarValueRange.Undefined;

		private CVarChangedDelegateList m_delegateList;

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

		public CVarType Type
		{
			get
			{
				return this.m_type;
			}
		}

		public string DefaultValue
		{
			get
			{
				return this.m_defaultValue.stringValue;
			}
		}

		public bool IsString
		{
			get
			{
				return this.m_type == CVarType.String;
			}
		}

		public string Value
		{
			get
			{
				return this.m_value.stringValue;
			}
			set
			{
				bool flag = this.m_value.stringValue != value;
				this.m_value.stringValue = value;
				this.m_value.floatValue = ((!this.IsInt && !this.IsFloat) ? 0f : StringUtils.ParseFloat(value, 0f));
				this.m_value.intValue = ((!this.IsInt && !this.IsFloat) ? 0 : ((int)this.FloatValue));
				if (flag)
				{
					this.NotifyValueChanged();
				}
			}
		}

		public CVarValueRange Range
		{
			get
			{
				return this.m_range;
			}
			set
			{
				this.m_range = value;
			}
		}

		public bool HasRange
		{
			get
			{
				return this.m_range.IsValid;
			}
		}

		public bool IsInt
		{
			get
			{
				return this.m_type == CVarType.Integer || this.m_type == CVarType.Boolean;
			}
		}

		public int IntValue
		{
			get
			{
				return this.m_value.intValue;
			}
			set
			{
				bool flag = this.m_value.intValue != value;
				this.m_value.stringValue = StringUtils.ToString(value);
				this.m_value.intValue = value;
				this.m_value.floatValue = (float)value;
				if (flag)
				{
					this.NotifyValueChanged();
				}
			}
		}

		public bool IsFloat
		{
			get
			{
				return this.m_type == CVarType.Float;
			}
		}

		public float FloatValue
		{
			get
			{
				return this.m_value.floatValue;
			}
			set
			{
				float floatValue = this.m_value.floatValue;
				this.m_value.stringValue = StringUtils.ToString(value);
				this.m_value.intValue = (int)value;
				this.m_value.floatValue = value;
				if (floatValue != value)
				{
					this.NotifyValueChanged();
				}
			}
		}

		public bool IsBool
		{
			get
			{
				return this.m_type == CVarType.Boolean;
			}
		}

		public bool BoolValue
		{
			get
			{
				return this.m_value.intValue != 0;
			}
			set
			{
				this.IntValue = ((!value) ? 0 : 1);
			}
		}

		public bool IsDefault
		{
			get
			{
				return this.m_value.Equals(this.m_defaultValue);
			}
			set
			{
				bool flag = this.IsDefault ^ value;
				this.m_value = this.m_defaultValue;
				if (flag)
				{
					this.NotifyValueChanged();
				}
			}
		}

		public CFlags Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		public CVar(string name, bool defaultValue, CFlags flags = CFlags.None) : this(name, CVarType.Boolean, flags)
		{
			this.IntValue = ((!defaultValue) ? 0 : 1);
			this.m_defaultValue = this.m_value;
		}

		public CVar(string name, int defaultValue, CFlags flags = CFlags.None) : this(name, CVarType.Integer, flags)
		{
			this.IntValue = defaultValue;
			this.m_defaultValue = this.m_value;
		}

		public CVar(string name, float defaultValue, CFlags flags = CFlags.None) : this(name, CVarType.Float, flags)
		{
			this.FloatValue = defaultValue;
			this.m_defaultValue = this.m_value;
		}

		public CVar(string name, string defaultValue, CFlags flags = CFlags.None) : this(name, CVarType.String, flags)
		{
			this.Value = defaultValue;
			this.m_defaultValue = this.m_value;
		}

		private CVar(string name, CVarType type, CFlags flags)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_id = ++CVar.s_nextId;
			this.m_name = name;
			this.m_type = type;
			this.m_flags = flags;
		}

		public void AddDelegate(CVarChangedDelegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			if (this.m_delegateList == null)
			{
				this.m_delegateList = new CVarChangedDelegateList(1);
				this.m_delegateList.Add(del);
			}
			else if (!this.m_delegateList.Contains(del))
			{
				this.m_delegateList.Add(del);
			}
		}

		public void RemoveDelegate(CVarChangedDelegate del)
		{
			if (del != null && this.m_delegateList != null)
			{
				this.m_delegateList.Remove(del);
				if (this.m_delegateList.Count == 0)
				{
					this.m_delegateList = null;
				}
			}
		}

		public void RemoveDelegates(object target)
		{
			if (target != null && this.m_delegateList != null)
			{
				for (int i = this.m_delegateList.Count - 1; i >= 0; i--)
				{
					if (this.m_delegateList.Get(i).Target == target)
					{
						this.m_delegateList.RemoveAt(i);
					}
				}
				if (this.m_delegateList.Count == 0)
				{
					this.m_delegateList = null;
				}
			}
		}

		private void NotifyValueChanged()
		{
			if (this.m_delegateList != null && this.m_delegateList.Count > 0)
			{
				this.m_delegateList.NotifyValueChanged(this);
			}
		}

		public bool Equals(CVar other)
		{
			return other != null && other.m_name == this.m_name && other.m_value.Equals(ref this.m_value) && other.m_defaultValue.Equals(ref this.m_defaultValue) && other.m_type == this.m_type;
		}

		public int CompareTo(CVar other)
		{
			return this.Name.CompareTo(other.Name);
		}

		public bool HasFlag(CFlags flag)
		{
			return (this.m_flags & flag) != CFlags.None;
		}

		public static implicit operator string(CVar cvar)
		{
			return cvar.m_value.stringValue;
		}

		public static implicit operator int(CVar cvar)
		{
			return cvar.m_value.intValue;
		}

		public static implicit operator float(CVar cvar)
		{
			return cvar.m_value.floatValue;
		}

		public static implicit operator bool(CVar cvar)
		{
			return cvar.m_value.intValue != 0;
		}
	}
}
