using UnityEngine;
using System.Collections;
using UnitySteer2D.Behaviors;

public class MonsterStateMachine : MonoBehaviour {
	private SteerForPursuit2D pursuitSteering;
	private SteerForEvasion2D evasionSteering;

	private Monster monster;
	private MonsterAttack monsterAttack;

	private Animator anim;
	public GameObject model;

	void Awake() {
		monster = GetComponent<Monster>();
		monsterAttack = GetComponent<MonsterAttack>();

		anim = GetComponentInChildren<Animator>();
	}

	void Start () {
		pursuitSteering = (SteerForPursuit2D) monster.iaToFollow.GetComponent<SteerForPursuit2D>();
		evasionSteering = (SteerForEvasion2D) monster.iaToFollow.GetComponent<SteerForEvasion2D>();
	}
	
	void Update () {
		bool pursuing = pursuitSteering != null ? pursuitSteering.cachedForce != Vector2.zero : false;
		bool evading = evasionSteering != null ? evasionSteering.cachedForce != Vector2.zero : false;

		if (monster.playerTarget != null) {
			if (pursuing && pursuitSteering.Vehicle.CanMove) {
				// pursuing
				anim.SetBool("Run", true);

				int sign = Vector2.Angle(pursuitSteering.cachedForce, Vector2.left) > 90 ? 1 : -1;
				model.transform.localScale = new Vector3(Mathf.Sign(sign), 1f, 1f);
			} else if (evading && evasionSteering.Vehicle.CanMove) {
				// evading
				anim.SetBool("Run", true);

				int sign = Vector2.Angle(evasionSteering.cachedForce, Vector2.left) > 90 ? 1 : -1;
				model.transform.localScale = new Vector3(Mathf.Sign(sign), 1f, 1f);

			} else {
				if (monster.fightMean == Monster.FightMean.Shoot) {
					if (canSightThrough()) {
						monsterAttack.AttackWithWeapon();
						anim.SetBool("Run", false);
					} else {
						// can't sight
						anim.SetBool("Run", false);
					}
				} else {
					anim.SetBool("Run", false);
				}
			}
		} else {
			// nothing to do
			anim.SetBool("Run", false);
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
