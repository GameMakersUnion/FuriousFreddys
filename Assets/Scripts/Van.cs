using UnityEngine;
using System.Collections;

public class Van : MonoBehaviour {
    public float speed;
    public int health;

	// Use this for initialization
	void Start () {
        health = 50;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("a")) {
            this.transform.position =  Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x -10 , this.transform.position.y, this.transform.position.z), speed); 
            print("A was pressed");
        }
        if (Input.GetKey("d"))
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z), speed);

            print("d press");
        }






    }
    public  Vector3 getPos()
    {
     

        return this.transform.position;
    }

}
