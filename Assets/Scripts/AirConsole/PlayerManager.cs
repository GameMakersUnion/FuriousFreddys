using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerManager: MonoBehaviour
{
    //private List<int> badguys = new List<int>();

    void Awake()
    {
        // register events
        //AirConsole.instance.onReady += OnReady;
        //AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        //AirConsole.instance.onDisconnect += OnDisconnect;
        //AirConsole.instance.onDeviceStateChange += OnDeviceStateChange;
        //AirConsole.instance.onCustomDeviceStateChange += OnCustomDeviceStateChange;
        //AirConsole.instance.onDeviceProfileChange += OnDeviceProfileChange;
        //AirConsole.instance.onAdShow += OnAdShow;
        //AirConsole.instance.onAdComplete += OnAdComplete;
        //AirConsole.instance.onGameEnd += OnGameEnd;
    }

    void OnConnect(int device_id)
    {
        SetActivePlayers();//Reassign the player numbers
        //Send player count to player spawner
    }

    void OnDisconnect(int device_id)
    {
        SetActivePlayers();//Reassign the player numbers
        //Send player count to player spawner
    }
    /// <summary>
    /// Set the players as active
    /// </summary>
    public void SetActivePlayers()
    {
        //Set the currently connected devices as the active players (assigning them a player number)
        AirConsole.instance.SetActivePlayers();
    }

    /// <summary>
    /// Gets all player device ids
    /// </summary>
    public List<int> GetPlayerDeviceIds()
    {
        return AirConsole.instance.GetControllerDeviceIds();
    }

    /// <summary>
    /// Gets single player device id from a player number
    /// </summary>
    public int GetPlayerDeviceId(int player)
    {
        return AirConsole.instance.ConvertPlayerNumberToDeviceId(player);
    }

    void OnDestroy()
    {

        // unregister events
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onConnect -= OnConnect;
            AirConsole.instance.onDisconnect -= OnDisconnect;
         
        }
    }
}
