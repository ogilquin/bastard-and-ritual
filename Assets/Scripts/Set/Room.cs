<<<<<<< HEAD
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Room : MonoBehaviour {
    public int height = 6;
    public int width = 6;
    
	public int minHittingMonsters = 1;
    public int maxHittingMonster = 5;
	public int minShootingMonsters = 0;
    public int maxShootingMonsters = 2;
    
    public BoxCollider2D wallTop;
    public BoxCollider2D wallBottom;
    public BoxCollider2D wallLeft;
    public BoxCollider2D wallRight;
    
    public Door doorTop;
    public Door doorBottom;
    public Door doorLeft;
    public Door doorRight;
    
    private List<Monster> monsters = new List<Monster>();
    private bool spawned = false;
    
    public void Enter(Door door)
    {
        Debug.Log("Enter room");
        gameObject.SetActive(true);
		int i = 0;
		foreach (Player player in GameManager.instance.players) {
			if (!player.GetLife().IsDead()) {
				Vector2 pos = door.transform.position;
				pos.x += -2 + i * 1;
				player.transform.position = pos;
				i++;
			}
		}
                
        SpawnMonstersOnce();
    }
    
    public void Exit()
    {
        gameObject.SetActive(false);
    }
    
    void SpawnMonstersOnce()
    {
        if(spawned)
            return;

        CloseDoors();
		Invoke("SpawnMonsters", 2f);
    }

	void SpawnMonsters()
    {
        int numHittingMonsters = Random.Range(minHittingMonsters, maxHittingMonster);
        int numShootingMonsters = Random.Range(minShootingMonsters, maxShootingMonsters);
        
        float halfWidth = ((float)width)/2f - 2f;
        float halfHeight = ((float)height)/2f - 2f;
        
		for (int i = 0; i < numHittingMonsters; i ++) {
			Vector2 position = new Vector2 (transform.position.x + Random.Range (-halfWidth, halfWidth), transform.position.y + Random.Range (-halfHeight, halfHeight - 1f));
			Monster monster = Instantiate(GameManager.instance.monsterSwordPrefab, position, Quaternion.identity) as Monster;
			monster.SetupMonster(Monster.FightMean.Hit, this);
			monsters.Add(monster);
		}

		for (int i = 0; i < numShootingMonsters; i ++) {
			Vector2 position = new Vector2 (transform.position.x + Random.Range (-halfWidth, halfWidth), transform.position.y + Random.Range (-halfHeight, halfHeight - 1f));
			Monster monster = Instantiate(GameManager.instance.monsterCrossBowPrefab, position, Quaternion.identity) as Monster;
			monster.SetupMonster(Monster.FightMean.Shoot, this);
			monsters.Add(monster);
		}
        
        spawned = true;
        
        OpenDoors();
	}
    
    void CloseDoors()
    {
        if(doorTop.gameObject.activeSelf) doorTop.Close();
        if(doorBottom.gameObject.activeSelf) doorBottom.Close();
        if(doorLeft.gameObject.activeSelf) doorLeft.Close();
        if(doorRight.gameObject.activeSelf) doorRight.Close();
    }
    
    void OpenDoors()
    {
        if(monsters.Count > 0)
            return;
            
        if(doorTop.gameObject.activeSelf) doorTop.Open();
        if(doorBottom.gameObject.activeSelf) doorBottom.Open();
        if(doorLeft.gameObject.activeSelf) doorLeft.Open();
        if(doorRight.gameObject.activeSelf) doorRight.Open();
    }
    
    public void Killed(Monster monster)
    {
        monsters.Remove(monster);
        OpenDoors();
    }
    
    void Update()
    {
        #if (UNITY_EDITOR)
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            
            if(wallTop){
                wallTop.size = new Vector2((float) width, 1f);
                wallTop.transform.localPosition = new Vector3(0f, ((float)height)/2f, 0f);
            }
            if(wallBottom){
                wallBottom.size = new Vector2((float) width, 1f);
                wallBottom.transform.localPosition = new Vector3(0f, (-(float)height)/2f, 0f);
            }
            if(wallLeft){
                wallLeft.size = new Vector2(1f, (float) height);
                wallLeft.transform.localPosition = new Vector3((-(float)width)/2f, 0f, 0f);
            }
            if(wallRight){
                wallRight.size = new Vector2(1f, (float) height);
                wallRight.transform.localPosition = new Vector3(((float)width)/2f, 0f, 0f);
            }
            
            if(doorTop){
                doorTop.transform.localPosition = new Vector3(0f, ((float)height)/2f - 1f, ((float)height)/2f - 1f);
            }
            if(doorBottom){
                doorBottom.transform.localPosition = new Vector3(0f, (-(float)height)/2f + 0.5f, (-(float)height)/2f);
            }
            if(doorLeft){
                doorLeft.transform.localPosition = new Vector3((-(float)width)/2f + 0.5f, 0f, 0f);
            }
            if(doorRight){
                doorRight.transform.localPosition = new Vector3(((float)width)/2f - 0.5f, 0f, 0f);
            }
        #endif
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }
}
=======
﻿using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Room : MonoBehaviour {
    public int height = 6;
    public int width = 6;
    
	public int minHittingMonsters = 1;
    public int maxHittingMonster = 5;
	public int minShootingMonsters = 0;
    public int maxShootingMonsters = 2;
    
    public BoxCollider2D wallTop;
    public BoxCollider2D wallBottom;
    public BoxCollider2D wallLeft;
    public BoxCollider2D wallRight;
    
    public Door doorTop;
    public Door doorBottom;
    public Door doorLeft;
    public Door doorRight;
    
    private List<Monster> monsters = new List<Monster>();
    private bool spawned = false;
    
    public void Enter(Door door)
    {
        Debug.Log("Enter room");
        gameObject.SetActive(true);

		foreach (Player player in GameManager.instance.players) {
			if (!player.GetLife().IsDead()) {
				player.transform.position = door.spawnPoint.transform.position;
            }
        }
        
        GameManager.instance.cam.FocusRoom(this);
        if(maxHittingMonster > 0 || maxShootingMonsters > 0)      
            SpawnMonstersOnce();
    }
    
    public void Exit()
    {
        gameObject.SetActive(false);
    }
    
    void SpawnMonstersOnce()
    {
        if(spawned)
            return;

        CloseDoors();
        Invoke("SpawnMonsters", 2f);
    }

	void SpawnMonsters()
    {
        int numHittingMonsters = Random.Range(minHittingMonsters, maxHittingMonster);
        int numShootingMonsters = Random.Range(minShootingMonsters, maxShootingMonsters);
        
        float halfWidth = ((float)width)/2f - 2f;
        float halfHeight = ((float)height)/2f - 2f;
        
		for (int i = 0; i < numHittingMonsters; i ++) {
			Vector2 position = new Vector2 (transform.position.x + Random.Range (-halfWidth, halfWidth), transform.position.y + Random.Range (-halfHeight, halfHeight - 1f));
			Monster monster = Instantiate(GameManager.instance.monsterSwordPrefab, position, Quaternion.identity) as Monster;
			monster.SetupMonster(Monster.FightMean.Hit, this);
			monsters.Add(monster);
		}

		for (int i = 0; i < numShootingMonsters; i ++) {
			Vector2 position = new Vector2 (transform.position.x + Random.Range (-halfWidth, halfWidth), transform.position.y + Random.Range (-halfHeight, halfHeight - 1f));
			Monster monster = Instantiate(GameManager.instance.monsterCrossBowPrefab, position, Quaternion.identity) as Monster;
			monster.SetupMonster(Monster.FightMean.Shoot, this);
			monsters.Add(monster);
		}
        
        spawned = true;
        
        OpenDoors();
	}
    
    void CloseDoors()
    {
        if(doorTop.gameObject.activeSelf) doorTop.Close();
        if(doorBottom.gameObject.activeSelf) doorBottom.Close();
        if(doorLeft.gameObject.activeSelf) doorLeft.Close();
        if(doorRight.gameObject.activeSelf) doorRight.Close();
    }
    
    void OpenDoors()
    {
        if(monsters.Count > 0)
            return;
            
        if(doorTop.gameObject.activeSelf) doorTop.Open();
        if(doorBottom.gameObject.activeSelf) doorBottom.Open();
        if(doorLeft.gameObject.activeSelf) doorLeft.Open();
        if(doorRight.gameObject.activeSelf) doorRight.Open();
    }
    
    public void Killed(Monster monster)
    {
        monsters.Remove(monster);
        OpenDoors();
    }
    
    void Update()
    {
        #if (UNITY_EDITOR)
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            
            if(wallTop){
                wallTop.size = new Vector2((float) width, 1f);
                wallTop.transform.localPosition = new Vector3(0f, ((float)height)/2f, 0f);
            }
            if(wallBottom){
                wallBottom.size = new Vector2((float) width, 1f);
                wallBottom.transform.localPosition = new Vector3(0f, (-(float)height)/2f, 0f);
            }
            if(wallLeft){
                wallLeft.size = new Vector2(1f, (float) height);
                wallLeft.transform.localPosition = new Vector3((-(float)width)/2f, 0f, 0f);
            }
            if(wallRight){
                wallRight.size = new Vector2(1f, (float) height);
                wallRight.transform.localPosition = new Vector3(((float)width)/2f, 0f, 0f);
            }
            
            if(doorTop){
                doorTop.transform.localPosition = new Vector3(0f, ((float)height)/2f - 1f, ((float)height)/2f - 1f);
            }
            if(doorBottom){
                doorBottom.transform.localPosition = new Vector3(0f, (-(float)height)/2f + 0.5f, (-(float)height)/2f);
            }
            if(doorLeft){
                doorLeft.transform.localPosition = new Vector3((-(float)width)/2f + 0.5f, 0f, 0f);
            }
            if(doorRight){
                doorRight.transform.localPosition = new Vector3(((float)width)/2f - 0.5f, 0f, 0f);
            }
        #endif
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }
}