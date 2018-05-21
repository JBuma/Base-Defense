using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public PlacableAttribute(string layer) {
		this.layer = layer;
	}
}