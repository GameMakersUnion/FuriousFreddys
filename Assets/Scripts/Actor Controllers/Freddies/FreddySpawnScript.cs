﻿using UnityEngine;
using System.Collections.Generic;

public class FreddySpawnScript : MonoBehaviour {

    private List<GunnerControlScript> freddies_ = new List<GunnerControlScript>();
    private int numPlayers_, maxPlayers = 4;

    public List<GunnerControlScript> Freddies
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

    // Use this for initialization
    void Awake()
    {
        NumPlayers = 4;
        for (int i = 1; i <= numPlayers_; i++)
        {
            InstantiatePlayer(i);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InstantiatePlayer(int playerNum)
    {
        Vector3 position = Vector3.zero;
        GunnerControlScript freddy = null;

        switch (playerNum)
        {
            case 1:
                position = new Vector3(-0.4f, -1.5f, 0);
                freddy = Resources.Load<GunnerControlScript>("Prefabs/FredRed");
                break;
            case 2:
                position = new Vector3(0.4f, -1.5f, 0);
                freddy = Resources.Load<GunnerControlScript>("Prefabs/FredYellow");
                break;
            case 3:
                position = new Vector3(-0.8f, -0.8f, 0);
                freddy = Resources.Load<GunnerControlScript>("Prefabs/FredBlue");
                break;
            case 4:
                position = new Vector3(0.8f, -0.8f, 0);
                freddy = Resources.Load<GunnerControlScript>("Prefabs/FredGreen");
                break;
            default:
                Debug.LogWarning("Unable to spawn player " + playerNum);
                break;
        }

        if (freddy != null)
        {
            Vector3 vehiclePos = this.transform.position;
            GunnerControlScript freddySpawn = Instantiate(freddy, position * transform.localScale.x + vehiclePos, Quaternion.identity) as GunnerControlScript;
            freddySpawn.transform.parent = transform;
            freddies_.Add(freddySpawn);
        }
    }

}