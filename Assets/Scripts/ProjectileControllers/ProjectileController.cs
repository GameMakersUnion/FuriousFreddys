﻿using UnityEngine;

// Controller for normal bullet-type projectiles
public class ProjectileController : MonoBehaviour, DamageVisitor {

	public int damage;
    public float timeAlive;

    private float deathTime;
    private GunnerControlScript owner; //the gunner freddy who fired it. this remains null if the bullet was shot by a zomb

    public GunnerControlScript Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    void Start()
    {
        deathTime = Time.time + timeAlive;
        owner = null;
    }

    void Update()
    {
        if (Time.time > deathTime) Destroy(gameObject);
    }

	//i moved this logic to be "receiving" instead of "generating"
	//i changed the prefab from trigger to regular rigidbody
	void OnTriggerEnter2D(Collider2D col)
	{

        if (!(col.gameObject.tag == "Vehicle" || col.gameObject.tag == "Cell" || col.gameObject.tag == "Bullet"))
            Destroy(gameObject);

        if (col.gameObject.tag == "Zombie")
		{

			//depreciated
			//this manner of interaction has been depreciated by the visitor pattern
			//also the zombie will be responsible for destroying itself when it's health hits zero, we shouldn't be invoking it's death from here.
			//also the zombie will be responsible for updateHealth, this shouldn't be publically exposed.

			//call take dmg method in zombie controller
			//coll.GetComponent<MasterZombieScript>().updateHealth(-damage);
			//Destroy(gameObject);
		}
	}
    /*
	void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.tag == "Zombie")
		{

			//depreciated
			//this manner of interaction has been depreciated by the visitor pattern
			//also the zombie will be responsible for destroying itself when it's health hits zero, we shouldn't be invoking it's death from here.
			//also the zombie will be responsible for updateHealth, this shouldn't be publically exposed.

			//call take dmg method in zombie controller
			//coll.GetComponent<MasterZombieScript>().updateHealth(-damage);
			//Destroy(gameObject);
		}
	}
    */
    public virtual int CauseDamageTo(DamageVisitable visitable)
	{
		return damage;
	}

}
