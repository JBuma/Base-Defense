using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField] int healthMax = 10;
	[SerializeField] int damageBase = 5;
	[SerializeField] float speedBase = 10f;
	// Use this for initialization
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		gameObject.tag = "Enemy";
	}
	public int getDamage() {
		return damageBase;
	}

	// Update is called once per frame
	void Update() {

	}
}