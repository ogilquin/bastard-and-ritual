using UnityEngine;
using System.Collections;

public class PlayerAttack : Attack {
    private Skill skill;

    private Vector2 direction = new Vector2(1f, 1f);
    private float rotation = 0f;
    private Player player;

    void Awake()
    {
        player = gameObject.GetComponent<Player>();
		CanAttack = true;
    }

	void Start()
    {
        EquipDefault();
	}

	void LateUpdate()
    {
        if (weapon)
        {
            if (weapon.rotate)
            {
                // Calcule de la rotation de l'arme
                Vector2 aim = player.GetController().Aim();
                direction = (aim == Vector2.zero) ? direction : aim;

                rotation = Vector2.Angle(direction, -Vector2.up);

                Vector3 position = weaponHolder.transform.localPosition;
                weaponHolder.transform.localPosition = position;
                weaponHolder.transform.eulerAngles = new Vector3(0f, 0f, rotation);
            }

            // Gestion des attaques
			if (player.GetController().IsFirePressed() && CanAttack)
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

    public void EquipSkill(Skill _skill)
    {
        if (skill)
            Destroy(skill.gameObject);

        skill = Instantiate(_skill);
        skill.transform.parent = player.transform;
        skill.transform.localPosition = Vector3.zero;
        skill.transform.localRotation = new Quaternion();
        skill.owner = player;
    }

    public void EquipDefault()
    {
        EquipWeapon(GameManager.instance.weapons[Random.Range(0, GameManager.instance.weapons.Length)]);
        EquipSkill(GameManager.instance.skills[Random.Range(0, GameManager.instance.skills.Length)]);
    }

    public Skill GetSkill()
    {
        return skill;
    }

	public override Life GetLife() {
		return player.GetLife();
	}
}
