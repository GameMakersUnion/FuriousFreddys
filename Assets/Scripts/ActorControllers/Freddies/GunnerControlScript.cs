using UnityEngine;
using System.Collections;
using System;

public class GunnerControlScript : PlayerControlScript {

    public WeaponController CurrWeaponScript; //TEMPLATE. Only used for instantiation of weapon.

    private GameObject CurrWeapon; //Use this for shooting etc.

    protected override void Start ()
    {
        base.Start();
        CurrWeapon = (GameObject)Instantiate(CurrWeaponScript.gameObject, Vector3.zero, tf.rotation);
        CurrWeapon.transform.parent = gameObject.transform;
        CurrWeapon.transform.localPosition = CurrWeaponScript.pos;
        Debug.Log("Gunner's parent is: " + transform.parent);
    }

    void Update()
    {
        
    }

    public override void Move(int direction)
    {
        //rotate left or right
        tf.Rotate(0, 0, moveFactor * -direction);
    }

    public override void Shoot()
    {
        //fire projectile
        CurrWeapon.GetComponent<WeaponController>().Fire();
        Debug.Log("CurrWeaponScript (template) name is: " + CurrWeaponScript.gameObject);
        Debug.Log("CurrWeapon (actual) name is: " + CurrWeapon);
        Debug.Log("Gunner's position is: " + transform.position);
        Debug.Log("Gunner's rotation is: " + transform.rotation.eulerAngles);
    }

}
