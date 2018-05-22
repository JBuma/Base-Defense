using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour {
	Inventory inventory;
	public int activeSlot = 0;
	// Use this for initialization
	void Start() {
		inventory = GameObject.Find("InventoryController").GetComponent<Inventory>();
	}

	// Update is called once per frame
	void Update() {
		if (!inventory.isOpen) {
			// changeSlot((int) Mathf.Round(activeSlot + Mathf.Sign(Input.GetAxis("Mouse ScrollWheel"))));
		}
	}
	public void changeSlot(int slotId) {
		// Debug.Log(slotId);
		inventory.slots[activeSlot].GetComponent<Image>().color = Color.white;

		if (slotId >= inventory.hotbarAmount) {
			activeSlot = 0;
		} else if (slotId < 0) {
			activeSlot = inventory.hotbarAmount - 1;
		} else {
			activeSlot = slotId;
		}

		inventory.slots[activeSlot].GetComponent<Image>().color = Color.magenta;
	}
}