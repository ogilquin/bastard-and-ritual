using UnityEngine;

public class TakeDamage : MonoBehaviour {
	public static float damageForceScale = 3;
	public Life life;

	GameObject item;
	Player player;
	Monster monster;
	private Vector2 vectorDamageScale;

	void Awake() {
		vectorDamageScale = new Vector2(-damageForceScale, -damageForceScale);
	}

	public void Setup(GameObject item) {
		player = (Player) gameObject.GetComponentInParent<Player>();
		monster = (Monster) gameObject.GetComponentInParent<Monster>();

		this.item = item;
		if (monster != null) {
			this.item = item.GetComponent<Monster>().iaToFollow;
		}
	}

    public void Damage(int damage, GameObject damager) {
		Vector2 damageForce = damager.transform.position - transform.position;
		damageForce.Normalize();
		damageForce.Scale(vectorDamageScale);
		Debug.Log(damageForce);

		if (monster != null) {
			Vector2 pos = item.transform.position;
			pos += damageForce;
			item.transform.position = pos;
		} else if (player != null) {
			Rigidbody2D rigidBody = item.GetComponent<Rigidbody2D>();
			rigidBody.AddForce(damageForce, ForceMode2D.Impulse);
			Vector2 pos = rigidBody.position;
			pos += damageForce;
			rigidBody.MovePosition(pos);
			//rigidBody.MovePosition(Vector2.Lerp(rigidBody.position, pos, ));

		}
		

		life.Damage(damage);
    }
}
