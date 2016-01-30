using UnityEngine;
using System.Collections;
using UnitySteer2D.Behaviors;

public class MonsterWeaponUsage : MonoBehaviour {
	private SteerForPursuit2D pursuitSteering;
	private SteerForEvasion2D evasionSteering;

	public GameObject currentTarget;

	void Awake() {
		pursuitSteering = (SteerForPursuit2D) gameObject.GetComponent<SteerForPursuit2D>();
		evasionSteering = (SteerForEvasion2D) gameObject.GetComponent<SteerForEvasion2D>();
	}

	void Start () {
	}
	
	void Update () {
		bool pursuing = pursuitSteering != null ? pursuitSteering.cachedForce != Vector2.zero : false;
		bool evading = evasionSteering != null ? evasionSteering.cachedForce != Vector2.zero : false;

		if (currentTarget != null) {
			if (pursuing) {
				Debug.Log("pursue");

				if (false) {
					Debug.Log("sword");
				}
			} else if (evading) {
				Debug.Log("evading");
			} else {
				if (true) {
					if (canSightThrough ()) {
						Debug.Log("shooting");
					} else {
						Debug.Log("can't sight");
					}
				}
			}
			
		} else {
			Debug.Log("nothing to do");
		}
	}

	bool canSightThrough() {
		RaycastHit2D [] raycastHits = Physics2D.RaycastAll(transform.position, currentTarget.transform.position);
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
