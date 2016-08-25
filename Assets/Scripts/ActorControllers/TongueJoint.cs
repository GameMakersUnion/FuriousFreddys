using UnityEngine;
using System.Collections;
using System;

public class TongueJoint : EntityControlScript {

	int damage;

	public override int Health
	{
		get { return health; }
		set { health = value; }
	}

	void Start()
	{
		damage = 1;
		health = 1;
	}

	void Update()
	{
		CheckDies();
	}

	public override void AcceptDamageFrom(DamageVisitor damager)
	{
		int damageAmount = damager.CauseDamageTo(this);
		health -= damageAmount;
	}

	public override int CauseDamageTo(DamageVisitable visitable)
	{
		return damage;
	}

	protected override void Die()
	{
		transform.parent.gameObject.GetComponent<VaperHookShot>().DestroyRope();
	}

	protected override void OnCollisionEnter2D(Collision2D col)
	{
		if (!col.gameObject.GetComponent<ProjectileController>()) return;

		DamageVisitor damager = col.gameObject.GetComponent<DamageVisitor>();
		DamageVisitable damagable = gameObject.GetComponent<DamageVisitable>();

		damagable.AcceptDamageFrom(damager);
	}

	protected override void OnCollisionExit2D(Collision2D col)
	{

	}

	public override void Move(int direction)
	{
		
	}
}
