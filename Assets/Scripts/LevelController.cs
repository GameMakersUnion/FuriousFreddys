using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public float scrollSpeed = 0.05f;
    private List<GameObject> roadSegments;
    public GameObject roadSegmentPrefab;

	// Use this for initialization
	void Start () {
        //List to hold roadSegments
        roadSegments = new List<GameObject>();

        //Add first segment
        roadSegments.Add(Instantiate(roadSegmentPrefab));

        //Add second segment and offset by size of previous segment
        GameObject roadSegment2 = Instantiate(roadSegmentPrefab);
        SpriteRenderer sr2 = roadSegment2.GetComponent<SpriteRenderer>();
        roadSegment2.transform.position =  new Vector3(roadSegment2.transform.position.x, sr2.bounds.extents.y * 2, roadSegment2.transform.position.z);
        roadSegments.Add(roadSegment2);

        //Add third segment and offset by size of second segment
        GameObject roadSegment3 = Instantiate(roadSegmentPrefab);
        SpriteRenderer sr3 = roadSegment3.GetComponent<SpriteRenderer>();
        roadSegment3.transform.position = new Vector3(roadSegment3.transform.position.x, sr3.bounds.extents.y * 4, roadSegment3.transform.position.z);
        roadSegments.Add(roadSegment3);

        Debug.Log(roadSegments);
    }
	
	// Update is called once per frame
	void Update () {
        //Iterate through roadSegments and adjust the y position by scrollSpeed
        foreach (GameObject road in roadSegments)
        {
            road.transform.position = new Vector3(road.transform.position.x, road.transform.position.y - scrollSpeed, road.transform.position.z);
        }
	}
}
