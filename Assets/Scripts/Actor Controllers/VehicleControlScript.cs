using UnityEngine;
using System.Collections;

public class VehicleControlScript : PlayerControlScript
{
    public int health;
    Rigidbody2D rb;


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
    public void Update()
    {
        // this is broken car is supposed to realign itself and try to return to the upright position
                       
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
