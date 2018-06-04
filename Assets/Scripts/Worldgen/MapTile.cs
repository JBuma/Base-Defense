using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTile : Tile {
	public enum TileType { Empty, Ground, Climbing }
	public Item item;

	TileType tileType = TileType.Empty;
	Quaternion rotation;

	public MapTile(int x, int y, Item item) {
		this.tileType = TileType.Empty;
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
		tileData.flags = TileFlags.LockTransform;
		tileData.transform = Matrix4x4.identity;
		tileData.transform = Matrix4x4.Rotate(this.rotation);
	}
	public void setRuleSprite(Sprite sprite) {
		this.sprite = sprite;
	}
	public TileType getTileType() {
		return tileType;
	}
	public void setTyleType(TileType type) {
		this.tileType = type;
	}
	public void setTileItem(Item item) {
		if (item.Sprite == null) {
			item.loadSprite();
		}
		this.item = item;
		if (item.Sprite == null) {
			item.loadSprite();
		}
		if (item.ID == -1) { this.tileType = TileType.Empty; return; }
		switch (item.hasAttributeOfType<RuleTileAttribute>() ? item.getAttributeOfType<RuleTileAttribute>().layer : item.getAttributeOfType<TileAttribute>().layer) {

			case "Climbing":
				this.tileType = TileType.Climbing;
				break;
			case "Ground":
				this.tileType = TileType.Ground;
				break;
			default:
				this.tileType = TileType.Empty;
				break;
		}
		this.sprite = this.item.Sprite;
	}
	public void setRotation(Quaternion rotation) {
		this.rotation = rotation;

	}
	public Item getTileItem() {
		return this.item;
	}
}