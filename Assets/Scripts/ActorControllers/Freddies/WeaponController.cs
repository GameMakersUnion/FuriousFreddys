using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public int magSize;
    public float fireRate, reloadTime;
    public Vector3 pos; //the position of the weapon that looks the most 'proper'
    public GameObject Projectile, MuzzleFlash;
    public BulletSpawner Spawner;

    public void Fire()
    {
        Spawner.Spawn(Projectile, MuzzleFlash);
    }

}
