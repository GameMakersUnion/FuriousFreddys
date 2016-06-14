using UnityEngine;
using System.Collections;

public class Switcher : MonoBehaviour {

    public PlayerControlScript[] Players;

    private PlayerControlScript Player;
    private int currPlayer;

	void Start () {
        currPlayer = 0;
        Player = Players[currPlayer];
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CyclePlayers();

    }

	void FixedUpdate () {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
            Player.Move(-1);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
            Player.Move(1);
    }

    void CyclePlayers()
    {
        if (++currPlayer >= Players.Length)
            currPlayer = 0;
        Debug.Log(currPlayer);
        Player = Players[currPlayer];
    }

}
