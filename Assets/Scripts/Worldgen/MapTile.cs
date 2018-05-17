﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTile : Tile {

	public enum TileType { Empty, Ground, Climbing }
	public Item item;

	TileType tileType = TileType.Empty;

	public MapTile(int x, int y, Item item) {
		if (item != null) {
			this.item = item;
		} else {
			item = new Item();
		}
		this.sprite = this.item.Sprite;
	}
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
		tileData.sprite = this.sprite;
		tileData.color = Color.white;
		tileData.colliderType = ColliderType.Grid;
		// tileData.flags = TileFlags.LockTransform;
		// tileData.colliderType = ColliderType.None;
	}
	public TileType getTileType() {
		return tileType;
	}
	public void setTyleType(TileType type) {
		this.tileType = type;
	}
	public void setTileItem(Item item) {
		this.item = item;
		this.sprite = this.item.Sprite;
	}
	public Item getTileItem() {
		return this.item;
	}
}