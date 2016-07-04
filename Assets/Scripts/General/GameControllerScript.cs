using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    public float gameTime;
    public ZombieController zombieController;
    public VaperControlScript vaperControlScript;
    //I fucked up big time, ill need to fix this shit later.
    GameObject van;
    VehicleControlScript vehicle;
    //end fuck up
    private float endTime;
    private bool gameOver;
    public int ZombiesSpawned;
	// Use this for initialization
	void Start () {
        van = GameObject.FindWithTag("Vehicle");
        vehicle = van.GetComponent<VehicleControlScript>();
        endTime = Time.time + gameTime;
        gameOver = false;
        SpawnZombies();
        

    }
	
	// Update is called once per frame
	void Update () {
        if (vehicle.currentHealth()  == 0&&  !gameOver)
        {
            print("game over");
            gameOver = true;
        }
	}

    void SpawnZombies()
    {
        Vector3 vanPos = van.transform.position;


        for (int i = 0; i < ZombiesSpawned; i++) {
            float x = Random.Range(-10.0f, 10.0f);
            float y = Random.Range(-5.0f, 5.0f);

            Vector3 spawnPos = vanPos - new Vector3(0 + x, 10 + y, 0);
            int g = Random.Range(1, 10);
            if (g == 7) {
                float xx = Random.Range(-10.0f, 10.0f);
                float yy = Random.Range(-5.0f, 5.0f);
                Vector3 spawnPos2 = vanPos - new Vector3(0 + xx, 10 + yy, 0);

               VaperControlScript vaper = (VaperControlScript) Instantiate(vaperControlScript, spawnPos2, Quaternion.identity);
                vaper.gameObject.name = "Vaper" + i;
            }
            ZombieController zombie = (ZombieController)Instantiate(zombieController, spawnPos, Quaternion.identity);
            zombie.gameObject.name = "Zombie" + i;
        }
    }

}
