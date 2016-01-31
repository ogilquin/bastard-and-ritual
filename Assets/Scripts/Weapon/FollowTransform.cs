using UnityEngine;

public class FollowTransform : MonoBehaviour {

	public Transform target;
    public bool followPosition = true;
    public bool followRotation = false;

	void Update ()
    {
        if(followPosition)
            transform.position = target.transform.position;

        if(followRotation)
            transform.rotation = target.transform.rotation;
	}
}
