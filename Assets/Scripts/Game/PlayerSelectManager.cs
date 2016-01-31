using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using XInputDotNetPure;

public class PlayerSelectManager : MonoBehaviour {
	public int minPlayer = 4;
    public int maxPlayer = 4;

    private List<PlayerList> players = new List<PlayerList>();
    private List<int> testable;
    
    private bool keyboard = false;
    private bool start = false;
    
    public Image[] uiPlayers;
    
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

        if (Input.GetButtonDown("K1_Fire") && keyboard == false)
        {
            AddPlayer(players.Count+1, 0, ControllerType.Keyboard);
            keyboard = true;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
            return;
        }
	}
    
    public void AddPlayer(int number, PlayerIndex playerIndex, ControllerType controllerType)
    {
        Debug.Log("Add player : " + number + ", controller: " + playerIndex);
        players.Add(new PlayerList(number, playerIndex, controllerType));
        
        uiPlayers[number-1].sprite = GameManager.instance.playerSkins[number-1].ui;
        UIManager.instance.Shake(50f, 10f, Vector2.zero);
    }
    
    public void StartGame(){
        if(start == true) return;
        if(minPlayer > players.Count || GameManager.instance.inGame == true)
            return;
           
		int traitorIndex = Random.Range(0, players.Count);

        Debug.Log("Launch game");
        start = true;
		GameManager.instance.LaunchGame(players.ToArray(), traitorIndex);
        Destroy(gameObject);
    }
}
