using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

	MapTile[, ] tiles;
	int width = 10;
	int height = 10;
	float blockChance = 0.3f;
	int smoothRate = 5;

	public Map(float blockChance, int smoothRate, int width = 10, int height = 10) {
		this.width = width;
		this.height = height;
		this.blockChance = blockChance;
		this.smoothRate = smoothRate;

		tiles = new MapTile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles[x, y] = new MapTile(x, y);
				if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
					tiles[x, y].setTyleType(MapTile.TileType.Ground);
				}
			}
		}
		randomizeMap();

		for (int i = 0; i < smoothRate; i++) {
			smoothMap();
		}

		// Debug.Log("Map created with " + (width * height) + " tiles");
	}
	void randomizeMap() {
		for (int x = 0; x < getWidth(); x++) {
			for (int y = 0; y < getHeight(); y++) {
				Vector3Int position = new Vector3Int(x, y, 0);
				if (Random.Range(0f, 1f) < blockChance) {
					getTileAt(x, y).setTyleType(MapTile.TileType.Ground);
				}
			}
		}
	}
	void smoothMap() {
		for (int x = 0; x < getWidth(); x++) {
			for (int y = 0; y < getHeight(); y++) {
				int neighbourTiles = GetSurroundingWallCount(x, y);
				if (neighbourTiles > 4) {
					tiles[x, y].setTyleType(MapTile.TileType.Ground);
				} else if (neighbourTiles <= 4) {
					tiles[x, y].setTyleType(MapTile.TileType.Empty);
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
						if (tiles[neighbourX, neighbourY].getTileType() == MapTile.TileType.Ground) {
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
	public int getWidth() {
		return width;
	}
	public int getHeight() {
		return height;
	}
}