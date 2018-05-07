using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[SerializeField] float playerSpeed = 4f;
	[SerializeField] float jumpPower = 10f;
	[SerializeField] float climbingSpeed = 3f;

	bool isClimbing = false;

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
		climbing();
	}
	private void movement() {
		float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		Vector2 playerVelocity = new Vector2(horizontalThrow * playerSpeed, rigidbody.velocity.y);
		rigidbody.velocity = playerVelocity;

		//If player is running in either direction
		if (Mathf.Abs(horizontalThrow) > Mathf.Epsilon) {
			//Flip sprite based on direction
			transform.localScale = new Vector2(Mathf.Sign(horizontalThrow), transform.localScale.y);

			if (isClimbing) {
				isClimbing = false;
			}

			animator.SetBool("Running", true);
		} else {
			animator.SetBool("Running", false);
		}
	}
	private void climbing() {

		// Makes sure the player can keep hanging on the ladder
		if (isClimbing) {
			rigidbody.gravityScale = 0f;
		} else {
			rigidbody.gravityScale = 1f;
		}

		if (!collider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
			isClimbing = false;
			return;
		}

		float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");

		if (Mathf.Abs(verticalThrow) > Mathf.Epsilon) {
			//Flip sprite based on direction
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalThrow * climbingSpeed);
			animator.SetBool("Climbing", true);
			isClimbing = true;
		} else {
			animator.SetBool("Climbing", false);

		}
		if (isClimbing && CrossPlatformInputManager.GetButtonUp("Vertical")) {
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
		}
	}
	private void jumping() {
		// Player can also jump while still on a ladder
		if (collider.IsTouchingLayers(LayerMask.GetMask("Ground")) || (collider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && isClimbing)) {
			// return;
			if (CrossPlatformInputManager.GetButtonDown("Jump")) {
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + jumpPower);
				isClimbing = false;
			}
		}

	}
}