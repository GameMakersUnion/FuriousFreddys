using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public float scrollSpeed = 0.0001f;
    private List<GameObject> roadSegments;
    public GameObject roadSegmentPrefab;

	// Use this for initialization
	void Start () {
        roadSegments = new List<GameObject>();
        roadSegments.Add(Instantiate(roadSegmentPrefab));

        GameObject roadSegment = Instantiate(roadSegmentPrefab);
        SpriteRenderer sr = roadSegment.GetComponent<SpriteRenderer>();
        roadSegment.transform.position =  new Vector3(roadSegment.transform.position.x, sr.bounds.extents.y * 2, roadSegment.transform.position.z);
        roadSegments.Add(roadSegment);
        Debug.Log(roadSegments);
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject road in roadSegments)
        {
            road.transform.position = new Vector3(road.transform.position.x, road.transform.position.y - scrollSpeed, road.transform.position.z);
        }
	}
}
