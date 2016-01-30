using UnityEngine;
using System.Collections;
using XInputDotNetPure;

[System.Serializable]
public class PlayerList
{
    public int number;
    public PlayerIndex controller;
    public ControllerType type;

    public PlayerList(int number, PlayerIndex controller, ControllerType type)
    {
        this.number = number;
        this.controller = controller;
        this.type = type;
    }
}
