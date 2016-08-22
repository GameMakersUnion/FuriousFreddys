using UnityEngine;
using System.Collections;

public interface DamageVisitor {

	int CauseDamageTo(DamageVisitable damager);
 
}
