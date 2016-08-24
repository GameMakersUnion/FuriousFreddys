using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleControlScript : EntityControlScript
{

	// required to limit multiple executions for a single OnCollisionEnter2D to once per instance
	[HideInInspector]
	public List<GameObject> isColliding = new List<GameObject>();
	
	Rigidbody2D rb;
	Gridilizer gr;

    public override int Health
    {
        get { return health; }
        set { health = value; }
    }

    protected override void Start () {
        base.Start();
        health = 1500;
        moveFactor = 750;
        rb = this.GetComponent<Rigidbody2D>();
		gr = this.GetComponent<Gridilizer>();
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

	protected override void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<TongueJoint>()) return;
		if (col.gameObject.GetComponent<ProjectileController>()) return;

		//if (isColliding.Contains(col.gameObject)) return;
		//isColliding.Add(col.gameObject);

		//laggy
		//print("V OnCollisionEnter executing... col.gameObject.name: " + col.gameObject.name + ", this.gameObject.name: " + this.gameObject.name);

		DamageVisitor damager = col.gameObject.GetComponent<DamageVisitor>();
		DamageVisitable damagable = gameObject.GetComponent<DamageVisitable>();
		if (damager == null)
		{
			//Debug.LogWarning("Non-damager " + col.gameObject.name + " collided with damageable " + gameObject.name + ", please implement DamageVisitor method on it, or exclude from check on this damagable. ");
			return;
		}

		damagable.AcceptDamageFrom(damager);
	}

	protected override void OnCollisionExit2D(Collision2D col) { } 

	//protected override void OnCollisionExit2D(Collision2D col)
	//{
	//	isColliding.Remove(col.gameObject);
	//}

	public override void AcceptDamageFrom(DamageVisitor damager)
	{
		//int damageAmount = damager.CauseDamageTo(this);
		//health -= damageAmount;
		//if (damageAmount != 0) ReportHealth();
	}

	public override int CauseDamageTo(DamageVisitable damagable)
	{
		return 0;
	}

}
