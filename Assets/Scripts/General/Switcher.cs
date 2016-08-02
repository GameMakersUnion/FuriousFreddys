using UnityEngine;
using System.Collections;

public class Switcher : MonoBehaviour {

    private PlayerControlScript[] Players;

    private PlayerControlScript Player;
    private int currPlayer;

	void Start () {
        

        currPlayer = 0;
        GameObject vehicle = GameObject.FindGameObjectWithTag("Vehicle");
        PlayerControlScript[] gunnerArray = vehicle.GetComponent<FreddySpawnScript>().Freddies.ToArray();

        Players = new PlayerControlScript[1 + gunnerArray.Length];
        Players[0] = vehicle.GetComponent<PlayerControlScript>();

        gunnerArray.CopyTo(Players, 1);
        Player = Players[currPlayer];
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CyclePlayers();
        if (Input.GetKeyDown(KeyCode.Space))
            Player.Shoot();
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
        else
        {
            Player.Move(0);
        }
    }

    void CyclePlayers()
    {
        if (++currPlayer >= Players.Length)
            currPlayer = 0;
       // Debug.Log(currPlayer);
        Player = Players[currPlayer];
    }

}
