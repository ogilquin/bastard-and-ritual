﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private int number = 0;
    private Controller controller;
    private PlayerAttack playerAttack;
    private Life life;
    private Sprite bras;

	[HideInInspector]
	public int numMonstersAttacking = 0;

	[HideInInspector]
	public bool isTraitor = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerAttack = gameObject.GetComponent<PlayerAttack>();
        life = gameObject.GetComponent<Life>();
    }

    public void SetupPlayer(PlayerList playerList)
    {
        this.number = playerList.number;
        
        if(playerList.type == ControllerType.Gamepad){
            this.controller = new ControllerGamepad(life, playerList.controller);
        } else if(playerList.type == ControllerType.Keyboard) {
            this.controller = new ControllerKeyboard(life);
        }
        
        Skin skin = gameObject.GetComponentInChildren<Skin>() as Skin;
        if(skin) skin.Skining(this.number);

		(GetComponentInChildren<TakeDamage>() as TakeDamage).Setup(gameObject);
    }

    public int GetNumber()
    {
        return number;
    }

    public Controller GetController()
    {
        return controller;
    }

    public void EquipWeapon(Weapon weapon)
    {
        playerAttack.EquipWeapon(weapon);
    }

    public void EquipSkill(Skill skill)
    {
        playerAttack.EquipSkill(skill);
    }
    
    public Life GetLife(){
        return life;
    }
}
