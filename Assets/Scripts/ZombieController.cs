using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

    Vector3 direction;
    public float speed;
   GameObject  van;
    
	// Use this for initialization
	void Start () {
        van = GameObject.FindGameObjectWithTag("Van");

	}
	
	// Update is called once per frame
	void Update () {
        SpriteRenderer SR = van.GetComponent<SpriteRenderer>();
        Vector3 start = transform.position;
        Vector3 destin = SR.bounds.ClosestPoint(start);

        if(!SR.bounds.Contains(start))
        transform.position = Vector3.Lerp(start, destin, speed);
         
	}
}
