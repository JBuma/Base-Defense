using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[SerializeField] float playerSpeed = 3;
	[SerializeField] float jumpPower = 10;

	Rigidbody2D rigidbody;
	Animator animator;
	Collider2D collider;

	void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		collider = GetComponent<Collider2D>();
	}

	void Update() {
		handleInput();
	}
	private void handleInput() {
		movement();
		jumping();
	}
	private void movement() {
		float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		Vector2 playerVelocity = new Vector2(horizontalThrow * playerSpeed, rigidbody.velocity.y);
		rigidbody.velocity = playerVelocity;

		//If player is running
		if (Mathf.Abs(horizontalThrow) > Mathf.Epsilon) {
			//Flip sprite based on direction
			transform.localScale = new Vector2(Mathf.Sign(horizontalThrow), transform.localScale.y);

			animator.SetBool("Running", true);
		} else {
			animator.SetBool("Running", false);
		}
	}
	private void jumping() {
		print("Player can jump: " + collider.IsTouchingLayers(LayerMask.GetMask("Ground")));
		print("Layermask: " + LayerMask.GetMask("Ground"));
		if (!collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
			return;
		}

		print("jumpy");
		if (CrossPlatformInputManager.GetButtonDown("Jump")) {
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + jumpPower);
		}
	}
}