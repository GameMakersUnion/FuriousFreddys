using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerManager: MonoBehaviour
{
    private List<PlayerControlScript> players = new List<PlayerControlScript>();
    private FreddySpawnScript spawnScript;

    void Awake()
    {
        // register events
        //AirConsole.instance.onReady += OnReady;
        //AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
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
    /// Add Player to Active Game
    /// </summary>
    /// <returns>PlayerControlScript</returns>
    PlayerControlScript AddPlayer()
    {
        //make new player
        PlayerControlScript player;
        if(players.Count == 0)
        {
            //PlayerControlScript player;
            GameObject gameobject = new GameObject("Driver");
            player = gameobject.AddComponent<DriverControlScript>();
            players.Add(player);
        }else
        {
            GameObject gameobject = new GameObject("Gunner");
            player = gameobject.AddComponent<GunnerControlScript>();
            players.Add(player);
        }
        //do airconsole stuff
        SetActivePlayers();
        return player;
    }

    /// <summary>
    /// Remove Player from the Active Game
    /// </summary>
    void RemovePlayer(PlayerControlScript player)
    {
        players.Remove(player);
        SetActivePlayers();
    }
    /// <summary>
    /// Gets the player Roles
    /// </summary>
    void GetPlayerRole()
    {

    }

    /// <summary>
    /// Set the players as active and assigns them a player number 0-4
    /// Every time this is run it will reset the player numbers
    /// </summary>
    public void SetActivePlayers()
    {
        //Set the currently connected devices as the active players (assigning them a player number)
        AirConsole.instance.SetActivePlayers();
        int i=0;
        foreach (PlayerControlScript player in players)
        {
            player.playerNumber = i;
            i++;
        }
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

    /// <summary>
    /// Get Active Players
    /// </summary>
    /// <returns>Your Soul</returns>
    public int GetActivePlayers()
    {
        return 0;
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
