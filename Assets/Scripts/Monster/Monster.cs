using UnityEngine;
using System.Collections;

using UnitySteer2D.Behaviors;

public class Monster : MonoBehaviour {
	public enum FightMean {
		Shoot, 
		Hit
	};
	
	public GameObject MonsterSwordIAPrefab;
	public GameObject MonsterCrossBowIAPrefab;

	public FightMean fightMean = FightMean.Hit;
	public GameObject iaToFollow;

	private MonsterAttack monsterAttack;
	private Life life;
    private Room spawner;

	void Awake() {
		monsterAttack = gameObject.GetComponent<MonsterAttack>();
		life = gameObject.GetComponent<Life>();
	}

	void Start () {
	
	}
   
	void Update () {
		if (iaToFollow != null) {
			transform.position = iaToFollow.transform.position;
		}
        
        if(life.IsDead()){
            //TODO: death
            spawner.Killed(this);
            gameObject.SetActive(false);
        }
	}

	public void SetupMonster(FightMean fightMean, Room spawner) {
		this.fightMean = fightMean;
        this.spawner = spawner;

		iaToFollow = (GameObject) Instantiate(MonsterSwordIAPrefab, transform.position, Quaternion.identity);
	}

	public Life GetLife(){
		return life;
	}
}
