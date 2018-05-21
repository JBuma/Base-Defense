using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OldItem {
	public int ID;
	public string Title;
	public int Value;
	public string Description;
	public bool Stackable;
	public string Slug;
	public Sprite Sprite;
	public string Type;
	// public string Layer { get; set; }

	public OldItem(int id, string title, int value, int health, string description, bool stackable, string slug, string type, string layer = "Ground") {
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Description = description;
		this.Stackable = stackable;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite>("Sprites/" + type + "s/" + slug);
		this.Type = type;
	}
	public OldItem() {
		this.ID = -1;
		this.Title = null;
	}
}