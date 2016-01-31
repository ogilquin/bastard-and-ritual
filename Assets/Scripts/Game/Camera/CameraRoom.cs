using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraRoom : MonoBehaviour {

    public float moveSpeed = 2f;
    public float zoomSpeed = 2f;
    
    public Room room;

    // Shaking !
    private bool shaking;
    private float intensity;
    private float reduction;
    private Vector2 direction;

    private Camera cam;

    void Awake()
    {
        cam = gameObject.GetComponent<Camera>() as Camera;
    }

    void LateUpdate()
    {   
        if(room)
        {
            Vector3 pos = new Vector3(room.transform.position.x, room.transform.position.y, room.transform.position.y - 500f);
            transform.position = Vector3.Lerp(transform.position, pos, moveSpeed * Time.deltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, (float)room.height / 2f, zoomSpeed * Time.deltaTime);
        }

        if (shaking)
        {
            if (intensity < 0.1f)
            {
                Reset();
            }
            else
            {
                if (direction == Vector2.zero)
                {
                    transform.position += new Vector3(Random.value * intensity - intensity / 2, Random.value * intensity - intensity / 2, 0f);
                }
                else
                {
                    Vector2 shake = new Vector2(Random.value * intensity - intensity / 2, Random.value * intensity - intensity / 2) + direction;
                    transform.position += new Vector3(shake.x, shake.y, 0f);
                }
                intensity = (0 - intensity) * (reduction * Time.deltaTime) + intensity;
            }
        }
    }

    void Reset()
    {
        shaking = false;
        intensity = 0f; ;
        reduction = 0f; ;
        direction = new Vector2();
    }

    public void ShakeCamera(float intensity, float reduction, Vector2 direction)
    {
        this.intensity = intensity;
        this.reduction = reduction;
        this.direction = direction;
        shaking = true;
    }

    public void FocusRoom(Room room)
    {
        this.room = room;
    }
}
