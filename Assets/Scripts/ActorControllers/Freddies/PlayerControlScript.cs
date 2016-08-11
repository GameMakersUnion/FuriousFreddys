using UnityEngine;
using System.Collections;

public abstract class PlayerControlScript : EntityControlScript {

    public override int Health
    {
        get { return -1; }
        set {}
    }

    protected Transform tf;

    protected override void Start()
    {
        tf = GetComponent<Transform>();
    }

    
    public abstract void PerformAction();

 }
