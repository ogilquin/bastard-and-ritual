using UnityEngine;
using System.Collections;
using UnitySteer2D.Behaviors;

public class MonsterShoot : MonoBehaviour {
	private SteerForPursuit2D pursuitSteering;
	private SteerForEvasion2D evasionSteering;

	private Monster monster;
	private MonsterAttack monsterAttack;

	void Awake() {
		monster = GetComponent<Monster>();
		monsterAttack = GetComponent<MonsterAttack>();
	}

	void Start () {
		
		pursuitSteering = (SteerForPursuit2D) monster.iaToFollow.GetComponent<SteerForPursuit2D>();
		evasionSteering = (SteerForEvasion2D) monster.iaToFollow.GetComponent<SteerForEvasion2D>();
	}
	
	void Update () {
		bool pursuing = pursuitSteering != null ? pursuitSteering.cachedForce != Vector2.zero : false;
		bool evading = evasionSteering != null ? evasionSteering.cachedForce != Vector2.zero : false;

		if (monster.playerTarget != null) {
			if (pursuing) {
				// pursuing
			} else if (evading) {
				// evading
			} else {
				if (true) {
					if (canSightThrough ()) {
						monsterAttack.AttackWithWeapon();
					} else {
						// can't sight
					}
				}
			}
			
		} else {
			// nothing to do
		}
	}

	bool canSightThrough() {
		RaycastHit2D [] raycastHits = Physics2D.RaycastAll(transform.position, monster.playerTarget.transform.position);
		foreach (RaycastHit2D hit in raycastHits) {
			ObstacleHeight obstacleHeight = (ObstacleHeight) hit.transform.gameObject.GetComponent<ObstacleHeight>();
			if (obstacleHeight != null) {
				if (! obstacleHeight.canSightThrough) {
					return false;
				}
			}
		}

		return true;
	}
}
