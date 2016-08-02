using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private float nextFire;

    public int magSize;
    public float fireRate, reloadTime;
    public Vector3 pos; //the position of the weapon that looks the most 'proper'
    public GameObject Projectile, MuzzleFlash;
    public BulletSpawner Spawner;
    
	void Start ()
    {
        nextFire = 0;
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            Spawner.Spawn(Projectile, MuzzleFlash);
            nextFire = Time.time + fireRate;
        }
    }

}
