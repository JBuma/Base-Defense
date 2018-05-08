using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[SerializeField] int healthMax = 100;
	int healthCurrent;
	[SerializeField] float runSpeed = 4f;
	[SerializeField] float jumpPower = 10f;
	[SerializeField] float climbingSpeed = 3f;
	[SerializeField] float ragdollTime = 0.5f;

	bool isClimbing = false;
	bool isRagdoll = false;

	Rigidbody2D rigidbody;
	Animator animator;
	CapsuleCollider2D colliderBody;
	BoxCollider2D colliderFeet;
	[SerializeField] UIController uiController;

	void Start() {
		initializeVars();

		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		colliderBody = GetComponent<CapsuleCollider2D>();
		colliderFeet = GetComponent<BoxCollider2D>();
		// uiController = GameObject.FindObjectOfType<UIController>();
	}
	void initializeVars() {
		healthCurrent = healthMax;
	}

	void Update() {
		handleInput();
	}
	private void handleInput() {
		movement();
		jumping();
		climbing();
	}
	IEnumerator resetRagdoll(float time) {
		print("Counting");
		yield return new WaitForSeconds(time);
		print("Done counting");
		isRagdoll = false;
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			handleDamage(collision.gameObject.GetComponent<Enemy>().getDamage());
			knockBack(10f, collision.transform.position - transform.position);
		}
	}
	void handleDamage(int damage) {
		healthCurrent -= damage;
		uiController.updateHealthBar(healthMax, healthCurrent);
	}
	void knockBack(float knockbackForce, Vector2 direction) {
		isRagdoll = true;

		float horizontalDirection = -Mathf.Sign(direction.x);
		Vector2 knockback = direction * knockbackForce * -1;
		print("Knockback: " + knockback);
		print("Player Velocity: " + rigidbody.velocity);
		rigidbody.velocity = rigidbody.velocity + knockback;
		StartCoroutine(resetRagdoll(ragdollTime));
	}
	private void movement() {
		if (isRagdoll) { return; }
		float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		Vector2 playerVelocity = new Vector2(horizontalThrow * runSpeed, rigidbody.velocity.y);
		rigidbody.velocity = playerVelocity;

		// TODO: Maybe look at for later??
		// rigidbody.AddForce(Vector2.right * horizontalThrow * runSpeed);
		// rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, 15f);

		//If player is running in either direction
		if (Mathf.Abs(horizontalThrow) > Mathf.Epsilon) {
			//Flip sprite based on direction
			transform.localScale = new Vector2(Mathf.Sign(horizontalThrow), transform.localScale.y);

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

		if (!colliderFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
			isClimbing = false;
			animator.SetBool("Climbing", false);
			return;
		}

		float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");

		if (Mathf.Abs(verticalThrow) > Mathf.Epsilon) {
			//Flip sprite based on direction
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalThrow * climbingSpeed);
			animator.SetBool("Climbing", true);
			isClimbing = true;
		}
		if (isClimbing && CrossPlatformInputManager.GetButtonUp("Vertical")) {
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
		}
	}
	private void jumping() {
		// Player can also jump while still on a ladder
		if (colliderFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) || (colliderFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")) && isClimbing)) {
			// return;
			if (CrossPlatformInputManager.GetButtonDown("Jump")) {
				rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + jumpPower);
				isClimbing = false;
			}
		}

	}
}