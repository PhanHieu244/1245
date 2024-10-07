using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	internal static class ReflectionUtils
	{
		private sealed class _FindAttributeTypes_c__AnonStorey0
		{
			internal Type attributeType;

			internal bool __m__0(Type type)
			{
				object[] customAttributes = type.GetCustomAttributes(this.attributeType, false);
				return customAttributes != null && customAttributes.Length > 0;
			}
		}

		private static readonly object[] EMPTY_INVOKE_ARGS = new object[0];

		public static bool Invoke(Delegate del, string[] invokeArgs)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return ReflectionUtils.Invoke(del.Target, del.Method, invokeArgs);
		}

		public static bool Invoke(object target, MethodInfo method, string[] invokeArgs)
		{
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length == 0)
			{
				return ReflectionUtils.Invoke(target, method, ReflectionUtils.EMPTY_INVOKE_ARGS);
			}
			List<object> list = new List<object>(invokeArgs.Length);
			Iterator<string> iter = new Iterator<string>(invokeArgs);
			ParameterInfo[] array = parameters;
			for (int i = 0; i < array.Length; i++)
			{
				ParameterInfo param = array[i];
				list.Add(ReflectionUtils.ResolveInvokeParameter(param, iter));
			}
			return ReflectionUtils.Invoke(target, method, list.ToArray());
		}

		private static bool Invoke(object target, MethodInfo method, object[] args)
		{
			if (method.ReturnType == typeof(bool))
			{
				return (bool)method.Invoke(target, args);
			}
			method.Invoke(target, args);
			return true;
		}

		private static object ResolveInvokeParameter(ParameterInfo param, Iterator<string> iter)
		{
			if (param.IsOptional && !iter.HasNext(1))
			{
				return param.DefaultValue;
			}
			Type parameterType = param.ParameterType;
			if (parameterType == typeof(string[]))
			{
				List<string> list = new List<string>();
				while (iter.HasNext(1))
				{
					list.Add(ReflectionUtils.NextArg(iter));
				}
				return list.ToArray();
			}
			if (parameterType == typeof(string))
			{
				return ReflectionUtils.NextArg(iter);
			}
			if (parameterType == typeof(float))
			{
				return ReflectionUtils.NextFloatArg(iter);
			}
			if (parameterType == typeof(int))
			{
				return ReflectionUtils.NextIntArg(iter);
			}
			if (parameterType == typeof(bool))
			{
				return ReflectionUtils.NextBoolArg(iter);
			}
			if (parameterType == typeof(Vector2))
			{
				float x = ReflectionUtils.NextFloatArg(iter);
				float y = ReflectionUtils.NextFloatArg(iter);
				return new Vector2(x, y);
			}
			if (parameterType == typeof(Vector3))
			{
				float x2 = ReflectionUtils.NextFloatArg(iter);
				float y2 = ReflectionUtils.NextFloatArg(iter);
				float z = ReflectionUtils.NextFloatArg(iter);
				return new Vector3(x2, y2, z);
			}
			if (parameterType == typeof(Vector4))
			{
				float x3 = ReflectionUtils.NextFloatArg(iter);
				float y3 = ReflectionUtils.NextFloatArg(iter);
				float z2 = ReflectionUtils.NextFloatArg(iter);
				float w = ReflectionUtils.NextFloatArg(iter);
				return new Vector4(x3, y3, z2, w);
			}
			if (parameterType == typeof(int[]))
			{
				List<int> list2 = new List<int>();
				while (iter.HasNext(1))
				{
					list2.Add(ReflectionUtils.NextIntArg(iter));
				}
				return list2.ToArray();
			}
			if (parameterType == typeof(float[]))
			{
				List<float> list3 = new List<float>();
				while (iter.HasNext(1))
				{
					list3.Add(ReflectionUtils.NextFloatArg(iter));
				}
				return list3.ToArray();
			}
			if (parameterType == typeof(bool[]))
			{
				List<bool> list4 = new List<bool>();
				while (iter.HasNext(1))
				{
					list4.Add(ReflectionUtils.NextBoolArg(iter));
				}
				return list4.ToArray();
			}
			throw new ReflectionException("Unsupported value type: " + parameterType);
		}

		public static int NextIntArg(Iterator<string> iter)
		{
			string text = ReflectionUtils.NextArg(iter);
			int result;
			if (int.TryParse(text, out result))
			{
				return result;
			}
			throw new ReflectionException("Can't parse int arg: '" + text + "'");
		}

		public static float NextFloatArg(Iterator<string> iter)
		{
			string text = ReflectionUtils.NextArg(iter);
			float result;
			if (float.TryParse(text, out result))
			{
				return result;
			}
			throw new ReflectionException("Can't parse float arg: '" + text + "'");
		}

		public static bool NextBoolArg(Iterator<string> iter)
		{
			string text = ReflectionUtils.NextArg(iter).ToLower();
			if (text == "1" || text == "yes" || text == "true")
			{
				return true;
			}
			if (text == "0" || text == "no" || text == "false")
			{
				return false;
			}
			throw new ReflectionException("Can't parse bool arg: '" + text + "'");
		}

		public static string NextArg(Iterator<string> iter)
		{
			if (!iter.HasNext(1))
			{
				throw new ReflectionException("Unexpected end of args");
			}
			string text = StringUtils.UnArg(iter.Next());
			if (!ReflectionUtils.IsValidArg(text))
			{
				throw new ReflectionException("Invalid arg: " + text);
			}
			return text;
		}

		public static bool IsValidArg(string arg)
		{
			return true;
		}

		public static List<Type> FindAttributeTypes<T>(Assembly assembly) where T : Attribute
		{
			return ReflectionUtils.FindAttributeTypes(assembly, typeof(T));
		}

		public static List<Type> FindAttributeTypes(Assembly assembly, Type attributeType)
		{
			return ReflectionUtils.FindTypes(assembly, delegate(Type type)
			{
				object[] customAttributes = type.GetCustomAttributes(attributeType, false);
				return customAttributes != null && customAttributes.Length > 0;
			});
		}

		public static List<Type> FindTypes(Assembly assembly, ReflectionTypeFilter filter)
		{
			List<Type> list = new List<Type>();
			try
			{
				Type[] types = assembly.GetTypes();
				Type[] array = types;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i];
					if (filter(type))
					{
						list.Add(type);
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Unable to list types");
			}
			return list;
		}
	}
}
