using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.AnimatedLineRenderer
{
	public class DemoScriptPhasor : MonoBehaviour
	{
		public PhasorScript PhasorScript;

		public GameObject AsteroidPrefab;

		public AudioSource ExplosionSound;

		public ParticleSystem ExplosionParticleSystem;

		private readonly List<GameObject> asteroids = new List<GameObject>();

		private int score;

		private void CreateAsteroid()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AsteroidPrefab);
			float num = UnityEngine.Random.Range(0.5f, 2f);
			float z = UnityEngine.Random.Range(0f, 360f);
			gameObject.transform.localScale = new Vector3(num, num, 1f);
			gameObject.transform.rotation = Quaternion.Euler(0f, 0f, z);
			float x = UnityEngine.Random.Range(0.5f, 0.9f);
			float y = UnityEngine.Random.Range(0.1f, 0.9f);
			Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0f));
			position.z = 0f;
			gameObject.transform.position = position;
			float max = 8f;
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(0f, max), UnityEngine.Random.Range(0f, max));
			gameObject.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(0f, 4f);
			this.asteroids.Add(gameObject);
		}

		private void DestroyAsteroid(GameObject asteroid)
		{
			if (asteroid != null)
			{
				this.ExplosionSound.PlayOneShot(this.ExplosionSound.clip);
				this.ExplosionParticleSystem.transform.position = asteroid.transform.position;
				short count = (short)Mathf.Max(8f, 50f * asteroid.transform.localScale.x * asteroid.transform.localScale.x);
				this.ExplosionParticleSystem.emission.SetBursts(new ParticleSystem.Burst[]
				{
					new ParticleSystem.Burst(0f, count)
				}, 1);
				this.ExplosionParticleSystem.Play();
				UnityEngine.Object.Destroy(asteroid);
			}
		}

		private void OnHit(RaycastHit2D[] hits)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit2D raycastHit2D = hits[i];
				this.DestroyAsteroid(raycastHit2D.collider.gameObject);
				this.score++;
			}
			GameObject gameObject = GameObject.Find("ScoreLabel");
			gameObject.GetComponent<Text>().text = "Score: " + this.score;
		}

		private void Start()
		{
			this.PhasorScript.HitCallback = new Action<RaycastHit2D[]>(this.OnHit);
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 target = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				target.z = 0f;
				this.PhasorScript.Fire(target);
			}
			for (int i = this.asteroids.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = this.asteroids[i];
				if (gameObject == null || !gameObject.GetComponent<Renderer>().isVisible)
				{
					UnityEngine.Object.Destroy(gameObject);
					this.asteroids.RemoveAt(i);
				}
			}
			if (UnityEngine.Random.Range(0, 50) == 5)
			{
				this.CreateAsteroid();
			}
		}
	}
}
