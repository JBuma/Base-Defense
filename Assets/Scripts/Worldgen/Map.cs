using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map {

	MapTile[, ] tiles;
	int width = 10;
	int height = 10;
	float blockChance = 0.3f;
	int smoothRate = 5;
	int minHeight = 10;
	int maxHeight = 10;
	int lowestGroundBlock;
	// Dictionary<int, Item> itemDatabase;
	ItemDatabase itemDatabase;

	PerlinNoise noise;

	public Map(ItemDatabase itemDatabase, float blockChance, int smoothRate, int width = 10, int height = 10, int minHeight = 10, int maxHeight = 10) {
		this.itemDatabase = itemDatabase;
		this.width = width;
		this.height = height;
		this.blockChance = blockChance;
		this.smoothRate = smoothRate;
		this.minHeight = minHeight;
		this.maxHeight = maxHeight;
		lowestGroundBlock = minHeight;
		// Debug.Log("Itemdatabase: " + itemDatabase);

		noise = new PerlinNoise(Random.Range(100000, 100000000));

		tiles = new MapTile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				// tiles[x, y] = new MapTile(x, y, new Item());
				tiles[x, y] = ScriptableObject.CreateInstance<MapTile>();
				tiles[x, y].setTileItem(ScriptableObject.CreateInstance<Item>());
			}
		}
		generateGroundLevel();
		// setEdges();
		randomizeMap();

		for (int i = 0; i < this.smoothRate; i++) {
			smoothMap();
		}
		Debug.Log("Map created with " + (width * height) + " tiles");
	}
	void generateGroundLevel() {
		// Debug.Log(this.itemDatabase);
		int lowest = maxHeight;
		for (int x = 0; x < width; x++) {
			int columnHeight = minHeight + noise.getNoise(x, maxHeight - minHeight);
			if (columnHeight < lowest) {
				lowest = columnHeight;
			}
			for (int y = 0; y < columnHeight; y++) {

				if (columnHeight < height) {
					tiles[x, y].setTyleType(MapTile.TileType.Ground);
					tiles[x, y].setTileItem(itemDatabase[0]);
				}
			}
		}
		lowestGroundBlock = lowest;
	}
	void setEdges() {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
					tiles[x, y].setTyleType(MapTile.TileType.Ground);
					tiles[x, y].setTileItem(itemDatabase[0]);
				}
			}
		}
	}
	void randomizeMap() {
		for (int x = 0; x < getWidth(); x++) {
			for (int y = 0; y < maxHeight; y++) {
				if (Random.Range(0f, 1f) < blockChance) {
					tiles[x, y].setTyleType(MapTile.TileType.Ground);
					tiles[x, y].setTileItem(itemDatabase[0]);
				} else {
					tiles[x, y].setTyleType(MapTile.TileType.Empty);
					tiles[x, y].setTileItem(ScriptableObject.CreateInstance<Item>());
				}
			}
		}
	}
	void smoothMap() {
		for (int x = 0; x < getWidth(); x++) {
			for (int y = 0; y < getHeight(); y++) {
				int neighbourTiles = GetSurroundingWallCount(x, y);
				if (neighbourTiles > 4) {
					tiles[x, y].setTileItem(itemDatabase[0]);
				} else if (neighbourTiles <= 4) {
					tiles[x, y].setTileItem(ScriptableObject.CreateInstance<Item>());
				}
			}
		}
	}

	int GetSurroundingWallCount(int tileX, int tileY) {
		int wallCount = 0;
		for (int neighbourX = tileX - 1; neighbourX <= tileX + 1; neighbourX++) {
			for (int neighbourY = tileY - 1; neighbourY <= tileY + 1; neighbourY++) {
				// Not at edges
				if (neighbourX > 0 && neighbourX < getWidth() && neighbourY > 0 && neighbourY < getHeight()) {
					// Not tile itself
					if (neighbourX != tileX || neighbourY != tileY) {
						// Debug.Log(tiles[neighbourX, neighbourY].item);
						// Debug.Log("Item name at tile (" + neighbourX + ", " + neighbourY + "): " + tiles[neighbourX, neighbourY].getTileItem().Title);
						if (tiles[neighbourX, neighbourY].item.ID != -1) {
							wallCount++;
						}
					}
				} else {
					wallCount++;
				}
			}
		}
		return wallCount;
	}

	public MapTile getTileAt(int x, int y) {
		if (tiles[x, y] == null) {
			return null;
		} else {
			return tiles[x, y];
		}
	}
	public void setTileAt(int x, int y, Item item) {
		if (tiles[x, y] != null) {
			tiles[x, y].setTileItem(item);
		}
	}
	public int getWidth() {
		return width;
	}
	public int getHeight() {
		return height;
	}
}