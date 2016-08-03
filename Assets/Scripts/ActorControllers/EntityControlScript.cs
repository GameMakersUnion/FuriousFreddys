using UnityEngine;
using System.Collections;

public abstract class EntityControlScript : MonoBehaviour {

    protected int health;
    protected float moveFactor;

    public abstract int Health { get; set; }

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void Move(int direction);

}
