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

    public int playerNumber = 1; //This is the player number 1-5
    public UnityEvent upButtonUp;
    public UnityEvent upButtonDown;
    public UnityEvent downButtonUp;
    public UnityEvent downButtonDown;
    public UnityEvent primaryButtonUp;
    public UnityEvent primaryButtonDown;
    public UnityEvent secondaryButtonUp;
    public UnityEvent secondaryButtonDown;
    
  

    // Use this for initialization
    void Awake()
    {
        //Register on Airconsole
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onMessage += OnMessage;

        //  = new Dictionary<string, UnityEvent>();

        if (upButtonUp == null)
            upButtonUp = new UnityEvent();
        if (upButtonDown == null)
            upButtonDown = new UnityEvent();
        if (downButtonUp == null)
            downButtonUp = new UnityEvent();
        if (downButtonDown == null)
            downButtonDown = new UnityEvent();
        if (primaryButtonUp == null)
            primaryButtonUp = new UnityEvent();
        if (primaryButtonDown == null)
            primaryButtonDown = new UnityEvent();
        if (secondaryButtonUp == null)
            secondaryButtonUp = new UnityEvent();
        if (secondaryButtonDown == null)
            secondaryButtonDown = new UnityEvent();
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

    void OnMessage(int from, JToken data)
    {
        Debug.Log((string)data);
        Debug.Log(from);
        if (from == playerNumber) { //Only invoke methods if its the player they want
            Debug.Log("Wee");
            switch ((string)data)
            {
                case "up-up": upButtonUp.Invoke(); break;
                case "up-down": upButtonDown.Invoke(); break;
                case "down-up": downButtonUp.Invoke(); break;
                case "down-down": downButtonDown.Invoke(); break;
                case "primary-up": primaryButtonUp.Invoke(); break;
                case "primary-down": primaryButtonDown.Invoke(); break;
                case "secondary-up": secondaryButtonUp.Invoke(); break;
                case "secondary-down": secondaryButtonDown.Invoke(); break;
            }
        }
        
    }


    // Update is called once per frame
    void Update()
    {

    }
}
