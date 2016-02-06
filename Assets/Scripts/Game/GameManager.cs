using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
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
	public Monster monsterSwordPrefab;
	public Monster monsterCrossBowPrefab;
	public Weapon[] hitWeapons;
	public Weapon[] shootWeapons;
	public Weapon[] playerWeapons;
    public Skill[] skills;
    public PlayerSkin[] playerSkins;
    public int floor = 0;
    public bool inGame = false;
    public List<Player> players = new List<Player>();
    public CameraRoom cam;
    private bool initGUI = true;
    private AsyncOperation asyncOp;

	[HideInInspector]
	public Room currentRoom;
    
    void Awake()
    {
        if(instance != this)
            Destroy(gameObject);
            
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        cam = Camera.main.gameObject.GetComponent<CameraRoom>();
    }
    
    void OnGUI()
    {
        if (initGUI)
        {
            GUILayout.Label("abcdefghijklmnopqrstuwxyz");
            initGUI = false;
        }
    }
    
    public void LaunchGame(PlayerList[] playersList, int traitorIndex)
    {
		int i = 0;
        foreach (PlayerList playerList in playersList)
        {
            Player p = Instantiate(playerPrefab, new Vector2(-9999f, -9999f), Quaternion.identity) as Player;
            p.SetupPlayer(playerList);
            players.Add(p);

			if (i == traitorIndex) {
				p.isTraitor = true;
				Debug.Log(i + " IS TRAITOR");
			}

			i++;
        }
        
        inGame = true;
        
        if(floor == 0)
            NextLevel();
    }
    
    public void NextLevel()
    {
        ++floor;
        StartCoroutine("LoadLevel", "level");
    }
    
    IEnumerator LoadLevel(string level) {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
        "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        asyncOp = SceneManager.LoadSceneAsync(level);
        asyncOp.allowSceneActivation = true;
        yield return asyncOp;
    }
    
    public void Reset()
    {
        for(var i = GameManager.instance.players.Count-1; i>=0; ++i)
        {
            Destroy(GameManager.instance.players[i].gameObject);
        }
        
        GameManager.instance.players.Clear();
        inGame = false;
        floor = 0;
        SceneManager.LoadScene(0);
    }
}
