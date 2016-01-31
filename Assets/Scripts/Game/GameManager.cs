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

    public string[] levels;
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
    
    public void LaunchGame(PlayerList[] playersList)
    {
        Debug.Log("Launch: "+playersList);
        foreach (PlayerList playerList in playersList)
        {
            Debug.Log("Launch: "+playerList);
            Player p = Instantiate(playerPrefab, new Vector2(-9999f, -9999f), Quaternion.identity) as Player;
            p.SetupPlayer(playerList);
            players.Add(p);
        }
        
        inGame = true;
        
        if(floor == 0)
            NextLevel();
    }
    
    public void NextLevel()
    {
        ++floor;
        if(levels.Length > 0)
            StartCoroutine("LoadLevel");
    }
    
    IEnumerator LoadLevel() {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
        "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        asyncOp = SceneManager.LoadSceneAsync(levels[Random.Range(0, levels.Length)]);
        asyncOp.allowSceneActivation = true;
        yield return asyncOp;
    }
}
