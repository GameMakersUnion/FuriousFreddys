﻿using UnityEngine;

public abstract class PlayerControlScript : EntityControlScript {

    private bool keyPress;
    protected Transform tf;

    public override int Health
    {
        get { return -1; }
        set {}
    }

    

    protected override void Start()
    {
        keyPress = false;
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
