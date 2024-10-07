using DG.Tweening;
using PreviewLabs;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItem : MonoBehaviour
{
	[SerializeField]
	private Image imgCooldownEffect;

	[SerializeField]
	private Image imgOverlay;

	[SerializeField]
	private Button btnItem;

	private float reloadTime = 0.5f;

	[SerializeField]
	private ItemType itemType;

	[SerializeField]
	private Text txtLevel;

	public bool isItemWaiting;

	public bool autoUse;

	public double remainingTime;

	public bool isUnlocked;

	public void ResumeItem()
	{
	}

	public void ShowCooldownEffect()
	{
		Tweener tweener = this.imgCooldownEffect.transform.DORotate(Vector3.forward * -360f, 0.7f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetId("rotateCooldownEffect");
		this.imgCooldownEffect.gameObject.SetActive(true);
	}

	public void HideCooldownEffect()
	{
		this.imgCooldownEffect.DOKill(false);
		this.imgCooldownEffect.gameObject.SetActive(false);
	}

	public void ItemPressed(MissileLauncher missile)
	{
		if (!this.isUnlocked)
		{
			return;
		}
		if (Tank.instance && Tank.instance.isMoving)
		{
			return;
		}
		if (DynamicGrid.instance.transform.childCount != 0)
		{
			Enemy enemy = DynamicGrid.instance.FindEnemyX();
			if (enemy)
			{
				this.remainingTime = (double)(this.reloadTime / GameController.instance.factorCooldown);
				this.isItemWaiting = true;
				if (Gun.instance)
				{
					if (Gun.instance.isWaiting)
					{
						if (Gun.instance._touchPos.x == -100f)
						{
							if (this.itemType != ItemType.Item2)
							{
								this.FireItem(enemy.transform.position, missile);
							}
							else
							{
								this.FireItem(DynamicGrid.instance.transform.position + Vector3.up * (float)UnityEngine.Random.Range(-1, 2), missile);
							}
						}
						else
						{
							this.FireItem(Gun.instance._touchPos, missile);
						}
					}
					else if (this.itemType != ItemType.Item2)
					{
						this.FireItem(enemy.transform.position, missile);
					}
					else
					{
						this.FireItem(DynamicGrid.instance.transform.position + Vector3.up * (float)UnityEngine.Random.Range(-3, 3), missile);
					}
				}
			}
		}
	}

	private void FireItem(Vector2 targetPos, MissileLauncher missile)
	{
		switch (this.itemType)
		{
		case ItemType.Item1:
			SoundController.instance.PlaySoundUseItem1();
			missile.FireItem1(targetPos, missile.lowerPosition.transform.position, Vector3.zero, 10f, 20f, 30f);
			break;
		case ItemType.Item2:
			SoundController.instance.PlaySoundUseItem123();
			missile.FireItem2(targetPos, missile.lowerPosition.transform.position, Vector3.zero, 10f, 20f, 30f);
			break;
		case ItemType.Item3:
			missile.FireItem3(targetPos, missile.originPos.transform.position, Vector3.zero, 10f, 20f, 30f);
			break;
		case ItemType.Item4:
			missile.FireItem4(targetPos, missile.lowerPosition.transform.position, Vector3.zero, 10f, 20f, 30f);
			break;
		case ItemType.Item5:
			missile.FireItem5(targetPos);
			break;
		case ItemType.Item6:
			missile.FireItem6(targetPos);
			break;
		case ItemType.Item7:
			missile.FireItem7(targetPos);
			break;
		}
	}

	private void Start()
	{
	}

	public void LockItem(int levelUnlock)
	{
		this.isUnlocked = false;
		this.btnItem.interactable = false;
		this.imgOverlay.GetComponent<Image>().fillAmount = 1f;
		this.imgOverlay.color = Color.white;
		this.txtLevel.text = "LEVEL " + levelUnlock.ToString();
	}

	public void UnlockItem()
	{
		this.isUnlocked = true;
		this.btnItem.interactable = true;
		this.imgOverlay.GetComponent<Image>().fillAmount = 0f;
		this.txtLevel.text = "READY";
		this.imgOverlay.color = new Color(0f, 0f, 0f, 0.38f);
	}

	private void Update()
	{
		if (this.isUnlocked)
		{
			if (this.isItemWaiting)
			{
				this.remainingTime -= (double)Time.deltaTime;
				this.txtLevel.text = Utilities.GetFormattedTime(this.remainingTime) + "s";
				this.btnItem.interactable = false;
				this.imgOverlay.fillAmount = (float)this.remainingTime / (this.reloadTime / GameController.instance.factorCooldown);
				if (this.remainingTime <= 0.0)
				{
					this.remainingTime = 0.0;
					this.txtLevel.text = "READY";
					this.isItemWaiting = false;
					this.btnItem.interactable = true;
					this.imgOverlay.fillAmount = 0f;
					this.SaveRemainingTime();
				}
			}
			else if (CanvasItem.instance.isAutoUse && Tank.instance && !Tank.instance.isMoving)
			{
				this.ItemPressed(MissileLauncher.instance);
			}
		}
	}

	public double GetLastTimeUsed()
	{
		switch (this.itemType)
		{
		case ItemType.Item1:
			return GameController.instance.game.Item1_last_used;
		case ItemType.Item2:
			return GameController.instance.game.Item2_last_used;
		case ItemType.Item3:
			return GameController.instance.game.Item3_last_used;
		case ItemType.Item4:
			return GameController.instance.game.Item4_last_used;
		case ItemType.Item5:
			return GameController.instance.game.Item5_last_used;
		case ItemType.Item6:
			return GameController.instance.game.Item6_last_used;
		case ItemType.Item7:
			return GameController.instance.game.Item7_last_used;
		default:
			return -1.0;
		}
	}

	public void LoadItemData()
	{
		switch (this.itemType)
		{
		case ItemType.Item1:
			this.reloadTime = (float)BaseValue.item1_reload_time;
			break;
		case ItemType.Item2:
			this.reloadTime = (float)BaseValue.item2_reload_time;
			break;
		case ItemType.Item3:
			this.reloadTime = (float)BaseValue.item3_reload_time;
			break;
		case ItemType.Item4:
			this.reloadTime = (float)BaseValue.item4_reload_time;
			break;
		case ItemType.Item5:
			this.reloadTime = (float)BaseValue.item5_reload_time;
			break;
		case ItemType.Item6:
			this.reloadTime = (float)BaseValue.item6_reload_time;
			break;
		case ItemType.Item7:
			this.reloadTime = (float)BaseValue.item7_reload_time;
			break;
		}
		this.isItemWaiting = false;
		this.remainingTime = this.GetRemainingTime();
		if (this.remainingTime <= 0.0)
		{
			this.btnItem.interactable = true;
			this.isItemWaiting = false;
		}
		else
		{
			this.btnItem.interactable = false;
			this.isItemWaiting = true;
		}
	}

	public void ResumeItemData()
	{
		switch (this.itemType)
		{
		case ItemType.Item1:
			this.reloadTime = (float)BaseValue.item1_reload_time;
			break;
		case ItemType.Item2:
			this.reloadTime = (float)BaseValue.item2_reload_time;
			break;
		case ItemType.Item3:
			this.reloadTime = (float)BaseValue.item3_reload_time;
			break;
		case ItemType.Item4:
			this.reloadTime = (float)BaseValue.item4_reload_time;
			break;
		case ItemType.Item5:
			this.reloadTime = (float)BaseValue.item5_reload_time;
			break;
		case ItemType.Item6:
			this.reloadTime = (float)BaseValue.item6_reload_time;
			break;
		case ItemType.Item7:
			this.reloadTime = (float)BaseValue.item7_reload_time;
			break;
		}
		this.remainingTime = this.GetRemainingTime();
		if (this.remainingTime <= 0.0)
		{
			this.btnItem.interactable = true;
			this.isItemWaiting = false;
		}
		else
		{
			this.btnItem.interactable = false;
			this.isItemWaiting = true;
		}
	}

	public double GetRemainingTime()
	{
		switch (this.itemType)
		{
		case ItemType.Item1:
			return GameController.instance.game.Item1_time_remaining;
		case ItemType.Item2:
			return GameController.instance.game.Item2_time_remaining;
		case ItemType.Item3:
			return GameController.instance.game.Item3_time_remaining;
		case ItemType.Item4:
			return GameController.instance.game.Item4_time_remaining;
		case ItemType.Item5:
			return GameController.instance.game.Item5_time_remaining;
		case ItemType.Item6:
			return GameController.instance.game.Item6_time_remaining;
		case ItemType.Item7:
			return GameController.instance.game.Item7_time_remaining;
		default:
			return -1.0;
		}
	}

	public void SaveRemainingTime()
	{
		if (this.isItemWaiting)
		{
			switch (this.itemType)
			{
			case ItemType.Item1:
				GameController.instance.game.Item1_time_remaining = this.remainingTime;
				break;
			case ItemType.Item2:
				GameController.instance.game.Item2_time_remaining = this.remainingTime;
				break;
			case ItemType.Item3:
				GameController.instance.game.Item3_time_remaining = this.remainingTime;
				break;
			case ItemType.Item4:
				GameController.instance.game.Item4_time_remaining = this.remainingTime;
				break;
			case ItemType.Item5:
				GameController.instance.game.Item5_time_remaining = this.remainingTime;
				break;
			case ItemType.Item6:
				GameController.instance.game.Item6_time_remaining = this.remainingTime;
				break;
			case ItemType.Item7:
				GameController.instance.game.Item7_time_remaining = this.remainingTime;
				break;
			}
		}
		else
		{
			switch (this.itemType)
			{
			case ItemType.Item1:
				GameController.instance.game.Item1_time_remaining = 0.0;
				break;
			case ItemType.Item2:
				GameController.instance.game.Item2_time_remaining = 0.0;
				break;
			case ItemType.Item3:
				GameController.instance.game.Item3_time_remaining = 0.0;
				break;
			case ItemType.Item4:
				GameController.instance.game.Item4_time_remaining = 0.0;
				break;
			case ItemType.Item5:
				GameController.instance.game.Item5_time_remaining = 0.0;
				break;
			case ItemType.Item6:
				GameController.instance.game.Item6_time_remaining = 0.0;
				break;
			case ItemType.Item7:
				GameController.instance.game.Item7_time_remaining = 0.0;
				break;
			}
		}
		PreviewLabs.PlayerPrefs.Flush();
	}

	public void ResetItemData()
	{
		this.remainingTime = 0.0;
		this.SaveRemainingTime();
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			this.ResumeItemData();
		}
	}

	public void OnPointerClick()
	{
	}
}
