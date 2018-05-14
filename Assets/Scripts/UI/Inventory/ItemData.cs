using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IPointerClickHandler {
	public Item item;
	public int amount;
	public int slotId;
	Canvas canvas;
	Inventory inventory;
	ItemMover itemMover;

	public bool isDragging = false;
	void Start() {
		canvas = GetComponent<Canvas>();
		inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		itemMover = GameObject.Find("Inventory").GetComponent<ItemMover>();
	}
	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log("Pointer up");
		if (!itemMover.isMoving) {
			itemMover.pickUpItem(this);
		} else if (itemMover.isMoving) {
			itemMover.dropItem(slotId);
		}
	}
	private void Update() {
		if (isDragging) {
			transform.position = Input.mousePosition;
		}
	}

}