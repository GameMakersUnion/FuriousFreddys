using System;
using UnityEngine;

public abstract class PlayerControlScript : EntityControlScript {
    public int playerNumber = 0; //Their player number



    public override int Health
    {
        get { return -1; }
        set {}
    }

    protected Transform tf;

    protected override void Start()
    {
        tf = GetComponent<Transform>();
    }

	protected override void OnCollisionEnter2D(Collision2D col)
	{

	}

	protected override void OnCollisionExit2D(Collision2D col)
	{
	
	}
    
    public abstract void PerformAction();

	//do nothing
	public override void AcceptDamageFrom(DamageVisitor damager)
	{

	}

}
