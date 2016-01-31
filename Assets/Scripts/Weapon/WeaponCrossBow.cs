using UnityEngine;

public class WeaponCrossBow : Weapon {
	private bool attack = false;

	public GameObject bulletSpawn;
	public Bullet bullet;

	public float firePower = 3f;

	public override bool Attack() 
	{
		if (!base.Attack())
			return false;
		
		rotate = false;
		attack = !attack;

		GameManager.instance.cam.ShakeCamera(0.3f, 7f, Vector2.zero);
		anim.SetTrigger("Attack");

		SpawnBullet();

		return true;
	}

	public void SpawnBullet() {
		if (bullet != null) {
			Vector3 directionEuler = transform.parent.eulerAngles;
			directionEuler.z += 180;

			Vector3 orientEuler = transform.parent.eulerAngles;
			orientEuler.z += 270;

			Quaternion directionRotation = Quaternion.Euler(directionEuler);
			Quaternion orientationRotation = Quaternion.Euler(orientEuler);

			Bullet b = (Bullet) Instantiate(bullet, bulletSpawn.transform.position, orientationRotation);

			Vector3 direction = directionRotation * Vector3.up;
			direction.Normalize();

			b.transform.SetParent(GameManager.instance.transform);
			b.Launch(new Vector2(direction.x, direction.y), firePower, this);//, owner);
		}
	}

	public void Rotate() {
		rotate = true;
	}
}