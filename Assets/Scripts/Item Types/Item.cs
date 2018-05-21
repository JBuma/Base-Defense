using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	public int ID;
	public string ItemName;
	public string Slug;
	public string Type; //TODO: make into enum
	public List<ItemAttribute> ItemAttributes;
	public Sprite Sprite;

	public Item() {
		ItemAttributes = new List<ItemAttribute>();
		this.Sprite = Resources.Load<Sprite>("Sprites/" + this.Type + "s/" + this.Slug);
	}

	public void addAttribute(ItemAttribute attribute) {
		ItemAttributes.Add(attribute);
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