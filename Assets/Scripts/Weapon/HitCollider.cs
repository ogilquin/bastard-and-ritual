using UnityEngine;

public class HitCollider : MonoBehaviour {
    public Weapon weapon;

    public void OnTriggerEnter2D(Collider2D collider) {
        TakeDamage other = collider.gameObject.GetComponent<TakeDamage>();
		if (other != null && other.life != weapon.life) {
			other.Damage(weapon.damage, gameObject);
		}
    }
}
