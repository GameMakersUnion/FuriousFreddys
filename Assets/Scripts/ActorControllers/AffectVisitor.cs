using UnityEngine;
using System.Collections;

public interface AffectVisitor {

	int CauseAffectTo(AffectVisitable damager);
 
}
