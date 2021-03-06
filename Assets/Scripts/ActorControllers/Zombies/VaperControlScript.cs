﻿    using UnityEngine;
    using System.Collections;

public class VaperControlScript : MasterZombieScript {
    public VaperHookShot vaperHookShoot;
    VaperHookShot hookShoot;
    public int tongueMinRange;
    public float speed;
    bool shooting;
    public int tongueMaxRange;
    VaperStatistics Vpstats = new VaperStatistics();

    public override void Start()
    {
        
        base.Start();

        shooting = false;
        hookShoot = (VaperHookShot)Instantiate(vaperHookShoot, this.transform.position, Quaternion.identity);
        hookShoot.gameObject.name = this.gameObject.name + " tongue";
        hookShoot.transform.parent = this.transform;
        hookShoot.setGameObject(this.gameObject);
        // print(tongueMinRange);

    }



    // Update is called once per frame
    public override void Update () {
        base.Update();

		Move();
		tryTongue();        
    }

	void Move()
	{
		float distanceTo = Vector3.Distance(base.start, base.destin);
		//print("start: " + base.start + " destin: " + base.destin + " distanceTo: " + distanceTo + " shooting: " + this.shooting);

		if (!shooting && distanceTo > tongueMinRange)
		{
			/// print("Im supposed to be doing shit");
			this.transform.position = Vector3.Lerp(start, destin, speed);
		}
	}

	void tryTongue()
	{
		float distanceTo = Vector3.Distance(base.start, base.destin);
		//print("start: " + base.start + " destin: " + base.destin + " distanceTo: " + distanceTo + " shooting: " + this.shooting);

		if (!(!shooting && distanceTo > tongueMinRange))
		{
			//shoot the fucking tongue
			transform.position = transform.position;
			if (!shooting)
			{
				shootTongue(start, destin, this.gameObject);
			}
		}
		if (shooting && distanceTo > tongueMaxRange || base.van == null)
		{
			breakTongue();
		}
	}

    void shootTongue(Vector3 s, Vector3 d, GameObject smoker ) {
		//it shoots the tongue?
		this.shooting = true;
		hookShoot.BuildRope();
		//print(this.gameObject.name + " i shoot tongue");
		hookShoot.destination = d;
		smoker.GetComponent<VaperControlScript>().Vpstats.hooksConnected++;

    }
    void breakTongue() {

        hookShoot.DestroyRope();
        this.rb.velocity = Vector2.zero;
        this.shooting = false;
        //print(this.gameObject.name + " tongue broke");

    }

	protected override void OnCollisionEnter2D(Collision2D col)
	{

	}

	protected override void OnCollisionExit2D(Collision2D col)
	{
		
	}
}
