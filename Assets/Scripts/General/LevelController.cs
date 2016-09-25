using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public bool MoveRoad;
    public bool ShowRoad;

	private float ROCK_SPAWN_RANGE = 5.0f;

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

    // Use this for initialization
    void Start () {
		createGameBounds();

		if (MoveRoad)
        {
			//Lists to hold GameObjects
			createRoadSegments();
        }
    }

    // Update is called once per frame
    void Update() {

        if (MoveRoad) {
            if (roadSegments == null)
            {
                createRoadSegments();
            }

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
			checkIfLevelOver();
		}

        //Hide or Show Road
        if (ShowRoad != roadSegments[0].GetComponent<SpriteRenderer>().enabled)
            setRoadSegementsVisibility(ShowRoad);

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
        roadSegments = new List<GameObject>();
		dirtSegments = new List<GameObject>();
		rocks = new List<GameObject>();
		colRoadSegments = new List<Collider2D>();

		//SpriteRenderer of the roadSegmentPrefab and dirtSegmentPrefab
		srRoadSegment = roadSegmentPrefab.GetComponent<SpriteRenderer>();
		srDirtSegment = dirtSegmentPrefab.GetComponent<SpriteRenderer>();

		//Collider2D of the vehiclePrefab
		colVehicle = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<Collider2D>();

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
        Sprite sprite = srRoadSegment.sprite;
        float unitWidth = sprite.textureRect.width / sprite.pixelsPerUnit;
        float unitHeight = sprite.textureRect.height / sprite.pixelsPerUnit;
        sr.transform.localScale = new Vector3(cameraWidth / unitWidth, cameraHeight / unitHeight);

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
            rockInstantiated.transform.position = new Vector3(rockInstantiated.transform.position.x + randX, Camera.main.orthographicSize * 2.0f, rockInstantiated.transform.position.z);
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

            if (road.transform.position.y <= srRoadSegment.bounds.extents.y * -4.0f)        //If road is below the origin by double it's length
            {
                newYPos = road.transform.position.y + srRoadSegment.bounds.extents.y * 8.0f - scrollSpeed;     //newYPos is set to above the origin by double road's length 
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

		Debug.Log(scrollSpeed);
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
        InstantiateSegmentOn(roadSegmentPrefab, roadSegments);
        InstantiateSegmentOn(roadSegmentPrefab, roadSegments);
        InstantiateSegmentOn(roadSegmentPrefab, roadSegments);
        InstantiateSegmentOn(roadSegmentPrefab, roadSegments);

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

    void InstantiateSegmentOn(GameObject prefab, List<GameObject> segments)
    {
        GameObject newSegment = Instantiate(prefab);
        newSegment.name = prefab.name + (1 + segments.Count);
        float segmentOffset = segments.Count * prefab.GetComponent<SpriteRenderer>().bounds.size.y;
        newSegment.transform.position = new Vector3(prefab.transform.position.x, prefab.transform.position.y + segmentOffset, prefab.transform.position.z);
        segments.Add(newSegment);
    }

	void addDirtSegmentsToList()
	{
		//Add first segment
		GameObject dirtSegment1 = Instantiate(dirtSegmentPrefab);
		dirtSegment1.transform.position = new Vector3(0.0f, 0.0f, dirtSegment1.transform.position.z);
		dirtSegments.Add(dirtSegment1);

		//Add second segment and offset by size of previous segment
		GameObject dirtSegment2 = Instantiate(dirtSegmentPrefab);
		dirtSegment2.transform.position = new Vector3(0.0f, srDirtSegment.bounds.extents.y * 2.0f, dirtSegment2.transform.position.z);
		dirtSegments.Add(dirtSegment2);

		//Add third segment and offset by size of previous segment
		GameObject dirtSegment3 = Instantiate(dirtSegmentPrefab);
		dirtSegment3.transform.position = new Vector3(0.0f, srDirtSegment.bounds.extents.y * 4.0f, dirtSegment3.transform.position.z);
		dirtSegments.Add(dirtSegment3);
	}

}
