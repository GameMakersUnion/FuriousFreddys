using UnityEngine;
using System.Collections;

public interface DamageVisitable {

	void AcceptDamageFrom(DamageVisitor damagable);

}
