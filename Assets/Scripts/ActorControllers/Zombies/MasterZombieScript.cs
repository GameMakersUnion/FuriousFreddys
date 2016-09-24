using UnityEngine;
using System.Collections;
using System;

public abstract class MasterZombieScript : EntityControlScript
{
    protected GameObject van;
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

	public override int Health
	{
		get { return health; }
		set { health = value; }
	}


    // Use this for initialization
    public virtual void Start()
    {
		rb = this.GetComponent<Rigidbody2D>();
		zC = this.GetComponent<Collider2D>();
		ZSR = this.GetComponent<SpriteRenderer>();
		ZSR.sortingLayerName = "Zombies";

		damage = 5;
		health = 20;
		this.contact = false;
		counter = 10;
		colcount = 10;
		hitHappenedRecently = false;
		
		van = GameObject.FindGameObjectWithTag("Vehicle");
		if (van == null) return;
        vehicle = van.GetComponent<VehicleControlScript>();
        VSR = van.GetComponent<SpriteRenderer>();
        vC = van.GetComponent<Collider2D>();
        
    }

    public virtual void Update()
    {
		Move();
		ApplyCooldown();
		RevertFace();
		CheckDies();
    }

	void Move()
	{
		if (VSR != null)
		{
			this.start = transform.position;
			this.destin = VSR.bounds.ClosestPoint(start);
		}
		else
		{
			GameObject newTarget = new GameObject("New Target For Zombies");
			newTarget.transform.position = Vector3.up * 3f;
			SpriteRenderer newRenderer = newTarget.AddComponent<SpriteRenderer>();
			VSR = newRenderer;
		}
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
			}
		}
	}

	void RevertFace()
	{
		if (ZSR == null) return;
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

		AffectVisitor damager = col.gameObject.GetComponent<AffectVisitor>();
		AffectVisitable damagable = gameObject.GetComponent<AffectVisitable>();

		damagable.AcceptAffectFrom(damager);

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


    /**
     * We don't need to see debug print lines every frame all the time.
     * Toggle on/off by selecting "ZombieController" in hierarchy and setting checkmark in inspector on the "GameControllerScript" component
     */
    public void print(object obj)
    {
        ZombieSpawnOnceController script = SingletonGodController.instance.gameControllerScript;

        if (script != null && script.printDebugInfo)
        {
            MonoBehaviour.print(obj);
        }
    }

	public override void AcceptAffectFrom(AffectVisitor damager)
	{
        
		int damageAmount = damager.CauseAffectTo(this);
        health -= damageAmount;

        this.stats.damageTaken += damageAmount;
        //EntityControlScript entity = (EntityControlScript)damager;
        //GunnerControlScript freddy = //entity.transform.parent.GetComponent<FreddyHuggerController>();
       // freddy.stats.damageDelt += damageAmount;
        //freddy.stats.shotsConnected++;
    }

	// i hate muddying this simple pure "CauseAffectTo" with this unclean conditional, it violates clean code conventions... i have no better choice at the moment... 
	// it's more like "AttemptCauseDamageNow"
	public override int CauseAffectTo(AffectVisitable damagable)
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
