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

		//Adds a listener to the gamepadreciever
		receiver.upButtonReleased.AddListener(UpButtonReleased);
		receiver.upButtonPressed.AddListener(UpButtonPressed);
		receiver.upButtonReleased.AddListener(UpButtonReleased);
		receiver.upButtonPressed.AddListener(UpButtonPressed);
		receiver.upButtonReleased.AddListener(PrimaryButtonReleased);
		receiver.upButtonPressed.AddListener(PrimaryButtonPressed);

    }

	protected override void OnCollisionEnter2D(Collision2D col)
	{

	}

	protected override void OnCollisionExit2D(Collision2D col)
	{
	
	}

    protected abstract void UpButtonPressed();
	protected abstract void UpButtonReleased();
	protected abstract void DownButtonPressed();
	protected abstract void DownButtonReleased();
	protected abstract void PrimaryButtonPressed();
	protected abstract void PrimaryButtonReleased();

    public abstract void PerformAction();

	//do nothing
	public override void AcceptDamageFrom(DamageVisitor damager)
	{

	}

}
