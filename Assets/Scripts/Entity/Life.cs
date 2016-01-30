using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

    public int maxLife;
    public Image bar;
    private int life;
    private bool dead = false;
    private Animator anim;
    
    void Awake()
    {
        life = maxLife;
        if(anim == null)
            anim = GetComponentInChildren<Animator>();
            
            ShowLife();
    }


    public void Damage(int damage)
    {
        life -= damage;
        
        if(anim)
            anim.SetTrigger("Hit");

        ShowLife();

        if (!dead && life <= 0)
        {
            dead = true;
        }
    }
    
    public bool IsDead(){
        return dead;
    }
    
    public void ShowLife()
    {
        this.bar.fillAmount = 1f / (float)maxLife * (float)life;
    }
}
