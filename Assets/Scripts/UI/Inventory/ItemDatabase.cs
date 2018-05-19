using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using Newtonsoft.Json;
using UnityEngine;

[ExecuteInEditMode]
public class ItemDatabase : MonoBehaviour {
	public bool loadDatabase = false;
	public DataBase_Items database;
	// private JsonData itemData;
	public List<Item> itemDatabase { get; set; }

	private void Start() {
		generateDatabase();

	}
	public void generateDatabase() {
		// itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		// Item_Collection itemData = JsonUtility.FromJson<Item_Collection>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		itemDatabase = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		Debug.Log(itemDatabase[0].Title);
	}

	public Item getItemByID(int id) {
		if (!database.ContainsKey(id)) { return null; }
		return database[id];
	}
	private void Update() {
		if (loadDatabase) {
			generateDatabase();
		}
		loadDatabase = false;
	}
}

[System.Serializable]
public struct Common_Data {
	public string Type;
}
public class Item_Collection {
	public Common_Data[] values;
}
public class DataBase_Items : Dictionary<int, Item> {
	public DataBase_Items(Item_Collection itemData) {
		// foreach (Data item in itemData.values) {
		// 	Item itemToAdd = item;
		// 	itemToAdd.Sprite = Resources.Load<Sprite>("Sprites/" + item.Type + "s/" + item.Slug);
		// 	if (this.ContainsKey((int) item.ID)) {
		// 		Debug.LogError("Item with id: " + item.ID + " already exists.");
		// 		return;
		// 	} else {
		// 		this [(int) item.ID] = itemToAdd;
		// 	}

		// }
	}
}
// public class Item {
// 	public int ID { get; private set; }
// 	public string Title { get; set; }
// 	public int Value { get; set; }
// 	public int Health { get; set; }
// 	public string Description { get; set; }
// 	public bool Stackable { get; set; }
// 	public string Slug { get; set; }
// 	public Sprite Sprite { get; set; }
// 	public string Type { get; set; }
// 	public string Layer { get; set; }

// 	public Item(int id, string title, int value, int health, string description, bool stackable, string slug, string type, string layer = "Ground") {
// 		this.ID = id;
// 		this.Title = title;
// 		this.Value = value;
// 		this.Health = health;
// 		this.Description = description;
// 		this.Stackable = stackable;
// 		this.Slug = slug;
// 		this.Sprite = Resources.Load<Sprite>("Sprites/" + type + "s/" + slug);
// 		this.Type = type;
// 		this.Layer = layer;
// 	}
// 	public Item() {
// 		this.ID = -1;
// 		this.Title = null;
// 	}
// }