using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldInteract : MonoBehaviour {
	[SerializeField] MapController mapController;
	[SerializeField] Inventory inventory;
	ItemDatabaseController itemDatabase;
	[SerializeField] GameObject throwable;
	Hotbar hotbar;

	private void Start() {
		hotbar = GameObject.Find("Inventory").GetComponent<Hotbar>();
		itemDatabase = inventory.GetComponent<ItemDatabaseController>();
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
				mapController.setTile((int) pos.x, (int) pos.y, ScriptableObject.CreateInstance<Item>());
			}
		}
	}
	private void placeBlock() {
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mapController.setTile((int) pos.x, (int) pos.y, inventory.itemList[hotbar.activeSlot]);
	}
	private void useItem() { // TODO: figure out how to handle different items, hardcoded for now.
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 playerPos = GameObject.Find("Player").transform.position;
		Vector2 heading = pos - playerPos;
		Vector2 direction = heading.normalized;
		GameObject item = Instantiate(throwable);
		// FIXME: Refactor this to not be shit
		item.transform.position = GameObject.Find("Player").transform.position;
		// item.GetComponent<Rigidbody2D>().AddForce(-(GameObject.Find("Player").transform.position - pos).normalized * inventory.itemList[hotbar.activeSlot].getAttributeOfType<ThrowableAttribute>().velocity);
		Debug.Log(pos);
		Debug.Log(playerPos);
		item.GetComponent<Rigidbody2D>().AddForce(direction * inventory.itemList[hotbar.activeSlot].getAttributeOfType<ThrowableAttribute>().velocity);
	}
}