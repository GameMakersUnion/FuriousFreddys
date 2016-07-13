using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;

/*
The component allows a gameobject to send messages to the gamepad
-Victor
*/
public class GamepadController : MonoBehaviour {

    public List<GameObject> gameObjects;
    public UnityEvent testCase;

    // Use this for initialization
    void Awake () {
        if (testCase == null)
            testCase = new UnityEvent();
    }

    public void OnMouseDown()
    {
        testCase.Invoke();
    }
    public void Foo()
    {
        Debug.Log("test");
    }
    // Update is called once per frame
    void Update () {
	
	}
}
