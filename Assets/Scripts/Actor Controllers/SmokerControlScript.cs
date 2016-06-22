    using UnityEngine;
    using System.Collections;

public class SmokerControlScript : MasterZombieScript {

    public int tongueRange;
    public float speed;


    // Update is called once per frame
    public override void Update () {
        base.Update();

        float distanceTo = Vector3.Distance(start, destin);

        if (distanceTo > tongueRange)
        {
            transform.position = Vector3.Lerp(start, destin, speed);

        }
        else {
            //shoot the fucking tongue
            transform.position = transform.position;

        }

    }

    void shootTongue() {



    }

}
