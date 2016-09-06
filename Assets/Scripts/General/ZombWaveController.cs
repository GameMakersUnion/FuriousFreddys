using UnityEngine;
using System.Collections.Generic;

public class ZombWaveController : MonoBehaviour
{

    /*
     * Factors of level difficulty:
     * what types of zombs (wave)
     * how many of each type of zomb (wave)
     * how often waves spawn (wave)
     * zomb health (zomb)
     * zomb damage (zomb)
     * speed of zombs (zomb)
     * 
     * How does difficulty increment over time?
     * -every so often, increase one of the above factors
     * 
     */

    private int minAmount, maxAmount; //min and max number of zombs to be spawned
    private float waveSpawnRate;
    private float diffChangeRate;

    public List<MasterZombieScript> Zombies;

    public GameObject testZomb;

    private float lowBoundX, highBoundX, lowBoundY, highBoundY; //bounds for spawn positions
    private float nextWaveSpawn, nextDiffChange;

    void Start()
    {
        lowBoundX = -8.5f;
        highBoundX = 8.5f;
        lowBoundY = -25f;
        highBoundY = -20f;

        minAmount = 3;
        maxAmount = 6;
        waveSpawnRate = 12f;
        diffChangeRate = 18f;

        nextWaveSpawn = Time.time + waveSpawnRate;
        nextDiffChange = Time.time + diffChangeRate;
    }

    void Update()
    {
        if (Time.time > nextWaveSpawn)
        {
            SpawnWave();
            nextWaveSpawn = Time.time + waveSpawnRate;
        }

        if (Time.time > nextDiffChange)
        {
            IncreaseDifficulty();
            nextDiffChange = Time.time + diffChangeRate;
        }

    }

    void IncreaseDifficulty()
    {
        Debug.Log("Things just got a bit more complicate...");

        int factor = Mathf.RoundToInt(Random.Range(0, 3));

        switch (factor)
        {
            case 0:
                maxAmount++;
                Debug.Log("Max zombs increased!");
                break;
            case 1:
                if (minAmount < maxAmount - 1)
                    minAmount++;
                Debug.Log("Min zombs increased!");
                break;
            case 2:
                if (waveSpawnRate > 3.5)
                    waveSpawnRate -= 0.5f;
                Debug.Log("Waves spawn more often!");
                break;
            default:
                Debug.Log("...or not.");
                break;
        }


    }

    void SpawnWave()
    {

        int amount = Random.Range(minAmount, maxAmount);

        for (int i = 0; i < Zombies.Count; i++)
        {
            int spawnAmount = amount / (i+1);
            for (int j = 0; j < spawnAmount; j++)
            {
                Debug.Log("spawning zombs");
                float x = Random.Range(lowBoundX, highBoundX);
                float y = Random.Range(lowBoundY, highBoundY);
                Instantiate(Zombies[i].gameObject, new Vector3(x, y, 0), Quaternion.identity);
            }
        }

    }

}
