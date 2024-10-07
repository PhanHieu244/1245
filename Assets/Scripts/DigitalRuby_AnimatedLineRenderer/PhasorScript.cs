using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.AnimatedLineRenderer
{
	[RequireComponent(typeof(AnimatedLineRenderer))]
	public class PhasorScript : MonoBehaviour
	{
		private sealed class _EndFireDelay_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float delay;

			internal int token;

			internal PhasorScript _this;

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

			public _EndFireDelay_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (this.delay > 0f)
					{
						this._current = new WaitForSeconds(this.delay);
						if (!this._disposing)
						{
							this._PC = 1;
						}
						return true;
					}
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._this.endFireToken == this.token)
				{
					this._this.EndFire(null);
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

		[HideInInspector]
		public Action<RaycastHit2D[]> HitCallback;

		[Tooltip("Source of the phasor")]
		public GameObject Source;

		[Tooltip("Target to fire at")]
		public GameObject Target;

		[Tooltip("Sound to make when the phasor fires")]
		public AudioSource FireSound;

		private AnimatedLineRenderer lineRenderer;

		private bool firing;

		private bool endingFiring;

		private int endFireToken;

		private void Start()
		{
			this.lineRenderer = base.GetComponent<AnimatedLineRenderer>();
		}

		private void Update()
		{
			if (this.CanEndFire())
			{
				RaycastHit2D[] array = Physics2D.CircleCastAll(this.lineRenderer.StartPoint, this.lineRenderer.EndWidth * 0.5f, this.lineRenderer.EndPoint - this.lineRenderer.StartPoint, Vector3.Distance(this.lineRenderer.EndPoint, this.lineRenderer.StartPoint));
				if (array != null && array.Length != 0)
				{
					this.EndFire(new Vector3?(array[0].point));
					if (this.HitCallback != null)
					{
						this.HitCallback(array);
					}
				}
			}
		}

		private bool CanEndFire()
		{
			return this.firing && !this.endingFiring && !this.lineRenderer.Resetting;
		}

		private void EndFire(Vector3? endPoint)
		{
			this.endFireToken++;
			this.endingFiring = true;
			this.lineRenderer.ResetAfterSeconds(0.2f, endPoint, delegate
			{
				this.firing = false;
				this.endingFiring = false;
			});
		}

		private IEnumerator EndFireDelay(float delay, int token)
		{
			PhasorScript._EndFireDelay_c__Iterator0 _EndFireDelay_c__Iterator = new PhasorScript._EndFireDelay_c__Iterator0();
			_EndFireDelay_c__Iterator.delay = delay;
			_EndFireDelay_c__Iterator.token = token;
			_EndFireDelay_c__Iterator._this = this;
			return _EndFireDelay_c__Iterator;
		}

		public bool Fire()
		{
			return this.Fire(this.Source.transform.position, this.Target.transform.position);
		}

		public bool Fire(Vector3 target)
		{
			return this.Fire(this.Source.transform.position, target);
		}

		public bool Fire(Vector3 source, Vector3 target)
		{
			if (this.firing)
			{
				return false;
			}
			this.firing = true;
			this.lineRenderer.Enqueue(source);
			this.lineRenderer.Enqueue(target);
			base.StartCoroutine(this.EndFireDelay(this.lineRenderer.SecondsPerLine, ++this.endFireToken));
			if (this.FireSound != null)
			{
				this.FireSound.Play();
			}
			return true;
		}
	}
}
