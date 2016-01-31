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

		foreach (Player player in players) {
			player.numMonstersAttacking = 0;
		}

		int numMonstersAliveAvailable = 0;
		foreach (Monster monster in monsters) {
			if (!monster.GetLife().IsDead()) {
				numMonstersAliveAvailable++;
			}
		}

		int numMonsterMaxPerPlayer = (int) Mathf.Ceil((float) numMonstersAliveAvailable / players.Length);

		foreach (Monster monster in monsters) {
			monster.playerTarget = null;

			if (!monster.GetLife().IsDead()) {
				PassiveVehicle2D targetPlayerVehicle = null;
				foreach (Player player in players) {
					if (!player.GetLife().IsDead()) {
						if (player.numMonstersAttacking < numMonsterMaxPerPlayer) {
							targetPlayerVehicle = (PassiveVehicle2D) player.GetComponent<PassiveVehicle2D>();
							monster.playerTarget = player;

							player.numMonstersAttacking++;
							numMonstersAliveAvailable--;
							break;
						}
					}
				}

				if (targetPlayerVehicle) {
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
	}
}
