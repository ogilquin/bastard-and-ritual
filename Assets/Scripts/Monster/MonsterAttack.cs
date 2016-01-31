using UnityEngine;
using System.Collections;

public class MonsterAttack : Attack {
	private Vector2 direction = new Vector2(1f, 1f);
	private float rotation = 0f;
	private Monster monster;

	public float distanceMinToAttackWithSword = .5f;
	public float durationBetweenTwoHit = 1f;
	public float durationBetweenTwoShoot = 1f;

	private float lastAttackTime = 0;

	void Awake() {
		monster = gameObject.GetComponent<Monster>();
		CanAttack = true;
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
			if (monster.fightMean == Monster.FightMean.Hit && monster.playerTarget != null && ! monster.playerTarget.GetLife().IsDead() && CanAttack) {
				float distanceToTarget = Vector2.Distance((Vector2) monster.transform.position, (Vector2) monster.playerTarget.transform.position);
				if (distanceToTarget < distanceMinToAttackWithSword) {
					if (Time.time - lastAttackTime >= durationBetweenTwoHit) {
						weapon.Attack();
						lastAttackTime = Time.time;
					}
				}
			}
		}
	}

	public void EquipDefault() {
		EquipWeapon(GameManager.instance.weapons[Random.Range(0, GameManager.instance.weapons.Length)]);
	}

	public override Life GetLife() {
		return monster.GetLife();
	}
}
