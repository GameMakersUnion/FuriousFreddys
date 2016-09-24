using UnityEngine;

public abstract class EntityControlScript : MonoBehaviour, AffectVisitable, AffectVisitor {

    protected int health;
    protected float moveFactor;
    //protected int damage;
    public abstract int Health { get; set; }
    
	protected virtual void Start () { }
	
	protected virtual void Update () { }

    protected virtual void FixedUpdate() { }

	protected abstract void OnCollisionEnter2D(Collision2D col);

	protected abstract void OnCollisionExit2D(Collision2D col);

	public abstract void Move(int direction);

	public abstract void AcceptAffectFrom(AffectVisitor visitor);

	public abstract int CauseAffectTo(AffectVisitable visitable);



	public void ReportHealth()
	{
		//print("health of : " + this + " is: " + this.health);
	}

	protected void CheckDies()
	{
		if (isDead())
		{
			Die();
		}
	}

	protected bool isDead()
	{
		return health <= 0;
	}

	protected virtual void Die()
	{
		Destroy(gameObject);
	}

}
