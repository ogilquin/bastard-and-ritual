using UnityEngine;

public class TakeDamage : MonoBehaviour {

    public Life life;

    public void Damage(int damage)
    {
        life.Damage(damage);
    }
}
