using UnityEngine;
using System.Collections.Generic;

public class FreddySpawnScript : MonoBehaviour {

    private List<PlayerControlScript> freddies_ = new List<PlayerControlScript>();

    private int numPlayers_, maxPlayers = 5;

    public List<PlayerControlScript> Freddies
    {
        get
        {
            return freddies_;
        }
        private set
        {
            freddies_ = value;
        }
    }

    [ExposeProperty]
    public int NumPlayers
    {
        get { return numPlayers_; }
        set
        {
            if (value > 0 && value <= maxPlayers)
                numPlayers_ = value;
        }
    }

    void Awake()
    {
        InstantiatePlayers(5);

        /*
        //only self-initialize in debug mode.
        //otherwise require PlayerManager to invoke directly....
        StateManager sm = SingletonGodController.instance.stateManager;
        if (true || sm.currentState == StateManager.gameState.GAMEPLAY) //TODO REMOVE  || true.!!   
        {
<<<<<<< HEAD
            InstantiatePlayers(numPlayers_); //numPlayers_ or maxPlayers??
=======
            InstantiatePlayers(maxPlayers);
>>>>>>> 6c3ad22d40dc7e3b1a8a19d372aa0b4dc28a4757
        }
        else
        {
            PlayerManager pm = Utils.FindComponentOn<SingletonGodController>("SingletonGodController").GetComponent<PlayerManager>();
            InstantiatePlayers(pm.playerCount);
        }
        */
    }
    /// <summary>
    /// Debug instantiate all players manually
    /// </summary>
    public void InstantiatePlayers(int numPlayers)
    {
        if (numPlayers < 0)
            return;
        for (int i = 1; i <= numPlayers; i++)
        {
            InstantiatePlayer(i);
        }
    }
    /// <summary>
    /// Instantiates Player and decides position and assigns sprite
    /// </summary>
    /// <param name="playerNum">int position and color and type</param>
    /// <returns>PlayerConstrolScript of player created</returns>
    private PlayerControlScript InstantiatePlayer(int playerNum)
    {
        Vector3 position = Vector3.zero;
        PlayerControlScript freddy = null;

        switch (playerNum)
        {
            
            case 1:
                position = new Vector3(-0.65f, 1f, 0);
                freddy = Resources.Load<DriverControlScript>("Freddy_Driver");
                break;
            case 2:
                position = new Vector3(0.4f, -1.5f, 0);
                freddy = Resources.Load<GunnerControlScript>("Freddy_Gunner");
                break;
            case 3:
                position = new Vector3(-0.8f, -0.8f, 0);
                freddy = Resources.Load<GunnerControlScript>("Freddy_Gunner");
                break;
            case 4:
                position = new Vector3(0.8f, -0.8f, 0);
                freddy = Resources.Load<GunnerControlScript>("Freddy_Gunner");
                break;
            case 5:
                position = new Vector3(-0.4f, -1.5f, 0);
                freddy = Resources.Load<GunnerControlScript>("Freddy_Gunner");
                break;
            default:
                Debug.LogWarning("Unable to spawn player " + playerNum);
                break;
        }

        if (freddy != null)
        {
            Vector3 vehiclePos = this.transform.position;
            PlayerControlScript freddySpawn = Instantiate(freddy, position * transform.localScale.x + vehiclePos, Quaternion.identity) as PlayerControlScript;
            freddySpawn.transform.parent = transform;
            freddies_.Add(freddySpawn);
        }
        else
        {
            Debug.Log("Freddy failed to spawn.");
        }
        return freddy;
    }

}
