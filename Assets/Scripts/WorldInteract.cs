using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldInteract : MonoBehaviour {

	// Use this for initialization
	[SerializeField] MapController mapController;
	[SerializeField] Inventory inventory;

	// Update is called once per frame
	void Update() {
		if (!inventory.isOpen) {
			if (Input.GetMouseButtonDown(0)) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mapController.setTile((int) pos.x, (int) pos.y, MapTile.TileType.Ground);
			}
			if (Input.GetMouseButtonDown(1)) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mapController.setTile((int) pos.x, (int) pos.y, MapTile.TileType.Empty);
			}
		}
	}
}