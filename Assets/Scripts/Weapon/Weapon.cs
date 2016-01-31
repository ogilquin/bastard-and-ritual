using UnityEngine;
using UnitySteer2D.Behaviors;

public class Weapon : MonoBehaviour {

    public bool invertedDepth = false;
    public Life life;
    public Sprite pickItem;
    public int damage = 1;
    public float attackRate = 2f;

    public bool rotate = true;
    protected bool ready = false;

	private Monster monster;
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

	public void AttackWithDelay(Monster monster, float delay, float delayToWait) {
		if (ready) {
			this.monster = monster;
			Invoke("Attack", delay);
			Invoke("enablePlayerCanMove", delayToWait);
		}
	}

	public void enablePlayerCanMove() {
		monster.iaToFollow.GetComponent<AutonomousVehicle2D>().CanMove = true;
	}

	public virtual void Ready()
	{
		ready = true;
	}

	public bool GetReady()
	{
		return ready;
	}

	public void SetReady(bool value)
	{
		ready = value;
	}
}
