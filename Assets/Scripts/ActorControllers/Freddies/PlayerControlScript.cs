using System;
using UnityEngine;

public abstract class PlayerControlScript : EntityControlScript {

    protected int playerNumber; //Their player number
    public bool ready=false;

    private bool upButtonPressed, downButtonPressed, primaryButtonPressed;
    protected Transform tf;
    protected GamepadReceiver receiver;

    public override int Health
    {
        get { return -1; }
        set {}
    }

	public int PlayerNumber
	{
		get { return playerNumber; }
		set { playerNumber = value; }
	}

    protected override void Start()
    {
		
        upButtonPressed = false;
        tf = GetComponent<Transform>();
        receiver = gameObject.AddComponent<GamepadReceiver>();
        receiver.playerNumber = playerNumber;
		print("PlayerScript Number "+ playerNumber);

        upButtonPressed = false;
        downButtonPressed = false;
        primaryButtonPressed = false;

		//Adds a listener to the gamepadreciever
		receiver.upButtonReleased.AddListener(UpButtonReleased);
		receiver.upButtonPressed.AddListener(UpButtonPressed);
		receiver.downButtonReleased.AddListener(DownButtonReleased);
		receiver.downButtonPressed.AddListener(DownButtonPressed);
		receiver.primaryButtonReleased.AddListener(PrimaryButtonReleased);
		receiver.primaryButtonPressed.AddListener(PrimaryButtonPressed);

    }

    protected override void Update()
    {
        base.Update();
        if (upButtonPressed) Move(-1);
        if (downButtonPressed) Move(1);
        if (primaryButtonPressed) PerformAction();
		if (receiver != null) { receiver.playerNumber = playerNumber; Debug.Log (playerNumber); } //Quick fix for mvp -- Victor
    }

    

    protected void UpButtonPressed() { upButtonPressed = true; }
    protected void UpButtonReleased() { upButtonPressed = false; }
    protected void DownButtonPressed() { downButtonPressed = true; }
    protected void DownButtonReleased() { downButtonPressed = false; }
    protected void PrimaryButtonPressed() { primaryButtonPressed = true; }
    protected void PrimaryButtonReleased() { primaryButtonPressed = false; }

    public abstract void PerformAction();

    protected override void OnCollisionEnter2D(Collision2D col)
    {

    }

    protected override void OnCollisionExit2D(Collision2D col)
    {

    }

    //do nothing
    public override void AcceptDamageFrom(DamageVisitor damager) {}

}
