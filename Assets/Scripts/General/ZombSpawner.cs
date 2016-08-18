using UnityEngine;
using System.Collections;

public class ZombSpawner : MonoBehaviour {

    public int amount; //amount of zombs to be spawned
    public float waveSpawnRate;
    public ZombieController[] Zombies;

    private float lowBoundX, highBoundX, lowBoundY, highBoundY; //bounds for spawn positions
    private float nextWaveSpawn;

    void Start ()
    {
        //amount = 10;
        lowBoundX = -8.5f;
        highBoundX = 8.5f;
        lowBoundY = -25f;
        highBoundY = -20f;

        nextWaveSpawn = Time.time + waveSpawnRate;
        Zombies[0].speed = 0.02f;
	}
	
	void Update ()
    {
	    if (Time.time > nextWaveSpawn)
        {
            SpawnWave();
            nextWaveSpawn = Time.time + waveSpawnRate;
        }
	}

    void SpawnWave()
    {

        for (int i = 0; i < Zombies.Length; i++)
        {
            for (int j = 0; j < amount; j++)
            {
                float x = Random.Range(lowBoundX, highBoundX);
                float y = Random.Range(lowBoundY, highBoundY);
                Instantiate(Zombies[0].gameObject, new Vector3(x, y, 0), Quaternion.identity);
            }
        }

    }

}
