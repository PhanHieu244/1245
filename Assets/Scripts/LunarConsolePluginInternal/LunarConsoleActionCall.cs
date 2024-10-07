using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	[Serializable]
	public class LunarConsoleActionCall
	{
		private sealed class _ResolveMethod_c__AnonStorey0
		{
			internal string methodName;

			internal Type paramType;

			internal bool __m__0(MethodInfo method)
			{
				if (method.Name != this.methodName)
				{
					return false;
				}
				ParameterInfo[] parameters = method.GetParameters();
				if (this.paramType == typeof(void))
				{
					return parameters.Length == 0;
				}
				return parameters.Length == 1 && (parameters[0].ParameterType == this.paramType || parameters[0].ParameterType.IsSubclassOf(this.paramType));
			}
		}

		private static readonly Type[] kParamTypes = new Type[]
		{
			typeof(int),
			typeof(float),
			typeof(string),
			typeof(bool)
		};

		[SerializeField]
		private UnityEngine.Object m_target;

		[SerializeField]
		private string m_methodName;

		[SerializeField]
		private LunarPersistentListenerMode m_mode;

		[SerializeField]
		private LunarArgumentCache m_arguments;

		private static ListMethodsFilter __f__mg_cache0;

		public UnityEngine.Object target
		{
			get
			{
				return this.m_target;
			}
		}

		public string methodName
		{
			get
			{
				return this.m_methodName;
			}
		}

		public LunarPersistentListenerMode mode
		{
			get
			{
				return this.m_mode;
			}
		}

		public void Invoke()
		{
			MethodInfo methodInfo;
			object[] parameters;
			switch (this.m_mode)
			{
			case LunarPersistentListenerMode.Void:
				methodInfo = LunarConsoleActionCall.ResolveMethod(this.m_target, this.m_methodName, typeof(void));
				parameters = new object[0];
				break;
			case LunarPersistentListenerMode.Bool:
				methodInfo = LunarConsoleActionCall.ResolveMethod(this.m_target, this.m_methodName, typeof(bool));
				parameters = new object[]
				{
					this.m_arguments.boolArgument
				};
				break;
			case LunarPersistentListenerMode.Int:
				methodInfo = LunarConsoleActionCall.ResolveMethod(this.m_target, this.m_methodName, typeof(int));
				parameters = new object[]
				{
					this.m_arguments.intArgument
				};
				break;
			case LunarPersistentListenerMode.Float:
				methodInfo = LunarConsoleActionCall.ResolveMethod(this.m_target, this.m_methodName, typeof(float));
				parameters = new object[]
				{
					this.m_arguments.floatArgument
				};
				break;
			case LunarPersistentListenerMode.String:
				methodInfo = LunarConsoleActionCall.ResolveMethod(this.m_target, this.m_methodName, typeof(string));
				parameters = new object[]
				{
					this.m_arguments.stringArgument
				};
				break;
			case LunarPersistentListenerMode.Object:
				methodInfo = LunarConsoleActionCall.ResolveMethod(this.m_target, this.m_methodName, typeof(UnityEngine.Object));
				parameters = new object[]
				{
					this.m_arguments.unityObjectArgument
				};
				break;
			default:
				Log.e("Unable to invoke action: unexpected invoke mode '{0}'", new object[]
				{
					this.m_mode
				});
				return;
			}
			if (methodInfo != null)
			{
				methodInfo.Invoke(this.m_target, parameters);
			}
			else
			{
				Log.e("Unable to invoke action: can't resolve method '{0}'", new object[]
				{
					this.m_methodName
				});
			}
		}

		private static MethodInfo ResolveMethod(object target, string methodName, Type paramType)
		{
			List<MethodInfo> list = ClassUtils.ListInstanceMethods(target.GetType(), delegate(MethodInfo method)
			{
				if (method.Name != methodName)
				{
					return false;
				}
				ParameterInfo[] parameters = method.GetParameters();
				if (paramType == typeof(void))
				{
					return parameters.Length == 0;
				}
				return parameters.Length == 1 && (parameters[0].ParameterType == paramType || parameters[0].ParameterType.IsSubclassOf(paramType));
			});
			return (list.Count != 1) ? null : list[0];
		}

		public static bool IsPersistantListenerValid(UnityEngine.Object target, string methodName, LunarPersistentListenerMode mode)
		{
			if (target == null)
			{
				return false;
			}
			List<MethodInfo> list = LunarConsoleActionCall.ListActionMethods(target);
			foreach (MethodInfo current in list)
			{
				if (current.Name == methodName)
				{
					ParameterInfo[] parameters = current.GetParameters();
					if (mode == LunarPersistentListenerMode.Void)
					{
						if (parameters.Length == 0)
						{
							bool result = true;
							return result;
						}
					}
					else if (parameters.Length == 1)
					{
						Type parameterType = parameters[0].ParameterType;
						if (mode == LunarPersistentListenerMode.Bool && parameterType == typeof(bool))
						{
							bool result = true;
							return result;
						}
						if (mode == LunarPersistentListenerMode.Float && parameterType == typeof(float))
						{
							bool result = true;
							return result;
						}
						if (mode == LunarPersistentListenerMode.Int && parameterType == typeof(int))
						{
							bool result = true;
							return result;
						}
						if (mode == LunarPersistentListenerMode.String && parameterType == typeof(string))
						{
							bool result = true;
							return result;
						}
						if (mode == LunarPersistentListenerMode.Object && parameterType.IsSubclassOf(typeof(UnityEngine.Object)))
						{
							bool result = true;
							return result;
						}
					}
				}
			}
			return false;
		}

		public static List<MethodInfo> ListActionMethods(object target)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			List<MethodInfo> arg_2C_0 = list;
			Type arg_2C_1 = target.GetType();
			if (LunarConsoleActionCall.__f__mg_cache0 == null)
			{
				LunarConsoleActionCall.__f__mg_cache0 = new ListMethodsFilter(LunarConsoleActionCall.IsValidActionMethod);
			}
			ClassUtils.ListMethods(arg_2C_0, arg_2C_1, LunarConsoleActionCall.__f__mg_cache0, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			return list;
		}

		private static bool IsValidActionMethod(MethodInfo method)
		{
			if (!method.IsPublic)
			{
				return false;
			}
			if (method.ReturnType != typeof(void))
			{
				return false;
			}
			if (method.IsAbstract)
			{
				return false;
			}
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length > 1)
			{
				return false;
			}
			object[] customAttributes = method.GetCustomAttributes(typeof(ObsoleteAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				return false;
			}
			if (parameters.Length == 1)
			{
				Type parameterType = parameters[0].ParameterType;
				if (!parameterType.IsSubclassOf(typeof(UnityEngine.Object)) && Array.IndexOf<Type>(LunarConsoleActionCall.kParamTypes, parameterType) == -1)
				{
					return false;
				}
			}
			return true;
		}
	}
}
