using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;

/*
The component allows the current gameobject to receive messages from the gamepad
-Victor
*/

public class GamepadReceiver : MonoBehaviour {

    public int playerNumber;
	//private PlayerControlScript playerControlScript;
    public UnityEvent upButtonReleased;
    public UnityEvent upButtonPressed;
    public UnityEvent downButtonReleased;
    public UnityEvent downButtonPressed;
    public UnityEvent primaryButtonReleased;
    public UnityEvent primaryButtonPressed;
    public UnityEvent secondaryButtonReleased;
    public UnityEvent secondaryButtonPressed;
    public UnityEvent onConnect;
    public UnityEvent onDisconnect;

    // Use this for initialization
    void Awake()
    {
        //Register on Airconsole
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onMessage += OnMessage;

        //  = new Dictionary<string, UnityEvent>();

        if (upButtonReleased == null)
            upButtonReleased = new UnityEvent();
        if (upButtonPressed == null)
            upButtonPressed = new UnityEvent();
        if (downButtonReleased == null)
            downButtonReleased = new UnityEvent();
        if (downButtonPressed == null)
            downButtonPressed = new UnityEvent();
        if (primaryButtonReleased == null)
            primaryButtonReleased = new UnityEvent();
        if (primaryButtonPressed == null)
            primaryButtonPressed = new UnityEvent();
        if (secondaryButtonReleased == null)
            secondaryButtonReleased = new UnityEvent();
        if (secondaryButtonPressed == null)
            secondaryButtonPressed = new UnityEvent();
    }

	void Start()
	{
		print("GamePad Number " + playerNumber);
	}

    void OnReady(string code)
    {
        //Log to on-screen Console
        Debug.Log("AirConsole is ready!");

        //Mark Buttons as Interactable as soon as AirConsole is ready
        Button[] allButtons = (Button[])GameObject.FindObjectsOfType((typeof(Button)));
        foreach (Button button in allButtons)
        {
            button.interactable = true;
        }
        //When ready make sure everyone is on store screen
        BroadcastMessageToAllDevices("screen-driver");
    }

    public void BroadcastMessageToAllDevices(string message = "")
    {
        AirConsole.instance.Broadcast(message);
    }

    void OnMessage(int device_id, JToken data)
    {
        //Debug.Log((string)data);
        //Debug.Log(device_id);
        int from = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
		//Debug.Log(from + " == " + playerNumber);
        if (from == playerNumber) { //Only invoke methods if its the player they want
            Debug.Log(playerNumber);
            Debug.Log((string)data);
            switch ((string)data)
            {
                case "up-up": upButtonReleased.Invoke(); break;
				case "up-down": upButtonPressed.Invoke(); break;
                case "down-up": downButtonReleased.Invoke(); break;
                case "down-down": downButtonPressed.Invoke(); break;
                case "primary-up": primaryButtonReleased.Invoke(); break;
                case "primary-down": primaryButtonPressed.Invoke(); break;
                case "secondary-up": secondaryButtonReleased.Invoke(); break;
                case "secondary-down": secondaryButtonPressed.Invoke(); break;
            }
        }
        
    }


    // Update is called once per frame
    void Update(){
    }
}
