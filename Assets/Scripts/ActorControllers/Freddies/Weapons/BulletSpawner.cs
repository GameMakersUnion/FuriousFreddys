using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    protected Transform tf;

    protected virtual void Start()
    {
        tf = transform;
    }

    public virtual void Spawn(GameObject Projectile, GameObject MuzzleFlash)
    {
        Debug.Log("Gun firing");
        if (MuzzleFlash != null)
            Instantiate(MuzzleFlash, tf.localPosition, tf.localRotation);
        Instantiate(Projectile, tf.position, tf.rotation);
    }

}
