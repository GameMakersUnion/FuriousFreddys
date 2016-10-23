using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour {

    private List<LobbyPanel> panels;
    private bool allReady = false;
    private PlayerManager playerManager;
    private int playerCount; //number of connected players
    private float countDown;
    private bool isCountDown;
    public Text countDownText;

	// Use this for initialization
	void Start () {
        panels = gameObject.GetComponentsInChildren<LobbyPanel>().ToList<LobbyPanel>();
        playerManager = UnityUtils.FindComponentOn<PlayerManager>("SingletonGodController");
	}
	
	// Update is called once per frame
	void Update () {
        allReady = true;
        //If all the panels are true
        playerCount = 0;
        foreach (LobbyPanel panel in panels)
        {
            //If player is connected
            if (panel.playerConnected)
            {
                playerCount++;
                if (!panel.playerReady)//If player is not ready
                {
                    allReady = false;
                }
            }
        }
        if (allReady && playerCount >= 2)
        {
            //Let player manager know players are ready
            if (playerManager && playerManager.status != PlayerManager.playersStatus.READY)
            {
                playerManager.PlayersStatus(PlayerManager.playersStatus.READY);
                //Start Countdown
                isCountDown = true;
                countDown = 3.0f;
                countDownText.text = "Game begins in 3 seconds!";
            }/*
            else if(playerManager.status != PlayerManager.playersStatus.READY)
            {
                playerManager.PlayersStatus(PlayerManager.playersStatus.UNREADY);
                //Stop Countdown
                isCountDown = false;
                countDownText.text = "Waiting for players to ready";
            }*/


        }else
        {
            
            playerManager.PlayersStatus(PlayerManager.playersStatus.UNREADY);
            isCountDown = false;
            if (playerCount < 2)
                countDownText.text = "You need at least 2 players";
            else
                countDownText.text = "Waiting for players to ready";
        }

        if (countDown >= 0 && isCountDown)
        {
            
            countDown -= Time.deltaTime;
            countDownText.text = "Game begins in " + System.Math.Round(countDown) + " seconds!";
            if (countDown == 0)
            {
                playerManager.PlayersStatus(PlayerManager.playersStatus.GO);
                countDownText.text = "GO!";
            }
        }

    }
}
