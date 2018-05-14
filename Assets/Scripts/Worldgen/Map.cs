﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

	MapTile[, ] tiles;
	int width = 10;
	int height = 10;
	float blockChance = 0.3f;
	int smoothRate = 5;
	int minHeight = 10;
	int maxHeight = 10;
	int lowestGroundBlock;

	PerlinNoise noise;

	public Map(float blockChance, int smoothRate, int width = 10, int height = 10, int minHeight = 10, int maxHeight = 10) {
		this.width = width;
		this.height = height;
		this.blockChance = blockChance;
		this.smoothRate = smoothRate;
		this.minHeight = minHeight;
		this.maxHeight = maxHeight;
		lowestGroundBlock = minHeight;

		noise = new PerlinNoise(Random.Range(100000, 100000000));

		tiles = new MapTile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles[x, y] = new MapTile(x, y);
			}
		}
		generateGroundLevel();
		// setEdges();
		randomizeMap();

		for (int i = 0; i < smoothRate; i++) {
			smoothMap();
		}

		// Debug.Log("Map created with " + (width * height) + " tiles");
	}
	void generateGroundLevel() {
		int lowest = maxHeight;
		for (int x = 0; x < width; x++) {
			int columnHeight = minHeight + noise.getNoise(x, maxHeight - minHeight);
			if (columnHeight < lowest) {
				lowest = columnHeight;
			}
			for (int y = 0; y < columnHeight; y++) {
				tiles[x, y].setTyleType(MapTile.TileType.Ground);
			}
		}
		lowestGroundBlock = lowest;
	}
	void setEdges() {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
					tiles[x, y].setTyleType(MapTile.TileType.Ground);
				}
			}
		}
	}
	void randomizeMap() {
		for (int x = 0; x < getWidth(); x++) {
			for (int y = 0; y < maxHeight; y++) {
				if (Random.Range(0f, 1f) < blockChance) {
					tiles[x, y].setTyleType(MapTile.TileType.Ground);
				} else {
					tiles[x, y].setTyleType(MapTile.TileType.Empty);
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