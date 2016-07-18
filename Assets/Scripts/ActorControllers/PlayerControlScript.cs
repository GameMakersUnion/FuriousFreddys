using UnityEngine;
using System.Collections;

public abstract class PlayerControlScript : MonoBehaviour {

    public float moveFactor;

    protected Transform tf;

    protected virtual void Start()
    {
        tf = GetComponent<Transform>();
    }

    public abstract void Move(int direction);
    public abstract void Shoot();

 }
