    using UnityEngine;
    using System.Collections;

public class SmokerControlScript : MasterZombieScript {

    public int tongueRange;
    public float MoveSpeed;


	// Update is called once per frame
	void Update () {

        Vector3 start = this.transform.position;
        Vector3 destin = VSR.bounds.ClosestPoint(start);
        

        

    }
}
