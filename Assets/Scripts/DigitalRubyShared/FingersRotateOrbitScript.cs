using System;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Rotation Orbit")]
	public class FingersRotateOrbitScript : MonoBehaviour
	{
		[Tooltip("The object to orbit")]
		public Transform OrbitTarget;

		[Tooltip("The object that will orbit around OrbitTarget")]
		public Transform Orbiter;

		[Tooltip("The axis to orbit around")]
		public Vector3 Axis = Vector3.up;

		[Range(0.01f, 1000f), Tooltip("The rotation speed in degrees per second")]
		public float RotationSpeed = 500f;

		private RotateGestureRecognizer rotationGesture;

		private void Start()
		{
			this.rotationGesture = new RotateGestureRecognizer();
			this.rotationGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.RotationGesture_Updated);
			FingersScript.Instance.AddGesture(this.rotationGesture);
		}

		private void RotationGesture_Updated(GestureRecognizer gesture)
		{
			this.Orbiter.transform.RotateAround(this.OrbitTarget.transform.position, this.Axis, this.rotationGesture.RotationDegreesDelta * Time.deltaTime * this.RotationSpeed);
		}
	}
}
