using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GoThroughMode : MonoBehaviour {
	public PlayerSelectManager playerSelectManager;

	public bool withKeyboard = false;

	public bool goThrouhgSelect = false;
	public bool goToPlay = false;

	public int numPlayers = 1;

	void Start () {
		if (goThrouhgSelect) {
			for (int i = 0; i < numPlayers; i++) {
				playerSelectManager.AddPlayer(i + 1, (PlayerIndex) i, withKeyboard ? ControllerType.Keyboard : ControllerType.Gamepad);
			}
		}

		if (goToPlay) {
			playerSelectManager.StartGame();
		}

	}
	
	void Update () {
	
	}
}
