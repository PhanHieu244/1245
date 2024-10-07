using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace DigitalRubyShared
{
	[AddComponentMenu("Fingers Gestures/Orbit")]
	public class FingersOrbitScript : MonoBehaviour
	{
		[Tooltip("The transform to orbit around.")]
		public Transform OrbitTarget;

		[Tooltip("The object to orbit around OrbitTarget.")]
		public Transform Orbiter;

		[Range(0.1f, 100f), Tooltip("The minimium distance to zoom towards to the orbit target.")]
		public float MinZoomDistance = 5f;

		[Range(0.1f, 1000f), Tooltip("The maximum distance to zoom away from the orbit target.")]
		public float MaxZoomDistance = 1000f;

		[Range(0.01f, 3f), Tooltip("The zoom speed")]
		public float ZoomSpeed = 3f;

		[Range(-100f, 100f), Tooltip("The speed (degrees per second) at which to orbit using x delta pan gesture values. Negative or positive values will cause orbit in the opposite direction.")]
		public float OrbitXSpeed = -30f;

		[Range(0f, 360f), Tooltip("The maximum degrees to orbit on the x axis from the starting x rotation. 0 for no limit. Set OrbitXSpeed to 0 to disable x orbit.")]
		public float OrbitXMaxDegrees;

		[Range(-100f, 100f), Tooltip("The speed (degrees per second) at which to orbit using y delta pan gesture values. Negative or positive values will cause orbit in the opposite direction.")]
		public float OrbitYSpeed = -30f;

		[Range(0f, 360f), Tooltip("The maximum degrees to orbit on the y axis from the starting y rotation. 0 for no limit. Set OrbitYSpeed to 0 to disable y orbit.")]
		public float OrbitYMaxDegrees;

		[Tooltip("Whether to allow orbit while zooming.")]
		public bool AllowOrbitWhileZooming = true;

		private bool allowOrbitWhileZooming;

		[Range(0f, 1f), Tooltip("How much the velocity of the orbit will cause additional orbit after the gesture stops. 1 for no inertia (orbits forever) or 0 for immediate stop.")]
		public float OrbitInertia = 0.925f;

		[Tooltip("Whether the pan and rotate orbit gestures must start on the orbit target to orbit. The tap gesture always requires that it be on the orbit target.")]
		public bool RequireOrbitGesturesToStartOnTarget;

		private ScaleGestureRecognizer scaleGesture;

		private PanGestureRecognizer panGesture;

		private TapGestureRecognizer tapGesture;

		private float xDegrees;

		private float yDegrees;

		private Vector2 panVelocity;



		public event Action OrbitTargetTapped;

		private void Start()
		{
			this.scaleGesture = new ScaleGestureRecognizer();
			this.scaleGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.ScaleGesture_Updated);
			this.panGesture = new PanGestureRecognizer();
			this.panGesture.MaximumNumberOfTouchesToTrack = 2;
			this.panGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.PanGesture_Updated);
			this.tapGesture = new TapGestureRecognizer();
			this.tapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapGesture_Updated);
			this.tapGesture.PlatformSpecificView = this.OrbitTarget.gameObject;
			FingersScript.Instance.AddGesture(this.scaleGesture);
			FingersScript.Instance.AddGesture(this.panGesture);
			FingersScript.Instance.AddGesture(this.tapGesture);
			if (this.RequireOrbitGesturesToStartOnTarget)
			{
				this.scaleGesture.PlatformSpecificView = this.OrbitTarget.gameObject;
				this.panGesture.PlatformSpecificView = this.OrbitTarget.gameObject;
			}
			this.Orbiter.transform.LookAt(this.OrbitTarget.transform);
		}

		private void Update()
		{
			if (this.allowOrbitWhileZooming != this.AllowOrbitWhileZooming)
			{
				this.allowOrbitWhileZooming = this.AllowOrbitWhileZooming;
				if (this.allowOrbitWhileZooming)
				{
					this.scaleGesture.AllowSimultaneousExecution(this.panGesture);
				}
				else
				{
					this.scaleGesture.DisallowSimultaneousExecution(this.panGesture);
				}
			}
			this.scaleGesture.ZoomSpeed = this.ZoomSpeed;
			this.UpdateOrbit(this.panVelocity.x, this.panVelocity.y);
			this.panVelocity *= this.OrbitInertia;
		}

		private void UpdateOrbit(float xVelocity, float yVelocity)
		{
			if (this.OrbitXSpeed != 0f)
			{
				float num = yVelocity * this.OrbitXSpeed * Time.deltaTime;
				if (this.OrbitXMaxDegrees > 0f)
				{
					float num2 = this.xDegrees + num;
					if (num2 > this.OrbitXMaxDegrees)
					{
						num = this.OrbitXMaxDegrees - this.xDegrees;
					}
					else if (num2 < -this.OrbitXMaxDegrees)
					{
						num = -this.OrbitXMaxDegrees - this.xDegrees;
					}
				}
				this.xDegrees += num;
				this.Orbiter.RotateAround(this.OrbitTarget.transform.position, this.Orbiter.transform.right, num);
			}
			if (this.OrbitYSpeed != 0f)
			{
				float num3 = xVelocity * this.OrbitYSpeed * Time.deltaTime;
				if (this.OrbitYMaxDegrees > 0f)
				{
					float num4 = this.yDegrees + num3;
					if (num4 > this.OrbitYMaxDegrees)
					{
						num3 = this.OrbitYMaxDegrees - this.yDegrees;
					}
					else if (num4 < -this.OrbitYMaxDegrees)
					{
						num3 = -this.OrbitYMaxDegrees - this.yDegrees;
					}
				}
				this.yDegrees += num3;
				this.Orbiter.RotateAround(this.OrbitTarget.transform.position, Vector3.up, num3);
			}
		}

		private void TapGesture_Updated(GestureRecognizer gesture)
		{
			if (gesture.State == GestureRecognizerState.Ended)
			{
				UnityEngine.Debug.Log("Orbit target tapped!");
				if (this.OrbitTargetTapped != null)
				{
					this.OrbitTargetTapped();
				}
			}
		}

		private void PanGesture_Updated(GestureRecognizer gesture)
		{
			if (gesture.State != GestureRecognizerState.Executing)
			{
				if (gesture.State == GestureRecognizerState.Ended)
				{
					if (this.OrbitInertia > 0f)
					{
						this.panVelocity = new Vector2(gesture.VelocityX * 0.01f, gesture.VelocityY * 0.01f);
						if (this.OrbitXSpeed == 0f)
						{
							this.panVelocity.x = 0f;
						}
						if (this.OrbitYSpeed == 0f)
						{
							this.panVelocity.y = 0f;
						}
					}
				}
				else if (gesture.State == GestureRecognizerState.Began)
				{
					this.panVelocity = Vector2.zero;
				}
				return;
			}
			this.UpdateOrbit(gesture.DeltaX, gesture.DeltaY);
		}

		private void ScaleGesture_Updated(GestureRecognizer gesture)
		{
			if (gesture.State != GestureRecognizerState.Executing)
			{
				return;
			}
			float num = Vector3.Distance(this.Orbiter.transform.position, this.OrbitTarget.transform.position);
			float num2 = 1f + (1f - this.scaleGesture.ScaleMultiplier);
			num = Mathf.Clamp(num * num2, this.MinZoomDistance, this.MaxZoomDistance);
			this.Orbiter.transform.position = this.Orbiter.transform.forward * -num;
		}
	}
}
