using UnityEngine;

public class PositionZ : MonoBehaviour {
    public float height = 0f;
    void Start()
    {
        Place();
    }
    
    void LateUpdate()
    {
        #if (UNITY_EDITOR)
        Place();
        #endif
    }
    
    public void Place()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y - height);
    }
}
