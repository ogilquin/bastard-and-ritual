using UnityEngine;

using UnitySteer2D.Behaviors;

public class TakeDamage : MonoBehaviour {
	public float damageForceScale = 5;
	public Life life;
	public float damageDuration = .5f;

	GameObject item;
	Player player;
	Monster monster;
	private Vector2 vectorDamageScale;
	private Rigidbody2D playerRigidBody;
	private Rigidbody2D monsterRigidBody;

	private AutonomousVehicle2D monsterVehicle;
	private Attack monsterAttack;
	private PlayerMovement playerMovement;
	private Attack playerAttack;

	private float damagedTime = -1;

	void Awake() {
		vectorDamageScale = new Vector2(-damageForceScale, -damageForceScale);
	}

	public void Setup(GameObject item) {
		player = (Player) gameObject.GetComponentInParent<Player>();
		monster = (Monster) gameObject.GetComponentInParent<Monster>();

		this.item = item;
		if (monster != null) {
			this.item = item.GetComponent<Monster>().iaToFollow;
			monsterRigidBody = this.item.GetComponent<Rigidbody2D>();
			monsterVehicle = monsterRigidBody.gameObject.GetComponent<AutonomousVehicle2D>();
			monsterAttack = monster.GetComponentInChildren<Attack>();
		} else if (player != null) {
			playerRigidBody = item.GetComponent<Rigidbody2D>();
			playerMovement = player.GetComponent<PlayerMovement>();
			playerAttack = player.GetComponentInChildren<Attack>();
		}
	}

    public void Damage(int damage, GameObject damager) {
		Vector2 damageForce = damager.transform.position - transform.position;
		damageForce.Normalize();
		damageForce.Scale(vectorDamageScale);

		if (monster != null) {
			monsterVehicle.CanMove = false;
			monsterAttack.CanAttack = false;
			monsterRigidBody.AddForce(damageForce, ForceMode2D.Impulse);
			damagedTime = Time.time;
		} else if (player != null) {
			playerMovement.CanMove = false;
			playerAttack.CanAttack = false;
			playerRigidBody.AddForce(damageForce, ForceMode2D.Impulse);
			damagedTime = Time.time;
		}
		
		life.Damage(damage);
    }

	void Update() {
		if (damagedTime >= 0 && Time.time - damagedTime >= damageDuration) {
			if (monster != null) {
				monsterVehicle.CanMove = true;
				monsterAttack.CanAttack = true;
			} else if (player != null) {
				playerMovement.CanMove = true;
				playerAttack.CanAttack = true;
			}

			damagedTime = -1;
		}
	}
}
