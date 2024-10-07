using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	public class DemoScript : MonoBehaviour
	{
		public GameObject Earth;

		public Text dpiLabel;

		public Text bottomLabel;

		public GameObject AsteroidPrefab;

		public Material LineMaterial;

		private Sprite[] asteroids;

		private TapGestureRecognizer tapGesture;

		private TapGestureRecognizer doubleTapGesture;

		private TapGestureRecognizer tripleTapGesture;

		private SwipeGestureRecognizer swipeGesture;

		private PanGestureRecognizer panGesture;

		private ScaleGestureRecognizer scaleGesture;

		private RotateGestureRecognizer rotateGesture;

		private LongPressGestureRecognizer longPressGesture;

		private float nextAsteroid = -3.40282347E+38f;

		private GameObject draggingAsteroid;

		private readonly List<Vector3> swipeLines = new List<Vector3>();

		private static Func<GameObject, bool?> __f__mg_cache0;

		private void DebugText(string text, params object[] format)
		{
			UnityEngine.Debug.Log(string.Format(text, format));
		}

		private GameObject CreateAsteroid(float screenX, float screenY)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AsteroidPrefab);
			gameObject.name = "Asteroid";
			SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
			component.sprite = this.asteroids[UnityEngine.Random.Range(0, this.asteroids.Length - 1)];
			if (screenX == -3.40282347E+38f || screenY == -3.40282347E+38f)
			{
				float x = UnityEngine.Random.Range(Camera.main.rect.min.x, Camera.main.rect.max.x);
				float y = UnityEngine.Random.Range(Camera.main.rect.min.y, Camera.main.rect.max.y);
				Vector3 position = new Vector3(x, y, 0f);
				position = Camera.main.ViewportToWorldPoint(position);
				position.z = gameObject.transform.position.z;
				gameObject.transform.position = position;
			}
			else
			{
				Vector3 position2 = new Vector3(screenX, screenY, 0f);
				position2 = Camera.main.ScreenToWorldPoint(position2);
				position2.z = gameObject.transform.position.z;
				gameObject.transform.position = position2;
			}
			gameObject.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(0f, 30f);
			Vector2 velocity = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(0f, 30f);
			gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
			float num = UnityEngine.Random.Range(1f, 4f);
			gameObject.transform.localScale = new Vector3(num, num, 1f);
			gameObject.GetComponent<Rigidbody2D>().mass *= num * num;
			return gameObject;
		}

		private void RemoveAsteroids(float screenX, float screenY, float radius)
		{
			Vector3 vector = new Vector3(screenX, screenY, 0f);
			vector = Camera.main.ScreenToWorldPoint(vector);
			RaycastHit2D[] array = Physics2D.CircleCastAll(vector, radius, Vector2.zero);
			RaycastHit2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				RaycastHit2D raycastHit2D = array2[i];
				UnityEngine.Object.Destroy(raycastHit2D.transform.gameObject);
			}
		}

		private void BeginDrag(float screenX, float screenY)
		{
			Vector3 vector = new Vector3(screenX, screenY, 0f);
			vector = Camera.main.ScreenToWorldPoint(vector);
			RaycastHit2D raycastHit2D = Physics2D.CircleCast(vector, 10f, Vector2.zero);
			if (raycastHit2D.transform != null && raycastHit2D.transform.gameObject.name == "Asteroid")
			{
				this.draggingAsteroid = raycastHit2D.transform.gameObject;
				this.draggingAsteroid.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				this.draggingAsteroid.GetComponent<Rigidbody2D>().angularVelocity = 0f;
			}
			else
			{
				this.longPressGesture.Reset();
			}
		}

		private void DragTo(float screenX, float screenY)
		{
			if (this.draggingAsteroid == null)
			{
				return;
			}
			Vector3 vector = new Vector3(screenX, screenY, 0f);
			vector = Camera.main.ScreenToWorldPoint(vector);
			this.draggingAsteroid.GetComponent<Rigidbody2D>().MovePosition(vector);
		}

		private void EndDrag(float velocityXScreen, float velocityYScreen)
		{
			if (this.draggingAsteroid == null)
			{
				return;
			}
			Vector3 b = Camera.main.ScreenToWorldPoint(Vector3.zero);
			Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(velocityXScreen, velocityYScreen, 0f));
			Vector3 vector = a - b;
			this.draggingAsteroid.GetComponent<Rigidbody2D>().velocity = vector;
			this.draggingAsteroid.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(5f, 45f);
			this.draggingAsteroid = null;
			this.DebugText("Long tap flick velocity: {0}", new object[]
			{
				vector
			});
		}

		private void HandleSwipe(float endX, float endY)
		{
			Vector2 v = new Vector2(this.swipeGesture.StartFocusX, this.swipeGesture.StartFocusY);
			Vector3 vector = Camera.main.ScreenToWorldPoint(v);
			Vector3 vector2 = Camera.main.ScreenToWorldPoint(new Vector2(endX, endY));
			float num = Vector3.Distance(vector, vector2);
			vector.z = (vector2.z = 0f);
			this.swipeLines.Add(vector);
			this.swipeLines.Add(vector2);
			if (this.swipeLines.Count > 4)
			{
				this.swipeLines.RemoveRange(0, this.swipeLines.Count - 4);
			}
			RaycastHit2D[] array = Physics2D.CircleCastAll(vector, 10f, (vector2 - vector).normalized, num);
			if (array.Length != 0)
			{
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"Raycast hits: ",
					array.Length,
					", start: ",
					vector,
					", end: ",
					vector2,
					", distance: ",
					num
				}));
				Vector3 b = Camera.main.ScreenToWorldPoint(Vector3.zero);
				Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(this.swipeGesture.VelocityX, this.swipeGesture.VelocityY, Camera.main.nearClipPlane));
				Vector3 a2 = a - b;
				Vector2 force = a2 * 500f;
				RaycastHit2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit2D raycastHit2D = array2[i];
					raycastHit2D.rigidbody.AddForceAtPosition(force, raycastHit2D.point);
				}
			}
		}

		private void TapGestureCallback(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				this.DebugText("Tapped at {0}, {1}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY
				});
				this.CreateAsteroid(gesture.FocusX, gesture.FocusY);
			}
		}

		private void CreateTapGesture()
		{
			this.tapGesture = new TapGestureRecognizer();
			this.tapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapGestureCallback);
			this.tapGesture.RequireGestureRecognizerToFail = this.doubleTapGesture;
			FingersScript.Instance.AddGesture(this.tapGesture);
		}

		private void DoubleTapGestureCallback(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				this.DebugText("Double tapped at {0}, {1}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY
				});
				this.RemoveAsteroids(gesture.FocusX, gesture.FocusY, 16f);
			}
		}

		private void CreateDoubleTapGesture()
		{
			this.doubleTapGesture = new TapGestureRecognizer();
			this.doubleTapGesture.NumberOfTapsRequired = 2;
			this.doubleTapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.DoubleTapGestureCallback);
			this.doubleTapGesture.RequireGestureRecognizerToFail = this.tripleTapGesture;
			FingersScript.Instance.AddGesture(this.doubleTapGesture);
		}

		private void SwipeGestureCallback(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				this.HandleSwipe(gesture.FocusX, gesture.FocusY);
				this.DebugText("Swiped from {0},{1} to {2},{3}; velocity: {4}, {5}", new object[]
				{
					gesture.StartFocusX,
					gesture.StartFocusY,
					gesture.FocusX,
					gesture.FocusY,
					this.swipeGesture.VelocityX,
					this.swipeGesture.VelocityY
				});
			}
		}

		private void CreateSwipeGesture()
		{
			this.swipeGesture = new SwipeGestureRecognizer();
			this.swipeGesture.Direction = SwipeGestureRecognizerDirection.Any;
			this.swipeGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.SwipeGestureCallback);
			this.swipeGesture.DirectionThreshold = 1f;
			FingersScript.Instance.AddGesture(this.swipeGesture);
		}

		private void PanGestureCallback(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Executing)
			{
				this.DebugText("Panned, Location: {0}, {1}, Delta: {2}, {3}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY,
					gesture.DeltaX,
					gesture.DeltaY
				});
				float num = this.panGesture.DeltaX / 25f;
				float num2 = this.panGesture.DeltaY / 25f;
				Vector3 position = this.Earth.transform.position;
				position.x += num;
				position.y += num2;
				this.Earth.transform.position = position;
			}
		}

		private void CreatePanGesture()
		{
			this.panGesture = new PanGestureRecognizer();
			this.panGesture.MinimumNumberOfTouchesToTrack = 2;
			this.panGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.PanGestureCallback);
			FingersScript.Instance.AddGesture(this.panGesture);
		}

		private void ScaleGestureCallback(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Executing)
			{
				this.DebugText("Scaled: {0}, Focus: {1}, {2}", new object[]
				{
					this.scaleGesture.ScaleMultiplier,
					this.scaleGesture.FocusX,
					this.scaleGesture.FocusY
				});
				this.Earth.transform.localScale *= this.scaleGesture.ScaleMultiplier;
			}
		}

		private void CreateScaleGesture()
		{
			this.scaleGesture = new ScaleGestureRecognizer();
			this.scaleGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.ScaleGestureCallback);
			FingersScript.Instance.AddGesture(this.scaleGesture);
		}

		private void RotateGestureCallback(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Executing)
			{
				this.Earth.transform.Rotate(0f, 0f, this.rotateGesture.RotationRadiansDelta * 57.29578f);
			}
		}

		private void CreateRotateGesture()
		{
			this.rotateGesture = new RotateGestureRecognizer();
			this.rotateGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.RotateGestureCallback);
			FingersScript.Instance.AddGesture(this.rotateGesture);
		}

		private void LongPressGestureCallback(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Began)
			{
				this.DebugText("Long press began: {0}, {1}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY
				});
				this.BeginDrag(gesture.FocusX, gesture.FocusY);
			}
			else if (gesture.State == GestureRecognizerState.Executing)
			{
				this.DebugText("Long press moved: {0}, {1}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY
				});
				this.DragTo(gesture.FocusX, gesture.FocusY);
			}
			else if (gesture.State == GestureRecognizerState.Ended)
			{
				this.DebugText("Long press end: {0}, {1}, delta: {2}, {3}", new object[]
				{
					gesture.FocusX,
					gesture.FocusY,
					gesture.DeltaX,
					gesture.DeltaY
				});
				this.EndDrag(this.longPressGesture.VelocityX, this.longPressGesture.VelocityY);
			}
		}

		private void CreateLongPressGesture()
		{
			this.longPressGesture = new LongPressGestureRecognizer();
			this.longPressGesture.MaximumNumberOfTouchesToTrack = 1;
			this.longPressGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.LongPressGestureCallback);
			FingersScript.Instance.AddGesture(this.longPressGesture);
		}

		private void PlatformSpecificViewTapUpdated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.Log("You triple tapped the platform specific label!");
			}
		}

		private void CreatePlatformSpecificViewTripleTapGesture()
		{
			this.tripleTapGesture = new TapGestureRecognizer();
			this.tripleTapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.PlatformSpecificViewTapUpdated);
			this.tripleTapGesture.NumberOfTapsRequired = 3;
			this.tripleTapGesture.PlatformSpecificView = this.bottomLabel.gameObject;
			FingersScript.Instance.AddGesture(this.tripleTapGesture);
		}

		private static bool? CaptureGestureHandler(GameObject obj)
		{
			if (obj.name.StartsWith("PassThrough"))
			{
				return new bool?(false);
			}
			if (obj.name.StartsWith("NoPass"))
			{
				return new bool?(true);
			}
			return null;
		}

		private void Start()
		{
			this.asteroids = Resources.LoadAll<Sprite>("Asteroids");
			this.CreatePlatformSpecificViewTripleTapGesture();
			this.CreateDoubleTapGesture();
			this.CreateTapGesture();
			this.CreateSwipeGesture();
			this.CreatePanGesture();
			this.CreateScaleGesture();
			this.CreateRotateGesture();
			this.CreateLongPressGesture();
			this.panGesture.AllowSimultaneousExecution(this.scaleGesture);
			this.panGesture.AllowSimultaneousExecution(this.rotateGesture);
			this.scaleGesture.AllowSimultaneousExecution(this.rotateGesture);
			FingersScript arg_95_0 = FingersScript.Instance;
			if (DemoScript.__f__mg_cache0 == null)
			{
				DemoScript.__f__mg_cache0 = new Func<GameObject, bool?>(DemoScript.CaptureGestureHandler);
			}
			arg_95_0.CaptureGestureHandler = DemoScript.__f__mg_cache0;
			FingersScript.Instance.ShowTouches = true;
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				this.ReloadDemoScene();
			}
		}

		private void LateUpdate()
		{
			if (Time.timeSinceLevelLoad > this.nextAsteroid)
			{
				this.nextAsteroid = Time.timeSinceLevelLoad + UnityEngine.Random.Range(1f, 4f);
				this.CreateAsteroid(-3.40282347E+38f, -3.40282347E+38f);
			}
			int num = UnityEngine.Input.touchCount;
			if (FingersScript.Instance.TreatMousePointerAsFinger && Input.mousePresent)
			{
				num += ((!Input.GetMouseButton(0)) ? 0 : 1);
				num += ((!Input.GetMouseButton(1)) ? 0 : 1);
				num += ((!Input.GetMouseButton(2)) ? 0 : 1);
			}
			string text = string.Empty;
			Touch[] touches = Input.touches;
			for (int i = 0; i < touches.Length; i++)
			{
				Touch touch = touches[i];
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					":",
					touch.fingerId,
					":"
				});
			}
			this.dpiLabel.text = string.Concat(new object[]
			{
				"Dpi: ",
				DeviceInfo.PixelsPerInch,
				Environment.NewLine,
				"Width: ",
				Screen.width,
				Environment.NewLine,
				"Height: ",
				Screen.height,
				Environment.NewLine,
				"Touches: ",
				num,
				", ids",
				text,
				Environment.NewLine
			});
		}

		private void OnRenderObject()
		{
			GL.PushMatrix();
			this.LineMaterial.SetPass(0);
			GL.LoadProjectionMatrix(Camera.main.projectionMatrix);
			GL.Begin(1);
			for (int i = 0; i < this.swipeLines.Count; i++)
			{
				GL.Color(Color.white);
				GL.Vertex(this.swipeLines[i]);
				GL.Vertex(this.swipeLines[++i]);
			}
			GL.End();
			GL.PopMatrix();
		}

		public void ReloadDemoScene()
		{
			SceneManager.LoadScene(0, LoadSceneMode.Single);
		}
	}
}
