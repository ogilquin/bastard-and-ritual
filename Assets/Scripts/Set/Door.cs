using UnityEngine;

[RequireComponent (typeof (Animator))]
public class Door : MonoBehaviour {
    
    public Door linkTo;
    public Transform spawnPoint;
    private bool closed = false;
    private Animator anim;
    private Room parent;
    
    void Awake()
    {
        if(linkTo == null)
            gameObject.SetActive(false);

        anim = gameObject.GetComponent<Animator>();
        parent = gameObject.GetComponentInParent<Room>();
    }
    
    public void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Door hit");
        if(closed == true)
            return;
            
        TakeDamage other = collider.gameObject.GetComponent<TakeDamage>();
        
        linkTo.GetRoom().Enter(linkTo);
        parent.Exit();
    }
    
    public void Close()
    {
        closed = true;
        anim.SetBool("Closed", true);
    }
    
    public void Open()
    {
        closed = false;
        anim.SetBool("Closed", false);
    }
    
    public Room GetRoom()
    {
        return parent;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
        if(linkTo)
            Gizmos.DrawLine(transform.position, linkTo.transform.position);
    }
}
