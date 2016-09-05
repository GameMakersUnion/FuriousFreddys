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

    private WeaponController CurrWeaponScript; //Prefab for weap to be instantiated from

    public WeaponController[] Weapons; //TEMP. for MVP only

    public PlayerStatistics stats;
    private int ammo; //what can be fired before reloading
    private int shotsFired;
    private float nextFire;
    private bool isReloading;
    private GameObject CurrWeapon; //THIS is used for shooting

    private int weapNum;

    protected override void Start()
    {
        base.Start();
        moveFactor = 200;
        Quaternion rot = Quaternion.Euler(0, 0, tf.rotation.eulerAngles.z + 180);

        weapNum = UnityEngine.Random.Range(0, Weapons.Length);

        /*
        weap = CurrWeaponScript;
        CurrWeapon = (GameObject)Instantiate(weap.gameObject, Vector3.zero, rot);
        */
        CurrWeaponScript = Weapons[weapNum];
        CurrWeapon = (GameObject)Instantiate(CurrWeaponScript.gameObject, Vector3.zero, rot); //TEMP FOR MVP. Use above when done.

        CurrWeapon.transform.parent = gameObject.transform;
        CurrWeapon.transform.localPosition = CurrWeaponScript.pos;
        ammo = CurrWeaponScript.magSize;
        shotsFired = 0;
        nextFire = 0;
        isReloading = false;
        //Debug.Log("Gunner's parent is: " + transform.parent);
    }

	protected override void UpButtonReleased()
	{
		//Do button released logic
	}
	protected override void UpButtonPressed()
	{
		//Do button pressed logic
	}
	protected override void DownButtonReleased()
	{
		//Do button realeasd logic
	}
	protected override void DownButtonPressed()
	{
		//Do button pressed logic
	}
	protected override void PrimaryButtonReleased()
	{
		//Do button realeasd logic
	}
	protected override void PrimaryButtonPressed()
	{
		//Do button pressed logic
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
                CurrWeapon.GetComponent<WeaponController>().Fire(this);
                shotsFired++;
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
