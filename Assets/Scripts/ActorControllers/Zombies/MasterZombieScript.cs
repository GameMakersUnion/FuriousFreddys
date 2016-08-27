﻿using UnityEngine;
using System.Collections;
using System;

public abstract class MasterZombieScript : EntityControlScript
{
    //I know i did it again... 
    GameObject van;
    protected VehicleControlScript vehicle;
    protected SpriteRenderer ZSR;
    protected SpriteRenderer VSR;
    protected Collider2D vC;
    protected Collider2D zC;
    public Sprite Oface;
    public Sprite Face;
    protected Vector3 start;
    protected Vector3 destin;
    public int damage;
    //public int health;
    protected bool contact;
    protected int counter;
    protected Rigidbody2D rb;
    protected int colcount;
	private bool hitHappenedRecently;
    public VaperStatistics stats = new VaperStatistics();
	//TODO: this is temporary. Ian will remove. Will phase this out, once done encapsulating it elsewhere
	public override int Health
	{
		get { return health; }
		set { health = value; }
	}


    // Use this for initialization
    public virtual void Start()
    {
        van = GameObject.FindGameObjectWithTag("Vehicle");
        vehicle = van.GetComponent<VehicleControlScript>();
        ZSR = this.GetComponent<SpriteRenderer>();
        ZSR.sortingLayerName = "Zombies";
        VSR = van.GetComponent<SpriteRenderer>();
        zC = this.GetComponent<Collider2D>();
        vC = van.GetComponent<Collider2D>();
        damage = 5;
		health = 20;
        this.contact = false;
        counter = 10;
        colcount = 10;
        rb = this.GetComponent<Rigidbody2D>();
		hitHappenedRecently = false;

    }

    public virtual void Update()
    {
        this.start = transform.position;
        this.destin = VSR.bounds.ClosestPoint(start);

		ApplyCooldown();
		RevertFace();
		CheckDies();
    }

	void ApplyCooldown()
	{
		if (hitHappenedRecently)
		{
			//begin cooldown counter
			if (this.counter == 0)
			{
				//reset cooldown
				hitHappenedRecently = false;
				this.counter = 30;
				print("COOLDOWN RESET");
			}
			else
			{
				this.counter--;
				//print("COOLDOWNWARD @ " + counter);
			}
		}
	}

	void RevertFace()
	{
		if (!this.contact)
		{
			if (colcount == 0)
			{
				ZSR.sprite = Face;
			}
			else
			{
				colcount--;
			}
		}
	}


	public override void Move(int direction)
	{
		//method that wouldn't make sense to implement, the signature of the parameter doesn't match what happens here... so it just gets discarded for now.

	}
 

	//depreciated
	//this manner of interaction has been depreciated by the visitor pattern
	//also the zombie will be responsible for destroying itself when it's health hits zero, we shouldn't be invoking it's death from here.
	//also the zombie will be responsible for updateHealth, this shouldn't be publically exposed.

	//public virtual void updateHealth(int change)
	//{
	//	this.health += change;

	//	if (this.health <= 0) {
	//		Destroy(this.gameObject);

	//	} 
	//}



    /*
     * Intial contact of the zombie to the car, stops it from moving 
     */
    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {
            this.contact = true;

            this.transform.position = this.transform.position;
        }



		ReceiveDamage(col);
    }

    void ReceiveDamage(Collision2D col)
	{
		if (!col.gameObject.GetComponent<ProjectileController>()) return;

		DamageVisitor damager = col.gameObject.GetComponent<DamageVisitor>();
		DamageVisitable damagable = gameObject.GetComponent<DamageVisitable>();

		damagable.AcceptDamageFrom(damager);

	}

    /*
     * Resets the zombie attack period to half
     * enables the rigidbody on the zombies
     */
	protected override void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {

            this.contact = false;
            //this.counter = 15;

        }
    }





    /*
     *every 30 or so frames damage the car
     * this gives the zombie an attack period of approx 30 frames
     */
    public virtual void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {

        }
    }

    /**
     * We don't need to see debug print lines every frame all the time.
     * Toggle on/off by selecting "ZombieController" in hierarchy and setting checkmark in inspector on the "GameControllerScript" component
     */
    public void print(object obj)
    {
        GameControllerScript script = SingletonGodController.instance.gameControllerScript;

        if (script != null && script.printDebugInfo)
        {
            MonoBehaviour.print(obj);
        }
    }

	public override void AcceptDamageFrom(DamageVisitor damager)
	{
        
		int damageAmount = damager.CauseDamageTo(this);
        health -= damageAmount;

        this.stats.damageTaken += damageAmount;
        //EntityControlScript entity = (EntityControlScript)damager;
        //GunnerControlScript freddy = //entity.transform.parent.GetComponent<FreddyFuckerController>();
       // freddy.stats.damageDelt += damageAmount;
        //freddy.stats.shotsConnected++;
    }

	// i hate muddying this simple pure "CauseDamageTo" with this unclean conditional, it violates clean code conventions... i have no better choice at the moment... 
	// it's more like "AttemptCauseDamageNow"
	public override int CauseDamageTo(DamageVisitable damagable)
	{
		int damage = (hitHappenedRecently) ? 0 : this.damage;
        if (!hitHappenedRecently)
        {
            hitHappenedRecently = true;
            this.stats.damageDelt = damage;
        }
        return damage;
	}
}
