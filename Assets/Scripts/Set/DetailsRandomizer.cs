using UnityEngine;
using System.Collections;

public class DetailsRandomizer : MonoBehaviour {
    [System.Serializable]
    public struct RateList{
        public int chances;
        public GameObject item;
    }
    public RateList[] elements;
    private int total = 0;
    
    void Awake ()
    {
        foreach(RateList element in elements)
            total += element.chances;
    }

	// Use this for initialization
	void Start ()
    {
        int current = 0;
        int random = Random.Range(0, total);
        foreach(RateList element in elements)
        {
            int next = current + element.chances;
            if(current <= random && random < next)
            {
                if(element.item)
                    Instantiate(element.item, transform.position, transform.rotation);
                return;
            }
            
            current = next;
        }
	}
}
