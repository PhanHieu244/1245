using System;
using UnityEngine;

namespace DigitalRuby.AnimatedLineRenderer
{
	public class DemoScriptDraw : MonoBehaviour
	{
		public AnimatedLineRenderer AnimatedLine;

		private void Start()
		{
		}

		private void Update()
		{
			if (this.AnimatedLine == null)
			{
				return;
			}
			if (Input.GetMouseButton(0))
			{
				Vector3 pos = UnityEngine.Input.mousePosition;
				pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, this.AnimatedLine.transform.position.z));
				this.AnimatedLine.Enqueue(pos);
			}
			else if (UnityEngine.Input.GetKey(KeyCode.R))
			{
				this.AnimatedLine.ResetAfterSeconds(0.5f, null);
			}
		}
	}
}
