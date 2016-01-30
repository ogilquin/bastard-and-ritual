using UnityEngine;
using System.Collections.Generic;
using XInputDotNetPure;

public class PlayerSelectManager : MonoBehaviour {
    
    public int minPlayer = 4;
    public int maxPlayer = 4;

    private List<PlayerList> players = new List<PlayerList>();
    private List<int> testable;
    
    void Awake()
    {
        testable = new List<int>();
        for (int i = 0; i < 4; ++i)
            testable.Add(i);
    }

	void Update ()
    {
        for(int i = testable.Count-1; i >= 0; --i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)testable[i];
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                if (testState.Buttons.A == ButtonState.Pressed)
                {
                    AddPlayer(players.Count+1, testPlayerIndex, ControllerType.Gamepad);
                    testable.Remove(testable[i]);
                    return;
                }
            }
        }
        
        for(int i = 0;  i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {             
                if(testState.Buttons.Start == ButtonState.Pressed)
                    StartGame();
            }
        }

        if (Input.GetButtonDown("K1_Fire"))
        {
            AddPlayer(players.Count+1, 0, ControllerType.Keyboard);
            return;
        }
	}
    
    void AddPlayer(int number, PlayerIndex playerIndex, ControllerType controllerType)
    {
        Debug.Log("Add player : " + number + ", controller: " + playerIndex);
        players.Add(new PlayerList(number, playerIndex, controllerType));
        //UIManager.instance.Shake(50f, 10f, Vector2.zero);
    }
    
    void StartGame(){
        if(minPlayer > players.Count || GameManager.instance.inGame == true)
            return;
            
        Debug.Log("Launch game");
        GameManager.instance.LaunchGame(players.ToArray());
        Destroy(gameObject);
    }
}
