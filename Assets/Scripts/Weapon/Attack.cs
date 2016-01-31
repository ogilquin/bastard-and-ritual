using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	public GameObject weaponHolder;

	protected Weapon weapon;
	[HideInInspector]
	public bool CanAttack = true;

	void Awake() {
	}

	void Start () {
	}
	
	void Update () {
	}

	public void EquipWeapon(Weapon _weapon) {
		if (weapon)
			Destroy(weapon.gameObject);

		weapon = Instantiate(_weapon) as Weapon;
		weapon.transform.parent = weaponHolder.transform;
		weapon.transform.localPosition = Vector3.zero;
		weapon.transform.localRotation = new Quaternion();
		weapon.life = GetLife();
	}

	public Weapon GetWeapon() {
		return weapon;
	}

	public virtual Life GetLife() {
		return null;
	}
}
