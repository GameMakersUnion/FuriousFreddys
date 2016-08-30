using System;
using UnityEngine;

public abstract class PlayerControlScript : EntityControlScript {

    public int playerNumber = 0; //Their player number
    public bool ready=false;


    private bool keyPress;
    protected Transform tf;
    protected GamepadReceiver receiver;

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
        receiver.upButtonPressed.AddListener(delegate { UpButtonPressed(); });

        receiver.downButtonPressed.AddListener(UpButtonPressed);

    }

	protected override void OnCollisionEnter2D(Collision2D col)
	{

	}

	protected override void OnCollisionExit2D(Collision2D col)
	{
	
	}

    protected abstract void UpButtonPressed();

    public abstract void PerformAction();

	//do nothing
	public override void AcceptDamageFrom(DamageVisitor damager)
	{

	}

}
