using System;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRubyShared
{
	[ExecuteInEditMode, RequireComponent(typeof(GridLayoutGroup)), RequireComponent(typeof(RectTransform))]
	public class DemoScriptDynamicGrid : MonoBehaviour
	{
		[Range(1f, 256f)]
		public int Rows = 2;

		[Range(1f, 256f)]
		public int Columns = 2;

		private GridLayoutGroup grid;

		private RectTransform rectTransform;

		private void Start()
		{
			this.grid = base.GetComponent<GridLayoutGroup>();
			this.rectTransform = base.GetComponent<RectTransform>();
		}

		private void Update()
		{
			float num = this.rectTransform.rect.width - this.grid.spacing.x * (float)(this.Columns - 1);
			float num2 = this.rectTransform.rect.height - this.grid.spacing.y * (float)(this.Rows - 1);
			this.grid.cellSize = new Vector2(num / (float)this.Columns, num2 / (float)this.Rows);
		}
	}
}
