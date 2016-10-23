using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public bool MoveRoad;
    public bool ShowRoad;

	private float ROCK_SPAWN_RANGE = 5.0f;
    private float COLLIDER_THICKNESS = 2.0f;

    public float ScrollSpeed { get { return scrollSpeed; } } //Limited accessibility deliberately
    private float scrollSpeed = 0.2f;  //The speed to scroll the roadSegments at
	private bool isOnRoad = false;
	private int levelLength = 5;    //Amount of roadSegments to pass until level ends

    private List<GameObject> roadSegments;  //The list of GameObjects to hold the roadSegmentPrefabs
    public GameObject roadSegmentPrefab;    //The roadSegmentPrefab which the each segment is made from
    private SpriteRenderer srRoadSegment;      //The SpriteRenderer of the roadSegmentPrefab, used to get bounds
	private List<Collider2D> colRoadSegments;

	private List<GameObject> dirtSegments;
	public GameObject dirtSegmentPrefab;
	private SpriteRenderer srDirtSegment;

	private Collider2D colVehicle;

    private List<GameObject> rocks;  //The list of GameObjects to hold the rockPrefabs
    public GameObject rockPrefab;       //The rockPrefab

    private Sprite[] roadSegmentsResources;
    List<GameObject> roadSegmentsNew;
    private bool[,] validMappingMatrix;

    // Use this for initialization
    void Start () {
        initVariables();

		createGameBounds();

        GetSprites("segments");

        roadSegmentsNew = UnityUtils.createGameObjects(roadSegmentsResources);

        SetSegmentProperties(roadSegmentsNew);

        GetValidRoadSegmentTransitionMatrix();

        AddSegmentsDefaultOrder(roadSegmentsNew);


        print(roadSegmentsNew[0].GetComponent<Collider2D>().bounds.size.y);
        //if (MoveRoad)
        //{
        //    //Lists to hold GameObjects
        //    createRoadSegments();
        //}
    }

    // Update is called once per frame
    void Update() {

        if (MoveRoad) {
            //if (roadSegments == null)
            //{
            //    createRoadSegments();
            //}

			//Check what surface the vehicle is touching, adjust speed
			checkGroundType();

            //Iterate through roadSegments and adjust the y position by newYPos
            moveRoadSegments();

			//Iterate through dirtSegments and adjust the y position by newYPos
			//moveDirtSegments();

            //Generate obstacles randomly
            generateRock();

            //Iterate through rocks and adjust the y position
            moveRocks();

			//
			//checkIfLevelOver();
		}

        //Hide or Show Road
        if (ShowRoad != roadSegments[0].GetComponent<SpriteRenderer>().enabled)
            setRoadSegementsVisibility(ShowRoad);

    }

    void initVariables()
    {
        //Collider2D of the vehiclePrefab
        colVehicle = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<Collider2D>();

        roadSegments = new List<GameObject>();

        colRoadSegments = new List<Collider2D>();

        rocks = new List<GameObject>();

        //dirtSegments = new List<GameObject>();
    }

    public void GetSprites(string foldernameOrFilename)
    {
        this.roadSegmentsResources = Resources.LoadAll<Sprite>(foldernameOrFilename);
    }


    public void GetValidRoadSegmentTransitionMatrix()
    {
        parseMapping();
    }

    public void parseMapping()
    {
        List<string> mappedLines = getDummyMapping();

        int len = mappedLines.Count;
        bool[,] validMappingMatrix = new bool[len, len];

        foreach (string line in mappedLines){
           parseLineIntoValidMappingMatrix(line, ref validMappingMatrix);
        }
        //CSharpUtil.print(validMappingMatrix);

        this.validMappingMatrix = validMappingMatrix;

    }

    //replace with file reader
    private List<string> getDummyMapping()
    {
        return new List<string>(){
		    "1-1, 1-2, 1-7",
		    "2-3, 2-4, 2-9",
		    "3-3, 3-4, 3-9",
		    "4-5, 4-6, 4-8",
		    "5-5, 5-6, 5-8",
		    "6-7, 6-1, 6-2",
		    "7-8, 7-5, 7-6",
		    "8-9, 8-3, 8-4",
            "9-1, 9-2, 9-7",
        };
    }

    //limited to single-char 
    void parseLineIntoValidMappingMatrix(string line, ref bool[,] matrix)
    {
        bool beforeDash = true;
        //bool comesFrom = true, goesTo = false;
        int count = 0;
        int xCoord =-1, yCoord =-1;

        foreach (char c in line)
        {
            if (beforeDash  && c >= '0' && c <= '9') 
            {
                xCoord = c - '0' - 1;  //what's a more readable way, int.Parse(c)? fails, etc.
                beforeDash = false;
            }
            else if (!beforeDash  && c >= '0' && c <= '9')
            {
                yCoord = c - '0' - 1;
                matrix[xCoord, yCoord] = true;
                beforeDash = true;
            }
            else if (count == line.Length - 1)
            {
                count = 0;
                continue;
            }
            count++;
        }
    }

    void createGameBounds()
    {
        GameObject gameBounds = new GameObject("GameBounds");
        gameBounds.transform.parent = this.transform;
        BoxCollider2D bc1 = gameBounds.AddComponent<BoxCollider2D>();
        bc1.offset = new Vector2(-10.5f, 0);
        bc1.size = new Vector2(1, 40);

        BoxCollider2D bc2 = gameBounds.AddComponent<BoxCollider2D>();
        bc2.offset = new Vector2(10.5f, 0);
        bc2.size = bc1.size;
    }

    public void createRoadSegments()
    {
        //List to hold roadSegments
        //roadSegments = new List<GameObject>();
		//dirtSegments = new List<GameObject>();
		//rocks = new List<GameObject>();
		//colRoadSegments = new List<Collider2D>();

		//SpriteRenderer of the roadSegmentPrefab and dirtSegmentPrefab
		srRoadSegment = roadSegmentPrefab.GetComponent<SpriteRenderer>();
		srDirtSegment = dirtSegmentPrefab.GetComponent<SpriteRenderer>();

        SetSegmentScaleToFitScreen(srRoadSegment);
        SetSegmentScaleToFitScreen(srDirtSegment);

        addRoadSegmentsToList();
        //addDirtSegmentsToList();
    }


    //Set segment's scale by calculating fit to screen
    void SetSegmentScaleToFitScreen(SpriteRenderer sr)
    {
        float cameraHeight = Camera.main.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;
        Sprite sprite = sr.sprite;
        float unitWidth = sprite.rect.width / sprite.pixelsPerUnit;
        float unitHeight = sprite.rect.height / sprite.pixelsPerUnit;
        sr.transform.localScale = new Vector3(cameraWidth / unitWidth, cameraHeight / unitHeight);

    }

    void SetSegmentRotation(GameObject segment)
    {
        segment.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }

    void SetSegmentColliders(GameObject segment)
    {
        //placeholder
        PolygonCollider2D pc1 = segment.AddComponent<PolygonCollider2D>();
        //pc1.offset = new Vector2(0, -10.5f);
        //pc1.size = new Vector2(40, 1);


        //Vector2[] side1;
        //Vector2[] side2;
        List<Vector2> side1 = new List<Vector2>();
        List<Vector2> side2 = new List<Vector2>();

        foreach (Vector2 point in pc1.points)
        {
            //build side 1
            if (point.y >= 0 ) {
                side1.Add(point);
                //GameObject go = new GameObject(segment.name + " " +  side1.Count + " " + point);
                //go.transform.position = SpatialUtil.V2toV3(point);
                GameObject got = UnityUtils.AddTextToCanvas(point.ToString());
                got.transform.parent = segment.transform;
                got.transform.position = SpatialUtil.V2toV3(point);
                SetSegmentRotation(got);

            }

            //build side 2
            else
            {
                side2.Add(point);
                //GameObject go = new GameObject(side2.Count + " " + point);
                //go.transform.position = SpatialUtil.V2toV3(point);
            }



        }

        CSharpUtil.print(side1);
        CSharpUtil.print(side2);



        Vector2 end1_smallest = side1.OrderByDescending(i => i.y).FirstOrDefault();
        Vector2 end1_largest = side1.OrderByDescending(i => i.y).LastOrDefault();

        end1_smallest += new Vector2(0,2);
        end1_largest += new Vector2(0,2);

        side1.Add(end1_smallest);
        side1.Add(end1_largest);

        //pc1.points = side1.ToArray();


        Vector2 end2_smallest = side2.OrderByDescending(i => i.y).FirstOrDefault();
        Vector2 end2_largest = side2.OrderByDescending(i => i.y).LastOrDefault();

        end2_smallest += new Vector2(0, -2);
        end2_largest += new Vector2(0, -2);

        side2.Add(end2_smallest);
        side2.Add(end2_largest);


        //PolygonCollider2D pc1 = segment.AddComponent<PolygonCollider2D>();
        pc1.points = side2.ToArray();

        PolygonCollider2D pc2 = segment.AddComponent<PolygonCollider2D>();
        pc2.points = side2.ToArray();

        //show text at point -- cleaner in method
        //Text t = UnityUtil.AddTextToCanvas(pc1.points[0].ToString());
        //t.transform = SpatialUtil.V2toV3(point);

    }

    bool flag = false;

    void SetSegmentProperties(List<GameObject> segments)
    {
        foreach (GameObject segment in segments)
        {
            //SetSegmentScaleToFitScreen(segment.GetComponent<SpriteRenderer>());
            SetSegmentRotation(segment);
            SetSegmentColliders(segment);
        }
    }

    void AddSegmentsDefaultOrder(List<GameObject> segments)
    {
        foreach (GameObject segment in segments)
        {
            QueueSegment(segment, ref roadSegments);
        }
    }

    void generateRock()
    {
        float obstacleGenChance = Random.Range(0.0f, 1.0f);     //Rand number between 0.0 and 1.0

        if (obstacleGenChance > 0.99f)      //If the Rand number is greater than 0.99, generate a rock
        {
            GameObject rockInstantiated = Instantiate(rockPrefab);
            float randX = Random.Range(-ROCK_SPAWN_RANGE, ROCK_SPAWN_RANGE);        //rand Xpos offset
            Vector3 randScale = new Vector3(Random.Range(0.1f, 0.8f), Random.Range(0.1f, 0.8f), 1.0f);      //Rand scale

            rockInstantiated.transform.localScale = randScale;
            rockInstantiated.transform.position += new Vector3(randX, Camera.main.orthographicSize * 2.0f, 0f);
            rocks.Add(rockInstantiated);       //Add rockInstantiated to List of rocks
        }
    }

    void moveRocks()
    {
        foreach (GameObject rock in rocks)
        {
            if (rock.transform.position.y <= -15.0f)        //If rock is offscreen, delete
            {
                rock.SetActive(false);
            }
            else                        //Else, move rock along road
            {
                rock.transform.position = new Vector3(rock.transform.position.x, rock.transform.position.y - scrollSpeed, rock.transform.position.z);
            }
        }
    }

    void moveRoadSegments()
    {
        foreach (GameObject road in roadSegments)
        {
            float newYPos;

            if (road.transform.position.y <= road.GetComponent<SpriteRenderer>().bounds.size.x * -4)        //If road is below the origin by quadruple it's length
            {
                newYPos = road.transform.position.y + road.GetComponent<SpriteRenderer>().bounds.size.x * 2 * (roadSegments.Count - 1) - scrollSpeed;     //newYPos is set to above the origin by double road's length plus all roads out
                print(road.name + " of size " + road.GetComponent<SpriteRenderer>().bounds.size + " moved from " + road.transform.position + " to: " + newYPos);
				levelLength--;
			}
            else
            {
                newYPos = road.transform.position.y - scrollSpeed;      //newYPos is set to scrollSpeed lower than road's current Y position
            }
            road.transform.position = new Vector3(road.transform.position.x, newYPos, road.transform.position.z);   //Set road's position to newYPos
        }
    }

	void moveDirtSegments()
	{
		foreach (GameObject dirt in dirtSegments)
		{
			float newYPos;

			if (dirt.transform.position.y <= srDirtSegment.bounds.extents.y * -4.0f)        //If rock is below the origin by double it's length
			{
				newYPos = dirt.transform.position.y + srDirtSegment.bounds.extents.y * 6.0f - scrollSpeed;     //newYPos is set to above the origin by double rock's length 
			}
			else
			{
				newYPos = dirt.transform.position.y - scrollSpeed;      //newYPos is set to scrollSpeed lower than rock's current Y position
			}
            dirt.transform.position = new Vector3(dirt.transform.position.x, newYPos, dirt.transform.position.z);   //Set rock's position to newYPos
        }
	}

	void checkGroundType()
	{
		isOnRoad = false;

		foreach (Collider2D colRoadSegment in colRoadSegments)
		{
			if (colVehicle != null && colVehicle.IsTouching(colRoadSegment))
			{
				isOnRoad = true;
			}
		}

		if (isOnRoad)
		{
			scrollSpeed = 0.5f;
		}
		else
		{
			scrollSpeed = 0.2f;
		}

		//Debug.Log(scrollSpeed);
	}

	void checkIfLevelOver()
	{
		if (levelLength <= 0)
		{
			Debug.Break();
		}
	}

    void setRoadSegementsVisibility(bool state)
    {
        foreach (GameObject road in roadSegments)
        {
            road.GetComponent<SpriteRenderer>().enabled = state;
        }
    }

	//START METHODS
	void addRoadSegmentsToList()
	{
		//Add first segment
        QueueSegment(Instantiate(roadSegmentPrefab), ref roadSegments);
        QueueSegment(Instantiate(roadSegmentPrefab), ref roadSegments);
        QueueSegment(Instantiate(roadSegmentPrefab), ref roadSegments);
        QueueSegment(Instantiate(roadSegmentPrefab), ref roadSegments);

        //InstantiateSegmentOn(dirtSegmentPrefab, dirtSegments);
        //InstantiateSegmentOn(dirtSegmentPrefab, roadSegments);
        //InstantiateSegmentOn(dirtSegmentPrefab, dirtSegments);
        //InstantiateSegmentOn(dirtSegmentPrefab, dirtSegments);

		//Add roadSegmentColliders to colRoadSegments
        //colRoadSegments.Add(roadSegment1.GetComponent<Collider2D>());
        //colRoadSegments.Add(roadSegment2.GetComponent<Collider2D>());
        //colRoadSegments.Add(roadSegment3.GetComponent<Collider2D>());
        //colRoadSegments.Add(roadSegment4.GetComponent<Collider2D>());
	}

    void QueueSegment(GameObject segment, ref List<GameObject> segments)
    {
        //newSegment.name = prefab.name + (1 + segments.Count);
        float segmentOffset = segments.Count * segment.GetComponent<SpriteRenderer>().bounds.size.y;
        segment.transform.position += new Vector3(0, +segmentOffset, 0);
        segments.Add(segment);
    }

	void addDirtSegmentsToList()
	{
		//Add first segment
		GameObject dirtSegment1 = Instantiate(dirtSegmentPrefab);
        dirtSegment1.transform.position = new Vector3(0, 0, dirtSegment1.transform.position.z);
        dirtSegments.Add(dirtSegment1);

		//Add second segment and offset by size of previous segment
		GameObject dirtSegment2 = Instantiate(dirtSegmentPrefab);
		dirtSegment2.transform.position = new Vector3(0, srDirtSegment.bounds.extents.y * 2.0f, dirtSegment2.transform.position.z);
		dirtSegments.Add(dirtSegment2);

		//Add third segment and offset by size of previous segment
		GameObject dirtSegment3 = Instantiate(dirtSegmentPrefab);
		dirtSegment3.transform.position = new Vector3(0, srDirtSegment.bounds.extents.y * 4.0f, dirtSegment3.transform.position.z);
		dirtSegments.Add(dirtSegment3);
	}

}
