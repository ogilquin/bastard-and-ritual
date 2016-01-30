using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<UIManager>();
            return _instance;
        }
    }

    private bool shaking;
    private float intensity;
    private float reduction;
    private Vector2 direction;

    new public CameraFollow camera;
    public Animator anim;

    public void Update()
    {
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

    public void Shake(float intensity, float reduction, Vector2 direction)
    {
        this.intensity = intensity;
        this.reduction = reduction;
        this.direction = direction;
        shaking = true;
    }

    public void SetMenu(State state)
    {
        anim.SetBool("Loading", false);
        anim.SetBool("SelectPlayer", false);
        anim.SetBool("InGame", false);

        switch (state)
        {
            case State.PlayerSelection:
                anim.SetBool("SelectPlayer", true);
                break;

            case State.Generation:
                anim.SetBool("Loading", true);
                break;

            case State.Play:
                anim.SetBool("InGame", true);
                break;
        }
    }
}