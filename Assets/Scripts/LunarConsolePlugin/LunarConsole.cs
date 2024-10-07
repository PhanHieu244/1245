using LunarConsolePluginInternal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace LunarConsolePlugin
{
	public sealed class LunarConsole : MonoBehaviour
	{
		private interface IPlatform : ICRegistryDelegate
		{
			void Update();

			void OnLogMessageReceived(string message, string stackTrace, LogType type);

			bool ShowConsole();

			bool HideConsole();

			void ClearConsole();

			void Destroy();
		}

		private class PlatformAndroid : LunarConsole.IPlatform, ICRegistryDelegate
		{
			private readonly int m_mainThreadId;

			private readonly jvalue[] m_args0 = new jvalue[0];

			private readonly jvalue[] m_args1 = new jvalue[1];

			private readonly jvalue[] m_args2 = new jvalue[2];

			private readonly jvalue[] m_args3 = new jvalue[3];

			private readonly jvalue[] m_args9 = new jvalue[9];

			private static readonly string kPluginClassName = "spacemadness.com.lunarconsole.console.ConsolePlugin";

			private readonly AndroidJavaClass m_pluginClass;

			private readonly IntPtr m_pluginClassRaw;

			private readonly IntPtr m_methodLogMessage;

			private readonly IntPtr m_methodShowConsole;

			private readonly IntPtr m_methodHideConsole;

			private readonly IntPtr m_methodClearConsole;

			private readonly IntPtr m_methodRegisterAction;

			private readonly IntPtr m_methodUnregisterAction;

			private readonly IntPtr m_methodRegisterVariable;

			private readonly IntPtr m_methodUpdateVariable;

			private readonly IntPtr m_methodDestroy;

			private readonly Queue<LunarConsole.LogMessageEntry> m_messageQueue;

			public PlatformAndroid(string targetName, string methodName, string version, int capacity, int trim, string gesture, LunarConsoleSettings settings)
			{
				string value = JsonUtility.ToJson(settings);
				this.m_mainThreadId = Thread.CurrentThread.ManagedThreadId;
				this.m_pluginClass = new AndroidJavaClass(LunarConsole.PlatformAndroid.kPluginClassName);
				this.m_pluginClassRaw = this.m_pluginClass.GetRawClass();
				IntPtr staticMethod = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "init", "(Ljava.lang.String;Ljava.lang.String;Ljava.lang.String;IILjava.lang.String;Ljava.lang.String;)V");
				jvalue[] array = new jvalue[]
				{
					this.jval(targetName),
					this.jval(methodName),
					this.jval(version),
					this.jval(capacity),
					this.jval(trim),
					this.jval(gesture),
					this.jval(value)
				};
				this.CallStaticVoidMethod(staticMethod, array);
				AndroidJNI.DeleteLocalRef(array[0].l);
				AndroidJNI.DeleteLocalRef(array[1].l);
				AndroidJNI.DeleteLocalRef(array[2].l);
				AndroidJNI.DeleteLocalRef(array[5].l);
				this.m_methodLogMessage = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "logMessage", "(Ljava.lang.String;Ljava.lang.String;I)V");
				this.m_methodShowConsole = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "show", "()V");
				this.m_methodHideConsole = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "hide", "()V");
				this.m_methodClearConsole = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "clear", "()V");
				this.m_methodRegisterAction = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "registerAction", "(ILjava.lang.String;)V");
				this.m_methodUnregisterAction = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "unregisterAction", "(I)V");
				this.m_methodRegisterVariable = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "registerVariable", "(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;IZFF)V");
				this.m_methodUpdateVariable = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "updateVariable", "(ILjava/lang/String;)V");
				this.m_methodDestroy = LunarConsole.PlatformAndroid.GetStaticMethod(this.m_pluginClassRaw, "destroyInstance", "()V");
				this.m_messageQueue = new Queue<LunarConsole.LogMessageEntry>();
			}

			~PlatformAndroid()
			{
				this.m_pluginClass.Dispose();
			}

			public void Update()
			{
				object messageQueue = this.m_messageQueue;
				lock (messageQueue)
				{
					while (this.m_messageQueue.Count > 0)
					{
						LunarConsole.LogMessageEntry logMessageEntry = this.m_messageQueue.Dequeue();
						this.OnLogMessageReceived(logMessageEntry.message, logMessageEntry.stackTrace, logMessageEntry.type);
					}
				}
			}

			public void OnLogMessageReceived(string message, string stackTrace, LogType type)
			{
				if (Thread.CurrentThread.ManagedThreadId == this.m_mainThreadId)
				{
					this.m_args3[0] = this.jval(message);
					this.m_args3[1] = this.jval(stackTrace);
					this.m_args3[2] = this.jval((int)type);
					this.CallStaticVoidMethod(this.m_methodLogMessage, this.m_args3);
					AndroidJNI.DeleteLocalRef(this.m_args3[0].l);
					AndroidJNI.DeleteLocalRef(this.m_args3[1].l);
				}
				else
				{
					object messageQueue = this.m_messageQueue;
					lock (messageQueue)
					{
						this.m_messageQueue.Enqueue(new LunarConsole.LogMessageEntry(message, stackTrace, type));
					}
				}
			}

			public bool ShowConsole()
			{
				bool result;
				try
				{
					this.CallStaticVoidMethod(this.m_methodShowConsole, this.m_args0);
					result = true;
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while calling 'LunarConsole.ShowConsole': " + ex.Message);
					result = false;
				}
				return result;
			}

			public bool HideConsole()
			{
				bool result;
				try
				{
					this.CallStaticVoidMethod(this.m_methodHideConsole, this.m_args0);
					result = true;
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while calling 'LunarConsole.HideConsole': " + ex.Message);
					result = false;
				}
				return result;
			}

			public void ClearConsole()
			{
				try
				{
					this.CallStaticVoidMethod(this.m_methodClearConsole, this.m_args0);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while calling 'LunarConsole.ClearConsole': " + ex.Message);
				}
			}

			public void Destroy()
			{
				try
				{
					this.CallStaticVoidMethod(this.m_methodDestroy, this.m_args0);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while destroying platform: " + ex.Message);
				}
			}

			public void OnActionRegistered(CRegistry registry, CAction action)
			{
				try
				{
					this.m_args2[0] = this.jval(action.Id);
					this.m_args2[1] = this.jval(action.Name);
					this.CallStaticVoidMethod(this.m_methodRegisterAction, this.m_args2);
					AndroidJNI.DeleteLocalRef(this.m_args2[1].l);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while calling 'LunarConsole.OnActionRegistered': " + ex.Message);
				}
			}

			public void OnActionUnregistered(CRegistry registry, CAction action)
			{
				try
				{
					this.m_args1[0] = this.jval(action.Id);
					this.CallStaticVoidMethod(this.m_methodUnregisterAction, this.m_args1);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while calling 'LunarConsole.OnActionUnregistered': " + ex.Message);
				}
			}

			public void OnVariableRegistered(CRegistry registry, CVar cvar)
			{
				try
				{
					this.m_args9[0] = this.jval(cvar.Id);
					this.m_args9[1] = this.jval(cvar.Name);
					this.m_args9[2] = this.jval(cvar.Type.ToString());
					this.m_args9[3] = this.jval(cvar.Value);
					this.m_args9[4] = this.jval(cvar.DefaultValue);
					this.m_args9[5] = this.jval((int)cvar.Flags);
					this.m_args9[6] = this.jval(cvar.HasRange);
					this.m_args9[7] = this.jval(cvar.Range.min);
					this.m_args9[8] = this.jval(cvar.Range.max);
					this.CallStaticVoidMethod(this.m_methodRegisterVariable, this.m_args9);
					AndroidJNI.DeleteLocalRef(this.m_args9[1].l);
					AndroidJNI.DeleteLocalRef(this.m_args9[2].l);
					AndroidJNI.DeleteLocalRef(this.m_args9[3].l);
					AndroidJNI.DeleteLocalRef(this.m_args9[4].l);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while calling 'LunarConsole.OnVariableRegistered': " + ex.Message);
				}
			}

			public void OnVariableUpdated(CRegistry registry, CVar cvar)
			{
				try
				{
					this.m_args2[0] = this.jval(cvar.Id);
					this.m_args2[1] = this.jval(cvar.Value);
					this.CallStaticVoidMethod(this.m_methodUpdateVariable, this.m_args2);
					AndroidJNI.DeleteLocalRef(this.m_args2[1].l);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Exception while calling 'LunarConsole.OnVariableUpdated': " + ex.Message);
				}
			}

			private static IntPtr GetStaticMethod(IntPtr classRaw, string name, string signature)
			{
				return AndroidJNIHelper.GetMethodID(classRaw, name, signature, true);
			}

			private void CallStaticVoidMethod(IntPtr method, jvalue[] args)
			{
				AndroidJNI.CallStaticVoidMethod(this.m_pluginClassRaw, method, args);
			}

			private bool CallStaticBoolMethod(IntPtr method, jvalue[] args)
			{
				return AndroidJNI.CallStaticBooleanMethod(this.m_pluginClassRaw, method, args);
			}

			private jvalue jval(string value)
			{
				return new jvalue
				{
					l = AndroidJNI.NewStringUTF(value)
				};
			}

			private jvalue jval(bool value)
			{
				return new jvalue
				{
					z = value
				};
			}

			private jvalue jval(int value)
			{
				return new jvalue
				{
					i = value
				};
			}

			private jvalue jval(float value)
			{
				return new jvalue
				{
					f = value
				};
			}
		}

		private struct LogMessageEntry
		{
			public readonly string message;

			public readonly string stackTrace;

			public readonly LogType type;

			public LogMessageEntry(string message, string stackTrace, LogType type)
			{
				this.message = message;
				this.stackTrace = stackTrace;
				this.type = type;
			}
		}

		[SerializeField]
		private LunarConsoleSettings m_settings = new LunarConsoleSettings();

		[Range(128f, 65536f), SerializeField, Tooltip("Logs will be trimmed to the capacity")]
		private int m_capacity = 4096;

		[Range(128f, 65536f), SerializeField, Tooltip("How many logs will be trimmed when console overflows")]
		private int m_trim = 512;

		[SerializeField, Tooltip("Gesture type to open the console")]
		private Gesture m_gesture = Gesture.SwipeDown;

		[SerializeField, Tooltip("If checked - removes <color>, <b> and <i> rich text tags from the output (may cause performance overhead)")]
		private bool m_removeRichTextTags;

		private static LunarConsole s_instance;

		private CRegistry m_registry;

		private bool m_variablesDirty;

		private LunarConsole.IPlatform m_platform;

		private IDictionary<string, LunarConsoleNativeMessageHandler> m_nativeHandlerLookup;

		private static Action _onConsoleOpened_k__BackingField;

		private static Action _onConsoleClosed_k__BackingField;

		private IDictionary<string, LunarConsoleNativeMessageHandler> nativeHandlerLookup
		{
			get
			{
				if (this.m_nativeHandlerLookup == null)
				{
					this.m_nativeHandlerLookup = new Dictionary<string, LunarConsoleNativeMessageHandler>();
					this.m_nativeHandlerLookup["console_open"] = new LunarConsoleNativeMessageHandler(this.ConsoleOpenHandler);
					this.m_nativeHandlerLookup["console_close"] = new LunarConsoleNativeMessageHandler(this.ConsoleCloseHandler);
					this.m_nativeHandlerLookup["console_action"] = new LunarConsoleNativeMessageHandler(this.ConsoleActionHandler);
					this.m_nativeHandlerLookup["console_variable_set"] = new LunarConsoleNativeMessageHandler(this.ConsoleVariableSetHandler);
					this.m_nativeHandlerLookup["track_event"] = new LunarConsoleNativeMessageHandler(this.TrackEventHandler);
				}
				return this.m_nativeHandlerLookup;
			}
		}

		public static Action onConsoleOpened
		{
			get;
			set;
		}

		public static Action onConsoleClosed
		{
			get;
			set;
		}

		public static LunarConsole instance
		{
			get
			{
				return LunarConsole.s_instance;
			}
		}

		public CRegistry registry
		{
			get
			{
				return this.m_registry;
			}
		}

		private void Awake()
		{
			this.InitInstance();
		}

		private void OnEnable()
		{
			this.EnablePlatform();
		}

		private void OnDisable()
		{
			this.DisablePlatform();
		}

		private void Update()
		{
			if (this.m_platform != null)
			{
				this.m_platform.Update();
			}
			if (this.m_variablesDirty)
			{
				this.m_variablesDirty = false;
				this.SaveVariables();
			}
		}

		private void OnDestroy()
		{
			this.DestroyInstance();
		}

		private void InitInstance()
		{
			if (LunarConsole.s_instance == null)
			{
				if (LunarConsole.IsPlatformSupported())
				{
					LunarConsole.s_instance = this;
					UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			else if (LunarConsole.s_instance != this)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void EnablePlatform()
		{
			if (LunarConsole.s_instance != null)
			{
				bool flag = this.InitPlatform(this.m_capacity, this.m_trim, this.m_settings);
			}
		}

		private void DisablePlatform()
		{
			if (LunarConsole.s_instance != null)
			{
				bool flag = this.DestroyPlatform();
			}
		}

		private static bool IsPlatformSupported()
		{
			return Application.platform == RuntimePlatform.Android;
		}

		private bool InitPlatform(int capacity, int trim, LunarConsoleSettings settings)
		{
			try
			{
				if (this.m_platform == null)
				{
					trim = Math.Min(trim, capacity);
					this.m_platform = this.CreatePlatform(capacity, trim, settings);
					if (this.m_platform != null)
					{
						this.m_registry = new CRegistry();
						this.m_registry.registryDelegate = this.m_platform;
						Application.logMessageReceivedThreaded += new Application.LogCallback(this.OnLogMessageReceived);
						this.ResolveVariables();
						this.LoadVariables();
						return true;
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Can't init platform");
			}
			return false;
		}

		private bool DestroyPlatform()
		{
			if (this.m_platform != null)
			{
				Application.logMessageReceivedThreaded -= new Application.LogCallback(this.OnLogMessageReceived);
				if (this.m_registry != null)
				{
					this.m_registry.Destroy();
					this.m_registry = null;
				}
				this.m_platform.Destroy();
				this.m_platform = null;
				return true;
			}
			return false;
		}

		private LunarConsole.IPlatform CreatePlatform(int capacity, int trim, LunarConsoleSettings settings)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				LunarConsoleNativeMessageCallback lunarConsoleNativeMessageCallback = new LunarConsoleNativeMessageCallback(this.NativeMessageCallback);
				return new LunarConsole.PlatformAndroid(base.gameObject.name, lunarConsoleNativeMessageCallback.Method.Name, Constants.Version, capacity, trim, LunarConsole.GetGestureName(this.m_gesture), settings);
			}
			return null;
		}

		private void DestroyInstance()
		{
			if (LunarConsole.s_instance == this)
			{
				this.DestroyPlatform();
				LunarConsole.s_instance = null;
			}
		}

		private static string GetGestureName(Gesture gesture)
		{
			return gesture.ToString();
		}

		private void ResolveVariables()
		{
			try
			{
				Assembly assembly = base.GetType().Assembly;
				List<Type> list = ReflectionUtils.FindAttributeTypes<CVarContainerAttribute>(assembly);
				foreach (Type current in list)
				{
					this.RegisterVariables(current);
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError("Unable to resolve variables: " + ex.Message);
			}
		}

		private void RegisterVariables(Type type)
		{
			try
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (fields != null && fields.Length > 0)
				{
					FieldInfo[] array = fields;
					for (int i = 0; i < array.Length; i++)
					{
						FieldInfo fieldInfo = array[i];
						if (fieldInfo.FieldType.IsAssignableFrom(typeof(CVar)))
						{
							CVar cVar = fieldInfo.GetValue(null) as CVar;
							if (cVar == null)
							{
								Log.w("Unable to register variable {0}.{0}", new object[]
								{
									type.Name,
									fieldInfo.Name
								});
							}
							else
							{
								CVarValueRange range = LunarConsole.ResolveVariableRange(fieldInfo);
								if (range.IsValid)
								{
									if (cVar.Type == CVarType.Float)
									{
										cVar.Range = range;
									}
									else
									{
										Log.w("'{0}' attribute is only available with 'float' variables", new object[]
										{
											typeof(CVarRangeAttribute).Name
										});
									}
								}
								this.m_registry.Register(cVar);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Unable to initialize cvar container: {0}", new object[]
				{
					type
				});
			}
		}

		private static CVarValueRange ResolveVariableRange(FieldInfo field)
		{
			try
			{
				object[] customAttributes = field.GetCustomAttributes(typeof(CVarRangeAttribute), true);
				if (customAttributes != null && customAttributes.Length > 0)
				{
					CVarRangeAttribute cVarRangeAttribute = customAttributes[0] as CVarRangeAttribute;
					if (cVarRangeAttribute != null)
					{
						float min = cVarRangeAttribute.min;
						float max = cVarRangeAttribute.max;
						CVarValueRange result;
						if (max - min < 1E-05f)
						{
							Log.w("Invalid range [{0}, {1}] for variable '{2}'", new object[]
							{
								min.ToString(),
								max.ToString(),
								field.Name
							});
							result = CVarValueRange.Undefined;
							return result;
						}
						result = new CVarValueRange(min, max);
						return result;
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Exception while resolving variable's range: {0}", new object[]
				{
					field.Name
				});
			}
			return CVarValueRange.Undefined;
		}

		private void LoadVariables()
		{
			try
			{
				string path = Path.Combine(Application.persistentDataPath, "lunar-mobile-console-variables.bin");
				if (File.Exists(path))
				{
					using (FileStream fileStream = File.OpenRead(path))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							int num = binaryReader.ReadInt32();
							for (int i = 0; i < num; i++)
							{
								string text = binaryReader.ReadString();
								string value = binaryReader.ReadString();
								CVar cVar = this.m_registry.FindVariable(text);
								if (cVar == null)
								{
									Log.w("Ignoring variable '%s'", new object[]
									{
										text
									});
								}
								else
								{
									cVar.Value = value;
									this.m_platform.OnVariableUpdated(this.m_registry, cVar);
								}
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Error while loading variables");
			}
		}

		private void SaveVariables()
		{
			try
			{
				string path = Path.Combine(Application.persistentDataPath, "lunar-mobile-console-variables.bin");
				using (FileStream fileStream = File.OpenWrite(path))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						CVarList cvars = this.m_registry.cvars;
						int num = 0;
						foreach (CVar current in cvars)
						{
							if (this.ShouldSaveVar(current))
							{
								num++;
							}
						}
						binaryWriter.Write(num);
						foreach (CVar current2 in cvars)
						{
							if (this.ShouldSaveVar(current2))
							{
								binaryWriter.Write(current2.Name);
								binaryWriter.Write(current2.Value);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Error while saving variables");
			}
		}

		private bool ShouldSaveVar(CVar cvar)
		{
			return !cvar.IsDefault && !cvar.HasFlag(CFlags.NoArchive);
		}

		private void OnLogMessageReceived(string message, string stackTrace, LogType type)
		{
			message = ((!this.m_removeRichTextTags) ? message : StringUtils.RemoveRichTextTags(message));
			this.m_platform.OnLogMessageReceived(message, stackTrace, type);
		}

		private void NativeMessageCallback(string param)
		{
			IDictionary<string, string> dictionary = StringUtils.DeserializeString(param);
			string text = dictionary["name"];
			if (string.IsNullOrEmpty(text))
			{
				Log.w("Can't handle native callback: 'name' is undefined");
				return;
			}
			LunarConsoleNativeMessageHandler lunarConsoleNativeMessageHandler;
			if (!this.nativeHandlerLookup.TryGetValue(text, out lunarConsoleNativeMessageHandler))
			{
				Log.w("Can't handle native callback: handler not found '" + text + "'");
				return;
			}
			try
			{
				lunarConsoleNativeMessageHandler(dictionary);
			}
			catch (Exception exception)
			{
				Log.e(exception, "Exception while handling native callback '{0}'", new object[]
				{
					text
				});
			}
		}

		private void ConsoleOpenHandler(IDictionary<string, string> data)
		{
			if (LunarConsole.onConsoleOpened != null)
			{
				LunarConsole.onConsoleOpened();
			}
			this.TrackEvent("Console", "console_open", -2147483648);
		}

		private void ConsoleCloseHandler(IDictionary<string, string> data)
		{
			if (LunarConsole.onConsoleClosed != null)
			{
				LunarConsole.onConsoleClosed();
			}
			this.TrackEvent("Console", "console_close", -2147483648);
		}

		private void ConsoleActionHandler(IDictionary<string, string> data)
		{
			string text;
			if (!data.TryGetValue("id", out text))
			{
				Log.w("Can't run action: data is not properly formatted");
				return;
			}
			int id;
			if (!int.TryParse(text, out id))
			{
				Log.w("Can't run action: invalid ID " + text);
				return;
			}
			if (this.m_registry == null)
			{
				Log.w("Can't run action: registry is not property initialized");
				return;
			}
			CAction cAction = this.m_registry.FindAction(id);
			if (cAction == null)
			{
				Log.w("Can't run action: ID not found " + text);
				return;
			}
			try
			{
				cAction.Execute();
			}
			catch (Exception exception)
			{
				Log.e(exception, "Can't run action {0}", new object[]
				{
					cAction.Name
				});
			}
		}

		private void ConsoleVariableSetHandler(IDictionary<string, string> data)
		{
			string text;
			if (!data.TryGetValue("id", out text))
			{
				Log.w("Can't set variable: missing 'id' property");
				return;
			}
			string text2;
			if (!data.TryGetValue("value", out text2))
			{
				Log.w("Can't set variable: missing 'value' property");
				return;
			}
			int variableId;
			if (!int.TryParse(text, out variableId))
			{
				Log.w("Can't set variable: invalid ID " + text);
				return;
			}
			if (this.m_registry == null)
			{
				Log.w("Can't set variable: registry is not property initialized");
				return;
			}
			CVar cVar = this.m_registry.FindVariable(variableId);
			if (cVar == null)
			{
				Log.w("Can't set variable: ID not found " + text);
				return;
			}
			try
			{
				switch (cVar.Type)
				{
				case CVarType.Boolean:
				{
					int num;
					if (int.TryParse(text2, out num) && (num == 0 || num == 1))
					{
						cVar.BoolValue = (num == 1);
						this.m_variablesDirty = true;
					}
					else
					{
						Log.e("Invalid boolean value: '{0}'", new object[]
						{
							text2
						});
					}
					break;
				}
				case CVarType.Integer:
				{
					int intValue;
					if (int.TryParse(text2, out intValue))
					{
						cVar.IntValue = intValue;
						this.m_variablesDirty = true;
					}
					else
					{
						Log.e("Invalid integer value: '{0}'", new object[]
						{
							text2
						});
					}
					break;
				}
				case CVarType.Float:
				{
					float floatValue;
					if (float.TryParse(text2, out floatValue))
					{
						cVar.FloatValue = floatValue;
						this.m_variablesDirty = true;
					}
					else
					{
						Log.e("Invalid float value: '{0}'", new object[]
						{
							text2
						});
					}
					break;
				}
				case CVarType.String:
					cVar.Value = text2;
					this.m_variablesDirty = true;
					break;
				default:
					Log.e("Unexpected variable type: {0}", new object[]
					{
						cVar.Type
					});
					break;
				}
			}
			catch (Exception exception)
			{
				Log.e(exception, "Exception while trying to set variable '{0}'", new object[]
				{
					cVar.Name
				});
			}
		}

		private void TrackEventHandler(IDictionary<string, string> data)
		{
			string text;
			if (!data.TryGetValue("category", out text) || text.Length == 0)
			{
				Log.w("Can't track event: missing 'category' parameter");
				return;
			}
			string text2;
			if (!data.TryGetValue("action", out text2) || text2.Length == 0)
			{
				Log.w("Can't track event: missing 'action' parameter");
				return;
			}
			int value = -2147483648;
			string text3;
			if (data.TryGetValue("value", out text3) && !int.TryParse(text3, out value))
			{
				Log.w("Can't track event: invalid 'value' parameter: {0}", new object[]
				{
					text3
				});
				return;
			}
			LunarConsoleAnalytics.TrackEvent(text, text2, value);
		}

		private void TrackEvent(string category, string action, int value = -2147483648)
		{
			base.StartCoroutine(LunarConsoleAnalytics.TrackEvent(category, action, value));
		}

		public static void Show()
		{
			if (LunarConsole.s_instance != null)
			{
				LunarConsole.s_instance.ShowConsole();
			}
			else
			{
				Log.w("Can't show console: instance is not initialized. Make sure you've installed it correctly");
			}
		}

		public static void Hide()
		{
			if (LunarConsole.s_instance != null)
			{
				LunarConsole.s_instance.HideConsole();
			}
			else
			{
				Log.w("Can't hide console: instance is not initialized. Make sure you've installed it correctly");
			}
		}

		public static void Clear()
		{
			if (LunarConsole.s_instance != null)
			{
				LunarConsole.s_instance.ClearConsole();
			}
			else
			{
				Log.w("Can't clear console: instance is not initialized. Make sure you've installed it correctly");
			}
		}

		public static void RegisterAction(string name, Action action)
		{
			Log.w("Can't register action: feature is not available in FREE version. Learn more about PRO version: https://goo.gl/TLInmD");
		}

		public static void UnregisterAction(Action action)
		{
		}

		public static void UnregisterAction(string name)
		{
		}

		public static void UnregisterAllActions(object target)
		{
		}

		public static void SetConsoleEnabled(bool enabled)
		{
		}

		public void MarkVariablesDirty()
		{
			this.m_variablesDirty = true;
		}

		private void ShowConsole()
		{
			if (this.m_platform != null)
			{
				this.m_platform.ShowConsole();
			}
		}

		private void HideConsole()
		{
			if (this.m_platform != null)
			{
				this.m_platform.HideConsole();
			}
		}

		private void ClearConsole()
		{
			if (this.m_platform != null)
			{
				this.m_platform.ClearConsole();
			}
		}

		private void RegisterConsoleAction(string name, Action actionDelegate)
		{
			if (this.m_registry != null)
			{
				this.m_registry.RegisterAction(name, actionDelegate);
			}
			else
			{
				Log.w("Can't register action '{0}': registry is not property initialized", new object[]
				{
					name
				});
			}
		}

		private void UnregisterConsoleAction(Action actionDelegate)
		{
			if (this.m_registry != null)
			{
				this.m_registry.Unregister(actionDelegate);
			}
			else
			{
				Log.w("Can't unregister action '{0}': registry is not property initialized", new object[]
				{
					actionDelegate
				});
			}
		}

		private void UnregisterConsoleAction(string name)
		{
			if (this.m_registry != null)
			{
				this.m_registry.Unregister(name);
			}
			else
			{
				Log.w("Can't unregister action '{0}': registry is not property initialized", new object[]
				{
					name
				});
			}
		}

		private void UnregisterAllConsoleActions(object target)
		{
			if (this.m_registry != null)
			{
				this.m_registry.UnregisterAll(target);
			}
			else
			{
				Log.w("Can't unregister actions for target '{0}': registry is not property initialized", new object[]
				{
					target
				});
			}
		}

		private void SetConsoleInstanceEnabled(bool enabled)
		{
			base.enabled = enabled;
		}
	}
}
