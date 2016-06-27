using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {


    private float scrollSpeed = 0.2f;  //The speed to scroll the roadSegments at

    private List<GameObject> roadSegments;  //The list of GameObjects to hold the roadSegmentPrefabs
    public GameObject roadSegmentPrefab;    //The roadSegmentPrefab which the each segment is made from
    private SpriteRenderer srRoadSegment;      //The SpriteRenderer of the roadSegmentPrefab, used to get bounds

    private List<GameObject> rocks;
    public GameObject rockPrefab;

    // Use this for initialization
    void Start () {
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
        roadSegment1.transform.position = new Vector3(roadSegment1.transform.position.x, roadSegment1.transform.position.y , roadSegment1.transform.position.z);
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

        //Add rocks to List
       // rockPrefab = GameObject.FindWithTag("Rock");
        
        
    }
	
	// Update is called once per frame
	void Update () {
       
        //Iterate through roadSegments and adjust the y position by newYPos
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

        //Generate obstacles randomly
       
        
        float obstacleGenChance = Random.Range(0.0f, 1.0f);
        if (obstacleGenChance > 0.99f)
        {
            GameObject rock1 = Instantiate(rockPrefab);
            rock1.transform.position = new Vector3(rock1.transform.position.x, Camera.main.orthographicSize * 2.0f, rock1.transform.position.z);
            rocks.Add(rock1);
        }
        foreach (GameObject rock in rocks)
        {   
            rock.transform.position = new Vector3(rock.transform.position.x, rock.transform.position.y - scrollSpeed, rock.transform.position.z);
            Debug.Log(rock.transform.position);
        }
	}
}
