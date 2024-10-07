using System;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	[Serializable]
	internal class LunarArgumentCache : ISerializationCallbackReceiver
	{
		[SerializeField]
		private UnityEngine.Object m_objectArgument;

		[SerializeField]
		private string m_objectArgumentAssemblyTypeName;

		[SerializeField]
		private int m_intArgument;

		[SerializeField]
		private float m_floatArgument;

		[SerializeField]
		private string m_stringArgument;

		[SerializeField]
		private bool m_boolArgument;

		public UnityEngine.Object unityObjectArgument
		{
			get
			{
				return this.m_objectArgument;
			}
			set
			{
				this.m_objectArgument = value;
				this.m_objectArgumentAssemblyTypeName = ((value != null) ? value.GetType().AssemblyQualifiedName : string.Empty);
			}
		}

		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_objectArgumentAssemblyTypeName;
			}
		}

		public int intArgument
		{
			get
			{
				return this.m_intArgument;
			}
			set
			{
				this.m_intArgument = value;
			}
		}

		public float floatArgument
		{
			get
			{
				return this.m_floatArgument;
			}
			set
			{
				this.m_floatArgument = value;
			}
		}

		public string stringArgument
		{
			get
			{
				return this.m_stringArgument;
			}
			set
			{
				this.m_stringArgument = value;
			}
		}

		public bool boolArgument
		{
			get
			{
				return this.m_boolArgument;
			}
			set
			{
				this.m_boolArgument = value;
			}
		}

		private void TidyAssemblyTypeName()
		{
			if (!string.IsNullOrEmpty(this.m_objectArgumentAssemblyTypeName))
			{
				int num = 2147483647;
				int num2 = this.m_objectArgumentAssemblyTypeName.IndexOf(", Version=");
				if (num2 != -1)
				{
					num = Math.Min(num2, num);
				}
				num2 = this.m_objectArgumentAssemblyTypeName.IndexOf(", Culture=");
				if (num2 != -1)
				{
					num = Math.Min(num2, num);
				}
				num2 = this.m_objectArgumentAssemblyTypeName.IndexOf(", PublicKeyToken=");
				if (num2 != -1)
				{
					num = Math.Min(num2, num);
				}
				if (num != 2147483647)
				{
					this.m_objectArgumentAssemblyTypeName = this.m_objectArgumentAssemblyTypeName.Substring(0, num);
				}
			}
		}

		public void OnBeforeSerialize()
		{
			this.TidyAssemblyTypeName();
		}

		public void OnAfterDeserialize()
		{
			this.TidyAssemblyTypeName();
		}
	}
}
