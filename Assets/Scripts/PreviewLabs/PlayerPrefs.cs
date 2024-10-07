using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace PreviewLabs
{
	public static class PlayerPrefs
	{
		private static readonly Hashtable playerPrefsHashtable;

		private static bool hashTableChanged;

		private static string serializedOutput;

		private static string serializedInput;

		private const string PARAMETERS_SEPERATOR = ";";

		private const string KEY_VALUE_SEPERATOR = ":";

		private static string[] seperators;

		private static readonly string fileName;

		private static readonly string secureFileName;

		private static byte[] bytes;

		private static bool wasEncrypted;

		private static bool securityModeEnabled;

		static PlayerPrefs()
		{
			PlayerPrefs.playerPrefsHashtable = new Hashtable();
			PlayerPrefs.hashTableChanged = false;
			PlayerPrefs.serializedOutput = string.Empty;
			PlayerPrefs.serializedInput = string.Empty;
			PlayerPrefs.seperators = new string[]
			{
				";",
				":"
			};
			PlayerPrefs.fileName = Application.persistentDataPath + "/PlayerPrefs.txt";
			PlayerPrefs.secureFileName = Application.persistentDataPath + "/AdvancedPlayerPrefs.txt";
			PlayerPrefs.bytes = Encoding.ASCII.GetBytes("iw3q" + SystemInfo.deviceUniqueIdentifier.Substring(0, 4));
			PlayerPrefs.wasEncrypted = false;
			PlayerPrefs.securityModeEnabled = false;
			StreamReader streamReader = null;
			if (File.Exists(PlayerPrefs.secureFileName))
			{
				streamReader = new StreamReader(PlayerPrefs.secureFileName);
				PlayerPrefs.wasEncrypted = true;
				PlayerPrefs.serializedInput = PlayerPrefs.Decrypt(streamReader.ReadToEnd());
			}
			else if (File.Exists(PlayerPrefs.fileName))
			{
				streamReader = new StreamReader(PlayerPrefs.fileName);
				PlayerPrefs.serializedInput = streamReader.ReadToEnd();
			}
			if (!string.IsNullOrEmpty(PlayerPrefs.serializedInput))
			{
				if (PlayerPrefs.serializedInput.Length > 0 && PlayerPrefs.serializedInput[PlayerPrefs.serializedInput.Length - 1] == '\n')
				{
					PlayerPrefs.serializedInput = PlayerPrefs.serializedInput.Substring(0, PlayerPrefs.serializedInput.Length - 1);
					if (PlayerPrefs.serializedInput.Length > 0 && PlayerPrefs.serializedInput[PlayerPrefs.serializedInput.Length - 1] == '\r')
					{
						PlayerPrefs.serializedInput = PlayerPrefs.serializedInput.Substring(0, PlayerPrefs.serializedInput.Length - 1);
					}
				}
				PlayerPrefs.Deserialize();
			}
			if (streamReader != null)
			{
				streamReader.Close();
			}
		}

		public static bool HasKey(string key)
		{
			return PlayerPrefs.playerPrefsHashtable.ContainsKey(key);
		}

		public static void SetString(string key, string value)
		{
			if (!PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				PlayerPrefs.playerPrefsHashtable.Add(key, value);
			}
			else
			{
				PlayerPrefs.playerPrefsHashtable[key] = value;
			}
			PlayerPrefs.hashTableChanged = true;
		}

		public static void SetInt(string key, int value)
		{
			if (!PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				PlayerPrefs.playerPrefsHashtable.Add(key, value);
			}
			else
			{
				PlayerPrefs.playerPrefsHashtable[key] = value;
			}
			PlayerPrefs.hashTableChanged = true;
		}

		public static void SetFloat(string key, float value)
		{
			if (!PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				PlayerPrefs.playerPrefsHashtable.Add(key, value);
			}
			else
			{
				PlayerPrefs.playerPrefsHashtable[key] = value;
			}
			PlayerPrefs.hashTableChanged = true;
		}

		public static void SetBool(string key, bool value)
		{
			if (!PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				PlayerPrefs.playerPrefsHashtable.Add(key, value);
			}
			else
			{
				PlayerPrefs.playerPrefsHashtable[key] = value;
			}
			PlayerPrefs.hashTableChanged = true;
		}

		public static void SetLong(string key, long value)
		{
			if (!PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				PlayerPrefs.playerPrefsHashtable.Add(key, value);
			}
			else
			{
				PlayerPrefs.playerPrefsHashtable[key] = value;
			}
			PlayerPrefs.hashTableChanged = true;
		}

		public static string GetString(string key)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return PlayerPrefs.playerPrefsHashtable[key].ToString();
			}
			return null;
		}

		public static string GetString(string key, string defaultValue)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return PlayerPrefs.playerPrefsHashtable[key].ToString();
			}
			PlayerPrefs.playerPrefsHashtable.Add(key, defaultValue);
			PlayerPrefs.hashTableChanged = true;
			return defaultValue;
		}

		public static int GetInt(string key)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return int.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
			}
			return 0;
		}

		public static int GetInt(string key, int defaultValue)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return int.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
			}
			PlayerPrefs.playerPrefsHashtable.Add(key, defaultValue);
			PlayerPrefs.hashTableChanged = true;
			return defaultValue;
		}

		public static long GetLong(string key)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return long.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
			}
			return 0L;
		}

		public static long GetLong(string key, long defaultValue)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return long.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
			}
			PlayerPrefs.playerPrefsHashtable.Add(key, defaultValue);
			PlayerPrefs.hashTableChanged = true;
			return defaultValue;
		}

		public static float GetFloat(string key)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return float.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
			}
			return 0f;
		}

		public static float GetFloat(string key, float defaultValue)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return float.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
			}
			PlayerPrefs.playerPrefsHashtable.Add(key, defaultValue);
			PlayerPrefs.hashTableChanged = true;
			return defaultValue;
		}

		public static bool GetBool(string key)
		{
			return PlayerPrefs.playerPrefsHashtable.ContainsKey(key) && bool.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
		}

		public static bool GetBool(string key, bool defaultValue)
		{
			if (PlayerPrefs.playerPrefsHashtable.ContainsKey(key))
			{
				return bool.Parse(PlayerPrefs.playerPrefsHashtable[key].ToString());
			}
			PlayerPrefs.playerPrefsHashtable.Add(key, defaultValue);
			PlayerPrefs.hashTableChanged = true;
			return defaultValue;
		}

		public static void DeleteKey(string key)
		{
			PlayerPrefs.playerPrefsHashtable.Remove(key);
		}

		public static void DeleteAll()
		{
			PlayerPrefs.playerPrefsHashtable.Clear();
		}

		public static bool WasReadPlayerPrefsFileEncrypted()
		{
			return PlayerPrefs.wasEncrypted;
		}

		public static void EnableEncryption(bool enabled)
		{
			PlayerPrefs.securityModeEnabled = enabled;
		}

		public static void Flush()
		{
			if (PlayerPrefs.hashTableChanged)
			{
				PlayerPrefs.Serialize();
				string value = (!PlayerPrefs.securityModeEnabled) ? PlayerPrefs.serializedOutput : PlayerPrefs.Encrypt(PlayerPrefs.serializedOutput);
				StreamWriter streamWriter = File.CreateText((!PlayerPrefs.securityModeEnabled) ? PlayerPrefs.fileName : PlayerPrefs.secureFileName);
				File.Delete((!PlayerPrefs.securityModeEnabled) ? PlayerPrefs.secureFileName : PlayerPrefs.fileName);
				if (streamWriter == null)
				{
					UnityEngine.Debug.LogWarning("PlayerPrefs::Flush() opening file for writing failed: " + PlayerPrefs.fileName);
					return;
				}
				streamWriter.Write(value);
				streamWriter.Close();
				PlayerPrefs.serializedOutput = string.Empty;
			}
		}

		private static void Serialize()
		{
			IDictionaryEnumerator enumerator = PlayerPrefs.playerPrefsHashtable.GetEnumerator();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				if (!flag)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(";");
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(PlayerPrefs.EscapeNonSeperators(enumerator.Key.ToString(), PlayerPrefs.seperators));
				stringBuilder.Append(" ");
				stringBuilder.Append(":");
				stringBuilder.Append(" ");
				stringBuilder.Append(PlayerPrefs.EscapeNonSeperators(enumerator.Value.ToString(), PlayerPrefs.seperators));
				stringBuilder.Append(" ");
				stringBuilder.Append(":");
				stringBuilder.Append(" ");
				stringBuilder.Append(enumerator.Value.GetType());
				flag = false;
			}
			PlayerPrefs.serializedOutput = stringBuilder.ToString();
		}

		private static void Deserialize()
		{
			string[] array = PlayerPrefs.serializedInput.Split(new string[]
			{
				" ; "
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				string[] array3 = text.Split(new string[]
				{
					" : "
				}, StringSplitOptions.None);
				PlayerPrefs.playerPrefsHashtable.Add(PlayerPrefs.DeEscapeNonSeperators(array3[0], PlayerPrefs.seperators), PlayerPrefs.GetTypeValue(array3[2], PlayerPrefs.DeEscapeNonSeperators(array3[1], PlayerPrefs.seperators)));
				if (array3.Length > 3)
				{
					UnityEngine.Debug.LogWarning("PlayerPrefs::Deserialize() parameterContent has " + array3.Length + " elements");
				}
			}
		}

		public static string EscapeNonSeperators(string inputToEscape, string[] seperators)
		{
			inputToEscape = inputToEscape.Replace("\\", "\\\\");
			for (int i = 0; i < seperators.Length; i++)
			{
				inputToEscape = inputToEscape.Replace(seperators[i], "\\" + seperators[i]);
			}
			return inputToEscape;
		}

		public static string DeEscapeNonSeperators(string inputToDeEscape, string[] seperators)
		{
			for (int i = 0; i < seperators.Length; i++)
			{
				inputToDeEscape = inputToDeEscape.Replace("\\" + seperators[i], seperators[i]);
			}
			inputToDeEscape = inputToDeEscape.Replace("\\\\", "\\");
			return inputToDeEscape;
		}

		private static string Encrypt(string originalString)
		{
			if (string.IsNullOrEmpty(originalString))
			{
				return string.Empty;
			}
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(PlayerPrefs.bytes, PlayerPrefs.bytes), CryptoStreamMode.Write);
			StreamWriter streamWriter = new StreamWriter(cryptoStream);
			streamWriter.Write(originalString);
			streamWriter.Flush();
			cryptoStream.FlushFinalBlock();
			streamWriter.Flush();
			return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
		}

		private static string Decrypt(string cryptedString)
		{
			if (string.IsNullOrEmpty(cryptedString))
			{
				return string.Empty;
			}
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			MemoryStream stream = new MemoryStream(Convert.FromBase64String(cryptedString));
			CryptoStream stream2 = new CryptoStream(stream, dESCryptoServiceProvider.CreateDecryptor(PlayerPrefs.bytes, PlayerPrefs.bytes), CryptoStreamMode.Read);
			StreamReader streamReader = new StreamReader(stream2);
			return streamReader.ReadToEnd();
		}

		private static object GetTypeValue(string typeName, string value)
		{
			if (typeName == "System.String")
			{
				return value.ToString();
			}
			if (typeName == "System.Int32")
			{
				return Convert.ToInt32(value);
			}
			if (typeName == "System.Boolean")
			{
				return Convert.ToBoolean(value);
			}
			if (typeName == "System.Single")
			{
				return Convert.ToSingle(value);
			}
			if (typeName == "System.Int64")
			{
				return Convert.ToInt64(value);
			}
			UnityEngine.Debug.LogError("Unsupported type: " + typeName);
			return null;
		}
	}
}
