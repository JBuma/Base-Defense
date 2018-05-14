using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {
	private Inventory inventory;
	private ItemMover itemMover;
	public int slotId;

	private void Start() {
		inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		itemMover = GameObject.Find("Inventory").GetComponent<ItemMover>();
	}
	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log("Slot clicked");
		if (itemMover.isMoving) {
			itemMover.dropItem(slotId);
		}
	}
}