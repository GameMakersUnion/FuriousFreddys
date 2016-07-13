using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public bool MoveRoad;

    public float ScrollSpeed { get { return scrollSpeed; } } //Limited accessibility deliberately
    private float scrollSpeed = 0.2f;  //The speed to scroll the roadSegments at

    private List<GameObject> roadSegments;  //The list of GameObjects to hold the roadSegmentPrefabs
    public GameObject roadSegmentPrefab;    //The roadSegmentPrefab which the each segment is made from
    private SpriteRenderer srRoadSegment;      //The SpriteRenderer of the roadSegmentPrefab, used to get bounds

    private List<GameObject> rocks;  //The list of GameObjects to hold the rockPrefabs
    public GameObject rockPrefab;       //The rockPrefab

    // Use this for initialization
    void Start () {
        if (MoveRoad)
        {
            createRoadSegments();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (MoveRoad){
            if (roadSegments == null)
            {
                createRoadSegments();
            }

            //Iterate through roadSegments and adjust the y position by newYPos
            moveRoadSegments();

            //Generate obstacles randomly
            generateRock();

            //Iterate through rocks and adjust the y position
            moveRocks();

        }
	}

    void createRoadSegments()
    {
        //List to hold roadSegments
        roadSegments = new List<GameObject>();
        rocks = new List<GameObject>();

        //SpriteRenderer of the roadSegmentPreFab
        srRoadSegment = roadSegmentPrefab.GetComponent<SpriteRenderer>();

        //Calculate srRoadSegment's scale when fit to screen
        float cameraHeight = Camera.main.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;
        Sprite sRoadSegment = srRoadSegment.sprite;
        float unitWidth = sRoadSegment.textureRect.width / sRoadSegment.pixelsPerUnit;
        float unitHeight = sRoadSegment.textureRect.height / sRoadSegment.pixelsPerUnit;
        srRoadSegment.transform.localScale = new Vector3(cameraWidth / unitWidth, cameraHeight / unitHeight);

        //Add first segment
        GameObject roadSegment1 = Instantiate(roadSegmentPrefab);
        roadSegment1.transform.position = new Vector3(roadSegment1.transform.position.x, roadSegment1.transform.position.y, roadSegment1.transform.position.z);
        roadSegments.Add(roadSegment1);

        //Add second segment and offset by size of previous segment
        GameObject roadSegment2 = Instantiate(roadSegmentPrefab);
        roadSegment2.transform.position = new Vector3(roadSegment2.transform.position.x, srRoadSegment.bounds.extents.y * 2.0f, roadSegment2.transform.position.z);
        roadSegments.Add(roadSegment2);

        //Add third segment and offset by size of second segment
        GameObject roadSegment3 = Instantiate(roadSegmentPrefab);
        roadSegment3.transform.position = new Vector3(roadSegment3.transform.position.x, srRoadSegment.bounds.extents.y * 4.0f, roadSegment3.transform.position.z);
        roadSegments.Add(roadSegment3);

        //Add fourth segment and offset by size of second segment
        GameObject roadSegment4 = Instantiate(roadSegmentPrefab);
        roadSegment4.transform.position = new Vector3(roadSegment4.transform.position.x, srRoadSegment.bounds.extents.y * 6.0f, roadSegment4.transform.position.z);
        roadSegments.Add(roadSegment4);

        Debug.Log(roadSegments);
    }

    void generateRock()
    {
        float obstacleGenChance = Random.Range(0.0f, 1.0f);     //Rand number between 0.0 and 1.0

        if (obstacleGenChance > 0.99f)      //If the Rand number is greater than 0.99, generate a rock
        {
            GameObject rock1 = Instantiate(rockPrefab);
            float randX = Random.Range(-5.0f, 5.0f);        //rand Xpos offset
            Vector3 randScale = new Vector3(Random.Range(0.1f, 0.8f), Random.Range(0.1f, 0.8f), 1.0f);      //Rand scale

            rock1.transform.localScale = randScale;
            rock1.transform.position = new Vector3(rock1.transform.position.x + randX, Camera.main.orthographicSize * 2.0f, rock1.transform.position.z);
            rocks.Add(rock1);       //Add rock1 to List of rocks
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
            }
            else
            {
                newYPos = road.transform.position.y - scrollSpeed;      //newYPos is set to scrollSpeed lower than road's current Y position
            }
            road.transform.position = new Vector3(road.transform.position.x, newYPos, road.transform.position.z);   //Set road's position to newYPos
        }
    }
}
