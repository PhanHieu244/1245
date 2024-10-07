using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	public class FingersScript : MonoBehaviour
	{
		private enum CaptureResult
		{
			ForcePassThrough,
			ForceDenyPassThrough,
			Default,
			Ignore
		}

		private sealed class _MainThreadCallback_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Action action;

			internal float _elapsed___1;

			internal float delay;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _MainThreadCallback_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (this.action != null)
					{
						this._elapsed___1 = 0f;
						this._current = null;
						if (!this._disposing)
						{
							this._PC = 1;
						}
						return true;
					}
					goto IL_A1;
				case 1u:
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if ((this._elapsed___1 += Time.unscaledDeltaTime) < this.delay)
				{
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				this.action();
				IL_A1:
				this._PC = -1;
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _PopulateGameObjectsForTouch_c__AnonStorey1
		{
			internal RaycastResult result;

			internal bool __m__0(RaycastResult cmp)
			{
				return cmp.gameObject == this.result.gameObject;
			}
		}

		[Tooltip("True to treat the mouse as a finger, false otherwise. Left, middle and right mouse buttons can be used as individual fingers and will all have the same location.")]
		public bool TreatMousePointerAsFinger = true;

		[Tooltip("Whether to treat touches as mouse pointer? This needs to be set before the script Awake method is called.")]
		public bool SimulateMouseWithTouches;

		[Tooltip("Objects that should pass gestures through. By default, some UI components block gestures, such as Panel, Button, Dropdown, etc. See the SetupDefaultPassThroughComponents method for the full list of defaults.")]
		public List<GameObject> PassThroughObjects;

		[Tooltip("Whether to show touches using the TouchCircles array. Make sure to turn this off before releasing your game or app.")]
		public bool ShowTouches;

		[Tooltip("If ShowTouches is true, this array is used to show the touches. The FingersScriptPrefab sets this up as 10 circles.")]
		public GameObject[] TouchCircles;

		[Tooltip("The default DPI to use if the DPI cannot be determined")]
		public int DefaultDPI = 200;

		[Tooltip("Whether to clear all gestures and remove them when a new level loads. This is almost always what you want, unless you are setting up your gestures once in your first scene and using them for all scenes in your game.")]
		public bool ClearGesturesOnLevelLoad = true;

		private const int mousePointerId1 = 2147483645;

		private const int mousePointerId2 = 2147483644;

		private const int mousePointerId3 = 2147483643;

		private readonly KeyValuePair<float, float>[] mousePrev = new KeyValuePair<float, float>[3];

		private readonly bool[] isMouseDown = new bool[3];

		private readonly List<GestureRecognizer> gestures = new List<GestureRecognizer>();

		private readonly List<GestureRecognizer> gesturesTemp = new List<GestureRecognizer>();

		private readonly HashSet<int> isTouchDown = new HashSet<int>();

		private readonly List<GestureTouch> touchesBegan = new List<GestureTouch>();

		private readonly List<GestureTouch> touchesMoved = new List<GestureTouch>();

		private readonly List<GestureTouch> touchesEnded = new List<GestureTouch>();

		private readonly Dictionary<int, List<GameObject>> gameObjectsForTouch = new Dictionary<int, List<GameObject>>();

		private readonly List<RaycastResult> captureRaycastResults = new List<RaycastResult>();

		private readonly List<GestureTouch> filteredTouches = new List<GestureTouch>();

		private readonly List<GestureTouch> touches = new List<GestureTouch>();

		private readonly Dictionary<int, Vector2> previousTouchPositions = new Dictionary<int, Vector2>();

		private readonly List<Component> components = new List<Component>();

		private readonly HashSet<Type> componentTypesToDenyPassThrough = new HashSet<Type>();

		private readonly HashSet<Type> componentTypesToIgnorePassThrough = new HashSet<Type>();

		private readonly Collider2D[] hackResults = new Collider2D[128];

		private float rotateAngle;

		private float pinchScale = 1f;

		private GestureTouch rotatePinch1;

		private GestureTouch rotatePinch2;

		private static FingersScript singleton;

		public Func<GameObject, bool?> CaptureGestureHandler;

		public ICollection<GestureTouch> Touches
		{
			get
			{
				return this.touches;
			}
		}

		public HashSet<Type> ComponentTypesToDenyPassThrough
		{
			get
			{
				return this.componentTypesToDenyPassThrough;
			}
		}

		public HashSet<Type> ComponentTypesToIgnorePassThrough
		{
			get
			{
				return this.componentTypesToIgnorePassThrough;
			}
		}

		public static FingersScript Instance
		{
			get
			{
				if (FingersScript.singleton == null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("FingersScriptPrefab") as GameObject);
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					FingersScript.singleton = gameObject.GetComponent<FingersScript>();
				}
				return FingersScript.singleton;
			}
		}

		public static bool HasInstance
		{
			get
			{
				return FingersScript.singleton != null;
			}
		}

		private IEnumerator MainThreadCallback(float delay, Action action)
		{
			FingersScript._MainThreadCallback_c__Iterator0 _MainThreadCallback_c__Iterator = new FingersScript._MainThreadCallback_c__Iterator0();
			_MainThreadCallback_c__Iterator.action = action;
			_MainThreadCallback_c__Iterator.delay = delay;
			return _MainThreadCallback_c__Iterator;
		}

		private FingersScript.CaptureResult ShouldCaptureGesture(GameObject obj)
		{
			if (obj == null)
			{
				return FingersScript.CaptureResult.Default;
			}
			if (this.CaptureGestureHandler != null)
			{
				bool? flag = this.CaptureGestureHandler(obj);
				if (flag.HasValue)
				{
					return (!flag.Value) ? FingersScript.CaptureResult.ForcePassThrough : FingersScript.CaptureResult.ForceDenyPassThrough;
				}
			}
			if (this.PassThroughObjects.Contains(obj))
			{
				return FingersScript.CaptureResult.ForcePassThrough;
			}
			foreach (GestureRecognizer current in this.gestures)
			{
				if (object.ReferenceEquals(current.PlatformSpecificView, obj))
				{
					FingersScript.CaptureResult result = FingersScript.CaptureResult.Default;
					return result;
				}
			}
			obj.GetComponents<Component>(this.components);
			try
			{
				foreach (Component current2 in this.components)
				{
					Type type = current2.GetType();
					if (this.componentTypesToDenyPassThrough.Contains(type))
					{
						FingersScript.CaptureResult result = FingersScript.CaptureResult.ForceDenyPassThrough;
						return result;
					}
					if (this.componentTypesToIgnorePassThrough.Contains(type))
					{
						FingersScript.CaptureResult result = FingersScript.CaptureResult.Ignore;
						return result;
					}
				}
			}
			finally
			{
				this.components.Clear();
			}
			return FingersScript.CaptureResult.Default;
		}

		private void PopulateGameObjectsForTouch(int pointerId, float x, float y)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			List<GameObject> list;
			if (this.gameObjectsForTouch.TryGetValue(pointerId, out list))
			{
				list.Clear();
			}
			else
			{
				list = new List<GameObject>();
				this.gameObjectsForTouch[pointerId] = list;
			}
			this.captureRaycastResults.Clear();
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			pointerEventData.Reset();
			pointerEventData.position = new Vector2(x, y);
			pointerEventData.clickCount = 1;
			EventSystem.current.RaycastAll(pointerEventData, this.captureRaycastResults);
			int num = Physics2D.OverlapPointNonAlloc(pointerEventData.position, this.hackResults);
			for (int i = 0; i < num; i++)
			{
				RaycastResult result = new RaycastResult
				{
					gameObject = this.hackResults[i].gameObject
				};
				if (this.captureRaycastResults.FindIndex((RaycastResult cmp) => cmp.gameObject == result.gameObject) < 0)
				{
					this.captureRaycastResults.Add(result);
				}
			}
			Array.Clear(this.hackResults, 0, num);
			if (this.captureRaycastResults.Count == 0)
			{
				this.captureRaycastResults.Add(default(RaycastResult));
			}
			foreach (RaycastResult current in this.captureRaycastResults)
			{
				switch (this.ShouldCaptureGesture(current.gameObject))
				{
				case FingersScript.CaptureResult.ForcePassThrough:
					list.Clear();
					return;
				case FingersScript.CaptureResult.ForceDenyPassThrough:
					list.Add(current.gameObject);
					return;
				case FingersScript.CaptureResult.Default:
					list.Add(current.gameObject);
					break;
				}
			}
		}

		private void GestureTouchFromTouch(ref Touch t, out GestureTouch g)
		{
			Vector2 vector;
			if (!this.previousTouchPositions.TryGetValue(t.fingerId, out vector))
			{
				vector.x = t.position.x;
				vector.y = t.position.y;
			}
			g = new GestureTouch(t.fingerId, t.position.x, t.position.y, vector.x, vector.y, t.pressure, t.rawPosition.x, t.rawPosition.y, t);
		}

		private void ProcessTouch(ref Touch t)
		{
			GestureTouch item;
			this.GestureTouchFromTouch(ref t, out item);
			if (this.isTouchDown.Contains(t.fingerId))
			{
				if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
				{
					this.touchesMoved.Add(item);
					this.previousTouchPositions[t.fingerId] = t.position;
				}
				else if (t.phase != TouchPhase.Began)
				{
					this.touchesEnded.Add(item);
					this.isTouchDown.Remove(t.fingerId);
					this.previousTouchPositions.Remove(t.fingerId);
				}
				else
				{
					UnityEngine.Debug.LogError("Invalid state, touch " + t.fingerId + " began but isTouchDown was already true for this touch");
				}
			}
			else if (t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled)
			{
				this.touchesBegan.Add(item);
				this.isTouchDown.Add(t.fingerId);
				this.previousTouchPositions[t.fingerId] = t.position;
			}
		}

		private void AddMouseTouch(int index, int pointerId, float x, float y)
		{
			float num = this.mousePrev[index].Key;
			float num2 = this.mousePrev[index].Value;
			num = ((num != -3.40282347E+38f) ? num : x);
			num2 = ((num2 != -3.40282347E+38f) ? num2 : y);
			GestureTouch item = new GestureTouch(pointerId, x, y, num, num2, 0f, x, y);
			if (Input.GetMouseButton(index))
			{
				this.mousePrev[index] = new KeyValuePair<float, float>(x, y);
				if (this.isMouseDown[index])
				{
					this.touchesMoved.Add(item);
				}
				else
				{
					this.touchesBegan.Add(item);
					this.isMouseDown[index] = true;
				}
			}
			else if (this.isMouseDown[index])
			{
				this.mousePrev[index] = new KeyValuePair<float, float>(-3.40282347E+38f, -3.40282347E+38f);
				this.touchesEnded.Add(item);
				this.isMouseDown[index] = false;
			}
		}

		private void ProcessTouches()
		{
			for (int i = 0; i < UnityEngine.Input.touchCount; i++)
			{
				Touch touch = UnityEngine.Input.GetTouch(i);
				this.ProcessTouch(ref touch);
			}
		}

		private void RotateAroundPoint(ref float rotX, ref float rotY, float anchorX, float anchorY, float angleRadians)
		{
			float num = Mathf.Cos(angleRadians);
			float num2 = Mathf.Sin(angleRadians);
			float num3 = rotX - anchorX;
			float num4 = rotY - anchorY;
			rotX = num * num3 - num2 * num4 + anchorX;
			rotY = num2 * num3 + num * num4 + anchorY;
		}

		private void ProcessMouseButtons()
		{
			if (!Input.mousePresent || !this.TreatMousePointerAsFinger)
			{
				return;
			}
			float x = UnityEngine.Input.mousePosition.x;
			float y = UnityEngine.Input.mousePosition.y;
			this.AddMouseTouch(0, 2147483645, x, y);
			this.AddMouseTouch(1, 2147483644, x, y);
			this.AddMouseTouch(2, 2147483643, x, y);
		}

		private void ProcessMouseWheel()
		{
			if (!Input.mousePresent || !this.TreatMousePointerAsFinger)
			{
				return;
			}
			Vector2 mouseScrollDelta = Input.mouseScrollDelta;
			float num = ((mouseScrollDelta.y != 0f) ? mouseScrollDelta.y : mouseScrollDelta.x) * 0.025f;
			int a = 4;
			int b = 4;
			if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl) || UnityEngine.Input.GetKeyDown(KeyCode.RightControl))
			{
				a = 2;
			}
			else if (UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl))
			{
				this.pinchScale += num;
				a = 1;
			}
			else if (UnityEngine.Input.GetKeyUp(KeyCode.LeftControl) || UnityEngine.Input.GetKeyUp(KeyCode.RightControl))
			{
				a = 3;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) || UnityEngine.Input.GetKeyDown(KeyCode.RightShift))
			{
				b = 2;
			}
			else if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
			{
				this.rotateAngle += num;
				b = 1;
			}
			else if (UnityEngine.Input.GetKeyUp(KeyCode.LeftShift) || UnityEngine.Input.GetKeyUp(KeyCode.RightShift))
			{
				b = 3;
			}
			int num2 = Mathf.Min(a, b);
			if (num2 == 4)
			{
				this.pinchScale = 1f;
				this.rotateAngle = 0f;
				return;
			}
			float x = UnityEngine.Input.mousePosition.x;
			float y = UnityEngine.Input.mousePosition.y;
			float num3 = x - 100f;
			float num4 = y;
			float num5 = x + 100f;
			float num6 = y;
			float num7 = 100f * this.pinchScale;
			num3 = x - num7;
			num4 = y;
			num5 = x + num7;
			num6 = y;
			this.RotateAroundPoint(ref num3, ref num4, x, y, this.rotateAngle);
			this.RotateAroundPoint(ref num5, ref num6, x, y, this.rotateAngle);
			if (num2 == 1)
			{
				this.rotatePinch1 = new GestureTouch(2147483642, num3, num4, this.rotatePinch1.X, this.rotatePinch1.Y, 0f, num3, num4);
				this.rotatePinch2 = new GestureTouch(2147483641, num5, num6, this.rotatePinch2.X, this.rotatePinch2.Y, 0f, num5, num6);
				this.touchesMoved.Add(this.rotatePinch1);
				this.touchesMoved.Add(this.rotatePinch2);
			}
			else if (num2 == 2)
			{
				this.rotatePinch1 = new GestureTouch(2147483642, num3, num4, num3, num4, 0f, num3, num4);
				this.rotatePinch2 = new GestureTouch(2147483641, num5, num6, num5, num6, 0f, num3, num4);
				this.touchesBegan.Add(this.rotatePinch1);
				this.touchesBegan.Add(this.rotatePinch2);
			}
			else if (num2 == 3)
			{
				this.touchesEnded.Add(this.rotatePinch1);
				this.touchesEnded.Add(this.rotatePinch2);
			}
		}

		private bool GameObjectMatchesPlatformSpecificView(List<GameObject> list, GestureRecognizer r)
		{
			GameObject gameObject = r.PlatformSpecificView as GameObject;
			if ((gameObject == null && EventSystem.current == null) || (gameObject != null && gameObject.GetComponent<Canvas>() != null))
			{
				return true;
			}
			if (list.Count == 0)
			{
				return gameObject == null;
			}
			foreach (GameObject current in list)
			{
				if (current == gameObject)
				{
					bool result = true;
					return result;
				}
				bool flag = current != null && (current.GetComponent<Collider2D>() != null || current.GetComponent<Collider>() != null);
				if (flag && gameObject == null)
				{
					bool result = true;
					return result;
				}
			}
			return false;
		}

		private ICollection<GestureTouch> FilterTouchesBegan(List<GestureTouch> touches, GestureRecognizer r)
		{
			this.filteredTouches.Clear();
			foreach (GestureTouch current in touches)
			{
				List<GameObject> list;
				if (!this.gameObjectsForTouch.TryGetValue(current.Id, out list) || this.GameObjectMatchesPlatformSpecificView(list, r))
				{
					this.filteredTouches.Add(current);
				}
			}
			return this.filteredTouches;
		}

		private void CleanupPassThroughObjects()
		{
			if (this.PassThroughObjects == null)
			{
				this.PassThroughObjects = new List<GameObject>();
			}
			for (int i = this.PassThroughObjects.Count - 1; i >= 0; i--)
			{
				if (this.PassThroughObjects[i] == null)
				{
					this.PassThroughObjects.RemoveAt(i);
				}
			}
		}

		private void ResetState(bool clearGestures)
		{
			if (clearGestures)
			{
				this.gestures.Clear();
			}
			else
			{
				foreach (GestureRecognizer current in this.gestures)
				{
					current.Reset();
				}
			}
			this.touchesBegan.Clear();
			this.touchesMoved.Clear();
			this.touchesEnded.Clear();
			this.gameObjectsForTouch.Clear();
			this.captureRaycastResults.Clear();
			this.filteredTouches.Clear();
			this.touches.Clear();
			this.previousTouchPositions.Clear();
			this.rotateAngle = 0f;
			this.pinchScale = 1f;
			this.rotatePinch1 = default(GestureTouch);
			this.rotatePinch2 = default(GestureTouch);
			this.isTouchDown.Clear();
			for (int i = 0; i < this.mousePrev.Length; i++)
			{
				this.mousePrev[i] = new KeyValuePair<float, float>(-3.40282347E+38f, -3.40282347E+38f);
			}
			for (int j = 0; j < this.isMouseDown.Length; j++)
			{
				this.isMouseDown[j] = false;
			}
			for (int k = this.PassThroughObjects.Count - 1; k >= 0; k--)
			{
				if (this.PassThroughObjects[k] == null)
				{
					this.PassThroughObjects.RemoveAt(k);
				}
			}
		}

		private void SetupDefaultPassThroughComponents()
		{
			this.componentTypesToDenyPassThrough.Add(typeof(Scrollbar));
			this.componentTypesToDenyPassThrough.Add(typeof(Button));
			this.componentTypesToDenyPassThrough.Add(typeof(Dropdown));
			this.componentTypesToDenyPassThrough.Add(typeof(Toggle));
			this.componentTypesToDenyPassThrough.Add(typeof(Slider));
			this.componentTypesToDenyPassThrough.Add(typeof(InputField));
			this.componentTypesToIgnorePassThrough.Add(typeof(Text));
		}

		private void SceneManagerSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			this.ResetState(this.ClearGesturesOnLevelLoad);
		}

		private void Awake()
		{
			if (FingersScript.singleton != null && FingersScript.singleton != this)
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject, true);
				return;
			}
			FingersScript.singleton = this;
			DeviceInfo.PixelsPerInch = (int)Screen.dpi;
			if (DeviceInfo.PixelsPerInch > 0)
			{
				DeviceInfo.UnitMultiplier = DeviceInfo.PixelsPerInch;
			}
			else
			{
				int defaultDPI = this.DefaultDPI;
				DeviceInfo.PixelsPerInch = defaultDPI;
				DeviceInfo.UnitMultiplier = defaultDPI;
			}
			GestureRecognizer.MainThreadCallback = delegate(float delay, Action callback)
			{
				base.StartCoroutine(this.MainThreadCallback(delay, callback));
			};
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.SceneManagerSceneLoaded);
			this.ResetState();
			Input.multiTouchEnabled = true;
			Input.simulateMouseWithTouches = this.SimulateMouseWithTouches;
			this.SetupDefaultPassThroughComponents();
		}

		private void Update()
		{
			if (base.gameObject.transform.childCount > 0 && base.gameObject.transform.GetChild(0).GetComponent<Canvas>() != null)
			{
				base.gameObject.transform.GetChild(0).gameObject.SetActive(this.ShowTouches);
			}
			this.CleanupPassThroughObjects();
			this.touchesBegan.Clear();
			this.touchesMoved.Clear();
			this.touchesEnded.Clear();
			this.ProcessTouches();
			this.ProcessMouseButtons();
			this.ProcessMouseWheel();
			foreach (GestureTouch current in this.touchesBegan)
			{
				this.PopulateGameObjectsForTouch(current.Id, current.X, current.Y);
			}
			this.gesturesTemp.AddRange(this.gestures);
			foreach (GestureRecognizer current2 in this.gesturesTemp)
			{
				current2.ProcessTouchesBegan(this.FilterTouchesBegan(this.touchesBegan, current2));
				current2.ProcessTouchesMoved(this.touchesMoved);
				current2.ProcessTouchesEnded(this.touchesEnded);
			}
			this.gesturesTemp.Clear();
			foreach (GestureTouch current3 in this.touchesEnded)
			{
				this.gameObjectsForTouch.Remove(current3.Id);
			}
			this.touches.Clear();
			this.touches.AddRange(this.touchesBegan);
			this.touches.AddRange(this.touchesMoved);
			this.touches.AddRange(this.touchesEnded);
		}

		private void LateUpdate()
		{
			if (this.ShowTouches && this.TouchCircles != null && this.TouchCircles.Length != 0)
			{
				int i = 0;
				foreach (GestureTouch current in this.Touches)
				{
					GameObject gameObject = this.TouchCircles[i++];
					gameObject.SetActive(true);
					gameObject.transform.position = new Vector3(current.X, current.Y);
				}
				while (i < this.TouchCircles.Length)
				{
					this.TouchCircles[i++].gameObject.SetActive(false);
				}
			}
		}

		private void OnDestroy()
		{
			if (FingersScript.singleton == this)
			{
				FingersScript.singleton = null;
			}
		}

		public bool AddGesture(GestureRecognizer gesture)
		{
			if (gesture == null || this.gestures.Contains(gesture))
			{
				return false;
			}
			this.gestures.Add(gesture);
			return true;
		}

		public bool RemoveGesture(GestureRecognizer gesture)
		{
			return this.gestures.Remove(gesture);
		}

		public void ResetState()
		{
			this.ResetState(false);
		}

		public static Rect RectTransformToScreenSpace(RectTransform transform)
		{
			Vector2 vector = Vector2.Scale(transform.rect.size, transform.lossyScale);
			float x = transform.position.x - vector.x * 0.5f;
			float y = transform.position.y - vector.y * 0.5f;
			return new Rect(x, y, vector.x, vector.y);
		}
	}
}
