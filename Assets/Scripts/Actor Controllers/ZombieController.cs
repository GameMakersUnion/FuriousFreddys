using UnityEngine;
using System.Collections;

public class ZombieController : MasterZombieScript {

    Vector3 direction;
    public float speed;
    
    


    // Update is called once per frame
    public override void Update () {
        base.Update();


        /*
         * This shit is broken. 
         * TODO : Delete this, create void onCollison(?)Enter(Collider2D Collider){} method
         */
    
            if (!this.contact)
            {
            this.rb.velocity = Vector3.zero;
            this.rb.angularVelocity = 0;
            transform.position = Vector3.Lerp(start, destin, speed);
            
        }
           
    }

    public override void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Vehicle")
        {
            base.OnCollisionExit2D(col);
            //Debug.Log("Exit");
            transform.position = Vector3.Lerp(start, destin, speed);
            ZSR.sprite = Face;
    
        }
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {
            this.rb.Sleep();
            //Debug.Log("Contact");
            base.OnCollisionEnter2D(col);
            ZSR.sprite = Oface;
        }
    }

}
