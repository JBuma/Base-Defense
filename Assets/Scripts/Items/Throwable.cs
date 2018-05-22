using System;
using UnityEngine;

public class Throwable : MonoBehaviour {
	Rigidbody2D rigidbody;

	private void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	private void Update() {
		transform.up = rigidbody.velocity;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		rigidbody.isKinematic = true;
	}
}