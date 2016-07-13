using UnityEngine;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb;
	private Transform tf;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.freezeRotation = true;
		tf = GetComponent<Transform> ();
		SetVelocity();
	}

	void SetVelocity() {
		float velX = -speed * Mathf.Sin (tf.localEulerAngles.z * Mathf.Deg2Rad);
		float velY = speed * Mathf.Cos (tf.localEulerAngles.z * Mathf.Deg2Rad);
		rb.velocity = new Vector2 (velX, velY);
	}

}
