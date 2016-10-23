using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerManager: MonoBehaviour
{
    public enum playersStatus { READY, UNREADY, GO };
    public playersStatus status { get; private set; }
    private List<PlayerControlScript> players = new List<PlayerControlScript>();
    //private List<PlayerControlScript> players = new List<PlayerControlScript>();
    public int playerCount { get; private set; }
    private FreddySpawnScript spawnScript;
    public const int maxPlayers = 5;

    [HideInInspector]
    public StateManager stateManager;



    void Awake()
    {
        if (AirConsole.instance == null)
        {
            Debug.LogWarning("Airconsole not found , add it to your scene noob");
            return;
        }
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

    void Start()
    {
        //Spawn readypanels

    }

    /// <summary>
    /// Start the countdown before going to gameplay
    /// </summary>
    void StartCountDown()
    {

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

    void Update()
    {
        if (stateManager)
        {
            //If we are in the lobby check the lobby manager to see if all players are ready
            if(stateManager.currentState == StateManager.gameState.LOBBY)
            {

            }
        }
    }

    /// <summary>
    /// Lets Player Manager know all players are ready
    /// </summary>
    public void PlayersStatus(playersStatus input)
    {
        status = input;
        
    }

    public playersStatus PlayerStatus()
    {
        return status;
    }


    /// <summary>
    /// Spawn all players onto the vehicle and transitions to GAMEPLAY state
    /// </summary>
    public void SpawnPlayers()
    {

        if (!stateManager)//If for some reason we didn't find the state manager
        {
            //Please locate statemanager
            stateManager = UnityUtils.FindComponentOn<StateManager>("StateManager");
        }
        //Change to Gameplay State
        stateManager.LoadGameplayScene();
        //Find Vehicle gameobject
        
        //VehicleControlScript vehicle = Utils.FindComponentOn<VehicleControlScript>("Vehicle");
        VehicleControlScript vehicle = SingletonGodController.instance.vehicleControlScript;

        //Spawn Freddies
        SetActivePlayers();
        //vehicle.GetComponent<FreddySpawnScript>().InstantiatePlayers(3);//GetPlayerDeviceIds().Count
        //update playerCount instead of above line

        playerCount = 3; //does this need to be fixed victor? -Tyler
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
