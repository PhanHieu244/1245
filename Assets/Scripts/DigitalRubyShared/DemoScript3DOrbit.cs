using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DigitalRubyShared
{
	public class DemoScript3DOrbit : MonoBehaviour
	{
		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				SceneManager.LoadScene(0);
			}
		}
	}
}
