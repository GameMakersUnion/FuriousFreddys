using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    protected Transform tf;

	protected virtual void Start ()
    {
        tf = GetComponent<Transform>();
	}

    public virtual void Spawn(GameObject Projectile, GameObject MuzzleFlash)
    {
        Instantiate(MuzzleFlash, tf.position, tf.rotation);
        Instantiate(Projectile, tf.position, tf.rotation);
    }

}
