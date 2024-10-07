using LunarConsolePlugin;
using System;
using System.Reflection;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	[Serializable]
	public class LunarConsoleLegacyAction
	{
		private static readonly object[] kEmptyArgs = new object[0];

		[SerializeField]
		private string m_name;

		[SerializeField]
		private GameObject m_target;

		[SerializeField]
		private string m_componentTypeName;

		[SerializeField]
		private string m_componentMethodName;

		private Type m_componentType;

		private MethodInfo m_componentMethod;

		public void Register()
		{
			if (string.IsNullOrEmpty(this.m_name))
			{
				Log.w("Unable to register action: name is null or empty");
			}
			else if (this.m_target == null)
			{
				Log.w("Unable to register action '{0}': target GameObject is missing", new object[]
				{
					this.m_name
				});
			}
			else if (string.IsNullOrEmpty(this.m_componentMethodName))
			{
				Log.w("Unable to register action '{0}' for '{1}': function is missing", new object[]
				{
					this.m_name,
					this.m_target.name
				});
			}
			else
			{
				LunarConsole.RegisterAction(this.m_name, new Action(this.Invoke));
			}
		}

		public void Unregister()
		{
			LunarConsole.UnregisterAction(new Action(this.Invoke));
		}

		private void Invoke()
		{
			if (this.m_target == null)
			{
				Log.e("Can't invoke action '{0}': target is not set", new object[]
				{
					this.m_name
				});
				return;
			}
			if (this.m_componentTypeName == null)
			{
				Log.e("Can't invoke action '{0}': method is not set", new object[]
				{
					this.m_name
				});
				return;
			}
			if (this.m_componentMethodName == null)
			{
				Log.e("Can't invoke action '{0}': method is not set", new object[]
				{
					this.m_name
				});
				return;
			}
			if ((this.m_componentType == null || this.m_componentMethod == null) && !this.ResolveInvocation())
			{
				return;
			}
			Component component = this.m_target.GetComponent(this.m_componentType);
			if (component == null)
			{
				Log.w("Missing component {0}", new object[]
				{
					this.m_componentType
				});
				return;
			}
			try
			{
				this.m_componentMethod.Invoke(component, LunarConsoleLegacyAction.kEmptyArgs);
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
		}

		private bool ResolveInvocation()
		{
			bool result;
			try
			{
				this.m_componentType = Type.GetType(this.m_componentTypeName);
				if (this.m_componentType == null)
				{
					Log.w("Can't resolve type {0}", new object[]
					{
						this.m_componentTypeName
					});
					result = false;
				}
				else
				{
					this.m_componentMethod = this.m_componentType.GetMethod(this.m_componentMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (this.m_componentMethod == null)
					{
						Log.w("Can't resolve method {0} of type {1}", new object[]
						{
							this.m_componentMethod,
							this.m_componentType
						});
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception);
				result = false;
			}
			return result;
		}

		public void Validate()
		{
			if (string.IsNullOrEmpty(this.m_name))
			{
				Log.w("Missing action name");
			}
			if (this.m_target == null)
			{
				Log.w("Missing action target");
			}
			if (this.m_componentType != null && this.m_componentMethodName != null)
			{
				this.ResolveInvocation();
			}
		}
	}
}
