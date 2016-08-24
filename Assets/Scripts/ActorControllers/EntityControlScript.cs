using UnityEngine;

public abstract class EntityControlScript : MonoBehaviour, DamageVisitable, DamageVisitor {

    protected int health;
    protected float moveFactor;

    public abstract int Health { get; set; }

	protected virtual void Start () {
	
	}
	
	protected virtual void Update () {
	
	}

    public abstract void Move(int direction);

	public abstract void AcceptDamageFrom(DamageVisitor visitor);

	public abstract int CauseDamageTo(DamageVisitable visitable);

	protected abstract void OnCollisionEnter2D(Collision2D col);

	protected abstract void OnCollisionExit2D(Collision2D col);

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
