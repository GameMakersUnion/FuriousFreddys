using UnityEngine;
using System.Collections;

/**
 * Why Singletons patterns are necessary
 * 
 * Benefits:
 * (1) Is because they provide a single instance to collect references that are painfully inefficient to load,
 * therefore avoiding repeatedly unnecessary.
 * 
 * (2) They persist across scene-loads, thereby keeping valuable information such as scores, money, damage, 
 * players selected, etc.
 * 
 * Technical explanation:
 * http://answers.unity3d.com/questions/576969/create-a-persistent-gameobject-using-singleton.html
 * http://wiki.unity3d.com/index.php/Singleton
 * https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
 */
public class SingletonGodController : MonoBehaviour {

    /**
     *  This instance variable is key to using the singleton pattern.
     */
    public static SingletonGodController instance = null;

    /**
     * Reference this veraible elsewhere outside this class by calling ``SingletonGodController.instance.iHasVariable``
     * Because this class is setup during the Awake phase, you can access it's variables after all Awakes complete, 
     * e.g. in the Start or Update phase of the Unity Lifecycle.
     */
    [Tooltip("example variable to demonstrate how to access variable from Singleton")]
    public bool iHasVariable;

    [HideInInspector]
    public ZombieSpawnOnceController gameControllerScript;

    [HideInInspector]
    public VehicleControlScript vehicleControlScript;
    [HideInInspector]
    public GameObject vehicle;

    [HideInInspector]
    public PlayerManager playerManager;

    [HideInInspector]
    public SwitchPlayer switchPlayer;

    [HideInInspector]
    public StateManager stateManager;

    [HideInInspector]
    public AnalyticsManager analyticsManager;

    /**
     * This Awake method block is the heart of a singleton. Don't play with.
     */
    void Awake () {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            gameControllerScript = Utils.FindComponentOn("ZombieController");

            playerManager = gameObject.GetComponent<PlayerManager>();
            
			//ugly hack?
			stateManager = gameObject.GetComponent<StateManager>();
            stateManager.DetermineScene();
            stateManager.SetState();
			if (stateManager.currentState == StateManager.gameState.GAMEPLAY)
			{
				vehicle = stateManager.LoadVehicle();
			}

            playerManager.stateManager = stateManager;
            analyticsManager = gameObject.GetComponent<AnalyticsManager>();

            if (stateManager.currentState == StateManager.gameState.GAMEPLAY)
            {
                vehicle = stateManager.LoadVehicle();
            }

        }
        else
        {
            Destroy(gameObject);
        }
	}

    /**
     *  Do whatever you want in the Start method.
     */
    void Start()
    {
        
    }
}
