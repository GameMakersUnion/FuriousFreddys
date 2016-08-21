using System;
using System.Collections;
using UnityEngine;

/*
 * The movement/shooting controls in this script are called from the switcher for now,
 * but will be called in their own update functions by controllers once the game
 * reaches that state.
 */
public class GunnerControlScript : PlayerControlScript
{

    public WeaponController CurrWeaponScript; //Prefab for weap to be instantiated from

    private int ammo; //what can be fired before reloading
    private float nextFire;
    private bool isReloading;
    private GameObject CurrWeapon; //THIS is used for shooting

    protected override void Start()
    {
        base.Start();
        moveFactor = 200;
        Quaternion rot = Quaternion.Euler(0, 0, tf.rotation.eulerAngles.z + 180);
        CurrWeapon = (GameObject)Instantiate(CurrWeaponScript.gameObject, Vector3.zero, rot);
        CurrWeapon.transform.parent = gameObject.transform;
        CurrWeapon.transform.localPosition = CurrWeaponScript.pos;
        ammo = CurrWeaponScript.magSize;
        nextFire = 0;
        isReloading = false;
        //Debug.Log("Gunner's parent is: " + transform.parent);
    }

    public override void Move(int direction)
    {
        CurrWeapon.transform.RotateAround
            (
            tf.position,
            Vector3.forward,
            moveFactor * direction * Time.deltaTime
            );
    }

    public override void PerformAction()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time > nextFire)
        {
            if (!isReloading && ammo <= 0)
                StartCoroutine(Reload());

            if (!isReloading)
            {
                CurrWeapon.GetComponent<WeaponController>().Fire();
                nextFire = Time.time + CurrWeaponScript.fireRate;
                ammo--;
                Debug.Log("Ammo is " + ammo);
            }

        }

    }

    private IEnumerator Reload()
    {
        Debug.Log("Starting reloading");
        isReloading = true;
        yield return new WaitForSecondsRealtime(CurrWeaponScript.reloadTime);
        ammo = CurrWeaponScript.magSize;
        isReloading = false;
        Debug.Log("Finished reloading");
    }

	public override int CauseDamageTo(DamageVisitable damagable)
	{
		return 0;
	}

}
