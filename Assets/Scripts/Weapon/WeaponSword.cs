using UnityEngine;

public class WeaponSword : Weapon {

    private bool attack = false;

    public override bool Attack()
    {
        if (!base.Attack())
            return false;

        rotate = false;
        attack = !attack;
        
        GameManager.instance.cam.ShakeCamera(0.3f, 7f, Vector2.zero);
        anim.SetTrigger("Attack");
        return true;
    }

    public void Rotate()
    {
        rotate = true;
    }
}