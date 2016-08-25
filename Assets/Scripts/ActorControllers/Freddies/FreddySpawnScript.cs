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

        NumPlayers = 5;
        for (int i = 1; i <= numPlayers_; i++)
        {
            InstantiatePlayer(i);
        }

    }
    /// <summary>
    /// Instantiates Player and decides position and assigns sprite
    /// </summary>
    /// <param name="playerNum">int position and color and type</param>
    /// <returns>PlayerConstrolScript of player created</returns>
    public PlayerControlScript InstantiatePlayer(int playerNum)
    {
        Vector3 position = Vector3.zero;
        PlayerControlScript freddy = null;

        switch (playerNum)
        {
            
            case 1:
                position = new Vector3(-0.65f, 1.35f, 0);
                freddy = Resources.Load<DriverControlScript>("FredOrange");
                break;
            case 2:
                position = new Vector3(0.4f, -1.5f, 0);
                freddy = Resources.Load<GunnerControlScript>("FredYellow");
                break;
            case 3:
                position = new Vector3(-0.8f, -0.8f, 0);
                freddy = Resources.Load<GunnerControlScript>("FredBlue");
                break;
            case 4:
                position = new Vector3(0.8f, -0.8f, 0);
                freddy = Resources.Load<GunnerControlScript>("FredGreen");
                break;
            case 5:
                position = new Vector3(-0.4f, -1.5f, 0);
                freddy = Resources.Load<GunnerControlScript>("FredRed");
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
