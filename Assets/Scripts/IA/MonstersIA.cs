using UnityEngine;
using System.Collections;

using UnitySteer2D.Behaviors;

public class MonstersIA : MonoBehaviour {
	void Start () {
	}
	
	void Update () {
		// select target
		Player [] players = Component.FindObjectsOfType<Player>();
		Monster [] monsters = Component.FindObjectsOfType<Monster>();

		foreach (Monster monster in monsters) {
			PassiveVehicle2D targetPlayerVehicle = (PassiveVehicle2D) players[0].GetComponent<PassiveVehicle2D>();

			if (monster.fightMean == Monster.FightMean.Hit) {
				SteerForPursuit2D steerForPursuit = (SteerForPursuit2D) monster.iaToFollow.GetComponent<SteerForPursuit2D>();
				steerForPursuit.Quarry = targetPlayerVehicle;
			} else if (monster.fightMean == Monster.FightMean.Shoot) {
				SteerForPursuit2D steerForPursuit = (SteerForPursuit2D) monster.iaToFollow.GetComponent<SteerForPursuit2D>();
				steerForPursuit.Quarry = targetPlayerVehicle;
				SteerForEvasion2D steerForEvasion = (SteerForEvasion2D) monster.iaToFollow.GetComponent<SteerForEvasion2D>();
				steerForEvasion.Menace = targetPlayerVehicle;
			}
		}
	}
}
