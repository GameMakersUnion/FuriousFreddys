using UnityEngine;

// Controller for normal bullet-type projectiles
public class ProjectileController : MonoBehaviour, DamageVisitor {

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

	void OnCollisionEnter2D(Collision2D col)
	{
		//demorgan's law
		if (!(col.gameObject.tag == "Vehicle" && col.gameObject.tag == "Cell" && col.gameObject.tag == "Bullet"))
		{
			Destroy(gameObject);
		}

	}
    public virtual int CauseDamageTo(DamageVisitable visitable)
	{
		return damage;
	}

}
