using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptDPad : MonoBehaviour
	{
		[Tooltip("Fingers DPad Script")]
		public FingersDPadScript DPadScript;

		[Tooltip("Object to move with the dpad")]
		public GameObject Mover;

		[Tooltip("Units per second to move the square with dpad")]
		public float Speed = 250f;

		[Tooltip("Whether dpad moves to touch start location")]
		public bool MoveDPadToGestureStartLocation;

		private Vector3 startPos;

		private void Awake()
		{
			this.DPadScript.DPadItemTapped = new Action<FingersDPadScript, FingersDPadItem, TapGestureRecognizer>(this.DPadTapped);
			this.DPadScript.DPadItemPanned = new Action<FingersDPadScript, FingersDPadItem, PanGestureRecognizer>(this.DPadPanned);
			this.startPos = this.Mover.transform.position;
			this.DPadScript.MoveDPadToGestureStartLocation = this.MoveDPadToGestureStartLocation;
		}

		private void DPadTapped(FingersDPadScript script, FingersDPadItem item, TapGestureRecognizer gesture)
		{
			if (item == FingersDPadItem.Center)
			{
				this.Mover.transform.position = this.startPos;
			}
		}

		private void DPadPanned(FingersDPadScript script, FingersDPadItem item, PanGestureRecognizer gesture)
		{
			Vector3 position = this.Mover.transform.position;
			switch (item)
			{
			case FingersDPadItem.Up:
				position.y += this.Speed * Time.deltaTime;
				break;
			case FingersDPadItem.Right:
				position.x += this.Speed * Time.deltaTime;
				break;
			case FingersDPadItem.Down:
				position.y -= this.Speed * Time.deltaTime;
				break;
			case FingersDPadItem.Left:
				position.x -= this.Speed * Time.deltaTime;
				break;
			}
			this.Mover.transform.position = position;
		}
	}
}
