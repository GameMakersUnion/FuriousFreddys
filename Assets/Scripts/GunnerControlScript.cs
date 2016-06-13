using UnityEngine;
using System.Collections;
using System;

public class GunnerControlScript : PlayerControlScript {
    
	protected override void Start () {
        base.Start();
    }

    public override void Move(int direction)
    {
        //rotate left or right
        
        tf.Rotate(0, 0, moveFactor * -direction);
    }

    public void Shoot()
    {
        //fire projectile
    }

}
