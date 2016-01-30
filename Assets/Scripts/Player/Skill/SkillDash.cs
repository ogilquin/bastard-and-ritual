using UnityEngine;
using System.Collections;

public class SkillDash : Skill {

    public float distance = 10f;

    protected PlayerMovement playerMovement;

    PlayerMovement GetPlayerMovement()
    {
        if (!playerMovement) {
            playerMovement = owner.GetComponent<PlayerMovement>();
        }

        return playerMovement;
    }
    
    public override bool Special(Vector2 direction)
    {
        if (!base.Special(direction)) 
            return false;

        GetPlayerMovement().Dash(direction * distance);
        return true;
    }
}
