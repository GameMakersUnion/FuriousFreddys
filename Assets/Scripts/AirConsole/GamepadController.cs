using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

/*
The component allows a gameobject to send messages to the gamepad
-Victor
*/
public class GamepadController : MonoBehaviour {

    public List<GameObject> gameObjects;

	// Use this for initialization
	void Awake () {
    }


    public void SendMessageToController(int player=0)
    {
        //Say Hi to the first controller in the GetControllerDeviceIds List.

        //We cannot assume that the first controller's device ID is '1', because device 1 
        //might have left and now the first controller in the list has a different ID.
        //Never hardcode device IDs!
        int idOfFirstController = AirConsole.instance.GetControllerDeviceIds()[0];

        AirConsole.instance.Message(idOfFirstController, "Hey there, first controller!");

        //Log to on-screen Console
        //logWindow.text = logWindow.text.Insert(0, "Sent a message to first Controller \n \n");
    }

    // Update is called once per frame
    void Update () {
	
	}
}
