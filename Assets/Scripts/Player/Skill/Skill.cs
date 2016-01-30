using UnityEngine;

public class Skill : MonoBehaviour {

    public Player owner;
    public float cooldown = 1f;

    private bool ready = true;

    public virtual bool Special(Vector2 direction)
    {
        if (!ready || direction == Vector2.zero) 
            return false;

        ready = false;
        Invoke("Ready", cooldown);
        return true;
    }

    public virtual void Ready()
    {
        ready = true;
    }
}
