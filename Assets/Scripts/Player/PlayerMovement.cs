using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public GameObject model;

    private Animator anim;
    private Player player;
    private Rigidbody2D rigidbody2d;

    public float speed = 1f;
    //public float accel = 6f;

    private Vector2 input = Vector2.zero;
    private Vector2 dash = Vector2.zero;
    private bool dashed = false;

	[HideInInspector]
	public bool CanMove = true;

    void Awake()
    {
        anim = model.GetComponent<Animator>();
        player = gameObject.GetComponent<Player>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

	void Start () {
        transform.position = Utils.PositionDepth(transform.position);
	}
	
	void Update () {
        input = player.GetController().Move();

        //Reverse player if going different direction
        if (input.x != 0)
        {
            model.transform.localScale = new Vector3(Mathf.Sign(input.x), 1f, 1f);
        }

        transform.position = Utils.PositionDepth(transform.position);
	}

    void FixedUpdate()
    {        
		if (CanMove) {
			rigidbody2d.velocity = input * speed;

			if (input == Vector2.zero)
			{
				if(anim) anim.SetBool("Run", false);
			}

			else
			{
				if(anim) anim.SetBool("Run", true);
			}
			
			if (dash != Vector2.zero)
			{
                if(anim && !dashed) {
                    anim.SetTrigger("Dash");
                    dashed = true;
                }
				rigidbody2d.velocity += dash;
				dash *= 0.8f;
				if (dash.magnitude < 0.01f)
					dash = Vector2.zero;
			} else {
                if(anim) anim.SetBool("Dash", false);
                dashed = false;
            }
		}
    }

    public void Dash(Vector2 direction)
    {
        dash = direction;
    }
}
