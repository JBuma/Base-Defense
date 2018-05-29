using System;
using UnityEngine;

public class Throwable : MonoBehaviour {
	Rigidbody2D rigidbody;
	bool stuck = false;

	private void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	private void Update() {
		if (!stuck) {
			transform.up = rigidbody.velocity;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		stuck = true;
		rigidbody.isKinematic = true;

		if (other.gameObject.tag == "enemy") {
			HingeJoint2D hj = gameObject.AddComponent<HingeJoint2D>();
			hj.connectedBody = other.rigidbody;
			rigidbody.mass = 0.00001f;
			// collider.material.bounciness = 0;
			rigidbody.freezeRotation = true;
			rigidbody.velocity = new Vector2(0, 0);
		} else {
			rigidbody.mass = 0.00001f;
			rigidbody.freezeRotation = true;
			rigidbody.velocity = new Vector2(0, 0);
		}
	}
}