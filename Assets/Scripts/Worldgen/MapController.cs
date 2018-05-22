using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class MapController : MonoBehaviour {
	public bool generateMap = false;

	[SerializeField] int mapWidth = 10;
	[SerializeField] int mapHeight = 10;
	[SerializeField][Range(0, 1)] float blockChance;
	[SerializeField][Range(0, 10)] int smoothRate;
	[SerializeField] Tile groundTile;
	[SerializeField] Tile backgroundTile;
	[SerializeField] Tilemap tilemap;
	[SerializeField] Tilemap background;
	[SerializeField] Tilemap climbing;
	[SerializeField] int minHeight = 10;
	[SerializeField] int maxHeight = 10;
	Map map;
	[SerializeField] ItemDatabaseController itemDatabase;
	private void Start() {
		// Debug.Log(itemDatabase.name);
		// Debug.Log(itemDatabase.database);
		generateNewMap();
	}

	void generateNewMap() {
		itemDatabase.generateNewDatabase();
		for (int i = 0; i < itemDatabase.itemDatabase.Count; i++) {
			// Debug.Log(itemDatabase.itemDatabase[i].ItemName + " has type: " + itemDatabase.itemDatabase[i].Type);
		}
		tilemap.ClearAllTiles();
		background.ClearAllTiles();
		map = new Map(itemDatabase.itemDatabase, blockChance, smoothRate, mapWidth, mapHeight, minHeight, maxHeight);
		renderMap();
	}
	void renderMap() {
		for (int x = 0; x < map.getWidth(); x++) {
			for (int y = 0; y < map.getHeight(); y++) {
				Vector3Int position = new Vector3Int(x, y, 0);
				background.SetTile(position, backgroundTile);
				renderTile(position);
			}
		}
	}

	void renderTile(Vector3Int position) {
		MapTile tileToRender = map.getTileAt(position.x, position.y);
		// Debug.Log(tileToRender.item.ID);
		if (tileToRender.item.ID == -1) {
			tilemap.SetTile(position, null);
		} else {
			tilemap.SetTile(position, tileToRender);
		}
	}
	// Script button to generate new map
	void Update() {
		if (generateMap) {
			// Debug.Log("WHYYY");
			generateNewMap();
		}
		generateMap = false;
	}
	public void setTile(int x, int y, Item item) {
		map.setTileAt(x, y, item);
		renderTile(new Vector3Int(x, y, 0));
	}
}