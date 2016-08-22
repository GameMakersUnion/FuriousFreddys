using UnityEngine;

public class MoveTestController : MonoBehaviour {

    public float minSpeed, maxSpeed, speedChangeRate;

    private float speed, nextSpeedChange;
    private GameObject target;
    private Transform tf;
    private Rigidbody2D rb;

	void Start () {
        changeSpeed();
        target = GameObject.FindGameObjectWithTag("Vehicle");
        tf = transform;
        rb = GetComponent<Rigidbody2D>();
	}

	void Update ()
    {
        tf.position = Vector2.MoveTowards(tf.position, target.transform.position, speed * Time.deltaTime);
        if (Time.time > nextSpeedChange) changeSpeed();
    }

    private void changeSpeed()
    {
        nextSpeedChange = Time.time + speedChangeRate;
        speed = Mathf.Round(Random.Range(minSpeed, maxSpeed));
        Debug.Log("Speed is " + speed);
    }

}
