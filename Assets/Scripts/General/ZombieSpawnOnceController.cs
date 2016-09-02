using UnityEngine;
using System.Collections;

public class ZombieSpawnOnceController : MonoBehaviour {

    [Tooltip("For debugging, takes performance hit")]
    public bool printDebugInfo;

    public float gameTime;
    public ZombieController zombieController;
    public VaperControlScript vaperControlScript;
    public FreddyFuckerController FFController;
    //I fucked up big time, ill need to fix this shit later.
    GameObject vehicle;
    VehicleControlScript vehicleScript;
    //end fuck up
    private float endTime;
    private bool gameOver;
    public int ZombiesSpawned;
    GameObject Gnmies;

    // Use this for initialization
    void Start () {
        vehicle = GameObject.FindWithTag("Vehicle");
        vehicleScript = vehicle.GetComponent<VehicleControlScript>();
        endTime = Time.time + gameTime;
        gameOver = false;
        Gnmies = new GameObject("NMIES");
        SpawnZombies();


    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("health is " + vehicleScript.Health);
        if (vehicleScript.Health  <= 0 &&  !gameOver)
        {
            Debug.Log("game over");
            // print("game over");
            Destroy(vehicle);
            gameOver = true;
            
        }
	}

    void SpawnZombies()
    {
        Vector3 vanPos = vehicle.transform.position;


        for (int i = 0; i < ZombiesSpawned; i++) {
            float x = Random.Range(-10.0f, 10.0f);
            float y = Random.Range(-5.0f, 5.0f);

            Vector3 spawnPos = vanPos - new Vector3(0 + x, 10 + y, 0);
         
            //int g = Random.Range(1, 10);
            int g = 7;
            if (g == 7) {
                float xx = Random.Range(-10.0f, 10.0f);
                float yy = Random.Range(-5.0f, 5.0f);
                Vector3 spawnPos2 = vanPos - new Vector3(0 + xx, 10 + yy, 0);
                //spawnPos2 = new Vector3(0, -2, 0);

                 VaperControlScript vaper = (VaperControlScript) Instantiate(vaperControlScript, spawnPos2, Quaternion.identity);
                 vaper.gameObject.name = "Vaper" + i;
               vaper.transform.parent = Gnmies.transform;

                FreddyFuckerController ff = (FreddyFuckerController)Instantiate(FFController, spawnPos2, Quaternion.identity);
                ff.gameObject.name = "FreddyFucker" + i;
                ff.transform.parent = Gnmies.transform;
            }
            ZombieController zombie = (ZombieController)Instantiate(zombieController, spawnPos, Quaternion.identity);
            zombie.gameObject.name = "Zombie" + i;
            zombie.transform.parent = Gnmies.transform;
            //print(spawnPos + " " + i);
            print(zombie.transform.position +  " " + i);
        }
    }

	/**
	 * We don't need to see debug print lines every frame all the time.
	 * Toggle on/off by selecting "ZombieController" in hierarchy and setting checkmark in inspector on the "GameControllerScript" component
	 */
	public void print(object obj)
	{
		ZombieSpawnOnceController script = SingletonGodController.instance.gameControllerScript;

		if (script != null && script.printDebugInfo)
		{
			MonoBehaviour.print(obj);
		}
	}
}
