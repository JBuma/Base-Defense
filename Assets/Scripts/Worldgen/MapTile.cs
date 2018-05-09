using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile {

	public enum TileType { Empty, Ground }

	TileType tileType = TileType.Empty;
	int x;
	int y;

	public MapTile(int x, int y) {
		this.x = x;
		this.y = y;
	}
	public TileType getTileType() {
		return tileType;
	}
	public void setTyleType(TileType type) {
		this.tileType = type;
	}
}