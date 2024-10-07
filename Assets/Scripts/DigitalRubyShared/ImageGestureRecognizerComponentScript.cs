using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Gesture/Image Recognition Gesture", 8)]
	public class ImageGestureRecognizerComponentScript : GestureRecognizerComponentScript<ImageGestureRecognizer>
	{
		[Header("Image gesture properties"), Range(1f, 5f), Tooltip("The maximum number of distinct paths for each image. Gesture will reset when max path count is hit.")]
		public int MaximumPathCount = 1;

		[Range(0.01f, 1f), Tooltip("The amount that the path must change direction (in radians) to count as a new direction (0.39 is 1.8 of PI).")]
		public float DirectionTolerance = 0.3f;

		[Range(0.01f, 1f), Tooltip("The distance in units that the touch must move before the gesture begins.")]
		public float ThresholdUnits = 0.4f;

		[Range(0.01f, 1f), Tooltip("Minimum difference beteen points in units to count as a new point.")]
		public float MinimumDistanceBetweenPointsUnits = 0.1f;

		[Range(0.01f, 1f), Tooltip("The amount that the gesture image must match an image from the set to count as a match (0 - 1).")]
		public float SimilarityMinimum = 0.8f;

		[Range(2f, 10f), Tooltip("The minimum number of points before the gesture will recognize.")]
		public int MinimumPointsToRecognize = 2;

		[Tooltip("The images that should be compared against to find a match. The values are a ulong which match the bits of each generated image. See DemoSceneImage & DemoScriptImage.cs for an example.")]
		public List<ImageGestureRecognizerComponentScriptImageEntry> GestureImages;

		private Dictionary<ImageGestureImage, string> _GestureImagesToKey_k__BackingField;

		public Dictionary<ImageGestureImage, string> GestureImagesToKey
		{
			get;
			private set;
		}

		protected override void Start()
		{
			base.Start();
			base.Gesture.MaximumPathCount = this.MaximumPathCount;
			base.Gesture.DirectionTolerance = this.DirectionTolerance;
			base.Gesture.ThresholdUnits = this.ThresholdUnits;
			base.Gesture.MinimumDistanceBetweenPointsUnits = this.MinimumDistanceBetweenPointsUnits;
			base.Gesture.SimilarityMinimum = this.SimilarityMinimum;
			base.Gesture.MinimumPointsToRecognize = this.MinimumPointsToRecognize;
			base.Gesture.GestureImages = new List<ImageGestureImage>();
			this.GestureImagesToKey = new Dictionary<ImageGestureImage, string>();
			foreach (ImageGestureRecognizerComponentScriptImageEntry current in this.GestureImages)
			{
				List<ulong> list = new List<ulong>();
				string[] array = current.Images.Split(new char[]
				{
					'\n'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					string text2 = text.Trim();
					if (text2.Length != 0)
					{
						string[] array2 = text.Trim().Split(new char[]
						{
							','
						});
						string[] array3 = array2;
						for (int j = 0; j < array3.Length; j++)
						{
							string text3 = array3[j];
							string text4 = text3.Trim();
							if (text4.StartsWith("0x"))
							{
								text4 = text4.Substring(2);
							}
							list.Add(ulong.Parse(text4, NumberStyles.HexNumber));
						}
						ImageGestureImage imageGestureImage = new ImageGestureImage(list.ToArray(), 16, current.ScorePadding);
						base.Gesture.GestureImages.Add(imageGestureImage);
						this.GestureImagesToKey[imageGestureImage] = current.Key;
						list.Clear();
					}
				}
			}
		}
	}
}
