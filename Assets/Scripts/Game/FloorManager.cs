using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour
{
    public int separation = 5;
    public int maxRooms   = 10;
    
    public Stair entrance;
    public Stair exit;
    public RoomList[] roomsList;
    private int totalChances = 0;
    private List<Room> rooms = new List<Room>();
    private List<RoomSpawn> queue = new List<RoomSpawn>();
    
    void Awake()
    {
        // Calculate the total spawn chances
        foreach(RoomList roomList in roomsList)
        {
            totalChances += roomList.chances;
        }
    }

    IEnumerator Start()
    {
        // TODO: More room when more level
        while(rooms.Count < maxRooms)
        {
            if(queue.Count == 0)
            {
                if(rooms.Count == 0){
                    Vector2 position = new Vector2();
                    RoomList size = GetRandomRoomSize();
                    queue.Add(new RoomSpawn(position, size.width, size.height, size.rooms[Random.Range(0, size.rooms.Length-1)], null, Directions.Null));
                } else {
                    Generate(rooms[Random.Range(0, rooms.Count-1)], Directions.Null);
                }
            }
            
            Spawner(queue[0]);
            queue.RemoveAt(0);
            
            yield return null;
        }
        
        rooms[rooms.Count-1].type = RoomType.Exit;
        
        queue.Clear();
        
        // TODO: generate Objects / monsters difficulty
                
        // Initialize rooms
        foreach(Room room in rooms){
            room.Initialize();
            if(room.type == RoomType.Entrance){
                Stair s = Instantiate(entrance, room.transform.position, Quaternion.identity) as Stair;
                s.transform.parent = room.transform;
                // no monsters in first level
                room.maxHittingMonster = 0;
                room.maxHittingMonster = 0;
                room.minShootingMonsters = 0;
                room.maxShootingMonsters = 0;
            } else if(room.type == RoomType.Exit){
                Stair s = Instantiate(exit, room.transform.position, Quaternion.identity) as Stair;
                s.transform.parent = room.transform;
            }
        }
    }
    
    RoomList GetRandomRoomSize()
    {
        var current = 0;
        int chance = Random.Range(0, totalChances-1);

        foreach(RoomList room in roomsList){
            if(current <= chance && chance < current + room.chances)
                return room;

            current += room.chances;
        }

        return roomsList[0];
    }

    void Generate(Room room, Directions direction)
    {
        var top    = (Random.value < 0.5f && direction != Directions.Down);
        var bottom = (Random.value < 0.5f && direction != Directions.Up);
        var left   = (Random.value < 0.5f && direction != Directions.Right);
        var right  = (Random.value < 0.5f && direction != Directions.Left);
        
        if(!top && !bottom && !left && !right){
            if(direction == Directions.Up){
                top = true;
            } else if(direction == Directions.Down) {
                bottom = true;
            } else if(direction == Directions.Left) {
                left = true;
            } else if(direction == Directions.Right) {
                right = true;
            } else {
                top = true;
            }
        }

        // top
        if(top){
            RoomList size = GetRandomRoomSize();
            Vector2 position = (Vector2)room.transform.position + new Vector2(0f, ((float)room.height/2f + (float)size.height/2f + (float)separation));
            AddToQueue(room, position, size, Directions.Up);
        }
        
        // bottom
        if(bottom){
            RoomList size = GetRandomRoomSize();
            Vector2 position = (Vector2)room.transform.position + new Vector2(0f, -((float)room.height/2f + (float)size.height/2f + (float)separation));
            AddToQueue(room, position, size, Directions.Down);
        }
        
        // left
        if(left){
            RoomList size = GetRandomRoomSize();
            Vector2 position = (Vector2)room.transform.position + new Vector2(-((float)room.width/2f + (float)size.width/2f + (float)separation), 0f);
            AddToQueue(room, position, size, Directions.Left);
        }
        
        // right
        if(right){
            RoomList size = GetRandomRoomSize();
            Vector2 position = (Vector2)room.transform.position + new Vector2(((float)room.width/2f + (float)size.width/2f + (float)separation), 0f);
            AddToQueue(room, position, size, Directions.Right);
        }
    }
    
    bool AddToQueue(Room last, Vector2 position, RoomList size, Directions direction)
    {
        if(!CollideRooms(position.x, position.y, size.width, size.height)){
            Room room = size.rooms[Random.Range(0, size.rooms.Length-1)];
            RoomSpawn spawn = new RoomSpawn(position, size.width, size.height, room, last, direction);
            queue.Add(spawn);
            return true;
        }
        
        return false;
    }

    Room Spawner(RoomSpawn spawn){
        if(rooms.Count >= maxRooms){
            return null;
        }

        if(CollideRooms(spawn.position.x, spawn.position.y, spawn.width, spawn.height))
            return null;

        Room room = Instantiate(spawn.room, spawn.position, Quaternion.identity) as Room;
        room.transform.parent = transform;
        
        if(spawn.direction == Directions.Up){
            room.doorBottom.linkTo = spawn.parent.doorTop;
            spawn.parent.doorTop.linkTo = room.doorBottom;
        } else if(spawn.direction == Directions.Down) {
            room.doorTop.linkTo = spawn.parent.doorBottom;
            spawn.parent.doorBottom.linkTo = room.doorTop;
        } else if(spawn.direction == Directions.Left) {
            room.doorRight.linkTo = spawn.parent.doorLeft;
            spawn.parent.doorLeft.linkTo = room.doorRight;
        } else if(spawn.direction == Directions.Right) {
            room.doorLeft.linkTo = spawn.parent.doorRight;
            spawn.parent.doorRight.linkTo = room.doorLeft;
        }

        if(rooms.Count == 0)
            room.type = RoomType.Entrance;
            
        rooms.Add(room);
        Generate(room, spawn.direction);
        return room;
    }

    bool CollideRooms(float ax, float ay, float aw, float ah)
    {
        foreach(Room room in rooms)
        {
            float aw2 = aw / 2;
            float ah2 = ah / 2;
            float bw2 = room.width / 2;
            float bh2 = room.height / 2;
        
            if(!(
                ax + aw2 < room.transform.position.x - bw2 || 
                ax - aw2 > room.transform.position.x + bw2 || 
                ay - ah2 > room.transform.position.y + bh2 || 
                ay + ah2 < room.transform.position.y - bh2
            )) return true;
        }
        
        return false;
    }
    
    public struct RoomSpawn{
        public Vector2 position;
        public int width;
        public int height;
        public Room room;
        public Room parent;
        public Directions direction;
        public RoomSpawn(Vector2 position, int width, int height, Room room, Room parent, Directions direction)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.room = room;
            this.parent = parent;
            this.direction = direction;
        }
    }

    [System.Serializable]
    public struct RoomList{
        public int width;
        public int height;
        public int chances;
        public Room[] rooms;
    }
}
