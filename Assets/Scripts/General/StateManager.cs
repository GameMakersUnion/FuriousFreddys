using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    private enum gameState { SPLASH, LOBBY, GAMEPLAY, GAMEOVER };
    private gameState currentState;
    private VehicleControlScript vehicle;

    void Start()
    {
        FindVehicleRef();

        currentState = gameState.SPLASH;
        Debug.Log("curr state is " + currentState);


    }

    void Update()
    {
        if (currentState == gameState.GAMEPLAY)
            CheckVehicleDeath();

    }

    void CheckVehicleDeath()
    {
        if (vehicle.Health <= 0)
        {
            //next state
            LoadGameOverScene();
        }
    }

    void FindVehicleRef()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Vehicle");
        if (temp)
        {
            vehicle = temp.GetComponent<VehicleControlScript>();
            Debug.Log("vehicle exists");
        }
    }

    public void LoadStartScene()
    {
        currentState = gameState.SPLASH;
        SceneManager.LoadScene("title-Tyler");
    }

    public void LoadLobbyScene()
    {
        currentState = gameState.LOBBY;
        SceneManager.LoadScene("ready-victor");
    }

    public void LoadGameplayScene()
    {
        FindVehicleRef();
        currentState = gameState.GAMEPLAY;
        SceneManager.LoadScene("combined-Ian");
    }

    public void LoadGameOverScene()
    {
        currentState = gameState.GAMEOVER;
        //load whatever
    }

}
