using UnityEngine;
using System.Collections;

public class Van : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
        health = 50;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("a")) {
            this.transform.position =  Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x -10 , this.transform.position.y, this.transform.position.z), 0.1f); 
            print("A was pressed");
        }
        if (Input.GetKeyDown("d"))
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z), 0.1f);

            print("d press");
        }






    }
  public  Vector3 getPos()
    {
     

        return this.transform.position;
    }

}
