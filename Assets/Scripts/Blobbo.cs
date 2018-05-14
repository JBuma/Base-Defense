using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Blobbo : MonoBehaviour {

	Enemy enemy;
	Rigidbody2D rigidbody;
	// Use this for initialization
	void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
		enemy = GetComponent<Enemy>();
	}

	// Update is called once per frame
	void Update() {
		rigidbody.velocity = isFacingRight() ? new Vector2(enemy.getSpeed(), 0f) : new Vector2(-enemy.getSpeed(), 0f);
	}
	bool isFacingRight() {
		return transform.localScale.x > 0;
	}
	private void OnTriggerExit2D(Collider2D other) {
		transform.localScale = new Vector2(-Mathf.Sign(rigidbody.velocity.x), 1f);
	}
}