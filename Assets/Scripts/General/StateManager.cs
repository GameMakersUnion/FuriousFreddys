using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    private enum gameState { SPLASH, LOBBY, GAMEPLAY, GAMEOVER, DEBUG };
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
            CheckGameOver();

    }

    void CheckGameOver()
    {
        if (vehicle.Health <= 0)
        {
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
        SceneManager.LoadScene("combined-Ian");
    }

    //called from the gameplay screen after the vehicle dies
    public void LoadGameOverScene()
    {
        currentState = gameState.GAMEOVER;
        //load whatever
    }

}
