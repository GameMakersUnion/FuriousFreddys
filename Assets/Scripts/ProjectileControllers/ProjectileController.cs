using UnityEngine;
using System.Collections;

// Controller for normal bullet-type projectiles
public class ProjectileController : MonoBehaviour {

	public int damage;
    public float timeAlive;

    private float deathTime;

    void Start()
    {
        deathTime = Time.time + timeAlive;
    }

    void Update()
    {
        if (Time.time > deathTime) Destroy(gameObject);
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            //call take dmg method in zombie controller

            Destroy(gameObject);
        }

	}

}
