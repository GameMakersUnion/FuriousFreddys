    using UnityEngine;
    using System.Collections;

public class VaperControlScript : MasterZombieScript {
    VaperHookShot vaperHookShoot;
    public int tongueRange;
    public float speed;
    bool shooting;



    public override void Start()
    {
        base.Start();
        shooting = false;

    }
    // Update is called once per frame
    public override void Update () {
        base.Update();

        float distanceTo = Vector3.Distance(start, destin);

        if (!shooting && distanceTo > tongueRange)
        {
            transform.position = Vector3.Lerp(start, destin, speed);

        }
        else {
            //shoot the fucking tongue
            transform.position = transform.position;
            if (!shooting) {
                shootTongue(start, destin);
                shooting = true;
            }
        }

    }

    void shootTongue(Vector3 s, Vector3 d ) {
        //it shoots the tongue?
       // VaperHookShot hookShoot = (VaperHookShot)Instantiate(vaperHookShoot, s, Quaternion.identity);
       // hookShoot.destination = d;


    }

}
