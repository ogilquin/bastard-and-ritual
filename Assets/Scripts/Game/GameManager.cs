using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    public Player playerPrefab;
    public int floor = 0;
    public bool inGame = false;
    public List<Player> players = new List<Player>();
    private bool initGUI = true;
    
    void Awake()
    {
        if(instance != this)
            Destroy(gameObject);
            
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }
    
    void OnGUI()
    {
        if (initGUI)
        {
            GUILayout.Label("abcdefghijklmnopqrstuwxyz");
            initGUI = false;
        }
    }
    
    public void LaunchGame(PlayerList[] playersList)
    {
        foreach (PlayerList playerList in playersList)
        {
            Player p = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity) as Player;
            p.SetupPlayer(playerList);
            players.Add(p);
        }
        
        inGame = true;
    }
    
    void Update()
    {

    }
}
