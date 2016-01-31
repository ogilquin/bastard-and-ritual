using UnityEngine;
using System.Collections;
using UnitySteer2D.Behaviors;

public class MonsterAttack : Attack {
	private Vector2 direction = new Vector2(1f, 1f);
	private float rotation = 0f;
	private Monster monster;

	private Animator anim;

	private float lastHit = 0;

	public float distanceMinToAttackWithSword = .5f;
	public float durationBetweenTwoHit = 1f;
	public float durationBetweenTwoShoot = 1f;

	private float lastAttackTime = 0;

	void Awake() {
		monster = gameObject.GetComponent<Monster>();
		CanAttack = true;
		anim = GetComponentInChildren<Animator>();
	}

	void Start() {
		EquipDefault();
	}

	void Update() {
		if (weapon) {
			if (weapon.rotate && monster.playerTarget != null) {
				// Calcule de la rotation de l'arme
				Vector2 aim = monster.playerTarget.transform.position - monster.transform.position;
				direction = (aim == Vector2.zero) ? direction : aim;

				rotation = Vector2.Angle(direction, -Vector2.up);
				if (direction.x < 0f)
					rotation = 360f - rotation;
				
				Vector3 position = weaponHolder.transform.localPosition;
				float vertical = (weapon.invertedDepth) ? -direction.y : direction.y;
				if (vertical > 0f) { position.z = 0f; } else { position.z = -0.3f; }

				// Applique la rotation et position de l'arme
				weaponHolder.transform.localPosition = position;
				weaponHolder.transform.eulerAngles = new Vector3(0f, 0f, rotation);
			}

			// Gestion des attaques
			// only sword
			if (monster.fightMean == Monster.FightMean.Hit && monster.playerTarget != null && ! monster.playerTarget.GetLife().IsDead() && CanAttack) {
				float distanceToTarget = Vector2.Distance((Vector2) monster.transform.position, (Vector2) monster.playerTarget.transform.position);
				if (distanceToTarget < distanceMinToAttackWithSword) {
					AttackWithWeapon();
				}
			}
		}
	}

	public void AttackWithWeapon() {
		if (Time.time - lastAttackTime >= durationBetweenTwoHit) {
			if (monster.fightMean == Monster.FightMean.Hit && weapon.GetReady() && Time.time - lastHit > .5f) {
				monster.iaToFollow.GetComponent<AutonomousVehicle2D>().CanMove = false;
				weapon.AttackWithDelay(monster, .3f, 1f);
				anim.SetTrigger("Attack");
				lastHit = Time.time;
			} else if (monster.fightMean == Monster.FightMean.Shoot && weapon.GetReady() && Time.time - lastHit > .5f) {
				weapon.Attack();
			}
		}
	}

	public void EquipDefault() {
		if (monster.fightMean == Monster.FightMean.Hit) {
			EquipWeapon(GameManager.instance.hitWeapons[Random.Range(0, GameManager.instance.hitWeapons.Length)]);
		} else if (monster.fightMean == Monster.FightMean.Shoot) {
			EquipWeapon(GameManager.instance.shootWeapons[Random.Range(0, GameManager.instance.shootWeapons.Length)]);
		}
	}

	public override Life GetLife() {
		return monster.GetLife();
	}
}
