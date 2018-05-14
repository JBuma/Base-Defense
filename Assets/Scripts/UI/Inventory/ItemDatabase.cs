using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	private Dictionary<int, Item> database = new Dictionary<int, Item>();
	private JsonData itemData;

	private void Start() {
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		constructDatabase();
	}
	private void constructDatabase() {
		for (int i = 0; i < itemData.Count; i++) {
			Item itemToAdd;
			// TODO: Rework aaaaalllll of this, make it dynamic. This is just bad. Very bad.
			// FIXME: For real. Fix it.
			if (itemData[i].ContainsKey("layer")) {
				itemToAdd = new Item((int) itemData[i]["id"], (string) itemData[i]["title"], (int) itemData[i]["value"],
					(int) itemData[i]["stats"]["health"], (string) itemData[i]["description"], (bool) itemData[i]["stackable"], (string) itemData[i]["slug"], (string) itemData[i]["type"], (string) itemData[i]["layer"]
				);
			} else {
				itemToAdd = new Item((int) itemData[i]["id"], (string) itemData[i]["title"], (int) itemData[i]["value"],
					(int) itemData[i]["stats"]["health"], (string) itemData[i]["description"], (bool) itemData[i]["stackable"], (string) itemData[i]["slug"], (string) itemData[i]["type"]
				);
			}
			if (database.ContainsKey((int) itemData[i]["id"])) {
				Debug.LogError("Item with id: " + itemData[i]["id"] + " already exists.");
				return;
			} else {
				database[(int) itemData[i]["id"]] = itemToAdd;
			}

		}
	}
	public Item getItemByID(int id) {
		if (!database.ContainsKey(id)) { return null; }
		return database[id];
	}
}
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
		this.Sprite = Resources.Load<Sprite>("Sprites/Blocks/" + slug);
		this.Type = type;
		this.Layer = layer;
	}
	public Item() {
		this.ID = -1;
		this.Title = null;
	}
}