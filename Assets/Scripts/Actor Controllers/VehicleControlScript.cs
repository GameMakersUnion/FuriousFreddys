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
        transform.rotation = Quaternion.EulerRotation(0, 0, 5f);
        Vector2 blah = Velocity;
        print(blah);
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

    public Vector2 Velocity
    {
        get
        {
            Vector2 foundVelocity = FindVelocity();
            return foundVelocity;
        }
    }

    private Vector2 FindVelocity(){

        LevelController lc = SetAndGetLevelController();
        float yAngle = transform.eulerAngles.z;
        float ySpeed = lc.ScrollSpeed;
        float xSize = UseTrigonometryGetSideSineLaw(yAngle, ySpeed);

        float xAngle = 90 - yAngle;
        float ySize = UseTrigonometryGetSideSineLaw(xAngle, xSize);

        Vector2 velocity = new Vector2(xSize, ySize);
        return velocity;

    }

    private float UseTrigonometryGetSideSineLaw(float angleA, float sideA)
    {
        //requires 90° triangle 
        float angleB = 90 - angleA; 
        float sideB = sideA * Mathf.Sin( angleA ) / Mathf.Sin( angleB );
        //print(Mathf.RoundToInt(angleA) + "," + Mathf.RoundToInt(angleB) + "," + Mathf.RoundToInt(sideA), + ",..."+ Mathf.RoundToInt(sideB));
        return sideB;
    }


    LevelController levelController;  //yucky side effect state
    private LevelController SetAndGetLevelController()
    {
        if (levelController == null)
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }
        return levelController;
    }
}
