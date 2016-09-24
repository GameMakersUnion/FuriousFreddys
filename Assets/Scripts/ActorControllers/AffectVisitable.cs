using UnityEngine;
using System.Collections;

public interface AffectVisitable {

	void AcceptAffectFrom(AffectVisitor damagable);

}
