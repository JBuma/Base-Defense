using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	[SerializeField] public int iconSize_Inventory = 60;
	[SerializeField] public int iconSize_Hotbar = 40;

	public bool isOpen = true;
	[SerializeField] GameObject inventoryPanel;
	[SerializeField] GameObject slotPanel;
	[SerializeField] GameObject hotBarSlotsPanel;
	[SerializeField] GameObject inventorySlot;
	[SerializeField] GameObject inventoryItem;
	public int hotbarAmount = 12;
	int slotAmount = 42;
	ItemDatabase database;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	private void Start() {
		database = GetComponent<ItemDatabase>();
		for (int i = 0; i < slotAmount + hotbarAmount; i++) {
			if (i < hotbarAmount) {
				items.Add(new Item());
				slots.Add(Instantiate(inventorySlot, hotBarSlotsPanel.transform));
				slots[i].GetComponent<Slot>().slotId = i;
			} else {
				items.Add(new Item());
				slots.Add(Instantiate(inventorySlot, slotPanel.transform));
				slots[i].GetComponent<Slot>().slotId = i;
			}

		}
		addItem(1, 10);
		addItem(3);
		addItem(2);
		addItem(1);
	}
	public GameObject findItemInInventory(int id) {
		for (int i = 0; i < slots.Count; i++) {
			if (items[i].ID == id) {
				return slots[i];
			}
		}
		return null;
	}
	public void toggleInventory() {
		isOpen = !isOpen;
		inventoryPanel.SetActive(isOpen);
	}
	public ItemData findItemInSlot(int slotId) {
		if (isItemInSlot(slotId)) {
			return slots[slotId].transform.GetChild(0).GetComponent<ItemData>();
		} else {
			return null;
		}
	}
	public bool isItemInSlot(int slotId) {
		return (items[slotId].ID != -1);
	}
	bool isItemInInventory(int id) {
		for (int i = 0; i < items.Count; i++) {
			if (items[i].ID == id) {
				return true;
			}
		}
		return false;
	}
	public void addItem(int id, int amount = 1) {
		Item itemToAdd = database.getItemByID(id);
		if (itemToAdd.Stackable && isItemInInventory(id)) {
			ItemData data = findItemInInventory(id).transform.GetChild(0).GetComponent<ItemData>();
			data.amount += amount;
			data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
			return;
		} else {
			for (int i = 0; i < items.Count; i++) {
				if (items[i].ID == -1) {
					items[i] = itemToAdd;
					GameObject itemObj = Instantiate(inventoryItem, slots[i].transform);
					ItemData data = itemObj.GetComponent<ItemData>();
					Slot slot = slots[i].GetComponent<Slot>();
					data.item = itemToAdd;
					data.slotId = i;
					slot.slotId = i;
					itemObj.transform.localPosition = Vector3.zero;
					itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
					itemObj.name = itemToAdd.Title;
					if (itemToAdd.Stackable) {
						data.amount += amount;
						data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
					}
					break;
				}
			}
		}
	}
}