using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptZoomPanCamera : MonoBehaviour
	{
		private sealed class _AnimationCoRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector3 _start___0;

			internal float _accumTime___1;

			internal DemoScriptZoomPanCamera _this;

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

			public _AnimationCoRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._start___0 = Camera.main.transform.position;
					this._accumTime___1 = Time.deltaTime;
					break;
				case 1u:
					this._accumTime___1 += Time.deltaTime;
					break;
				default:
					return false;
				}
				if (this._accumTime___1 <= 0.5f)
				{
					Camera.main.transform.position = Vector3.Lerp(this._start___0, this._this.cameraAnimationTargetPosition, this._accumTime___1 / 0.5f);
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
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

		private ScaleGestureRecognizer scaleGesture;

		private PanGestureRecognizer panGesture;

		private TapGestureRecognizer tapGesture;

		private Vector3 cameraAnimationTargetPosition;

		private IEnumerator AnimationCoRoutine()
		{
			DemoScriptZoomPanCamera._AnimationCoRoutine_c__Iterator0 _AnimationCoRoutine_c__Iterator = new DemoScriptZoomPanCamera._AnimationCoRoutine_c__Iterator0();
			_AnimationCoRoutine_c__Iterator._this = this;
			return _AnimationCoRoutine_c__Iterator;
		}

		private void Start()
		{
			this.scaleGesture = new ScaleGestureRecognizer
			{
				ZoomSpeed = 6f
			};
			this.scaleGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.Gesture_Updated);
			FingersScript.Instance.AddGesture(this.scaleGesture);
			this.panGesture = new PanGestureRecognizer();
			this.panGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.PanGesture_Updated);
			FingersScript.Instance.AddGesture(this.panGesture);
			this.scaleGesture.AllowSimultaneousExecution(this.panGesture);
			this.tapGesture = new TapGestureRecognizer();
			this.tapGesture.StateUpdated += new GestureRecognizerStateUpdatedDelegate(this.TapGesture_Updated);
			FingersScript.Instance.AddGesture(this.tapGesture);
		}

		private void TapGesture_Updated(GestureRecognizer gesture)
		{
			if (this.tapGesture.State != GestureRecognizerState.Ended)
			{
				return;
			}
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(this.tapGesture.FocusX, this.tapGesture.FocusY, 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				this.cameraAnimationTargetPosition = new Vector3(raycastHit.transform.position.x, raycastHit.transform.position.y, Camera.main.transform.position.z);
				base.StopAllCoroutines();
				base.StartCoroutine(this.AnimationCoRoutine());
			}
		}

		private void PanGesture_Updated(GestureRecognizer gesture)
		{
			if (this.panGesture.State == GestureRecognizerState.Executing)
			{
				base.StopAllCoroutines();
				float z = (!Camera.main.orthographic) ? 10f : 0f;
				Vector3 position = new Vector3(this.panGesture.DeltaX, this.panGesture.DeltaY, z);
				Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, z));
				Vector3 b = Camera.main.ScreenToWorldPoint(position);
				Vector3 translation = a - b;
				Camera.main.transform.Translate(translation);
			}
			else if (this.panGesture.State == GestureRecognizerState.Ended)
			{
				Camera.main.GetComponent<Rigidbody>().velocity = new Vector3(this.panGesture.VelocityX * -0.002f, this.panGesture.VelocityY * -0.002f, 0f);
			}
		}

		private void Gesture_Updated(GestureRecognizer gesture)
		{
			if (this.scaleGesture.State != GestureRecognizerState.Executing || this.scaleGesture.ScaleMultiplier == 1f)
			{
				return;
			}
			float num = 1f + (1f - this.scaleGesture.ScaleMultiplier);
			if (Camera.main.orthographic)
			{
				float orthographicSize = Mathf.Clamp(Camera.main.orthographicSize * num, 1f, 100f);
				Camera.main.orthographicSize = orthographicSize;
			}
			else
			{
				Vector3 forward = Camera.main.transform.forward;
				Vector3 position = Camera.main.transform.position;
				position.z = 0f;
				float num2 = Vector3.Distance(position, Camera.main.transform.position);
				float d = Mathf.Clamp(num2 * num, 1f, 100f);
				Camera.main.transform.position = position - forward * d;
			}
		}

		public void OrthographicCameraOptionChanged(bool orthographic)
		{
			Camera.main.orthographic = orthographic;
		}
	}
}
