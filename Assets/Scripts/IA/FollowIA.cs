using UnityEngine;
using System.Collections;

public class FollowIA : MonoBehaviour {
	public Transform toFollow;

	void Update () {
		if (toFollow != null) {
			transform.position = toFollow.position;
		}
	}
}
