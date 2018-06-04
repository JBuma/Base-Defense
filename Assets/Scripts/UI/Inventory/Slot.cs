using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {
	private Inventory inventory;
	private ItemMover itemMover;
	public int slotId;
	public bool isHotbar = false;

	private void Start() {
		inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		itemMover = GameObject.Find("Inventory").GetComponent<ItemMover>();
	}
	public void OnPointerClick(PointerEventData eventData) {
		if (itemMover.isMoving) {
			itemMover.dropItem(slotId);
		}
	}
}