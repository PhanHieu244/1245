using System;
using UnityEngine;

namespace UB
{
	[ExecuteInEditMode]
	public class D2FogsNoiseTexPE : EffectBase
	{
		public Color Color = new Color(1f, 1f, 1f, 1f);

		public Texture2D Noise;

		public float Size = 1f;

		public float HorizontalSpeed = 0.2f;

		public float VerticalSpeed;

		[Range(0f, 5f)]
		public float Density = 2f;

		public Shader Shader;

		private Material _material;

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Shader == null)
			{
				this.Shader = Shader.Find("UB/PostEffects/D2FogsNoiseTex");
			}
			if (this._material)
			{
				UnityEngine.Object.DestroyImmediate(this._material);
				this._material = null;
			}
			if (this.Shader)
			{
				this._material = new Material(this.Shader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
				if (this._material.HasProperty("_Color"))
				{
					this._material.SetColor("_Color", this.Color);
				}
				if (this._material.HasProperty("_NoiseTex"))
				{
					this._material.SetTexture("_NoiseTex", this.Noise);
				}
				if (this._material.HasProperty("_Size"))
				{
					this._material.SetFloat("_Size", this.Size);
				}
				if (this._material.HasProperty("_Speed"))
				{
					this._material.SetFloat("_Speed", this.HorizontalSpeed);
				}
				if (this._material.HasProperty("_VSpeed"))
				{
					this._material.SetFloat("_VSpeed", this.VerticalSpeed);
				}
				if (this._material.HasProperty("_Density"))
				{
					this._material.SetFloat("_Density", this.Density);
				}
			}
			if (this.Shader != null && this._material != null)
			{
				Graphics.Blit(source, destination, this._material);
			}
		}
	}
}
