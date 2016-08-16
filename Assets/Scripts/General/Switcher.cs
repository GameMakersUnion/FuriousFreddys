using UnityEngine;
using System.Collections;

public class Switcher : MonoBehaviour {

    private PlayerControlScript[] Players;

    private PlayerControlScript Player;
    private int currPlayer;

	void Start () {
        
        currPlayer = 0;
        GameObject vehicle = GameObject.FindGameObjectWithTag("Vehicle");
        Players = vehicle.GetComponent<FreddySpawnScript>().Freddies.ToArray();
        Player = Players[currPlayer];
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CyclePlayers();
        if (Input.GetKey(KeyCode.Space))
            Player.PerformAction();
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            Player.Move(-1);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            Player.Move(1);
        }
        /*
        else
        {
            Player.Move(0);
        }
        */
    }

    //something to start the game with
    void AssignPlayers()
    {

    }

    void CyclePlayers()
    {
        if (++currPlayer >= Players.Length)
            currPlayer = 0;
       // Debug.Log(currPlayer);
        Player = Players[currPlayer];
    }

}
