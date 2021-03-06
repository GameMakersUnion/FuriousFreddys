﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class VehicleControlScript : EntityControlScript
{

	// required to limit multiple executions for a single OnCollisionEnter2D to once per instance
	[HideInInspector]
	public List<GameObject> isColliding = new List<GameObject>();
    VehicleDriverStatistics stats;
	Rigidbody2D rb;
	Gridilizer gr;
    Text HealthText;

    public override int Health
    {
        get { return health; }
        set { health = value; }
    }

    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        gr = this.GetComponent<Gridilizer>();

    }

    protected override void Start () {
        base.Start();
        health = 1500;

        HealthText = GameObject.Find("TruckHealth").GetComponent<Text>();
        if (HealthText == null) Debug.Log("HealthText has not been found");
        UpdateHealthText();

        moveFactor = 100000f;

        PlayerManager pm = UnityUtils.FindComponentOn<SingletonGodController>("SingletonGodController").GetComponent<PlayerManager>();
        FreddySpawnScript fs = this.GetComponent<FreddySpawnScript>();
        fs.InstantiatePlayers(pm.playerCount);

    }

    public override void Move(int direction)
    {

        //shift left or right

        //tf.Translate(moveFactor * direction * Time.deltaTime, 0, 0);

        //rb.velocity = new Vector3(moveFactor * direction * Time.deltaTime, 0, 0);
		if (rb == null) return;
        rb.AddForce(new Vector2(moveFactor * direction, 0.0f), ForceMode2D.Force);
    }
    protected override void Update()
    {
        base.Update();

		RealignRotationToRoad();

    }

	protected override void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<TongueJoint>()) return;
		if (col.gameObject.GetComponent<ProjectileController>()) return;

		AffectVisitor damager = col.gameObject.GetComponent<AffectVisitor>();
		AffectVisitable damagable = gameObject.GetComponent<AffectVisitable>();
		if (damager == null)
		{
			//Debug.LogWarning("Non-damager " + col.gameObject.name + " collided with damageable " + gameObject.name + ", please implement AffectVisitor method on it, or exclude from check on this damagable. ");
			return;
		}

		damagable.AcceptAffectFrom(damager);
	}

	protected override void OnCollisionExit2D(Collision2D col) { } 

	//protected override void OnCollisionExit2D(Collision2D col)
	//{
	//	isColliding.Remove(col.gameObject);
	//}


	protected void RealignRotationToRoad()
	{
		// this is broken car is supposed to realign itself and try to return to the upright position

		//print(this.transform.rotation.eulerAngles.z);
		Vector3 rotation = this.transform.rotation.eulerAngles;

		if (rotation.z < 180)
		{
			transform.rotation = Quaternion.Euler((Vector3.Lerp(rotation, Vector3.zero, 0.05f)));
		}
		else
		{
			transform.rotation = Quaternion.Euler((Vector3.Lerp(rotation, new Vector3(0, 0, 360), 0.05f)));

		}


		if (Mathf.Abs(rotation.z - 360) < 0.5 || (rotation.z - 360) < 0.5)
		{
			this.rb.angularVelocity = 0;

		}

		//if (health <= 0) Destroy(gameObject);

	}

	public void UpdateHealthText()
	{
		HealthText.text = "Truck Health: " + health;
	}


	public override void AcceptAffectFrom(AffectVisitor damager)
	{
        //please see the version of this method in CellController
    }

    public override int CauseAffectTo(AffectVisitable damagable)
	{
		return 0;
	}



}
