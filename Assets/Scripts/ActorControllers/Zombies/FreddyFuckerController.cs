using UnityEngine;
using System.Collections;

public class FreddyFuckerController : MasterZombieScript {
    public float speed;
    bool jumping;
    public int jumpMaxRange;
    public int jumpMinRange;
    GunnerControlScript attached;
    // Use this for initialization
    public override void Start () {
        base.Start();
        jumping = false;
        
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        float distanceTo = Vector3.Distance(base.start, base.destin);
        //print("start: " + base.start + " destin: " + base.destin + " distanceTo: " + distanceTo + " jumping: " + this.jumping);


        if (!attached)
        {
            if (!jumping && distanceTo < jumpMinRange)
            {
               // print("Im supposed to be doing shit");
                this.transform.position = Vector3.Lerp(start, destin, speed);
                //jumping = false;
            }
            else
            {
                //shoot the fucking tongue
                if (!jumping)
                {

                    //transform.position = transform.position;
                    jumping = true;
                    //jumping = true;
                    jump(start, destin, this.gameObject);

                }


            }
            if (jumping && distanceTo > jumpMaxRange)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;
                transform.position = transform.position;
                // breakTongue();
                jumping = false;
            }
        }
        else {
            //go fuck yourself
            //this.transform.position = transform.parent.position;
            this.transform.position = new Vector2(transform.parent.position.x + Random.Range(-0.20F, 0.20F), transform.parent.position.y + Random.Range(-0.20F, 0.20F));
        }

        if (health == 0) {
            if (attached != null) {
                attached.RemoveFromList(this);
            }
        }


    }



    public void jump(Vector3 start, Vector3 destin, GameObject ob) {

      
        ob.GetComponent<Rigidbody2D>().AddForce(15000.0F* (destin - start));

    }
    /*
    public new void OnCollisionEnter2D(Collision2D col)
    {
        print(col.gameObject.name);
        if (col.gameObject.tag == "Freddy")
        {
            attach(col.gameObject);
        }
        else {
           // jumping = false;

        }
    }
    */

    public  void OnTriggerEnter2D(Collider2D col)
    {
        print(col.gameObject.name);
        if (col.gameObject.tag == "Gunner" && !attached)
        {
            attach(col.gameObject);
        }
        else
        {
             jumping = false;

        }
    }



    public void attach(GameObject ob) {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        this.gameObject.layer = 13;
        attached = ob.GetComponent<GunnerControlScript>();
        this.transform.parent = ob.transform;
        ob.GetComponent<GunnerControlScript>().AddToList(this);
    }


}
