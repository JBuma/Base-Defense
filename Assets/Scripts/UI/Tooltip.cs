using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
	private Item item;
	private string data;
	[SerializeField] GameObject tooltip;
	private Text text;

	void Start() {
		// tooltip = GameObject.Find("Tooltip");
		text = tooltip.transform.GetChild(0).GetComponent<Text>();
		tooltip.SetActive(false);
	}
	public void activate(Item item) {
		this.item = item;
		constructDataString();
		tooltip.SetActive(true);
	}
	public void deActivate() {
		tooltip.SetActive(false);
	}
	private void constructDataString() {
		data = item.Title;
		text.text = data;
	}
}