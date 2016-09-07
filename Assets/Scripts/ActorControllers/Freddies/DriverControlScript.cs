using System;
using UnityEngine;

public class DriverControlScript : PlayerControlScript {

    private VehicleControlScript vehicle;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        vehicle = GetComponentInParent<VehicleControlScript>();
        //vehicle = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<VehicleControlScript>();
	}
	

    public override void Move(int direction)
    {
        Debug.Log(playerNumber + " steering... in " + direction);
        vehicle.Move(direction);
    }

    public override void PerformAction()
    {
        Debug.Log(playerNumber + " buffing...");
        //powerup or buff or w/e
    }

	public override int CauseDamageTo(DamageVisitable damagable)
	{
		return 0;
	}

}
