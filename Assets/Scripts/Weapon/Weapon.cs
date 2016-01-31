using UnityEngine;

public class Weapon : MonoBehaviour {

    public bool invertedDepth = false;
    public Life life;
    public Sprite pickItem;
    public int damage = 1;
    public float attackRate = 2f;

    public bool rotate = true;
    protected bool ready = false;

    protected Animator anim;

	public virtual void Awake ()
    {
        anim = gameObject.GetComponent<Animator>();
	}

    public virtual void Start()
    {
        Ready();
    }

    public virtual bool Attack()
    {
        if (!ready) 
            return false;

        ready = false;
        Invoke("Ready", attackRate);
        return true;
    }

    public virtual void Ready()
    {
        ready = true;
    }
}
