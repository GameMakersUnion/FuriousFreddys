using UnityEngine;
using System.Collections.Generic;

public class RockRandomCollider : MonoBehaviour {

    enum cols { BOX, CIRCLE, POLYGON };

    // Use this for initialization
    void Start () {

        Dictionary<cols, Collider2D> compDict = new Dictionary<cols, Collider2D>()
        {
            { cols.BOX, new BoxCollider2D() },
            { cols.CIRCLE, new CircleCollider2D() },
            { cols.POLYGON, new PolygonCollider2D() }
        };

        int result = Random.Range(0, 3);
        switch (result)
        {
            case 0:
                gameObject.AddComponent<BoxCollider2D>();
                break;
            case 1:
                gameObject.AddComponent<CircleCollider2D>();
                break;
            case 2:
                gameObject.AddComponent<PolygonCollider2D>();
                break;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
