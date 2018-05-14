﻿using System.Collections;
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
	[SerializeField] int minHeight = 10;
	[SerializeField] int maxHeight = 10;
	Map map;
	private void Start() {
		generateNewMap();
	}

	void generateNewMap() {
		tilemap.ClearAllTiles();
		background.ClearAllTiles();
		map = new Map(blockChance, smoothRate, mapWidth, mapHeight, minHeight, maxHeight);
		renderMap();
	}
	void renderMap() {
		for (int x = 0; x < map.getWidth(); x++) {
			for (int y = 0; y < map.getHeight(); y++) {
				Vector3Int position = new Vector3Int(x, y, 0);
				background.SetTile(position, backgroundTile);
				setTile(position);
			}
		}
	}

	void setTile(Vector3Int position) {
		switch (map.getTileAt(position.x, position.y).getTileType()) {
			case MapTile.TileType.Ground:
				tilemap.SetTile(position, groundTile);
				break;
			default:
				tilemap.SetTile(position, null);
				break;
		}
	}

	// Script button to generate new map
	void Update() {
		if (generateMap) {
			generateNewMap();
		}
		generateMap = false;
	}
	public void setTile(int x, int y, MapTile.TileType type) {
		map.getTileAt(x, y).setTyleType(type);
		setTile(new Vector3Int(x, y, 0));
	}
}