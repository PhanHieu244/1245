using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class JSONTemplates
{
	private static readonly HashSet<object> touched = new HashSet<object>();

	public static JSONObject TOJSON(object obj)
	{
		if (JSONTemplates.touched.Add(obj))
		{
			JSONObject obj2 = JSONObject.obj;
			FieldInfo[] fields = obj.GetType().GetFields();
			FieldInfo[] array = fields;
			for (int i = 0; i < array.Length; i++)
			{
				FieldInfo fieldInfo = array[i];
				JSONObject jSONObject = JSONObject.nullJO;
				if (!fieldInfo.GetValue(obj).Equals(null))
				{
					MethodInfo method = typeof(JSONTemplates).GetMethod("From" + fieldInfo.FieldType.Name);
					if (method != null)
					{
						jSONObject = (JSONObject)method.Invoke(null, new object[]
						{
							fieldInfo.GetValue(obj)
						});
					}
					else if (fieldInfo.FieldType == typeof(string))
					{
						jSONObject = JSONObject.CreateStringObject(fieldInfo.GetValue(obj).ToString());
					}
					else
					{
						jSONObject = JSONObject.Create(fieldInfo.GetValue(obj).ToString(), -2, false, false);
					}
				}
				if (jSONObject)
				{
					if (jSONObject.type != JSONObject.Type.NULL)
					{
						obj2.AddField(fieldInfo.Name, jSONObject);
					}
					else
					{
						UnityEngine.Debug.LogWarning(string.Concat(new string[]
						{
							"Null for this non-null object, property ",
							fieldInfo.Name,
							" of class ",
							obj.GetType().Name,
							". Object type is ",
							fieldInfo.FieldType.Name
						}));
					}
				}
			}
			PropertyInfo[] properties = obj.GetType().GetProperties();
			PropertyInfo[] array2 = properties;
			for (int j = 0; j < array2.Length; j++)
			{
				PropertyInfo propertyInfo = array2[j];
				JSONObject jSONObject2 = JSONObject.nullJO;
				if (!propertyInfo.GetValue(obj, null).Equals(null))
				{
					MethodInfo method2 = typeof(JSONTemplates).GetMethod("From" + propertyInfo.PropertyType.Name);
					if (method2 != null)
					{
						jSONObject2 = (JSONObject)method2.Invoke(null, new object[]
						{
							propertyInfo.GetValue(obj, null)
						});
					}
					else if (propertyInfo.PropertyType == typeof(string))
					{
						jSONObject2 = JSONObject.CreateStringObject(propertyInfo.GetValue(obj, null).ToString());
					}
					else
					{
						jSONObject2 = JSONObject.Create(propertyInfo.GetValue(obj, null).ToString(), -2, false, false);
					}
				}
				if (jSONObject2)
				{
					if (jSONObject2.type != JSONObject.Type.NULL)
					{
						obj2.AddField(propertyInfo.Name, jSONObject2);
					}
					else
					{
						UnityEngine.Debug.LogWarning(string.Concat(new string[]
						{
							"Null for this non-null object, property ",
							propertyInfo.Name,
							" of class ",
							obj.GetType().Name,
							". Object type is ",
							propertyInfo.PropertyType.Name
						}));
					}
				}
			}
			return obj2;
		}
		UnityEngine.Debug.LogWarning("trying to save the same data twice");
		return JSONObject.nullJO;
	}

	public static Vector2 ToVector2(JSONObject obj)
	{
		float x = (!obj["x"]) ? 0f : obj["x"].f;
		float y = (!obj["y"]) ? 0f : obj["y"].f;
		return new Vector2(x, y);
	}

	public static JSONObject FromVector2(Vector2 v)
	{
		JSONObject obj = JSONObject.obj;
		if (v.x != 0f)
		{
			obj.AddField("x", v.x);
		}
		if (v.y != 0f)
		{
			obj.AddField("y", v.y);
		}
		return obj;
	}

	public static JSONObject FromVector3(Vector3 v)
	{
		JSONObject obj = JSONObject.obj;
		if (v.x != 0f)
		{
			obj.AddField("x", v.x);
		}
		if (v.y != 0f)
		{
			obj.AddField("y", v.y);
		}
		if (v.z != 0f)
		{
			obj.AddField("z", v.z);
		}
		return obj;
	}

	public static Vector3 ToVector3(JSONObject obj)
	{
		float x = (!obj["x"]) ? 0f : obj["x"].f;
		float y = (!obj["y"]) ? 0f : obj["y"].f;
		float z = (!obj["z"]) ? 0f : obj["z"].f;
		return new Vector3(x, y, z);
	}

	public static JSONObject FromVector4(Vector4 v)
	{
		JSONObject obj = JSONObject.obj;
		if (v.x != 0f)
		{
			obj.AddField("x", v.x);
		}
		if (v.y != 0f)
		{
			obj.AddField("y", v.y);
		}
		if (v.z != 0f)
		{
			obj.AddField("z", v.z);
		}
		if (v.w != 0f)
		{
			obj.AddField("w", v.w);
		}
		return obj;
	}

	public static Vector4 ToVector4(JSONObject obj)
	{
		float x = (!obj["x"]) ? 0f : obj["x"].f;
		float y = (!obj["y"]) ? 0f : obj["y"].f;
		float z = (!obj["z"]) ? 0f : obj["z"].f;
		float w = (!obj["w"]) ? 0f : obj["w"].f;
		return new Vector4(x, y, z, w);
	}

	public static JSONObject FromMatrix4x4(Matrix4x4 m)
	{
		JSONObject obj = JSONObject.obj;
		if (m.m00 != 0f)
		{
			obj.AddField("m00", m.m00);
		}
		if (m.m01 != 0f)
		{
			obj.AddField("m01", m.m01);
		}
		if (m.m02 != 0f)
		{
			obj.AddField("m02", m.m02);
		}
		if (m.m03 != 0f)
		{
			obj.AddField("m03", m.m03);
		}
		if (m.m10 != 0f)
		{
			obj.AddField("m10", m.m10);
		}
		if (m.m11 != 0f)
		{
			obj.AddField("m11", m.m11);
		}
		if (m.m12 != 0f)
		{
			obj.AddField("m12", m.m12);
		}
		if (m.m13 != 0f)
		{
			obj.AddField("m13", m.m13);
		}
		if (m.m20 != 0f)
		{
			obj.AddField("m20", m.m20);
		}
		if (m.m21 != 0f)
		{
			obj.AddField("m21", m.m21);
		}
		if (m.m22 != 0f)
		{
			obj.AddField("m22", m.m22);
		}
		if (m.m23 != 0f)
		{
			obj.AddField("m23", m.m23);
		}
		if (m.m30 != 0f)
		{
			obj.AddField("m30", m.m30);
		}
		if (m.m31 != 0f)
		{
			obj.AddField("m31", m.m31);
		}
		if (m.m32 != 0f)
		{
			obj.AddField("m32", m.m32);
		}
		if (m.m33 != 0f)
		{
			obj.AddField("m33", m.m33);
		}
		return obj;
	}

	public static Matrix4x4 ToMatrix4x4(JSONObject obj)
	{
		Matrix4x4 result = default(Matrix4x4);
		if (obj["m00"])
		{
			result.m00 = obj["m00"].f;
		}
		if (obj["m01"])
		{
			result.m01 = obj["m01"].f;
		}
		if (obj["m02"])
		{
			result.m02 = obj["m02"].f;
		}
		if (obj["m03"])
		{
			result.m03 = obj["m03"].f;
		}
		if (obj["m10"])
		{
			result.m10 = obj["m10"].f;
		}
		if (obj["m11"])
		{
			result.m11 = obj["m11"].f;
		}
		if (obj["m12"])
		{
			result.m12 = obj["m12"].f;
		}
		if (obj["m13"])
		{
			result.m13 = obj["m13"].f;
		}
		if (obj["m20"])
		{
			result.m20 = obj["m20"].f;
		}
		if (obj["m21"])
		{
			result.m21 = obj["m21"].f;
		}
		if (obj["m22"])
		{
			result.m22 = obj["m22"].f;
		}
		if (obj["m23"])
		{
			result.m23 = obj["m23"].f;
		}
		if (obj["m30"])
		{
			result.m30 = obj["m30"].f;
		}
		if (obj["m31"])
		{
			result.m31 = obj["m31"].f;
		}
		if (obj["m32"])
		{
			result.m32 = obj["m32"].f;
		}
		if (obj["m33"])
		{
			result.m33 = obj["m33"].f;
		}
		return result;
	}

	public static JSONObject FromQuaternion(Quaternion q)
	{
		JSONObject obj = JSONObject.obj;
		if (q.w != 0f)
		{
			obj.AddField("w", q.w);
		}
		if (q.x != 0f)
		{
			obj.AddField("x", q.x);
		}
		if (q.y != 0f)
		{
			obj.AddField("y", q.y);
		}
		if (q.z != 0f)
		{
			obj.AddField("z", q.z);
		}
		return obj;
	}

	public static Quaternion ToQuaternion(JSONObject obj)
	{
		float x = (!obj["x"]) ? 0f : obj["x"].f;
		float y = (!obj["y"]) ? 0f : obj["y"].f;
		float z = (!obj["z"]) ? 0f : obj["z"].f;
		float w = (!obj["w"]) ? 0f : obj["w"].f;
		return new Quaternion(x, y, z, w);
	}

	public static JSONObject FromColor(Color c)
	{
		JSONObject obj = JSONObject.obj;
		if (c.r != 0f)
		{
			obj.AddField("r", c.r);
		}
		if (c.g != 0f)
		{
			obj.AddField("g", c.g);
		}
		if (c.b != 0f)
		{
			obj.AddField("b", c.b);
		}
		if (c.a != 0f)
		{
			obj.AddField("a", c.a);
		}
		return obj;
	}

	public static Color ToColor(JSONObject obj)
	{
		Color result = default(Color);
		for (int i = 0; i < obj.Count; i++)
		{
			string text = obj.keys[i];
			if (text != null)
			{
				if (!(text == "r"))
				{
					if (!(text == "g"))
					{
						if (!(text == "b"))
						{
							if (text == "a")
							{
								result.a = obj[i].f;
							}
						}
						else
						{
							result.b = obj[i].f;
						}
					}
					else
					{
						result.g = obj[i].f;
					}
				}
				else
				{
					result.r = obj[i].f;
				}
			}
		}
		return result;
	}

	public static JSONObject FromLayerMask(LayerMask l)
	{
		JSONObject obj = JSONObject.obj;
		obj.AddField("value", l.value);
		return obj;
	}

	public static LayerMask ToLayerMask(JSONObject obj)
	{
		return new LayerMask
		{
			value = (int)obj["value"].n
		};
	}

	public static JSONObject FromRect(Rect r)
	{
		JSONObject obj = JSONObject.obj;
		if (r.x != 0f)
		{
			obj.AddField("x", r.x);
		}
		if (r.y != 0f)
		{
			obj.AddField("y", r.y);
		}
		if (r.height != 0f)
		{
			obj.AddField("height", r.height);
		}
		if (r.width != 0f)
		{
			obj.AddField("width", r.width);
		}
		return obj;
	}

	public static Rect ToRect(JSONObject obj)
	{
		Rect result = default(Rect);
		for (int i = 0; i < obj.Count; i++)
		{
			string text = obj.keys[i];
			if (text != null)
			{
				if (!(text == "x"))
				{
					if (!(text == "y"))
					{
						if (!(text == "height"))
						{
							if (text == "width")
							{
								result.width = obj[i].f;
							}
						}
						else
						{
							result.height = obj[i].f;
						}
					}
					else
					{
						result.y = obj[i].f;
					}
				}
				else
				{
					result.x = obj[i].f;
				}
			}
		}
		return result;
	}

	public static JSONObject FromRectOffset(RectOffset r)
	{
		JSONObject obj = JSONObject.obj;
		if (r.bottom != 0)
		{
			obj.AddField("bottom", r.bottom);
		}
		if (r.left != 0)
		{
			obj.AddField("left", r.left);
		}
		if (r.right != 0)
		{
			obj.AddField("right", r.right);
		}
		if (r.top != 0)
		{
			obj.AddField("top", r.top);
		}
		return obj;
	}

	public static RectOffset ToRectOffset(JSONObject obj)
	{
		RectOffset rectOffset = new RectOffset();
		for (int i = 0; i < obj.Count; i++)
		{
			string text = obj.keys[i];
			if (text != null)
			{
				if (!(text == "bottom"))
				{
					if (!(text == "left"))
					{
						if (!(text == "right"))
						{
							if (text == "top")
							{
								rectOffset.top = (int)obj[i].n;
							}
						}
						else
						{
							rectOffset.right = (int)obj[i].n;
						}
					}
					else
					{
						rectOffset.left = (int)obj[i].n;
					}
				}
				else
				{
					rectOffset.bottom = (int)obj[i].n;
				}
			}
		}
		return rectOffset;
	}

	public static AnimationCurve ToAnimationCurve(JSONObject obj)
	{
		AnimationCurve animationCurve = new AnimationCurve();
		if (obj.HasField("keys"))
		{
			JSONObject field = obj.GetField("keys");
			for (int i = 0; i < field.list.Count; i++)
			{
				animationCurve.AddKey(JSONTemplates.ToKeyframe(field[i]));
			}
		}
		if (obj.HasField("preWrapMode"))
		{
			animationCurve.preWrapMode = (WrapMode)obj.GetField("preWrapMode").n;
		}
		if (obj.HasField("postWrapMode"))
		{
			animationCurve.postWrapMode = (WrapMode)obj.GetField("postWrapMode").n;
		}
		return animationCurve;
	}

	public static JSONObject FromAnimationCurve(AnimationCurve a)
	{
		JSONObject obj = JSONObject.obj;
		obj.AddField("preWrapMode", a.preWrapMode.ToString());
		obj.AddField("postWrapMode", a.postWrapMode.ToString());
		if (a.keys.Length > 0)
		{
			JSONObject jSONObject = JSONObject.Create();
			for (int i = 0; i < a.keys.Length; i++)
			{
				jSONObject.Add(JSONTemplates.FromKeyframe(a.keys[i]));
			}
			obj.AddField("keys", jSONObject);
		}
		return obj;
	}

	public static Keyframe ToKeyframe(JSONObject obj)
	{
		Keyframe result = new Keyframe((!obj.HasField("time")) ? 0f : obj.GetField("time").n, (!obj.HasField("value")) ? 0f : obj.GetField("value").n);
		if (obj.HasField("inTangent"))
		{
			result.inTangent = obj.GetField("inTangent").n;
		}
		if (obj.HasField("outTangent"))
		{
			result.outTangent = obj.GetField("outTangent").n;
		}
		if (obj.HasField("tangentMode"))
		{
			result.tangentMode = (int)obj.GetField("tangentMode").n;
		}
		return result;
	}

	public static JSONObject FromKeyframe(Keyframe k)
	{
		JSONObject obj = JSONObject.obj;
		if (k.inTangent != 0f)
		{
			obj.AddField("inTangent", k.inTangent);
		}
		if (k.outTangent != 0f)
		{
			obj.AddField("outTangent", k.outTangent);
		}
		if (k.tangentMode != 0)
		{
			obj.AddField("tangentMode", k.tangentMode);
		}
		if (k.time != 0f)
		{
			obj.AddField("time", k.time);
		}
		if (k.value != 0f)
		{
			obj.AddField("value", k.value);
		}
		return obj;
	}
}
