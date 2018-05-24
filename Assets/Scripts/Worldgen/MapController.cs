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
		// generateNewMap();
	}

	void generateNewMap() {
		itemDatabase.generateNewDatabase();
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
		if (tileToRender.item.ID == -1) {
			climbing.SetTile(position, null);
			tilemap.SetTile(position, null);
		} else {
			if (tileToRender.item.hasAttributeOfType<RuleTileAttribute>()) {
				int mask = map.isGroundTile(position.x, position.y + 1) ? 1 : 0;
				mask += map.isGroundTile(position.x + 1, position.y) ? 2 : 0;
				mask += map.isGroundTile(position.x, position.y - 1) ? 4 : 0;
				mask += map.isGroundTile(position.x - 1, position.y) ? 8 : 0;
				if (tileToRender.item.getAttributeOfType<RuleTileAttribute>().spriteList == null) {
					tileToRender.item.getAttributeOfType<RuleTileAttribute>().loadSprites(tileToRender.item);
				}
				tileToRender.setRuleSprite(tileToRender.item.getAttributeOfType<RuleTileAttribute>().getSprite(mask));
				Debug.Log(mask + " masknum: " + tileToRender.sprite);
			}
			switch (tileToRender.item.getAttributeOfType<TileAttribute>().layer) {
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