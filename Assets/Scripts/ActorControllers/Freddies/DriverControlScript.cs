using UnityEngine;
using System.Collections;
using System;

public class DriverControlScript : PlayerControlScript {

    private VehicleControlScript vehicle;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        vehicle = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<VehicleControlScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Move(int direction)
    {
        vehicle.Move(direction);
    }

    public override void PerformAction()
    {
        //powerup or buff or w/e
    }

    

}
