using UnityEngine;

public class Life : MonoBehaviour {

    public int maxLife;
    public UILife lifeUI;
    private int life;
    private bool dead = false;
    private Animator anim;
    
    void Awake()
    {
        if(anim == null)
            anim = GetComponentInChildren<Animator>();
    }


    public void Damage(int damage)
    {
        life -= damage;
        
        if(anim)
            anim.SetTrigger("Hit");

        if (lifeUI)
            lifeUI.ShowLife(life, maxLife);

        if (!dead && life <= 0)
        {
            dead = true;
        }
    }
    
    public bool IsDead(){
        return dead;
    }
}
