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
 
    public UnityEvent leftButton;
    public UnityEvent rightButton;
    public UnityEvent primaryButton;
    public UnityEvent secondaryButton;

    // Use this for initialization
    void Awake()
    {
        //Register on Airconsole
        AirConsole.instance.onMessage += OnMessage;

        if (leftButton == null)
            leftButton = new UnityEvent();
        if (rightButton == null)
            rightButton = new UnityEvent();
        if (primaryButton == null)
            primaryButton = new UnityEvent();
        if (secondaryButton == null)
            secondaryButton = new UnityEvent();
    }

    void OnMessage(int from, JToken data)
    {
        // Rotate the AirConsole Logo to the right
        if ((string)data == "left")
        {
            leftButton.Invoke();
        }

        // Rotate the AirConsole Logo to the right
        if ((string)data == "right")
        {
            rightButton.Invoke();
        }

        // Stop rotating the AirConsole Logo
        //'stop' is sent when a button on the controller is released
        if ((string)data == "primary")
        {
            primaryButton.Invoke();
        }

        //Show an Ad
        if ((string)data == "secondary")
        {
            secondaryButton.Invoke();
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
