using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Room : MonoBehaviour {
    
    public Door[] doors;
    public int height = 6;
    public int width = 6;
    
    public BoxCollider2D wallTop;
    public BoxCollider2D wallBottom;
    public BoxCollider2D wallLeft;
    public BoxCollider2D wallRight;
    
    public Door doorTop;
    public Door doorBottom;
    public Door doorLeft;
    public Door doorRight;
    
    public bool hasMonsters = true;
    
    
    void Update()
    {
        #if (UNITY_EDITOR)
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            
            if(wallTop){
                wallTop.size = new Vector2((float) width, 1f);
                wallTop.transform.localPosition = new Vector3(0f, ((float)height)/2f, 0f);
            }
            if(wallBottom){
                wallBottom.size = new Vector2((float) width, 1f);
                wallBottom.transform.localPosition = new Vector3(0f, (-(float)height)/2f, 0f);
            }
            if(wallLeft){
                wallLeft.size = new Vector2(1f, (float) height);
                wallLeft.transform.localPosition = new Vector3((-(float)width)/2f, 0f, 0f);
            }
            if(wallRight){
                wallRight.size = new Vector2(1f, (float) height);
                wallRight.transform.localPosition = new Vector3(((float)width)/2f, 0f, 0f);
            }
            
            if(doorTop){
                doorTop.transform.localPosition = new Vector3(0f, ((float)height)/2f - 1f, ((float)height)/2f - 1f);
            }
            if(doorBottom){
                doorBottom.transform.localPosition = new Vector3(0f, (-(float)height)/2f + 0.5f, (-(float)height)/2f);
            }
            if(doorLeft){
                doorLeft.transform.localPosition = new Vector3((-(float)width)/2f + 0.5f, 0f, 0f);
            }
            if(doorRight){
                doorRight.transform.localPosition = new Vector3(((float)width)/2f - 0.5f, 0f, 0f);
            }
        #endif
        

    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }
}
