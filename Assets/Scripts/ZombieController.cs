using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

    Vector3 direction;
    public float speed;
    public Van  van;
    
	// Use this for initialization
	void Start () {
	    


	}
	
	// Update is called once per frame
	void Update () {
        SpriteRenderer SR = van.GetComponent<SpriteRenderer>();
        Vector3 destin = van.transform.position;
        Vector3 start = transform.position;
        if(!SR.bounds.Contains(start))
        transform.position = Vector3.Slerp(start, destin, speed);
         
	}
}
