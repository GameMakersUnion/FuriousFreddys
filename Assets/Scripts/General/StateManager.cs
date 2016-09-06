using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class StateManager : MonoBehaviour
{

    public enum gameState { SPLASH, LOBBY, GAMEPLAY, GAMEOVER, DEBUG_GAMEPLAY };
    public gameState currentState { get; private set; } 
    private VehicleControlScript vehicle;
    private Scene activeScene;

    void Start()
    {
        FindVehicleRef();

        currentState = gameState.SPLASH;
        Debug.Log("curr state is " + currentState);
        SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(OnSceneLoad);

        DetermineScene();
        SetState();
        
    }

    void OnSceneLoad(Scene s, LoadSceneMode c)
    {
        if (currentState == gameState.GAMEPLAY)
        {
            FindVehicleRef();
        }

    }

    void Update()
    {
        if (currentState == gameState.GAMEPLAY)
            CheckGameOver();
        

    }

    void CheckGameOver()
    {
        if (vehicle.Health <= 0)
        {
            LoadGameOverScene();
        }
    }

    public void DetermineScene()
    {
        activeScene = SceneManager.GetActiveScene();
    }

    public GameObject LoadVehicle()
    {
        GameObject vehicleResource = (GameObject)Resources.Load("Vehicle");
        GameObject vehicle = (GameObject)Instantiate(vehicleResource, Vector3.zero, Quaternion.identity);
        vehicle.name = "Vehicle";

        vehicle.GetComponent<FreddySpawnScript>().InstantiatePlayers(5);
        SingletonGodController.instance.switchPlayer = gameObject.GetComponent<SwitchPlayer>();

        return vehicle;
    }


    public void SetState()
    {
        if (activeScene.name == "classes-Tyler")
        {
            currentState = gameState.GAMEPLAY;
        }
        else if (activeScene.name == "combined-victor")
        {
            currentState = gameState.GAMEPLAY;
        }
        else if (activeScene.name == "ready-victor")
        {
            currentState = gameState.LOBBY;
        }
    }

    /*
    void OnLevelWasLoaded()
    {
        Debug.Log("WAAAATTT");
    }*/


    void FindVehicleRef()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Vehicle");
        if (temp)
        {
            vehicle = temp.GetComponent<VehicleControlScript>();
            Debug.Log("vehicle exists");
        }
    }

    //called when game starts up
    public void LoadStartScene()
    {
        currentState = gameState.SPLASH;
        SceneManager.LoadScene("title-Tyler");
    }

    //called by start button on splash screen, or by game over screen after a few seconds (?)
    public void LoadLobbyScene()
    {
        currentState = gameState.LOBBY;
        SceneManager.LoadScene("ready-victor");
    }

    //called in lobby screen, a few seconds after all players have connected and readied up (?)
    public void LoadGameplayScene()
    {
        FindVehicleRef();
        currentState = gameState.GAMEPLAY;
        SceneManager.LoadScene("combined-victor");

    }

    //called from the gameplay screen after the vehicle dies
    public void LoadGameOverScene()
    {
        currentState = gameState.GAMEOVER;

        //load whatever
    }

}
