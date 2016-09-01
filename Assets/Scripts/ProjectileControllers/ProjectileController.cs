using UnityEngine;

// Controller for normal bullet-type projectiles
public class ProjectileController : MonoBehaviour, DamageVisitor {

	public int damage;
    public float timeAlive;

    private float deathTime;
    private GunnerControlScript owner; //the gunner freddy who fired it. this remains null if the bullet was shot by a zomb

    public GunnerControlScript Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    void Start()
    {
        deathTime = Time.time + timeAlive;
        owner = null;
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
