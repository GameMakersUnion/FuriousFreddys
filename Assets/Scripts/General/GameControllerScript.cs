using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    public float gameTime;
    public ZombieController zombieController;
    public SmokerControlScript smoker;
    GameObject van;
    private float endTime;
    private bool gameOver;

	// Use this for initialization
	void Start () {
        van = GameObject.FindWithTag("Vehicle");
        endTime = Time.time + gameTime;
        gameOver = false;
        SpawnZombies();
        

    }
	
	// Update is called once per frame
	void Update () {
   
        if (Time.time > endTime && !gameOver)
        {
            //game over, display text, zombies stop
            //Debug.Log("game over");
            gameOver = true;
        }
	}

    void SpawnZombies()
    {
        Vector3 vanPos = van.transform.position;
        Debug.Log(vanPos);

        for (int i = 0; i < 10; i++) {
            float x = Random.Range(-10.0f, 10.0f);
            float y = Random.Range(-5.0f, 5.0f);

            Vector3 spawnPos = vanPos - new Vector3(0 + x, 10 + y, 0);
            int g = Random.Range(1, 10);
            if (g == 7) {
                float xx = Random.Range(-10.0f, 10.0f);
                float yy = Random.Range(-5.0f, 5.0f);
                Vector3 spawnPos2 = vanPos - new Vector3(0 + xx, 10 + yy, 0);

                Instantiate(smoker, spawnPos2, Quaternion.identity);
            }
            Instantiate(zombieController, spawnPos, Quaternion.identity);
        }
    }

}
