using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieController : MasterZombieScript
{

	// required to limit multiple executions for a single OnCollisionEnter2D to once per instance
	private List<GameObject> isColliding = new List<GameObject>();

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

		Move();

    }

    protected override void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {
            base.OnCollisionExit2D(col);
           transform.position = Vector3.Lerp(start, destin, speed);
            //transform.Translate(Vector3.Lerp(start, destin, speed));

            this.colcount = 10;
            //print("exiting" + this.name);

        }




		isColliding.Remove(col.gameObject);
    }

    

    protected override void OnCollisionEnter2D(Collision2D col)
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



		ReceiveDamage(col);

    }

	protected void Move()
	{
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

	void ReceiveDamage(Collision2D col)
	{
		if (!col.gameObject.GetComponent<ProjectileController>()) return;

		if (isColliding.Contains(col.gameObject)) return;
		isColliding.Add(col.gameObject);

		DamageVisitor damager = col.gameObject.GetComponent<DamageVisitor>();
		DamageVisitable damagable = gameObject.GetComponent<DamageVisitable>();
		damagable.AcceptDamageFrom(damager);

	}

	//redundant method call
	//public override int CauseDamageTo(DamageVisitable damagable)
	//{
	//	//i don't agree this "damage" variable should have public access, there's no reason this for that and invites confusion, ugly coupling, etc.
	//	//we should only access "damage" through specific interfaces such as this "CauseDamageTo" method, so the access scope of damage should be private or protected
	//	return damage;
	//}

}
