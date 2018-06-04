using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField] int healthMax = 10;
	[SerializeField] int damageBase = 5;
	[SerializeField] float speedBase = 10f;
	void Start() {
		gameObject.tag = "Enemy";
	}
	public int getDamage() {
		return damageBase;
	}
	public float getSpeed() {
		return speedBase;
	}
}