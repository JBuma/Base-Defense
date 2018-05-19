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
	[SerializeField] Inventory inventory;
	Hotbar hotbar;

	private KeyCode[] keyCodes = {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
	};

	void Start() {
		initializeVars();

		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		colliderBody = GetComponent<CapsuleCollider2D>();
		colliderFeet = GetComponent<BoxCollider2D>();
		hotbar = inventory.GetComponent<Hotbar>();
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
		if (Input.GetKeyUp(KeyCode.E)) {
			inventory.toggleInventory();
		}
		changeHotbarSlot();
	}

	void changeHotbarSlot() {
		if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f) {
			// Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel") > 0f);
			hotbar.changeSlot(hotbar.activeSlot - 1);
		} else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f) {
			// Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel") < 0f);
			hotbar.changeSlot(hotbar.activeSlot + 1);
		}
		for (int i = 0; i < keyCodes.Length; i++) {
			if (Input.GetKeyDown(keyCodes[i])) {
				hotbar.changeSlot(i);
			}
		}
		if (Input.GetKeyUp(KeyCode.Alpha0)) {
			hotbar.changeSlot(9);
		}
		if (Input.GetKeyUp(KeyCode.Minus)) {
			hotbar.changeSlot(10);
		}
		if (Input.GetKeyUp(KeyCode.Equals)) {
			hotbar.changeSlot(11);
		}
	}
	IEnumerator resetRagdoll(float time) {
		yield return new WaitForSeconds(time);
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

		Vector2 knockback = direction * knockbackForce * -1;
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
			//Flips sprite based on direction
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

		float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");

		if (colliderFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")) && (Mathf.Abs(verticalThrow) > Mathf.Epsilon)) {
			//Flip sprite based on direction
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, verticalThrow * climbingSpeed);
			animator.SetBool("Climbing", true);
			isClimbing = true;
		}
		if (isClimbing && CrossPlatformInputManager.GetButtonUp("Vertical")) {
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
		}
		if (!colliderFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
			isClimbing = false;
			animator.SetBool("Climbing", false);
			return;
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