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
	public Sprite sprite;
	public TileAttribute(Sprite sprite, string layer = "ground") {
		this.sprite = sprite;
		this.layer = layer;
	}
}
public class ConsumableAttribute : ItemAttribute, IConsumable {
	public void Consume() {

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