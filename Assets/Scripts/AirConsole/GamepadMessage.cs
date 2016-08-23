using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GamepadMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void SendMessageToController(string message,int player = 0)
    {
        //Say Hi to the first controller in the GetControllerDeviceIds List.

        //We cannot assume that the first controller's device ID is '1', because device 1 
        //might have left and now the first controller in the list has a different ID.
        //Never hardcode device IDs!
        int idOfFirstController = AirConsole.instance.GetControllerDeviceIds()[player];

        AirConsole.instance.Message(idOfFirstController, message);

        //Log to on-screen Console
        //logWindow.text = logWindow.text.Insert(0, "Sent a message to first Controller \n \n");
    }
    //
    public void SendMessageToControllerToPlayerOne(string message)
    {
        SendMessageToController(message,0);
    }
    public void SendMessageToControllerToPlayerTwo(string message)
    {
        SendMessageToController(message, 1);
    }
    public void SendMessageToControllerToPlayerThree(string message)
    {
        SendMessageToController(message, 2);
    }
    public void SendMessageToControllerToPlayerFour(string message)
    {
        SendMessageToController(message, 3);
    }
    public void SendMessageToControllerToPlayerFive(string message)
    {
        SendMessageToController(message, 4);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
