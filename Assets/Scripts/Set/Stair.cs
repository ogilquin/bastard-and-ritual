using UnityEngine;

[RequireComponent (typeof (Animator))]
public class Stair : MonoBehaviour {
    public Transform spawnPoint;
    public bool usable = true;
    
    public void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Stair hit");
        if(usable == false)
            return;
            
        Player other = collider.gameObject.GetComponent<Player>();
        Debug.Log(other);
        if(other)
        {
            Debug.Log(other);
            Vector2 movementDir = other.GetController().Move();
            if(movementDir.magnitude > 0.1f)
            {
                Vector2 playerDir = (Vector2)transform.position - (Vector2)other.transform.position;
                float angle = Vector2.Angle(playerDir, movementDir);
                
                if(Mathf.Abs(angle) < 45f)
                {
                    GameManager.instance.NextLevel();
                }
            }
        }
    }
    
}