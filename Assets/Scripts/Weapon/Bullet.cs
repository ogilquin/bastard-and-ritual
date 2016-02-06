using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	protected Rigidbody2D rigidbody2d;
	protected Weapon weapon;

	public virtual void Awake()
	{
		rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
	}

	public virtual void FixedUpdate()
	{
		transform.position = Utils.PositionDepth(transform.position);
	}

	public virtual void Launch(Vector2 direction, float force, Weapon weapon)//, Player owner)
	{
		//this.owner = owner;
		this.weapon = weapon;
		rigidbody2d.velocity = direction * force;
	}
	/*
	public virtual Player GetOwner()
	{
		return owner;
	}*/


	public virtual void OnCollisionEnter2D(Collision2D collision)
	{
		bool isWall = collision.gameObject.tag == "Wall";

		TakeDamage takeDamage = collision.gameObject.GetComponent<TakeDamage>();
		if (takeDamage == null) {
			takeDamage = collision.gameObject.GetComponentInChildren<TakeDamage>();
		}

		if (isWall || takeDamage != null) {
			if (isWall) {
				rigidbody2d.velocity = Vector2.zero;
				rigidbody2d.angularVelocity = 0;
				((CircleCollider2D) gameObject.GetComponent<CircleCollider2D>()).enabled = false;
			} else {
				print(takeDamage);
				TakeDamage ownerTakeDamage = weapon.transform.parent.parent.gameObject.GetComponentInChildren<TakeDamage>();
				
				if (ownerTakeDamage != takeDamage) {
					takeDamage.Damage(weapon.damage, collision.gameObject);
					Destroy(gameObject);
				}
			}
		}
	}
}