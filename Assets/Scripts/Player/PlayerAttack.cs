﻿using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
    public GameObject weaponHolder;

    private Weapon weapon;
    private Skill skill;

    private Vector2 direction = new Vector2(1f, 1f);
    private float rotation = 0f;
    private Player player;

    void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }

	void Start()
    {
        EquipDefault();
	}

	void Update()
    {
        if (weapon)
        {
            if (weapon.rotate)
            {
                // Calcule de la rotation de l'arme
                Vector2 aim = player.GetController().Aim();
                direction = (aim == Vector2.zero) ? direction : aim;

                rotation = Vector2.Angle(direction, -Vector2.up);
                if (direction.x < 0f)
                    rotation = 360f - rotation;

                Vector3 position = weaponHolder.transform.localPosition;
                float vertical = (weapon.invertedDepth) ? -direction.y : direction.y;
                if (vertical > 0f) { position.z = 0f; } else { position.z = -0.3f; }

                // Applique la rotation et position de l'arme
                weaponHolder.transform.localPosition = position;
                weaponHolder.transform.eulerAngles = new Vector3(0f, 0f, rotation);
            }

            // Gestion des attaques
            if (player.GetController().IsFirePressed())
            {
                weapon.Attack();
            }
        }

        if (skill)
        {
            if (player.GetController().IsSpecialPressed())
            {
                skill.Special(player.GetController().Move().normalized);
            }
        }
        
	}

    public void EquipWeapon(Weapon _weapon)
    {
        if (weapon)
            Destroy(weapon.gameObject);

        weapon = Instantiate(_weapon) as Weapon;
        weapon.transform.parent = weaponHolder.transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = new Quaternion();
        weapon.life = player.GetLife();
    }

    public void EquipSkill(Skill _skill)
    {
        if (skill)
            Destroy(skill.gameObject);

        skill = Instantiate(_skill);
        skill.transform.parent = player.transform;
        skill.transform.localPosition = Vector3.zero;
        skill.transform.localRotation = new Quaternion();
    }

    public void EquipDefault()
    {
        EquipWeapon(GameManager.instance.weapons[Random.Range(0, GameManager.instance.weapons.Length)]);
        //EquipSkill(GameManager.instance.skills[Random.Range(0, GameManager.instance.skills.Length)]);
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public Skill GetSkill()
    {
        return skill;
    }
}
