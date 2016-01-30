using UnityEngine;
using XInputDotNetPure;

public class ControllerGamepad : Controller {

    Life player;
    PlayerIndex index;

    public ControllerGamepad(Life player, PlayerIndex playerIndex)
    {
        Debug.Log("Gamepad");
        this.player = player;
        this.index = playerIndex;
    }

    public override bool IsFirePressed()
    {
        return (!player.IsDead() && (GamePad.GetState(index).Buttons.X == ButtonState.Pressed));
    }

    public override bool IsSpecialPressed()
    {
        return (!player.IsDead() && (GamePad.GetState(index).Buttons.A == ButtonState.Pressed));
    }

    public override Vector2 Move()
    {
        GamePadState state = GamePad.GetState(index);
        return (!player.IsDead()) ? new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y) : Vector2.zero;
    }

    public override Vector2 Aim()
    {
        GamePadState state = GamePad.GetState(index);
        return (!player.IsDead()) ? new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y) : Vector2.zero;
    }
}
