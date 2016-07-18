using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    private Transform tf;

    void Start()
    {
        tf = transform;
    }

    public virtual void Spawn(GameObject Projectile, GameObject MuzzleFlash)
    {
        if (MuzzleFlash != null)
            Instantiate(MuzzleFlash, tf.localPosition, tf.localRotation);
        Instantiate(Projectile, tf.position, tf.rotation);
    }

}
