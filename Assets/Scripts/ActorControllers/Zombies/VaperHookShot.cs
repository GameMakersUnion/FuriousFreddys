using UnityEngine;
using System.Collections;

public class VaperHookShot : MonoBehaviour
{
    
    GameObject van;
    GameObject smoker;
    bool hooked;
    public Vector3 destination;
    private Rigidbody2D vrb;
    Transform target;
    public float resolution = 0.5F;     //  Sets the amount of joints there are in the rope (1 = 1 joint for every 1 unit)
    public float ropeDrag = 0.1F;       //  Sets each joints Drag
    public float ropeMass =5;           //  Sets each joints Mass
    public float ropeColRadius = 0.5F;  //  Sets the radius of the collider in the SphereCollider component
    private Vector3[] segmentPos;       //  DONT MESS!	This is for the Line Renderer's Reference and to set up the positions of the gameObjects
    private GameObject[] joints;        //  DONT MESS!	This is the actual joint objects that will be automatically created
    private LineRenderer line;          //  DONT MESS!	 The line renderer variable is set up when its assigned as a new component
    private int segments = 0;           //  DONT MESS!	The number of segments is calculated based off of your distance * resolution
    private bool rope = false;          //  DONT MESS!	This is to keep errors out of your debug window! Keeps the rope from rendering when it doesnt exist...

    //Joint Settings
    public Vector3 swingAxis = new Vector3(1, 0, 0);        //  Sets which axis the character joint will swing on (1 axis is best for 2D, 2-3 axis is best for 3D (Default= 3 axis))
    public float lowTwistLimit = -100.0F;                   //  The lower limit around the primary axis of the character joint. 
    public float highTwistLimit = 100.0F;                   //  The upper limit around the primary axis of the character joint.
    public float swing1Limit = 20.0F;
    void Awake() {
        
        ropeMass = 10;
		van = GameObject.FindGameObjectWithTag("Vehicle");
		if (van == null) return;
		target = van.GetComponent<Transform>();
		line = gameObject.GetComponent<LineRenderer>();
		vrb = GetComponentInParent<Rigidbody2D>();
        
        //BuildRope();
        //DestroyRope();
    }
 


    void Update()
    {

    }


    void LateUpdate()
    {
		UpdateRopePosition();
    }


    public void setGameObject(GameObject zom) {
        smoker = zom;
    }


    public void BuildRope()
    {
		if (target == null) return;

        line = gameObject.GetComponent<LineRenderer>();

        // Find the amount of segments based on the distance and resolution
        // Example: [resolution of 1.0 = 1 joint per unit of distance]

        segments = (int)(Vector3.Distance(smoker.transform.position, target.position) * resolution);
        line.SetVertexCount(segments);
        segmentPos = new Vector3[segments];
        joints = new GameObject[segments];
        segmentPos[0] = smoker.transform.position + Vector3.back;
        segmentPos[segments - 1] = target.position + Vector3.back;

        // Find the distance between each segment
        var segs = segments - 1;
        var seperation = ((target.position - smoker.transform.position) / segs);

        for (int s = 1; s < segments; s++)
        {
            // Find the each segments position using the slope from above
            Vector3 vector = (seperation * s) + smoker.transform.position;
            segmentPos[s] = vector;

            //Add Physics to the segments
            AddJointPhysics(s);
        }

        // Attach the joints to the target object and parent it to this object	
		HingeJoint2D end = target.gameObject.AddComponent<HingeJoint2D>();
        end.connectedBody = joints[joints.Length - 1].transform.GetComponent<Rigidbody2D>();

        // Rope = true, The rope now exists in the scene!
        rope = true;
    }

    void AddJointPhysics(int n)
    {
        joints[n] = new GameObject("Joint_" + n);
        joints[n].layer = 12;
        joints[n].transform.parent = transform;
        Rigidbody2D rigid = joints[n].AddComponent<Rigidbody2D>();
        // joints[n].GetComponent<Rigidbody2D>().gravityScale = 0;
        CircleCollider2D col = joints[n].AddComponent<CircleCollider2D>();
        HingeJoint2D ph = joints[n].AddComponent<HingeJoint2D>();
		joints[n].AddComponent<TongueJoint>();
		joints[n].transform.position = segmentPos[n] + Vector3.back;

        rigid.drag = ropeDrag;
        rigid.mass = ropeMass;

		// print( joints[n].GetComponent<Rigidbody2D>().mass + " " + rigid.mass + " " + ropeMass);
        col.radius = ropeColRadius;

        if (n == 1)
        {
            ph.connectedBody = smoker.transform.GetComponent<Rigidbody2D>();
        }
        else
        {
            ph.connectedBody = joints[n - 1].GetComponent<Rigidbody2D>();
        }

    }

	public void UpdateRopePosition() {

		if (target == null) return;
		// Does rope exist?  If so, update its position
        if (rope)
        {
            for (int i = 0; i < segments; i++)
            {
                if (i == 0)
                {
                    line.SetPosition(i, smoker.transform.position);
                }
                else
                if (i == segments - 1)
                {
                    line.SetPosition(i, target.transform.position);
                }
                else
                {
                    line.SetPosition(i, joints[i].transform.position);
                }
            }
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
	}

    public void DestroyRope()
    {
		if (joints != null)
		{
			// Stop Rendering Rope then Destroy all of its components
			rope = false;
			for (int dj = 0; dj < joints.Length; dj++)
			{
				Destroy(joints[dj]);
			}
			if (target != null)
			{
				Destroy(target.GetComponent<HingeJoint2D>());
			}
		}
        segmentPos = new Vector3[0];
        joints = new GameObject[0];
        segments = 0;
    }

}
