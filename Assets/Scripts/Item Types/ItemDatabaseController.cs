using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class ItemDatabaseController : MonoBehaviour {
	public ItemDatabase itemDatabase;
	public Dictionary<int, MapTile> tileDatabase;
	private List<Item> itemData;
	public bool loadDatabase = false;
	void Start() {
		generateNewDatabase();
	}
	public void generateNewDatabase() {
		var settings = new JsonSerializerSettings();
		settings.TypeNameHandling = TypeNameHandling.Auto;

		itemData = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"), settings);
		itemDatabase = new ItemDatabase(itemData);
		Debug.Log(itemDatabase[0].ItemName + " has type: " + itemDatabase[0].Type);
		foreach (Item item in itemData) {
			AssetDatabase.CreateAsset(item, "Assets/TEST/" + item.Slug + ".asset");
			AssetDatabase.SaveAssets();
		}
	}

	// Update is called once per frame
	void Update() {
		if (loadDatabase) {
			generateNewDatabase();
		}
		loadDatabase = false;
	}
}

[System.Serializable]
public class ItemDatabase : Dictionary<int, Item> {
	public ItemDatabase(List<Item> itemData) {
		foreach (Item item in itemData) {
			if (!this.ContainsKey(item.ID)) {
				this [item.ID] = item;
			}
		}
	}
	public Item getItemByID(int id) {
		return this.ContainsKey(id) ? this [id] : null;
	}
}