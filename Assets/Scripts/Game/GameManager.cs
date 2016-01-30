using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

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

    private bool initGUI = true;

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
    
    
    void Update()
    {

    }
}
