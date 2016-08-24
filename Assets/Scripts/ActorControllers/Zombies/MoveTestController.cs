using UnityEngine;

public class MoveTestController : MonoBehaviour
{

    public float minSpeed, maxSpeed, speedChangeRate;

    private float speed, nextSpeedChange;
    private GameObject target;
    private Transform tf;
    private Rigidbody2D rb;

	void Start ()
    {
        changeSpeed();
        target = GameObject.FindGameObjectWithTag("Vehicle");
        tf = transform;
        rb = GetComponent<Rigidbody2D>();
	}

	void Update ()
    {
        Vector3 targetPosition = target.transform.position - new Vector3(0, 2, 0);
        tf.position = Vector2.MoveTowards(tf.position, targetPosition, speed * Time.deltaTime);
        if (Time.time > nextSpeedChange) changeSpeed();
    }

    void OnTriggerEnter2D()
    {

    }

    private void changeSpeed()
    {
        nextSpeedChange = Time.time + speedChangeRate;
        speed = Mathf.Round(Random.Range(minSpeed, maxSpeed));
        //Debug.Log("Speed is " + speed);
    }

}
