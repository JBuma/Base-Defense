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

	public List<Item> itemList = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	private void Start() {
		// GetComponent<ItemDatabaseController>().generateNewDatabase();
		Debug.Log(GetComponent<ItemDatabaseController>());
		database = GetComponent<ItemDatabaseController>().itemDatabase;
		for (int i = 0; i < slotAmount + hotbarAmount; i++) {
			itemList.Add(new Item());
			if (i < hotbarAmount) {
				// items.Add(new Item());
				slots.Add(Instantiate(inventorySlot, hotBarSlotsPanel.transform));
				// slots[i].GetComponent<Slot>().slotId = i;
			} else {
				slots.Add(Instantiate(inventorySlot, slotPanel.transform));
			}
			slots[i].GetComponent<Slot>().slotId = i;
			// Debug.Log("Slot id " + i + " has item id: " + items[i].ID);

		}
		addItem(1, 10);
		addItem(3);
		addItem(2);
		addItem(1);
		addItem(4, 20);
	}
	public GameObject findItemInInventory(int id) {
		for (int i = 0; i < slots.Count; i++) {
			if (itemList[i].ID == id) {
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
		return (itemList[slotId].ID != -1);
	}
	bool isItemInInventory(int id) {
		for (int i = 0; i < itemList.Count; i++) {
			if (itemList[i].ID == id) {
				return true;
			}
		}
		return false;
	}
	public void addItem(int id, int amount = 1) {
		Item itemToAdd = database.getItemByID(id);
		if (itemToAdd.getAttributeOfType<StackableAttribute>().stackable && isItemInInventory(id)) {
			ItemData data = findItemInInventory(id).transform.GetChild(0).GetComponent<ItemData>();
			data.amount += amount;
			data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
			return;
		} else {
			for (int i = 0; i < itemList.Count; i++) {
				if (itemList[i].ID == -1) {
					itemList[i] = itemToAdd;
					GameObject itemObj = Instantiate(inventoryItem, slots[i].transform);
					ItemData data = itemObj.GetComponent<ItemData>();
					Slot slot = slots[i].GetComponent<Slot>();
					data.item = itemToAdd;
					data.slotId = i;
					slot.slotId = i;
					if (slot.slotId < hotbarAmount) {
						itemObj.GetComponent<RectTransform>().sizeDelta = new Vector2(iconSize_Hotbar, iconSize_Hotbar);
					}
					itemObj.transform.localPosition = Vector3.zero;
					itemObj.GetComponent<Image>().sprite = itemToAdd.getSprite();
					itemObj.name = itemToAdd.ItemName;
					if (itemToAdd.getAttributeOfType<StackableAttribute>().stackable) {
						data.amount += amount;
						data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
					}
					break;
				}
			}
		}
	}
}