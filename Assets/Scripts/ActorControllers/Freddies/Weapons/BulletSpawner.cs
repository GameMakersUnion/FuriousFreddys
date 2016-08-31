using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    protected Transform tf;

    protected virtual void Start()
    {
        tf = transform;
    }

    public virtual void Spawn(GameObject projectile, GameObject muzzleFlash, GunnerControlScript freddy)
    {
        Debug.Log("Gun firing");
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, tf.localPosition, tf.localRotation);
        ProjectileController bullet = Instantiate(projectile, tf.position, tf.rotation) as ProjectileController;
        bullet.Owner = freddy;
    }

}
