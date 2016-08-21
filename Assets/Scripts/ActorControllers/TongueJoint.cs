using UnityEngine;
using System.Collections;

public class TongueJoint : MonoBehaviour, DamageVisitable, DamageVisitor {

	int damage = 1;
	int health = 1;

	void Update()
	{
		CheckHealth();
	}

	public virtual void AcceptDamageFrom(DamageVisitor damager)
	{
		int damageAmount = damager.CauseDamageTo(this);
		health -= damageAmount;
	}

	public virtual int CauseDamageTo(DamageVisitable visitable)
	{
		return damage;
	}

	//protected abstract void OnCollisionEnter2D(Collision2D col);

	//protected abstract void OnCollisionExit2D(Collision2D col);


	void CheckHealth()
	{
		transform.parent.gameObject.GetComponent<VaperHookShot>().DestroyRope();

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (!col.gameObject.GetComponent<ProjectileController>()) return;

		DamageVisitor damager = col.gameObject.GetComponent<DamageVisitor>();
		DamageVisitable damagable = gameObject.GetComponent<DamageVisitable>();

		damagable.AcceptDamageFrom(damager);
	}
}
