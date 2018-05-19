using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item {
	public int ID { get; private set; }
	public string Title { get; set; }
	public int Value { get; set; }
	public int Health { get; set; }
	public string Description { get; set; }
	public bool Stackable { get; set; }
	public string Slug { get; set; }
	public Sprite Sprite { get; set; }
	public string Type { get; set; }
	public string Layer { get; set; }

	public Item(int id, string title, int value, int health, string description, bool stackable, string slug, string type, string layer = "Ground") {
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Health = health;
		this.Description = description;
		this.Stackable = stackable;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite>("Sprites/" + type + "s/" + slug);
		this.Type = type;
		this.Layer = layer;
	}
	public Item() {
		this.ID = -1;
		this.Title = null;
	}
}