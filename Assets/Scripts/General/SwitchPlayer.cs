using UnityEngine;
using System.Collections.Generic;

public class SwitchPlayer : MonoBehaviour {

    private List<PlayerControlScript> Players;

    private PlayerControlScript Player;

	void Start () {

        GameObject vehicle = GameObject.FindGameObjectWithTag("Vehicle");
        if (vehicle == null)
        {
            vehicle = SingletonGodController.instance.vehicle;
        }
        Players = vehicle.GetComponent<FreddySpawnScript>().Freddies;
        Player = Players[0];
	}
	
    void Update()
    {

        if (Players.Contains(Player)) { 
            if (Input.GetKeyDown(KeyCode.Q))
                CyclePlayers();
        
            if (Input.GetKey(KeyCode.Space))
                Player.PerformAction();
        }
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
    }

    //something to start the game with
    void AssignPlayers()
    {

    }

    void CyclePlayers()
    {
        int current = Players.IndexOf(Player);
        if (++current >= Players.Count)
            current = 0;
       // Debug.Log(currPlayer);
        Player = Players[current];
    }

}
