using UnityEngine;
using System.Collections;

public class PositionZ : MonoBehaviour {
    void Start()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Debug.Log(newPos);
        transform.position = newPos;
    }
    
    void Update()
    {
        
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Debug.Log(newPos);
        transform.position = newPos;
    }
}
