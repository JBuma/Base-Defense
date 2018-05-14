using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour {

	Inventory inventory;
	public bool isMoving = false;
	public ItemData movingItem;
	private void Start() {
		inventory = GetComponent<Inventory>();
	}
	private void Update() {
		if (isMoving) {
			movingItem.transform.position = Input.mousePosition;
		}
	}
	public void pickUpItem(ItemData item) {
		isMoving = true;
		movingItem = item;
		movingItem.GetComponent<Canvas>().overrideSorting = true;
		movingItem.GetComponent<Canvas>().sortingOrder = 1;
		movingItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
		movingItem.transform.position = Input.mousePosition;
	}
	public void dropItem(int slotId) {
		if (!inventory.isItemInSlot(slotId)) {
			inventory.items[movingItem.slotId] = new Item();
			movingItem.slotId = slotId;
			inventory.items[slotId] = movingItem.item;
			movingItem.transform.SetParent(inventory.slots[slotId].transform);
		}
		movingItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
		movingItem.GetComponent<Canvas>().overrideSorting = false;
		movingItem.GetComponent<Canvas>().sortingOrder = 0;
		movingItem.transform.position = inventory.slots[movingItem.slotId].transform.position;
		movingItem.transform.localPosition = new Vector2(0, 0);
		isMoving = false;
	}
}