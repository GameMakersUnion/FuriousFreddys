using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeaponScript : MonoBehaviour
{

    public WeaponController[] WeaponScripts;

    private GameObject CurrWeapon;
    private WeaponController CurrWeaponScript;
    private Transform tf;
    private Dictionary<KeyCode, int> Keys = new Dictionary<KeyCode, int>();
    
	void Start ()
    {
        tf = GetComponent<Transform>();
        Keys.Add(KeyCode.Alpha1, 0);
        Keys.Add(KeyCode.Alpha2, 1);
        Keys.Add(KeyCode.Alpha3, 2);

        EquipWeapon(WeaponScripts[Keys[KeyCode.Alpha1]]);
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            CurrWeaponScript.Fire();
	    if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipWeapon(WeaponScripts[Keys[KeyCode.Alpha1]]);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipWeapon(WeaponScripts[Keys[KeyCode.Alpha2]]);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipWeapon(WeaponScripts[Keys[KeyCode.Alpha3]]);
    }
    
    void EquipWeapon(WeaponController Weapon)
    {
        Destroy(CurrWeapon);
        CurrWeapon = (GameObject)Instantiate(Weapon.gameObject, Vector3.zero, tf.rotation);
        CurrWeapon.transform.parent = gameObject.transform;
        CurrWeapon.transform.localPosition = Weapon.pos;
        CurrWeaponScript = CurrWeapon.GetComponent<WeaponController>();
    }

}
