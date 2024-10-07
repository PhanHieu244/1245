using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LunarConsolePluginInternal
{
	public static class StringUtils
	{
		private static readonly char[] kSpaceSplitChars = new char[]
		{
			' '
		};

		private static readonly Regex kRichTagRegex = new Regex("(<color=.*?>)|(<b>)|(<i>)|(</color>)|(</b>)|(</i>)");

		private static List<string> s_tempList;

		private static readonly string Quote = "\"";

		private static readonly string SingleQuote = "'";

		private static readonly string EscapedQuote = "\\\"";

		private static readonly string EscapedSingleQuote = "\\'";

		internal static string TryFormat(string format, params object[] args)
		{
			if (format != null && args != null && args.Length > 0)
			{
				try
				{
					return string.Format(format, args);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Error while formatting string: " + ex.Message);
				}
				return format;
			}
			return format;
		}

		public static bool StartsWithIgnoreCase(string str, string prefix)
		{
			return str != null && prefix != null && str.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
		}

		public static bool EqualsIgnoreCase(string a, string b)
		{
			return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
		}

		public static IList<string> Filter(IList<string> strings, string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return strings;
			}
			IList<string> list = new List<string>();
			foreach (string current in strings)
			{
				if (StringUtils.StartsWithIgnoreCase(current, prefix))
				{
					list.Add(current);
				}
			}
			return list;
		}

		public static int ParseInt(string str)
		{
			return StringUtils.ParseInt(str, 0);
		}

		public static int ParseInt(string str, int defValue)
		{
			if (!string.IsNullOrEmpty(str))
			{
				int num;
				bool flag = int.TryParse(str, out num);
				return (!flag) ? defValue : num;
			}
			return defValue;
		}

		public static int ParseInt(string str, out bool succeed)
		{
			if (!string.IsNullOrEmpty(str))
			{
				int num;
				succeed = int.TryParse(str, out num);
				return (!succeed) ? 0 : num;
			}
			succeed = false;
			return 0;
		}

		public static float ParseFloat(string str)
		{
			return StringUtils.ParseFloat(str, 0f);
		}

		public static float ParseFloat(string str, float defValue)
		{
			if (!string.IsNullOrEmpty(str))
			{
				float num;
				bool flag = float.TryParse(str, out num);
				return (!flag) ? defValue : num;
			}
			return defValue;
		}

		public static float ParseFloat(string str, out bool succeed)
		{
			if (!string.IsNullOrEmpty(str))
			{
				float num;
				succeed = float.TryParse(str, out num);
				return (!succeed) ? 0f : num;
			}
			succeed = false;
			return 0f;
		}

		public static bool ParseBool(string str)
		{
			return StringUtils.ParseBool(str, false);
		}

		public static bool ParseBool(string str, bool defValue)
		{
			if (!string.IsNullOrEmpty(str))
			{
				bool flag2;
				bool flag = bool.TryParse(str, out flag2);
				return (!flag) ? defValue : flag2;
			}
			return defValue;
		}

		public static bool ParseBool(string str, out bool succeed)
		{
			if (!string.IsNullOrEmpty(str))
			{
				bool flag;
				succeed = bool.TryParse(str, out flag);
				return succeed && flag;
			}
			succeed = false;
			return false;
		}

		public static float[] ParseFloats(string str)
		{
			return (str == null) ? null : StringUtils.ParseFloats(str.Split(StringUtils.kSpaceSplitChars, StringSplitOptions.RemoveEmptyEntries));
		}

		public static float[] ParseFloats(string[] args)
		{
			if (args != null)
			{
				float[] array = new float[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					if (!float.TryParse(args[i], out array[i]))
					{
						return null;
					}
				}
				return array;
			}
			return null;
		}

		public static bool IsNumeric(string str)
		{
			double num;
			return double.TryParse(str, out num);
		}

		public static bool IsInteger(string str)
		{
			int num;
			return int.TryParse(str, out num);
		}

		internal static int StartOfTheWordOffset(string value, int index)
		{
			return StringUtils.StartOfTheWord(value, index) - index;
		}

		internal static int StartOfTheWord(string value, int index)
		{
			int num = index - 1;
			while (num >= 0 && StringUtils.IsSeparator(value[num]))
			{
				num--;
			}
			while (num >= 0 && !StringUtils.IsSeparator(value[num]))
			{
				num--;
			}
			return num + 1;
		}

		internal static int EndOfTheWordOffset(string value, int index)
		{
			return StringUtils.EndOfTheWord(value, index) - index;
		}

		internal static int EndOfTheWord(string value, int index)
		{
			int num = index;
			while (num < value.Length && StringUtils.IsSeparator(value[num]))
			{
				num++;
			}
			while (num < value.Length && !StringUtils.IsSeparator(value[num]))
			{
				num++;
			}
			return num;
		}

		private static bool IsSeparator(char ch)
		{
			return !char.IsLetter(ch) && !char.IsDigit(ch);
		}

		internal static int MoveLineUp(string value, int index)
		{
			if (index <= 0 || index > value.Length)
			{
				return index;
			}
			int num = StringUtils.StartOfPrevLineIndex(value, index);
			if (num == -1)
			{
				return index;
			}
			int num2 = StringUtils.OffsetInLine(value, index);
			int b = StringUtils.EndOfPrevLineIndex(value, index);
			return Mathf.Min(num + num2, b);
		}

		internal static int MoveLineDown(string value, int index)
		{
			if (index < 0 || index >= value.Length)
			{
				return index;
			}
			int num = StringUtils.StartOfNextLineIndex(value, index);
			if (num == -1)
			{
				return index;
			}
			int num2 = StringUtils.OffsetInLine(value, index);
			int b = StringUtils.EndOfNextLineIndex(value, index);
			return Mathf.Min(num + num2, b);
		}

		internal static int StartOfLineOffset(string value, int index)
		{
			return StringUtils.StartOfLineIndex(value, index) - index;
		}

		internal static int StartOfLineIndex(string value, int index)
		{
			return (index <= 0) ? 0 : (value.LastIndexOf('\n', index - 1) + 1);
		}

		internal static int EndOfLineOffset(string value, int index)
		{
			return StringUtils.EndOfLineIndex(value, index) - index;
		}

		internal static int EndOfLineIndex(string value, int index)
		{
			if (index < value.Length)
			{
				int num = value.IndexOf('\n', index);
				if (num != -1)
				{
					return num;
				}
			}
			return value.Length;
		}

		internal static int OffsetInLine(string value, int index)
		{
			return index - StringUtils.StartOfLineIndex(value, index);
		}

		internal static int StartOfPrevLineIndex(string value, int index)
		{
			int num = StringUtils.EndOfPrevLineIndex(value, index);
			return (num == -1) ? (-1) : StringUtils.StartOfLineIndex(value, num);
		}

		internal static int EndOfPrevLineIndex(string value, int index)
		{
			return StringUtils.StartOfLineIndex(value, index) - 1;
		}

		internal static int StartOfNextLineIndex(string value, int index)
		{
			int num = StringUtils.EndOfLineIndex(value, index);
			return (num >= value.Length) ? (-1) : (num + 1);
		}

		internal static int EndOfNextLineIndex(string value, int index)
		{
			int num = StringUtils.StartOfNextLineIndex(value, index);
			return (num == -1) ? (-1) : StringUtils.EndOfLineIndex(value, num);
		}

		internal static int LinesBreaksCount(string value)
		{
			if (value != null)
			{
				int num = 0;
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] == '\n')
					{
						num++;
					}
				}
				return num;
			}
			return 0;
		}

		internal static int Strlen(string str)
		{
			return (str == null) ? 0 : str.Length;
		}

		public static string RemoveRichTextTags(string line)
		{
			return StringUtils.kRichTagRegex.Replace(line, string.Empty);
		}

		internal static string GetSuggestedText(string token, string[] strings, bool removeTags = false)
		{
			return StringUtils.GetSuggestedText0(token, strings, removeTags);
		}

		internal static string GetSuggestedText(string token, IList<string> strings, bool removeTags = false)
		{
			return StringUtils.GetSuggestedText0(token, (IList)strings, removeTags);
		}

		private static string GetSuggestedText0(string token, IList strings, bool removeTags)
		{
			if (token == null)
			{
				return null;
			}
			if (StringUtils.s_tempList == null)
			{
				StringUtils.s_tempList = new List<string>();
			}
			else
			{
				StringUtils.s_tempList.Clear();
			}
			IEnumerator enumerator = strings.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Current;
					string text2 = text;
					if (token.Length == 0 || StringUtils.StartsWithIgnoreCase(text2, token))
					{
						StringUtils.s_tempList.Add(text2);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return StringUtils.GetSuggestedTextFiltered0(token, StringUtils.s_tempList);
		}

		internal static string GetSuggestedTextFiltered(string token, IList<string> strings)
		{
			return StringUtils.GetSuggestedTextFiltered0(token, (IList)strings);
		}

		internal static string GetSuggestedTextFiltered(string token, string[] strings)
		{
			return StringUtils.GetSuggestedTextFiltered0(token, strings);
		}

		private static string GetSuggestedTextFiltered0(string token, IList strings)
		{
			if (token == null)
			{
				return null;
			}
			if (strings.Count == 0)
			{
				return null;
			}
			if (strings.Count == 1)
			{
				return (string)strings[0];
			}
			string text = (string)strings[0];
			if (token.Length == 0)
			{
				token = text;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				char c2 = char.ToLower(c);
				bool flag = false;
				for (int j = 1; j < strings.Count; j++)
				{
					string text2 = (string)strings[j];
					if (i >= text2.Length || char.ToLower(text2[i]) != c2)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					return (stringBuilder.Length <= 0) ? null : stringBuilder.ToString();
				}
				stringBuilder.Append(c);
			}
			return (stringBuilder.Length <= 0) ? null : stringBuilder.ToString();
		}

		internal static string Arg(string value)
		{
			if (value != null && value.Length > 0)
			{
				value = value.Replace(StringUtils.Quote, StringUtils.EscapedQuote);
				value = value.Replace(StringUtils.SingleQuote, StringUtils.EscapedSingleQuote);
				if (value.IndexOf(' ') != -1)
				{
					value = StringUtils.TryFormat("\"{0}\"", new object[]
					{
						value
					});
				}
				return value;
			}
			return "\"\"";
		}

		internal static string UnArg(string value)
		{
			if (value != null && value.Length > 0)
			{
				if ((value.StartsWith(StringUtils.Quote) && value.EndsWith(StringUtils.Quote)) || (value.StartsWith(StringUtils.SingleQuote) && value.EndsWith(StringUtils.SingleQuote)))
				{
					value = value.Substring(1, value.Length - 2);
				}
				value = value.Replace(StringUtils.EscapedQuote, StringUtils.Quote);
				value = value.Replace(StringUtils.EscapedSingleQuote, StringUtils.SingleQuote);
				return value;
			}
			return string.Empty;
		}

		internal static string NonNullOrEmpty(string str)
		{
			return (str == null) ? string.Empty : str;
		}

		internal static string ToString(int value)
		{
			return value.ToString();
		}

		internal static string ToString(float value)
		{
			return value.ToString("G");
		}

		internal static string ToString(bool value)
		{
			return value.ToString();
		}

		internal static string ToString(ref Color value)
		{
			if (value.a > 0f)
			{
				return string.Format("{0} {1} {2} {3}", new object[]
				{
					value.r.ToString("G"),
					value.g.ToString("G"),
					value.b.ToString("G"),
					value.a.ToString("G")
				});
			}
			return string.Format("{0} {1} {2}", value.r.ToString("G"), value.g.ToString("G"), value.b.ToString("G"));
		}

		internal static string ToString(ref Rect value)
		{
			return string.Format("{0} {1} {2} {3}", new object[]
			{
				value.x.ToString("G"),
				value.y.ToString("G"),
				value.width.ToString("G"),
				value.height.ToString("G")
			});
		}

		internal static string ToString(ref Vector2 value)
		{
			return string.Format("{0} {1}", value.x.ToString("G"), value.y.ToString("G"));
		}

		internal static string ToString(ref Vector3 value)
		{
			return string.Format("{0} {1} {2}", value.x.ToString("G"), value.y.ToString("G"), value.z.ToString("G"));
		}

		internal static string ToString(ref Vector4 value)
		{
			return string.Format("{0} {1} {2} {3}", new object[]
			{
				value.x.ToString("G"),
				value.y.ToString("G"),
				value.z.ToString("G"),
				value.w.ToString("G")
			});
		}

		public static string Join<T>(IList<T> list, string separator = ",")
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append(list[i]);
				if (i < list.Count - 1)
				{
					stringBuilder.Append(separator);
				}
			}
			return stringBuilder.ToString();
		}

		public static string ToDisplayName(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c = '\0';
			for (int i = 0; i < value.Length; i++)
			{
				char c2 = value[i];
				if (i == 0)
				{
					c2 = char.ToUpper(c2);
				}
				else if ((char.IsUpper(c2) || (char.IsDigit(c2) && !char.IsDigit(c))) && stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(c2);
				c = c2;
			}
			return stringBuilder.ToString();
		}

		public static IDictionary<string, string> DeserializeString(string data)
		{
			string[] array = data.Split(new char[]
			{
				'\n'
			});
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				int num = text.IndexOf(':');
				string key = text.Substring(0, num);
				string value = text.Substring(num + 1, text.Length - (num + 1)).Replace("\\n", "\n");
				dictionary[key] = value;
			}
			return dictionary;
		}
	}
}
