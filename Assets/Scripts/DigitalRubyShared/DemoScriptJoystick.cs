using System;
using UnityEngine;

namespace DigitalRubyShared
{
	public class DemoScriptJoystick : MonoBehaviour
	{
		[Tooltip("Fingers Joystick Script")]
		public FingersJoystickScript JoystickScript;

		[Tooltip("Object to move with the joystick")]
		public GameObject Mover;

		[Tooltip("Units per second to move the square with joystick")]
		public float Speed = 250f;

		[Tooltip("Whether joystick moves to touch location")]
		public bool MoveJoystickToGestureStartLocation;

		private void Awake()
		{
			this.JoystickScript.JoystickExecuted = new Action<FingersJoystickScript, Vector2>(this.JoystickExecuted);
			this.JoystickScript.MoveJoystickToGestureStartLocation = this.MoveJoystickToGestureStartLocation;
		}

		private void JoystickExecuted(FingersJoystickScript script, Vector2 amount)
		{
			Vector3 position = this.Mover.transform.position;
			position.x += amount.x * this.Speed * Time.deltaTime;
			position.y += amount.y * this.Speed * Time.deltaTime;
			this.Mover.transform.position = position;
		}
	}
}
