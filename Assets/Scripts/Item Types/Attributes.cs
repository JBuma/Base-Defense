using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ItemAttribute : ScriptableObject { }

public class ValueAttribute : ItemAttribute {
	public int value;
	public ValueAttribute(int value = 0) {
		this.value = value;
	}
}
public class WeightAttribute : ItemAttribute {
	public int weight;
	public WeightAttribute(int value = 0) {
		this.weight = value;
	}
}
public class StackableAttribute : ItemAttribute {
	public bool stackable = true;
	public StackableAttribute() { }
}
public class PlacableAttribute : ItemAttribute {
	public string layer = "Ground";
	public PlacableAttribute(string layer = "ground") {
		this.layer = layer;
	}
}
public class TileAttribute : PlacableAttribute {
	public string layer;
	public TileAttribute(string layer = "Ground") {
		this.layer = layer;
	}
}
public class ConsumableAttribute : ItemAttribute, IConsumable {
	public void Consume() {

	}
}
public class RuleTileAttribute : TileAttribute {
	public Sprite[] spriteList;

	public RuleTileAttribute() {
		// this.sprite = sprite;
		// this.layer = layer;
	}
	public void loadSprites(Item item) {
		this.spriteList = new Sprite[6];
		for (int i = 0; i < 5; i++) {
			spriteList[i] = Resources.Load<Sprite>("Sprites/" + item.Type + "s/" + item.Slug + "/" + item.Slug + "_" + i);
			// Debug.Log(spriteList[i]);
		}
		spriteList[5] = Resources.Load<Sprite>("Sprites/" + item.Type + "s/" + item.Slug);
	}
	public Sprite getSprite(int spriteValue) {
		if (this.spriteList == null) { Debug.LogError("SpriteList not found!"); return null; };
		switch (spriteValue) {
			case 1:
			case 2:
			case 4:
			case 8:
				return spriteList[1];
			case 3:
			case 5:
			case 10:
			case 12:
				return spriteList[2];
			case 6:
			case 9:
				return spriteList[3];
			case 7:
			case 11:
			case 13:
			case 14:
				return spriteList[4];
			case 15:
				return spriteList[5];
			default:
				return spriteList[0];
				break;
		}
	}
}
public class ThrowableAttribute : ItemAttribute {
	public int damage;
	public float velocity;
	public Item item;

	public void throwItem(Item item) {
		GameObject parent = GameObject.Find("Physics Objects");
		GameObject throwable = Instantiate(new GameObject(), parent.transform);
		throwable.AddComponent(new Rigidbody2D().GetType());
		throwable.tag = "Projectiles";
		throwable.AddComponent(this.GetType());
		throwable.GetComponent<ThrowableAttribute>().damage = this.damage;
		throwable.GetComponent<ThrowableAttribute>().item = item;
	}
}