using UnityEngine;

public class ZombWaveController : MonoBehaviour {

    /*
     * Factors of level difficulty:
     * number of zombies
     * speed of zombs
     * 
     * 
     */

    public int amount; //amount of zombs to be spawned
    public float waveSpawnRate;
    public MasterZombieScript[] Zombies;

    private float lowBoundX, highBoundX, lowBoundY, highBoundY; //bounds for spawn positions
    private float nextWaveSpawn;

    void Start ()
    {
        lowBoundX = -8.5f;
        highBoundX = 8.5f;
        lowBoundY = -25f;
        highBoundY = -20f;

        nextWaveSpawn = Time.time + waveSpawnRate;
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
