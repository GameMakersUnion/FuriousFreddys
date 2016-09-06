using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour {

    public int playerNumber = 0; //Player Number
    private int deviceId = 0;//0=Screen
    private Image image;
    public bool playerReady;
    public bool playerConnected;
    public GamepadReceiver receiver;

	// Use this for initialization
	void Start () {
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
        image = GetComponent<Image>();
        playerReady = false;
        receiver = gameObject.GetComponent<GamepadReceiver>();
        receiver.playerNumber = playerNumber;

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
            playerConnected = true;
        }

       
    }

    void OnDisconnect(int device_id)
    {
        int convertedPlayerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (playerNumber == convertedPlayerNumber)
        {
            deviceId = convertedPlayerNumber;
            if (image)
                image.color = Color.white;
            playerConnected = false;
        }
        
    }

    // Update is called once per frame
    void Update () {
  
	}

    /// <summary>
    /// Change Player Ready State
    /// </summary>
    public void ReadyPlayer()
    {
        playerReady = !playerReady;
        if(playerReady)
            image.color = Color.green;
        else if (playerConnected)
            image.color = Color.red;
        else
            image.color = Color.white;
    }
}
