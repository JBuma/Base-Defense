using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	float healthbarStartSize;
	[SerializeField] RectTransform healthbarBar;
	// Use this for initialization
	void Start() {
		// healthbarBar = GameObject.Find("/Healthbar/Bar").GetComponent<RectTransform>();
		healthbarStartSize = healthbarBar.sizeDelta.x;
	}

	// Update is called once per frame
	void Update() {

	}
	public void updateHealthBar(int maxHealth, int currentHealth) {
		float size = (healthbarStartSize / maxHealth) * currentHealth;
		healthbarBar.sizeDelta = new Vector2(size, healthbarBar.sizeDelta.y);
	}
}