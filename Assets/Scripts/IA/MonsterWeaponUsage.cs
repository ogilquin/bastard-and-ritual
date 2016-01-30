using UnityEngine;
using System.Collections;
using UnitySteer2D.Behaviors;

public class MonsterWeaponUsage : MonoBehaviour {
	private SteerForPursuit2D pursuitSteering;
	private SteerForEvasion2D evasionSteering;

	public GameObject currentTarget;

	void Start () {
		pursuitSteering = (SteerForPursuit2D) gameObject.GetComponent<SteerForPursuit2D>();
		evasionSteering = (SteerForEvasion2D) gameObject.GetComponent<SteerForEvasion2D>();
	}
	
	void Update () {
		bool pursuing = pursuitSteering != null ? pursuitSteering.cachedForce != Vector2.zero : false;
		bool evading = evasionSteering != null ? evasionSteering.cachedForce != Vector2.zero : false;

		if (pursuing) {
			Debug.Log("pursue");
		} else if (evading) {
			Debug.Log("evading");
		} else {
			Debug.Log("idle");
			/*RaycastHit2D [] raycastHits = Physics2D.RaycastAll(transform.position, currentTarget.transform.position);
			foreach (RaycastHit2D hit in raycastHits) {
				ObstacleHeight obstacleHeight = (ObstacleHeight) hit.transform.gameObject.GetComponent<ObstacleHeight>();
				if (obstacleHeight != null) {

				}
			}*/
		}
	}
}
