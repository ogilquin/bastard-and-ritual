using UnityEngine;

public class ControllerKeyboard : Controller {

    private Life player;

    public ControllerKeyboard(Life player)
    {
        this.player = player;
    }

    public override bool IsFirePressed()
    {
        return ButtonPress("K1_Fire");
    }

    public override bool IsSpecialPressed()
    {
        return ButtonPress("K1_Special");
    }
	public override bool IsTrapPressed()
	{
		return ButtonPress("K1_Special");
	}


    public override Vector2 Move()
    {
        return JoystickAxis("K1_MoveHorizontal", "K1_MoveVertical");
    }

    public override Vector2 Aim()
    {
        return JoystickAxis("K1_MoveHorizontal", "K1_MoveVertical");
    }

    bool ButtonPress(string button)
    {
        return (!player.IsDead() && Input.GetButton(button));
    }

    Vector2 JoystickAxis(string axisX, string axisY)
    {
        return (!player.IsDead()) ? new Vector2(Input.GetAxis(axisX), Input.GetAxis(axisY)) : Vector2.zero;
    }
}
