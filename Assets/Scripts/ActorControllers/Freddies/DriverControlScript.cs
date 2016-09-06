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

    protected override void UpButtonReleased()
    {
        //Do button released logic
        Move(-1);
    }
	protected override void UpButtonPressed()
	{
		//Do button pressed logic
	}
	protected override void DownButtonReleased()
	{
		//Do button realeasd logic
        Move(1);
    }
	protected override void DownButtonPressed()
	{
		//Do button pressed logic
	}
	protected override void PrimaryButtonReleased()
	{
		//Do button realeasd logic
	}
	protected override void PrimaryButtonPressed()
	{
		//Do button pressed logic
        PerformAction();
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
