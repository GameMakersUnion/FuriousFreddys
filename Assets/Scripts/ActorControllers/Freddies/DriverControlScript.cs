using System;
using UnityEngine;

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

    protected override void UpButtonPressed()
    {
        Debug.Log("up button pressed");
    }

    public override void Move(int direction)
    {
        vehicle.Move(direction);
    }

    public override void PerformAction()
    {
        //powerup or buff or w/e
    }

	public override int CauseDamageTo(DamageVisitable damagable)
	{
		return 0;
	}

}
