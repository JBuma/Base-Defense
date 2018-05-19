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
	// [SerializeField] int gridSize = 32;
	[SerializeField] Tile groundTile;
	[SerializeField] Tile backgroundTile;
	[SerializeField] Tilemap tilemap;
	[SerializeField] Tilemap background;
	[SerializeField] Tilemap climbing;
	[SerializeField] int minHeight = 10;
	[SerializeField] int maxHeight = 10;
	Map map;
	[SerializeField] ItemDatabase itemDatabase;
	// Dictionary<int, Tile> tileSprites = new Dictionary<int, Tile>();
	private void Start() {
		Debug.Log(itemDatabase.name);
		Debug.Log(itemDatabase.database);
		generateNewMap();
	}

	void generateNewMap() {
		itemDatabase.generateDatabase();
		tilemap.ClearAllTiles();
		background.ClearAllTiles();
		map = new Map(itemDatabase.database, blockChance, smoothRate, mapWidth, mapHeight, minHeight, maxHeight);
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
		if (tileToRender.item.ID == -1) {
			tilemap.SetTile(position, null);
		} else {
			// Debug.Log("Set the tile at" + position.x + "," + position.y + " with sprite: " + tileToRender.sprite);
			switch (tileToRender.item.Layer) {
				case "Climbing":
					climbing.SetTile(position, tileToRender);
					break;
				default:
					tilemap.SetTile(position, tileToRender);
					break;
			}
		}
	}

	// Script button to generate new map
	void Update() {
		if (generateMap) {
			generateNewMap();
		}
		generateMap = false;
	}
	public void setTile(int x, int y, Item item) {
		map.setTileAt(x, y, item);
		renderTile(new Vector3Int(x, y, 0));
	}
}