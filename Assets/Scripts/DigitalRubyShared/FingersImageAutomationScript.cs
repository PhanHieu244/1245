using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	public class FingersImageAutomationScript : MonoBehaviour
	{
		public RawImage Image;

		public ImageGestureRecognizer ImageGesture = new ImageGestureRecognizer();

		public Material LineMaterial;

		public Text MatchLabel;

		public InputField ScriptText;

		private ImageGestureImage _LastImage_k__BackingField;

		protected Dictionary<ImageGestureImage, string> RecognizableImages;

		private List<List<Vector2>> lineSet = new List<List<Vector2>>();

		private List<Vector2> currentPointList;

		protected ImageGestureImage LastImage
		{
			get;
			private set;
		}

		public void DeleteLastScriptLine()
		{
			if (this.ScriptText != null)
			{
				string[] array = this.ScriptText.text.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length > 0)
				{
					this.ScriptText.text = string.Join(Environment.NewLine, array, 0, array.Length - 1).Trim(new char[]
					{
						','
					});
				}
			}
		}

		protected virtual void Start()
		{
			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Tap_Updated);
			FingersScript.Instance.AddGesture(tapGestureRecognizer);
			this.ImageGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.ImageGestureUpdated);
			this.ImageGesture.MaximumPathCountExceeded += new EventHandler(this.MaximumPathCountExceeded);
			if (this.RecognizableImages != null)
			{
				this.ImageGesture.GestureImages = new List<ImageGestureImage>(this.RecognizableImages.Keys);
			}
			FingersScript.Instance.AddGesture(this.ImageGesture);
		}

		protected virtual void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
				{
					SceneManager.LoadScene(0);
				}
				else
				{
					this.ImageGesture.Reset();
					this.ResetLines();
					this.UpdateImage();
				}
			}
		}

		private void UpdateImage()
		{
			Texture2D texture2D = new Texture2D(16, 16, TextureFormat.ARGB32, false, false);
			texture2D.filterMode = FilterMode.Point;
			texture2D.wrapMode = TextureWrapMode.Clamp;
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					if (this.ImageGesture.Image.Pixels[j + i * 16] == 0)
					{
						texture2D.SetPixel(j, i, Color.clear);
					}
					else
					{
						texture2D.SetPixel(j, i, Color.white);
					}
				}
			}
			texture2D.Apply();
			this.Image.texture = texture2D;
			this.LastImage = this.ImageGesture.Image.Clone();
			if (this.ImageGesture.MatchedGestureImage == null)
			{
				this.MatchLabel.text = "No match";
			}
			else
			{
				this.MatchLabel.text = "Match: " + this.RecognizableImages[this.ImageGesture.MatchedGestureImage];
			}
			Text expr_FA = this.MatchLabel;
			string text = expr_FA.text;
			expr_FA.text = string.Concat(new object[]
			{
				text,
				" (",
				this.ImageGesture.MatchedGestureCalculationTimeMilliseconds,
				" ms)"
			});
		}

		private void UpdateScriptText()
		{
			if (this.ScriptText != null)
			{
				if (this.ScriptText.text.Length != 0)
				{
					InputField expr_2C = this.ScriptText;
					expr_2C.text = expr_2C.text + "," + Environment.NewLine;
				}
				InputField expr_4C = this.ScriptText;
				expr_4C.text += this.LastImage.GetCodeForRowsInitialize();
			}
		}

		private void AddTouches(ICollection<GestureTouch> touches)
		{
			GestureTouch? gestureTouch = null;
			using (IEnumerator<GestureTouch> enumerator = touches.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					GestureTouch current = enumerator.Current;
					gestureTouch = new GestureTouch?(current);
				}
			}
			if (gestureTouch.HasValue)
			{
				Vector3 vector = new Vector3(gestureTouch.Value.X, gestureTouch.Value.Y, 0f);
				vector = Camera.main.ScreenToWorldPoint(vector);
				this.currentPointList.Add(vector);
			}
		}

		private void ImageGestureUpdated(GestureRecognizer imageGesture)
		{
			if (imageGesture.State == GestureRecognizerState.Ended)
			{
				this.AddTouches(imageGesture.CurrentTrackedTouches);
				this.UpdateImage();
				this.UpdateScriptText();
			}
			else if (imageGesture.State == GestureRecognizerState.Began)
			{
				this.currentPointList = new List<Vector2>();
				this.lineSet.Add(this.currentPointList);
				this.AddTouches(imageGesture.CurrentTrackedTouches);
			}
			else if (imageGesture.State == GestureRecognizerState.Executing)
			{
				this.AddTouches(imageGesture.CurrentTrackedTouches);
			}
		}

		private void ResetLines()
		{
			this.currentPointList = null;
			this.lineSet.Clear();
			this.UpdateImage();
		}

		private void MaximumPathCountExceeded(object sender, EventArgs e)
		{
			this.ResetLines();
		}

		private void Tap_Updated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.Log("Tap Gesture Ended");
			}
		}

		private void OnRenderObject()
		{
			GL.PushMatrix();
			this.LineMaterial.SetPass(0);
			GL.LoadProjectionMatrix(Camera.main.projectionMatrix);
			GL.Begin(1);
			foreach (List<Vector2> current in this.lineSet)
			{
				for (int i = 1; i < current.Count; i++)
				{
					GL.Color(Color.white);
					GL.Vertex(current[i - 1]);
					GL.Vertex(current[i]);
				}
			}
			GL.End();
			GL.PopMatrix();
		}
	}
}
