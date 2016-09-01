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
        GameObject bullet = (GameObject) Instantiate(projectile, tf.position, tf.rotation);
        if (bullet == null) Debug.Log("wut");
        bullet.GetComponent<ProjectileController>().Owner = freddy;
    }

    protected void Shoot()
    {

    }

}
