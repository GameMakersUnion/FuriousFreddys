using UnityEngine;
using System.Collections;

public class VehicleControlScript : PlayerControlScript
{
    public int health;
    Rigidbody2D rb;
    bool moveLeft=false;
    bool moveRight = false;

    protected override void Start () {
        base.Start();
        health = 500;
        rb= this.GetComponent<Rigidbody2D>();
        //print(this.GetComponent<Collider2D>().bounds.extents + transform.position + "v");
    }

    public override void Move(int direction)
    {
        //shift left or right

        //tf.Translate(moveFactor * direction * Time.deltaTime, 0, 0);
        
        rb.velocity = new Vector3(moveFactor * direction * Time.deltaTime, 0, 0);

    }
    public void MoveLeftUp()
    {
        moveLeft = false;

    }
    public void MoveLeftDown()
    {
        moveLeft = true;

    }
    public void MoveRightUp()
    {
        moveRight = false;

    }
    public void MoveRightDown()
    {
        moveRight = true;
  
    }
    public void Update()
    {
        if (moveLeft){
            Move(-1);
            
        }else
        if (moveRight){
            Move(1);

        }else
        {
            //Move(0);
        }

        // this is broken car is supposed to realign itself and try to return to the upright position

        //print(this.transform.rotation.eulerAngles.z);
        Vector3 rotation = this.transform.rotation.eulerAngles;

        if (rotation.z < 180)
        {
            transform.rotation = Quaternion.Euler((Vector3.Lerp(rotation, Vector3.zero, 0.05f)));
        }
        else {
            transform.rotation = Quaternion.Euler((Vector3.Lerp(rotation, new Vector3(0, 0, 360), 0.05f)));

        }


        if (Mathf.Abs(rotation.z - 360) <  0.5 ||(rotation.z - 360) <  0.5 ) {
            this.rb.angularVelocity = 0;

        }

    }
    
    /**
     * returns the current health of the car
     */ 
    public int currentHealth() {
        return health;
    }

    /*
     * @param the amount of health points that will be added or removed from the car
     */ 
    public int updateHealth( int damage, string name) {
        
        health = currentHealth() - damage;
        if (health < 0) {
            health = 0;
        }
        //Debug.Log(health);
        return health;
    }




}
