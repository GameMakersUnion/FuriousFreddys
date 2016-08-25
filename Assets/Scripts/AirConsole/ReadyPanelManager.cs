using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class ReadyPanelManager : MonoBehaviour {

    public int playerNumber = 0; //Player Number
    private int deviceId = 0;//0=Screen
    private Image image;

	// Use this for initialization
	void Start () {
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
        image = GetComponent<Image>();
    }

    void OnConnect(int device_id)
    {
        AirConsole.instance.SetActivePlayers();
        int convertedPlayerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (playerNumber == convertedPlayerNumber)
        {
            deviceId = convertedPlayerNumber;
            if (image)
                image.color = Color.red;
        }
        Debug.Log("Player Number:"+playerNumber);
        Debug.Log("Coverted Player Number:"+convertedPlayerNumber);
        Debug.Log("Device Number:" + device_id);

    }

    void OnDisconnect(int device_id)
    {
        int convertedPlayerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (playerNumber == convertedPlayerNumber)
        {
            deviceId = convertedPlayerNumber;
            if (image)
                image.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update () {
  
	}
}
