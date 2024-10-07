using System;
using System.Collections.Generic;
using System.Reflection;

namespace LunarConsolePluginInternal
{
	public static class ClassUtils
	{
		public static T Cast<T>(object obj) where T : class
		{
			return obj as T;
		}

		public static T TryCast<T>(object obj) where T : class
		{
			return obj as T;
		}

		public static T CreateInstance<T>(Type t, params object[] args) where T : class
		{
			try
			{
				return (T)((object)Activator.CreateInstance(t, args));
			}
			catch (Exception exception)
			{
				Log.e(exception, "Exception while creating an instance of type '{0}'", new object[]
				{
					t
				});
			}
			return (T)((object)null);
		}

		public static bool IsValidEnumValue<T>(int value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		public static bool IsValidEnumValue<T>(T value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		public static string TypeShortName(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type == typeof(long))
			{
				return "long";
			}
			if (type == typeof(bool))
			{
				return "bool";
			}
			return type.Name;
		}

		public static List<MethodInfo> ListInstanceMethods(Type type, ListMethodsFilter filter)
		{
			return ClassUtils.ListInstanceMethods(new List<MethodInfo>(), type, filter);
		}

		public static List<MethodInfo> ListInstanceMethods(List<MethodInfo> outList, Type type, ListMethodsFilter filter)
		{
			return ClassUtils.ListMethods(outList, type, filter, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static List<MethodInfo> ListMethods(List<MethodInfo> outList, Type type, ListMethodsFilter filter, BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			MethodInfo[] methods = type.GetMethods(flags);
			if (filter == null)
			{
				outList.AddRange(methods);
				return outList;
			}
			MethodInfo[] array = methods;
			for (int i = 0; i < array.Length; i++)
			{
				MethodInfo methodInfo = array[i];
				if (filter(methodInfo))
				{
					outList.Add(methodInfo);
				}
			}
			return outList;
		}

		public static bool ShouldListMethod(MethodInfo m, string prefix)
		{
			return StringUtils.StartsWithIgnoreCase(m.Name, prefix);
		}

		public static T GetObjectField<T>(object target, string name)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			FieldInfo[] fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			FieldInfo[] array = fields;
			for (int i = 0; i < array.Length; i++)
			{
				FieldInfo fieldInfo = array[i];
				if (fieldInfo.Name == name)
				{
					return (T)((object)fieldInfo.GetValue(target));
				}
			}
			throw new ArgumentException("Can't find field: " + name);
		}

		public static Type TypeForName(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			try
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				for (int i = 0; i < assemblies.Length; i++)
				{
					Assembly assembly = assemblies[i];
					try
					{
						Type[] types = assembly.GetTypes();
						for (int j = 0; j < types.Length; j++)
						{
							Type type = types[j];
							if (type.FullName == typeName)
							{
								return type;
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Exception while resolving type for name '{0}'", new object[]
				{
					typeName
				});
			}
			return null;
		}
	}
}
