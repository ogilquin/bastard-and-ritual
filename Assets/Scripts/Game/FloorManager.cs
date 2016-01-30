using UnityEngine;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour
{
    public Room entrance;
    // public Room exit;
    // public Room current;
    
    private Room[] rooms;

    void Start()
    {
        // Get all rooms
        rooms = FindObjectsOfType(typeof(Room)) as Room[];
        foreach(Room room in rooms){
            room.Exit();
        }
        
        if(entrance)
            entrance.Enter(entrance.doorBottom);
            //EnterRoom(entrance.doorBottom);
    }
    
    
    void Update()
    {

    }
    
    // public void EnterRoom(Door door)
    // {
    //     if(current)
    //         current.Exit();
            
    //     current = door.GetRoom();
    //     current.Enter(door);
    // }
}
