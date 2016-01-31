using UnityEngine;
using System.Collections;

public class FireTrapWeapon : Weapon {
	public GameObject [] itemsOn;
	public GameObject [] itemsOff;

	public Collider2D collider;

	public float effectDuration = 1f;
	public float collideAfterDuration = .3f;

	void Awake() {
		base.Awake();
		collider.enabled = false;
	}

	void Start () {
		ready = true;
	}
	
	void Update () {
	}

	public override bool Attack() {
		if (!ready) {
			return false;
		}
		ready = false;
		print("lalaaaaaaaaaa");

		foreach (GameObject itemOff in itemsOff) {
			itemOff.SetActive(false);
		}
		
		foreach (GameObject itemOn in itemsOn) {
			itemOn.SetActive(true);
		}
		
		Animator animator = GetComponent<Animator>();
		if (animator != null) {
			animator.SetTrigger("Attack");
			Invoke("DisableAttack", effectDuration);
		}

		Invoke("EnableCollider", collideAfterDuration);

		return true;
	}

	void EnableCollider() {
		collider.enabled = true;
	}

	void DisableAttack() {
		foreach (GameObject itemOff in itemsOff) {
			itemOff.SetActive(true);
		}

		foreach (GameObject itemOn in itemsOn) {
			itemOn.SetActive(false);
		}

		collider.enabled = false;

		ready = true;
	}
}
