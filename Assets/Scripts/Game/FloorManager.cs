using UnityEngine;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour
{
	public uint numHittingMonsters = 0;
	public uint numShootingMonsters = 0;

	public List<Monster> monsters = new List<Monster>();
	public Monster MonsterSwordPrefab;
	public Monster MonsterCrossBowPrefab;

    void Awake()
	{
		SpawnMonsters();
    }

    void Start()
    {
    }
    
    
    void Update()
    {

    }

	void SpawnMonsters() {
		for (int i = 0; i < numHittingMonsters; i ++) {
			Vector2 position = new Vector2 (Random.Range (5, 8), Random.Range (-2, 2));
			Monster monster = Instantiate(MonsterSwordPrefab, position, Quaternion.identity) as Monster;
			monster.SetupMonster(Monster.FightMean.Hit);
			monsters.Add(monster);
		}

		for (int i = 0; i < numShootingMonsters; i ++) {
			Vector2 position = new Vector2 (Random.Range (5, 8), Random.Range (-2, 2));
			Monster monster = Instantiate(MonsterCrossBowPrefab, position, Quaternion.identity) as Monster;
			monster.SetupMonster(Monster.FightMean.Shoot);
			monsters.Add(monster);
		}
	}
}
