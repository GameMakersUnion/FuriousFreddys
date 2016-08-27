using System;
using UnityEngine;

public abstract class PlayerControlScript : EntityControlScript {

    public int playerNumber = 0; //Their player number



    private bool keyPress;
    protected Transform tf;
    private GamepadReceiver receiver;

    public override int Health
    {
        get { return -1; }
        set {}
    }

    

    protected override void Start()
    {
        keyPress = false;
        tf = GetComponent<Transform>();
        receiver = gameObject.AddComponent<GamepadReceiver>();
        receiver.upButtonPressed.AddListener(UpButtonPressed);

    }

	protected override void OnCollisionEnter2D(Collision2D col)
	{

	}

	protected override void OnCollisionExit2D(Collision2D col)
	{
	
	}

    protected void UpButtonPressed()
    {
        Debug.Log("up button pressed");
    }

    public abstract void PerformAction();

	//do nothing
	public override void AcceptDamageFrom(DamageVisitor damager)
	{

	}

}
