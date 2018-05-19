using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldInteract : MonoBehaviour {
	[SerializeField] MapController mapController;
	[SerializeField] Inventory inventory;
	ItemDatabase itemDatabase;
	Hotbar hotbar;

	private void Start() {
		hotbar = inventory.GetComponent<Hotbar>();
		itemDatabase = inventory.GetComponent<ItemDatabase>();
	}

	void Update() {
		if (!inventory.isOpen) {
			if (Input.GetMouseButtonDown(0)) {
				if (inventory.isItemInSlot(hotbar.activeSlot)) {
					switch (inventory.findItemInSlot(hotbar.activeSlot).item.Type) {
						case "block":
							placeBlock();
							break;
						case "item":
							useItem();
							break;
						default:
							break;
					}

				}
			}
			if (Input.GetMouseButtonDown(1)) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mapController.setTile((int) pos.x, (int) pos.y, new Item());
			}
		}
	}
	private void placeBlock() {
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mapController.setTile((int) pos.x, (int) pos.y, inventory.items[hotbar.activeSlot]);
	}
	private void useItem() { // TODO: figure out how to handle different items, hardcoded for now.
		Debug.Log("Used item");
	}
}