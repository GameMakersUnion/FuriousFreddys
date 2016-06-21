using UnityEngine;
using System.Collections;

public class ZombieController : MasterZombieScript {

    Vector3 direction;
    public float speed;

    // Update is called once per frame
    void Update () {
        
        Vector3 start = transform.position;
      

        Vector3 destin = VSR.bounds.ClosestPoint(start);

        if (!VSR.bounds.Contains(ZSR.bounds.ClosestPoint(destin)))
        {
            ZSR.sprite = Face;
            transform.position = Vector3.Lerp(start, destin, speed);
        }
        else {
            ZSR.sprite = Oface;
        }


        getZombieSprite();
    }

 


}
