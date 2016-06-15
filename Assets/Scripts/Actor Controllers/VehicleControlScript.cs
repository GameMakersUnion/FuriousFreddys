using UnityEngine;
using System.Collections;

public class VehicleControlScript : PlayerControlScript
{
    
	protected override void Start () {
        base.Start();
	}

    public override void Move(int direction)
    {
        //shift left or right
        tf.Translate(moveFactor * direction * Time.deltaTime, 0, 0);
    }

}
