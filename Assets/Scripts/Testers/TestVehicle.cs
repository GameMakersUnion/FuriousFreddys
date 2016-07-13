using UnityEngine;
using System.Collections;

public class TestVehicle : MonoBehaviour {

	void Start () {
        //transform.rotation = Quaternion.EulerRotation(0, 0, 0);
        transform.Rotate(0, 0, 20f);
	
	}
	
	void Update () {
	
        Vector2 blah = Velocity;
		DebugDrawVelocityRays(blah);
        print(blah);
    }
    


    public Vector2 Velocity
    {
        get
        {
            Vector2 foundVelocity = FindVelocity();
            return foundVelocity;
        }
    }

    private Vector2 FindVelocity(){

        LevelController lc = SetAndGetLevelController();
        float yAngle = transform.eulerAngles.z;
        float ySpeed = lc.ScrollSpeed;
        float xSize = UseTrigonometryGetSideSineLaw(yAngle, ySpeed);

        float xAngle = 90 - yAngle;
        float ySize = UseTrigonometryGetSideSineLaw(xAngle, xSize);


        Vector2 velocity = new Vector2(xSize, ySize);
        return velocity;

    }

    private float UseTrigonometryGetSideSineLaw(float angleA, float sideA)
    {
        //requires 90° triangle 
        float angleB = 90 - angleA; 
        float sideB = sideA * Mathf.Sin( angleA ) / Mathf.Sin( angleB );
        //print(Mathf.RoundToInt(angleA) + "," + Mathf.RoundToInt(angleB) + "," + Mathf.RoundToInt(sideA), + ",..."+ Mathf.RoundToInt(sideB));
        return sideB;
    }


    LevelController levelController;  //yucky side effect state
    private LevelController SetAndGetLevelController()
    {
        if (levelController == null)
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }
        return levelController;
    }

	private void DebugDrawVelocityRays(Vector2 velocity){
		Debug.DrawRay(transform.position, velocity, Color.white);
		Debug.DrawRay(transform.position, new Vector2(velocity.x, 0), Color.blue);
		Debug.DrawRay(transform.position, new Vector2(0, velocity.y), Color.red);
	}
}