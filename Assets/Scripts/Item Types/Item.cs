using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class Item : ScriptableObject {
	public int ID;
	public string ItemName;
	public string Slug;
	public string Type; //TODO: make into enum
	public List<ItemAttribute> ItemAttributes;
	public Sprite Sprite;

	public Item(int id) {
		this.ID = id;
		ItemAttributes = new List<ItemAttribute>();
	}
	public Item() {
		this.ID = -1;
		ItemAttributes = new List<ItemAttribute>();
	}
	public void loadSprite() {
		this.Sprite = Resources.Load<Sprite>("Sprites/" + this.Type + "s/" + this.Slug);
	}
	public Sprite getSprite() {
		if (this.Sprite == null) {
			loadSprite();
		}
		return this.Sprite;
	}

	public void addAttribute(ItemAttribute attribute) {
		ItemAttributes.Add(attribute);
	}
	public bool hasAttributeOfType<TAttribute>() where TAttribute : ItemAttribute {
		foreach (ItemAttribute attr in ItemAttributes) {
			if (attr is TAttribute) {
				return true;
			}
		}
		return false;
	}
	public TAttribute getAttributeOfType<TAttribute>() where TAttribute : ItemAttribute {
		foreach (ItemAttribute attr in ItemAttributes) {
			if (attr is TAttribute) {
				return attr as TAttribute;
			}
		}
		return null;
	}
}