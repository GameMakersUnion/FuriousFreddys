    using UnityEngine;
    using System.Collections;

public class VaperControlScript : MasterZombieScript {
   public VaperHookShot vaperHookShoot;
    VaperHookShot hookShoot;
    public int tongueMinRange;
    public float speed;
    bool shooting;
    public int tongueMaxRange;


    public override void Start()
    {
        
        base.Start();

        shooting = false;
        hookShoot = (VaperHookShot)Instantiate(vaperHookShoot, this.transform.position, Quaternion.identity);
        hookShoot.gameObject.name = this.gameObject.name + " tongue";
        hookShoot.transform.parent = this.transform;
        hookShoot.setGameObject(this.gameObject);
        // print(tongueMinRange);

    }



    // Update is called once per frame
    public override void Update () {
        base.Update();

        float distanceTo = Vector3.Distance(base.start, base.destin);
        //print("start: " + base.start + " destin: " + base.destin + " distanceTo: " + distanceTo + " shooting: " + this.shooting);
      


        if (!shooting && distanceTo > tongueMinRange)
        {
           /// print("Im supposed to be doing shit");
            this.transform.position = Vector3.Lerp(start, destin, speed);

        }
        else {
            //shoot the fucking tongue
            transform.position = transform.position;
            if (!shooting)
            {
                shootTongue(start, destin, this.gameObject);
                
            }
        
                
        }
        if (shooting && distanceTo > tongueMaxRange) {
            breakTongue();
            
        }
        

    }

    void shootTongue(Vector3 s, Vector3 d, GameObject smoker ) {
        //it shoots the tongue?
        this.shooting = true;
        hookShoot.BuildRope();
        //print(this.gameObject.name + " i shoot tongue");
       hookShoot.destination = d;


    }
    void breakTongue() {

        hookShoot.DestroyRope();
        this.rb.velocity = Vector2.zero;
        this.shooting = false;
        //print(this.gameObject.name + " tongue broke");

    }


}
