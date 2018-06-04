using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMover : MonoBehaviour {
	public Inventory inventory;
	Tooltip tooltip;
	public bool isMoving = false;
	public ItemData movingItem;
	private void Start() {
		inventory = GameObject.Find("InventoryController").GetComponent<Inventory>();
		tooltip = GetComponent<Tooltip>();
	}
	private void Update() {
		if (isMoving) {
			movingItem.transform.position = Input.mousePosition;
			tooltip.activate(movingItem.item);
		}
	}
	public void pickUpItem(ItemData item) {
		isMoving = true;
		movingItem = item;

		// Make sure mouse detection still works
		movingItem.GetComponent<Canvas>().overrideSorting = true;
		movingItem.GetComponent<Canvas>().sortingOrder = 1;
		movingItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
		movingItem.transform.position = Input.mousePosition;
	}
	public void dropItem(int slotId) {
		if (!inventory.isItemInSlot(slotId)) {
			inventory.itemList[movingItem.slotId] = new Item();
			movingItem.slotId = slotId;
			inventory.itemList[slotId] = movingItem.item;
			movingItem.transform.SetParent(inventory.slots[slotId].transform);
		} else if (inventory.isItemInSlot(slotId)) {
			ItemData itemInSlot = inventory.findItemInSlot(slotId);
			itemInSlot.transform.SetParent(inventory.slots[movingItem.slotId].transform);
			itemInSlot.transform.localPosition = new Vector2(0, 0);
			itemInSlot.slotId = movingItem.slotId;
			inventory.itemList[itemInSlot.slotId] = itemInSlot.item;
			inventory.itemList[slotId] = movingItem.item;
			movingItem.slotId = slotId;
			movingItem.transform.SetParent(inventory.slots[slotId].transform);

		}

		// Reset mouse detection
		movingItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
		movingItem.GetComponent<Canvas>().overrideSorting = false;
		movingItem.GetComponent<Canvas>().sortingOrder = 0;
		movingItem.transform.position = inventory.slots[movingItem.slotId].transform.position;
		movingItem.transform.localPosition = new Vector2(0, 0);
		if (movingItem.slotId < inventory.hotbarAmount) {
			movingItem.GetComponent<RectTransform>().sizeDelta = new Vector2(inventory.iconSize_Hotbar, inventory.iconSize_Hotbar);
		} else {
			movingItem.GetComponent<RectTransform>().sizeDelta = new Vector2(inventory.iconSize_Inventory, inventory.iconSize_Inventory);
		}
		isMoving = false;
	}
}