using UnityEngine;
using System.Collections;

public class ZombieController : MasterZombieScript
{

    Vector3 direction;
    public float speed;
    

     public override void Start() {
        base.Start();
        //transform.position = Vector3.Lerp(start, destin, speed);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();


        /*
         * This shit is broken. 
         * It prevents the zombie faces from going ape shit crazy because i dont know how to code
         */

        if (!this.contact)
        {
            // this.rb.velocity = Vector3.zero;
            this.rb.angularVelocity = 0;
            //transform.Translate(Vector3.Lerp(start, destin, speed));
           transform.position = Vector3.Lerp(start, destin, speed);
            //print(transform.position);

        }
    }

    public override void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {
            base.OnCollisionExit2D(col);
           transform.position = Vector3.Lerp(start, destin, speed);
            //transform.Translate(Vector3.Lerp(start, destin, speed));

            this.colcount = 10;
            //print("exiting" + this.name);

        }
    }

    

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {
            //transform.position(transform.)
            //transform.Translate(transform.position);
            //this.rb.Sleep();
            base.OnCollisionEnter2D(col);
            ZSR.sprite = Oface;
           // print("entering" + this.name);
        }
    }

}
