﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CellController : EntityControlScript {

	private int health;
	public  int layers;
	public  int healthOrig; 
	VehicleControlScript vehicle;
	Gridilizer gr;
	SpriteRenderer sr;

	Dictionary<int, int> damageMultiplyer = new Dictionary<int,int>();

	public override int Health
	{
		get { return health; }
		set { health = (value >= 0) ? value : 0; }
	}


	protected override void Start()
	{
		base.Start();
        //Init();
	}

	protected override void Update()
	{
		CheckSwapCellSprite();
	}


    public void Init()
    {
        vehicle = transform.parent.parent.GetComponent<VehicleControlScript>();
        gr = transform.parent.parent.GetComponent<Gridilizer>();
        sr = transform.GetComponent<SpriteRenderer>();
    }

	
	float lastPercentage;

	bool[] levels;

	int level = 1;

	//ugggggggggly
	bool set99 = true;
	bool set66 = false;
	bool set33 = false;


	private void CheckSwapCellSprite()
	{

		//silly way to init array, TODO refactor nicer later.
		if (levels == null)
		{
			levels = new bool[layers];

			int count = 0;
			var enumerator = levels.GetEnumerator();
			{
				bool item;
				while (enumerator.MoveNext())
				{
					item = (bool)enumerator.Current;
					item = false;
					count++;
					damageMultiplyer.Add(count, count);
				}
			}
		}
		levels[0] = true;



		//gr.SetCellToLayer();
		float percentage = ((float)health / healthOrig) * 100;

		if (percentage != lastPercentage)
		{
			//print("percentage cell health: " + this.name + " " + percentage);
			lastPercentage = percentage;

		}



		if (percentage < 66 && !set66) 
		{
			set66 = true;
			print(66);
			gr.SetCellToLayerLower(sr);
		}
		else if (percentage < 33 && !set33)
		{
			print(33);
			set33 = true;
			gr.SetCellToLayerLower(sr);
		}
	}

	public override void Move(int direction) { }

	public override void AcceptAffectFrom(AffectVisitor damager) 
	{
		int damageAmount = damager.CauseAffectTo(this);
       
		Health -= damageAmount;
		vehicle.Health -= damageAmount * damageMultiplyer[level];
        
        vehicle.UpdateHealthText();
        
        if (vehicle.Health <= 0)
        {
            Destroy(vehicle.gameObject);
        }

        /*
		if (damageAmount != 0) {
			print("health of : " + this + " is: " + health);
			vehicle.ReportHealth(); 
		}
        */
    }

	public override int CauseAffectTo(AffectVisitable damagable) 
	{
		return 0;
	}

	protected override void OnCollisionEnter2D(Collision2D col) 
	{

		if (col.gameObject.GetComponent<TongueJoint>()) return;
		if (col.gameObject.GetComponent<ProjectileController>()) return;

		//if (vehicle.isColliding.Contains(col.gameObject)) return;
		//vehicle.isColliding.Add(col.gameObject);

		AffectVisitor damager = col.gameObject.GetComponent<AffectVisitor>();
		AffectVisitable damagable = gameObject.GetComponent<AffectVisitable>();

		if (damager == null)
		{
			//Debug.LogWarning("Non-damager " + col.gameObject.name + " collided with damageable " + gameObject.name + ", please implement AffectVisitor method on it, or exclude from check on this damagable. ");
			return;
		}

		damagable.AcceptAffectFrom(damager);

	}

	protected override void OnCollisionExit2D(Collision2D col) 
	{
		//vehicle.isColliding.Remove(col.gameObject);
	}

}
