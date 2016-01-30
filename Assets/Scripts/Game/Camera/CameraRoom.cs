using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraRoom : MonoBehaviour {

    public float moveSpeed = 2f;
    public float zoomSpeed = 2f;
    public float minZoom = 4f;
    public float maxZoom = 10f;
    public float minZoomDistance = 5f;
    public float maxZoomDistance = 30f;

    [Range(0f, 1f)]
    public float zoom = 0.75f;
    public Vector3 followOffset = Vector3.zero;
    public List<GameObject> followTargets = new List<GameObject>();

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
        int length = followTargets.Count;

        if (length > 0)
        {
            Vector2 c = new Vector2(0f, 0f);
            float far = 0f;

            foreach (GameObject u in followTargets)
            {
                Vector2 current = new Vector2(u.transform.position.x, u.transform.position.y);
                c += current;
                float d = Vector2.Distance(current, new Vector2(transform.position.x, transform.position.y));
                if (d > far) far = d;
            }

            zoom = Mathf.Clamp01((far - minZoomDistance) / (maxZoomDistance - minZoomDistance));

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom * (maxZoom - minZoom) + minZoom, zoomSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(c.x / length, c.y / length, transform.position.z) + followOffset, moveSpeed * Time.deltaTime);
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

    public void AddFollowTarget(GameObject target)
    {
        if(!followTargets.Contains(target))
            followTargets.Add(target);
    }

    public void RemoveFollowTarget(GameObject target)
    {
        followTargets.Remove(target);
    }

    public List<GameObject> GetFollowTarget()
    {
        return followTargets;
    }
}
