using UnityEngine;
using System.Collections;

public class FreddyFuckerController : MasterZombieScript {
    public float speed;
    bool jumping;
    public int jumpMaxRange;
    public int jumpMinRange;
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



        if (!jumping && distanceTo < jumpMinRange)
        {
             print("Im supposed to be doing shit");
            this.transform.position = Vector3.Lerp(start, destin, speed);

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
            transform.position = transform.position;
            // breakTongue();
            jumping = false;
        }


    }



    public void jump(Vector3 start, Vector3 destin, GameObject ob) {

        print("JUMPED");
        ob.GetComponent<Rigidbody2D>().AddForce(1000.0F* (destin - start));

    }






}
