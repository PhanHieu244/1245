using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DigitalRuby.AnimatedLineRenderer
{
	[RequireComponent(typeof(LineRenderer))]
	public class AnimatedLineRenderer : MonoBehaviour
	{
		private struct QueueItem
		{
			public Vector3 Position;

			public float ElapsedSeconds;

			public float TotalSeconds;

			public float TotalSecondsInverse;
		}

		private sealed class _ResetAfterSecondsInternal_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float seconds;

			internal Action callback;

			internal float _elapsedSeconds___0;

			internal float _secondsInverse___0;

			internal Color _c1___0;

			internal Color _c2___0;

			internal float _a___1;

			internal AnimatedLineRenderer _this;

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

			public _ResetAfterSecondsInternal_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (this.seconds <= 0f)
					{
						this._this.Reset();
						if (this.callback != null)
						{
							this.callback();
						}
						return false;
					}
					this._elapsedSeconds___0 = 0f;
					this._secondsInverse___0 = 1f / this.seconds;
					this._c1___0 = new Color(this._this.StartColor.r, this._this.StartColor.g, this._this.StartColor.b, 1f);
					this._c2___0 = new Color(this._this.EndColor.r, this._this.EndColor.g, this._this.EndColor.b, 1f);
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._elapsedSeconds___0 < this.seconds)
				{
					this._a___1 = 1f - this._secondsInverse___0 * this._elapsedSeconds___0;
					this._elapsedSeconds___0 += Time.deltaTime;
					this._c1___0.a = this._a___1;
					this._c2___0.a = this._a___1;
					this._this.lineRenderer.SetColors(this._c1___0, this._c2___0);
					this._current = new WaitForSeconds(0.01f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.Reset();
				if (this.callback != null)
				{
					this.callback();
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

		[Tooltip("The minimum distance that must be in between line segments (0 for infinite). Attempts to make lines with distances smaller than this will fail.")]
		public float MinimumDistance;

		[Tooltip("Seconds that each new line segment should animate with")]
		public float SecondsPerLine = 0.1f;

		[Tooltip("Start color for the line renderer since Unity does not provider a getter for this")]
		public Color StartColor = Color.white;

		[Tooltip("End color for the line renderer since Unity does not provide a getter for this")]
		public Color EndColor = Color.white;

		[Tooltip("Start line width")]
		public float StartWidth = 2f;

		[Tooltip("End line width")]
		public float EndWidth = 2f;

		[Tooltip("Sort layer name")]
		public string SortLayerName = "Default";

		[Tooltip("Order in sort layer")]
		public int OrderInSortLayer = 1;

		private LineRenderer lineRenderer;

		private readonly Queue<AnimatedLineRenderer.QueueItem> queue = new Queue<AnimatedLineRenderer.QueueItem>();

		private AnimatedLineRenderer.QueueItem prev;

		private AnimatedLineRenderer.QueueItem current;

		private AnimatedLineRenderer.QueueItem? lastQueued;

		private int index = -1;

		private float remainder;

		private Vector3 _StartPoint_k__BackingField;

		private Vector3 _EndPoint_k__BackingField;

		private bool _Resetting_k__BackingField;

		public LineRenderer LineRenderer
		{
			get
			{
				return this.lineRenderer;
			}
		}

		public Vector3 StartPoint
		{
			get;
			private set;
		}

		public Vector3 EndPoint
		{
			get;
			private set;
		}

		public bool Resetting
		{
			get;
			private set;
		}

		private void ProcessCurrent()
		{
			if (this.current.ElapsedSeconds == this.current.TotalSeconds)
			{
				if (this.queue.Count == 0)
				{
					return;
				}
				this.prev = this.current;
				this.current = this.queue.Dequeue();
				if (++this.index == 0)
				{
					this.lineRenderer.SetVertexCount(1);
					this.StartPoint = this.current.Position;
					this.current.ElapsedSeconds = (this.current.TotalSeconds = (this.current.TotalSecondsInverse = 0f));
					this.lineRenderer.SetPosition(0, this.current.Position);
					return;
				}
				this.lineRenderer.SetVertexCount(this.index + 1);
			}
			float num = this.current.ElapsedSeconds + Time.deltaTime + this.remainder;
			if (num > this.current.TotalSeconds)
			{
				this.remainder = num - this.current.TotalSeconds;
				this.current.ElapsedSeconds = this.current.TotalSeconds;
			}
			else
			{
				this.remainder = 0f;
				this.current.ElapsedSeconds = num;
			}
			this.current.ElapsedSeconds = Mathf.Min(this.current.TotalSeconds, this.current.ElapsedSeconds + Time.deltaTime);
			float t = this.current.TotalSecondsInverse * this.current.ElapsedSeconds;
			this.EndPoint = Vector3.Lerp(this.prev.Position, this.current.Position, t);
			this.lineRenderer.SetPosition(this.index, this.EndPoint);
		}

		private void Start()
		{
			this.lineRenderer = base.GetComponent<LineRenderer>();
			this.lineRenderer.SetVertexCount(0);
		}

		private void Update()
		{
			this.ProcessCurrent();
			if (!this.Resetting)
			{
				this.lineRenderer.SetColors(this.StartColor, this.EndColor);
				this.lineRenderer.SetWidth(this.StartWidth, this.EndWidth);
				this.lineRenderer.sortingLayerName = this.SortLayerName;
				this.lineRenderer.sortingOrder = this.OrderInSortLayer;
			}
		}

		private IEnumerator ResetAfterSecondsInternal(float seconds, Action callback)
		{
			AnimatedLineRenderer._ResetAfterSecondsInternal_c__Iterator0 _ResetAfterSecondsInternal_c__Iterator = new AnimatedLineRenderer._ResetAfterSecondsInternal_c__Iterator0();
			_ResetAfterSecondsInternal_c__Iterator.seconds = seconds;
			_ResetAfterSecondsInternal_c__Iterator.callback = callback;
			_ResetAfterSecondsInternal_c__Iterator._this = this;
			return _ResetAfterSecondsInternal_c__Iterator;
		}

		public bool Enqueue(Vector3 pos)
		{
			return this.Enqueue(pos, this.SecondsPerLine);
		}

		public bool Enqueue(Vector3 pos, float duration)
		{
			if (this.Resetting)
			{
				return false;
			}
			if (this.MinimumDistance > 0f)
			{
				AnimatedLineRenderer.QueueItem? queueItem = this.lastQueued;
				if (queueItem.HasValue)
				{
					Vector3 position = this.lastQueued.Value.Position;
					float num = Vector3.Distance(position, pos);
					if (num < this.MinimumDistance)
					{
						return false;
					}
				}
			}
			float num2 = Mathf.Max(0f, duration);
			AnimatedLineRenderer.QueueItem queueItem2 = new AnimatedLineRenderer.QueueItem
			{
				Position = pos,
				TotalSecondsInverse = ((num2 != 0f) ? (1f / num2) : 0f),
				TotalSeconds = num2,
				ElapsedSeconds = 0f
			};
			this.queue.Enqueue(queueItem2);
			this.lastQueued = new AnimatedLineRenderer.QueueItem?(queueItem2);
			return true;
		}

		public void Reset()
		{
			this.index = -1;
			this.prev = (this.current = default(AnimatedLineRenderer.QueueItem));
			this.lastQueued = null;
			if (this.lineRenderer != null)
			{
				this.lineRenderer.SetVertexCount(0);
			}
			this.remainder = 0f;
			this.queue.Clear();
			this.Resetting = false;
			Vector3 zero = Vector3.zero;
			this.EndPoint = zero;
			this.StartPoint = zero;
		}

		public void ResetAfterSeconds(float seconds)
		{
			this.ResetAfterSeconds(seconds, null, null);
		}

		public void ResetAfterSeconds(float seconds, Vector3? endPoint)
		{
			this.ResetAfterSeconds(seconds, endPoint, null);
		}

		public void ResetAfterSeconds(float seconds, Vector3? endPoint, Action callback)
		{
			this.Resetting = true;
			if (endPoint.HasValue)
			{
				this.current.Position = endPoint.Value;
				if (this.index > 0)
				{
					this.lineRenderer.SetPosition(this.index, endPoint.Value);
				}
			}
			base.StartCoroutine(this.ResetAfterSecondsInternal(seconds, callback));
		}
	}
}
