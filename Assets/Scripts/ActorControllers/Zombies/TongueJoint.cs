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

	public override void AcceptAffectFrom(AffectVisitor damager)
	{
		print("doh");
		int damageAmount = damager.CauseAffectTo(this);
		health -= damageAmount;
	}

	public override int CauseAffectTo(AffectVisitable visitable)
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

		AffectVisitor damager = col.gameObject.GetComponent<AffectVisitor>();
		AffectVisitable damagable = gameObject.GetComponent<AffectVisitable>();

		damagable.AcceptAffectFrom(damager);
	}

	protected override void OnCollisionExit2D(Collision2D col)
	{

	}

	public override void Move(int direction)
	{
		
	}
}
