using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace StatsAndInfo
{
	[Serializable]
	public class InfoGatherer : MonoBehaviour
	{
		public enum InfoType
		{
			FPS,
			FPSMin,
			FPSMax,
			FPSAvg,
			GPU,
			GPUMemory,
			CPU,
			CPUCores,
			CPUFrequency,
			RAMTotal,
			RAMAlloc,
			OperatingSystem,
			DeviceModel
		}

		public bool showOnStart;

		public bool saveMinMaxAvgData;

		[Tooltip("How many times should the info get updated")]
		public int updateFrequency;

		[HideInInspector]
		public int dataAmount;

		[HideInInspector]
		public int fpsWarningLimit;

		[HideInInspector]
		public int totalFPS;

		[HideInInspector]
		public bool fpsWarning;

		[HideInInspector]
		public Color fpsWarningColor;

		[HideInInspector]
		public Color fpsInitialColor;

		[HideInInspector]
		public List<Action> actions;

		[HideInInspector]
		public Text[] texts;

		private int fps;

		private int min;

		private int max;

		private int avg;

		private bool init;

		private bool shouldUpdate;

		private float timer;

		private float period;

		private void Initialize()
		{
			this.init = true;
			if (this.saveMinMaxAvgData)
			{
				int @int = PlayerPrefs.GetInt("FPSMin", -9);
				if (@int == -9)
				{
					this.min = 1000000;
					PlayerPrefs.SetInt("FPSMin", this.min);
				}
				else
				{
					this.min = @int;
				}
				@int = PlayerPrefs.GetInt("FPSMax", -9);
				if (@int == -9)
				{
					this.max = 0;
					PlayerPrefs.SetInt("FPSMax", this.max);
				}
				else
				{
					this.max = @int;
				}
				@int = PlayerPrefs.GetInt("TotalFPS", -9);
				if (@int == -9)
				{
					this.totalFPS = 0;
					this.dataAmount = 0;
					PlayerPrefs.SetInt("TotalFPS", 0);
					PlayerPrefs.SetInt("FPSDataAmount", this.dataAmount);
				}
				else
				{
					this.totalFPS = @int;
					this.dataAmount = PlayerPrefs.GetInt("FPSDataAmount");
				}
			}
			else
			{
				this.min = 1000000;
				this.max = 0;
				this.avg = 0;
				this.dataAmount = 0;
				this.totalFPS = 0;
			}
			this.actions = new List<Action>();
			if (this.updateFrequency <= 0)
			{
				this.updateFrequency = 10;
			}
			this.period = 1f / (float)this.updateFrequency;
			this.timer = 0f;
			if (this.texts[0])
			{
				this.actions.Add(new Action(this.UpdateFPS));
				this.fpsInitialColor = this.texts[0].color;
			}
			if (this.texts[1])
			{
				if (!this.actions.Contains(new Action(this.UpdateFPS)))
				{
					this.actions.Add(new Action(this.UpdateFPS));
				}
				this.actions.Add(new Action(this.UpdateFPSMin));
			}
			if (this.texts[2])
			{
				if (!this.actions.Contains(new Action(this.UpdateFPS)))
				{
					this.actions.Add(new Action(this.UpdateFPS));
				}
				this.actions.Add(new Action(this.UpdateFPSMax));
			}
			if (this.texts[3])
			{
				if (!this.actions.Contains(new Action(this.UpdateFPS)))
				{
					this.actions.Add(new Action(this.UpdateFPS));
				}
				this.actions.Add(new Action(this.UpdateFPSAvg));
			}
			if (this.texts[4])
			{
				this.actions.Add(new Action(this.UpdateGPU));
			}
			if (this.texts[5])
			{
				this.actions.Add(new Action(this.UpdateGPUMem));
			}
			if (this.texts[6])
			{
				this.UpdateCPU();
			}
			if (this.texts[7])
			{
				this.actions.Add(new Action(this.UpdateCPUCores));
			}
			if (this.texts[8])
			{
				this.actions.Add(new Action(this.UpdateCPUFreq));
			}
			if (this.texts[9])
			{
				this.UpdateRAMTotal();
			}
			if (this.texts[10])
			{
				this.actions.Add(new Action(this.UpdateRAMAlloc));
			}
			if (this.texts[11])
			{
				this.UpdateOS();
			}
			if (this.texts[12])
			{
				this.UpdateDeviceModel();
			}
			if (this.showOnStart)
			{
				this.shouldUpdate = true;
				Text[] array = this.texts;
				for (int i = 0; i < array.Length; i++)
				{
					Text text = array[i];
					if (text)
					{
						text.gameObject.SetActive(true);
					}
				}
			}
			else
			{
				this.shouldUpdate = false;
				Text[] array2 = this.texts;
				for (int j = 0; j < array2.Length; j++)
				{
					Text text2 = array2[j];
					if (text2)
					{
						text2.gameObject.SetActive(false);
					}
				}
			}
		}

		private void Update()
		{
			if (!this.init)
			{
				this.Initialize();
			}
			if (this.shouldUpdate)
			{
				if (this.timer >= this.period)
				{
					this.timer -= this.period;
					foreach (Action current in this.actions)
					{
						current();
					}
				}
				this.timer += Time.deltaTime;
			}
		}

		public void Hide()
		{
			this.shouldUpdate = false;
			for (int i = 0; i < this.texts.Length; i++)
			{
				if (this.texts[i])
				{
					this.texts[i].gameObject.SetActive(false);
				}
			}
		}

		public void ResetFPSMin()
		{
			this.min = this.fps;
		}

		public void ResetFPSMax()
		{
			this.max = this.fps;
		}

		public void ResetFPSAvg()
		{
			this.avg = this.fps;
			this.totalFPS = 0;
			this.dataAmount = 0;
		}

		public void Show()
		{
			this.shouldUpdate = true;
			for (int i = 0; i < this.texts.Length; i++)
			{
				if (this.texts[i])
				{
					this.texts[i].gameObject.SetActive(true);
				}
			}
		}

		public void SetFPSWarning(int limit, Color color)
		{
			this.fpsWarningLimit = limit;
			this.fpsWarningColor = color;
		}

		public bool GetEnabled(InfoGatherer.InfoType type)
		{
			switch (type)
			{
			case InfoGatherer.InfoType.FPS:
				return this.actions.Contains(new Action(this.UpdateFPS)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.FPSMin:
				return this.actions.Contains(new Action(this.UpdateFPSMin)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.FPSMax:
				return this.actions.Contains(new Action(this.UpdateFPSMax)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.FPSAvg:
				return this.actions.Contains(new Action(this.UpdateFPSAvg)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.GPU:
				return this.actions.Contains(new Action(this.UpdateGPU)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.GPUMemory:
				return this.actions.Contains(new Action(this.UpdateGPUMem)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.CPU:
				return this.actions.Contains(new Action(this.UpdateCPU)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.CPUCores:
				return this.actions.Contains(new Action(this.UpdateCPUCores)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.CPUFrequency:
				return this.actions.Contains(new Action(this.UpdateCPUFreq)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.RAMTotal:
				return this.actions.Contains(new Action(this.UpdateRAMTotal)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.RAMAlloc:
				return this.actions.Contains(new Action(this.UpdateRAMAlloc)) && this.texts[(int)type] != null;
			case InfoGatherer.InfoType.OperatingSystem:
				return this.actions.Contains(new Action(this.UpdateOS)) && this.texts[(int)type] != null;
			default:
				return false;
			}
		}

		public void SetEnabled(InfoGatherer.InfoType type, bool enabled, Text text = null)
		{
			switch (type)
			{
			case InfoGatherer.InfoType.FPS:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateFPS), (int)type, text);
					this.fpsInitialColor = ((!(text == null)) ? text.color : this.texts[0].color);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateFPS));
				}
				break;
			case InfoGatherer.InfoType.FPSMin:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateFPSMin), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateFPSMin));
				}
				break;
			case InfoGatherer.InfoType.FPSMax:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateFPSMax), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateFPSMax));
				}
				break;
			case InfoGatherer.InfoType.FPSAvg:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateFPSAvg), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateFPSAvg));
				}
				break;
			case InfoGatherer.InfoType.GPU:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateGPU), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateGPU));
				}
				break;
			case InfoGatherer.InfoType.GPUMemory:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateGPUMem), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateGPUMem));
				}
				break;
			case InfoGatherer.InfoType.CPU:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateCPU), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateCPU));
				}
				break;
			case InfoGatherer.InfoType.CPUCores:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateCPUCores), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateCPUCores));
				}
				break;
			case InfoGatherer.InfoType.CPUFrequency:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateCPUFreq), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateCPUFreq));
				}
				break;
			case InfoGatherer.InfoType.RAMTotal:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateRAMTotal), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateRAMTotal));
				}
				break;
			case InfoGatherer.InfoType.RAMAlloc:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateRAMAlloc), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateRAMAlloc));
				}
				break;
			case InfoGatherer.InfoType.OperatingSystem:
				if (enabled)
				{
					this.EnableInfo(new Action(this.UpdateOS), (int)type, text);
				}
				else
				{
					this.DisableInfo(new Action(this.UpdateOS));
				}
				break;
			}
		}

		private void EnableInfo(Action a, int i, Text text)
		{
			if (!this.actions.Contains(a))
			{
				if (!this.texts[i] && !text)
				{
					UnityEngine.Debug.LogError("Stats&Info  -  You are trying to enable the info " + (InfoGatherer.InfoType)i + ", but its text field is empty. Either set it in the inspector, or pass a 'Text' value with the 'SetEnabled' function.");
					return;
				}
				this.actions.Add(a);
				if (text)
				{
					this.texts[i] = text;
				}
			}
		}

		private void DisableInfo(Action a)
		{
			if (this.actions.Contains(a))
			{
				this.actions.Remove(a);
			}
		}

		public void UpdateFPS()
		{
			this.fps = (int)(1f / Time.deltaTime);
			if (this.texts[0] != null)
			{
				this.texts[0].text = this.fps.ToString();
				if (this.fpsWarning)
				{
					if (this.fps < this.fpsWarningLimit)
					{
						this.texts[0].color = this.fpsWarningColor;
					}
					else
					{
						this.texts[0].color = this.fpsInitialColor;
					}
				}
			}
		}

		public void UpdateFPSMin()
		{
			if (this.fps < this.min && this.fps != 0)
			{
				this.min = this.fps;
				PlayerPrefs.SetInt("FPSMin", this.min);
			}
			this.texts[1].text = this.min.ToString();
		}

		public void UpdateFPSMax()
		{
			if (this.fps > this.max)
			{
				this.max = this.fps;
				PlayerPrefs.SetInt("FPSMax", this.max);
			}
			this.texts[2].text = this.max.ToString();
		}

		public void UpdateFPSAvg()
		{
			this.dataAmount++;
			this.totalFPS += this.fps;
			this.avg = (int)((float)this.totalFPS / (float)this.dataAmount);
			PlayerPrefs.SetInt("TotalFPS", this.totalFPS);
			PlayerPrefs.SetInt("FPSDataAmount", this.dataAmount);
			this.texts[3].text = this.avg.ToString();
		}

		public void UpdateGPU()
		{
			this.texts[4].text = SystemInfo.graphicsDeviceName;
		}

		public void UpdateGPUMem()
		{
			this.texts[5].text = SystemInfo.graphicsMemorySize.ToString() + " MB";
		}

		public void UpdateCPU()
		{
			if (SystemInfo.processorType != null)
			{
				this.texts[6].text = SystemInfo.processorType;
			}
		}

		public void UpdateCPUCores()
		{
			this.texts[7].text = SystemInfo.processorCount.ToString();
		}

		public void UpdateCPUFreq()
		{
			this.texts[8].text = (Mathf.Ceil((float)SystemInfo.processorFrequency / 10f) / 100f).ToString("0.00") + " GHz";
		}

		public void UpdateRAMTotal()
		{
			this.texts[9].text = SystemInfo.systemMemorySize.ToString() + " MB";
		}

		public void UpdateRAMAlloc()
		{
			this.texts[10].text = (Profiler.GetTotalReservedMemory() / 1000000f).ToString("0") + " MB";
		}

		public void UpdateOS()
		{
			this.texts[11].text = SystemInfo.operatingSystem;
		}

		public void UpdateDeviceModel()
		{
			this.texts[12].text = SystemInfo.deviceModel;
		}
	}
}
