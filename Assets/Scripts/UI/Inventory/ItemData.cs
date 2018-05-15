using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
	public Item item;
	public int amount;
	public int slotId;
	Canvas canvas;
	Inventory inventory;
	ItemMover itemMover;
	Tooltip tooltip;

	public bool isDragging = false;
	void Start() {
		canvas = GetComponent<Canvas>();
		inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		itemMover = inventory.GetComponent<ItemMover>();
		tooltip = inventory.GetComponent<Tooltip>();
	}
	public void OnPointerClick(PointerEventData eventData) {
		if (!itemMover.isMoving) {
			itemMover.pickUpItem(this);
		} else if (itemMover.isMoving) {
			itemMover.dropItem(slotId);
		}
	}
	public void OnPointerEnter(PointerEventData eventData) {
		if (inventory.isOpen) {
			tooltip.activate(item);
		}
	}
	public void OnPointerExit(PointerEventData eventData) {
		tooltip.deActivate();
	}
	private void Update() {
		if (isDragging) {
			transform.position = Input.mousePosition;
		}
	}

}