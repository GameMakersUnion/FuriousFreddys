using UnityEngine;

/*
 * The movement/shooting controls in this script are called from the switcher for now,
 * but will be called in their own update functions by controllers once the game
 * reaches that state.
 */
public class GunnerControlScript : PlayerControlScript {

    public WeaponController CurrWeaponScript; //TEMPLATE. Only used for instantiation of weapon.

    private GameObject CurrWeapon; //Use this for shooting etc.

    protected override void Start ()
    {
        base.Start();
        moveFactor = 200;
        Quaternion rot = Quaternion.Euler(0, 0, tf.rotation.eulerAngles.z + 180);
        CurrWeapon = (GameObject)Instantiate(CurrWeaponScript.gameObject, Vector3.zero, rot);
        CurrWeapon.transform.parent = gameObject.transform;
        CurrWeapon.transform.localPosition = CurrWeaponScript.pos;
        //Debug.Log("Gunner's parent is: " + transform.parent);
    }

    void Update()
    {
        
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
        CurrWeapon.GetComponent<WeaponController>().Fire(); //fire projectile
        /*
        Debug.Log("CurrWeaponScript (template) name is: " + CurrWeaponScript.gameObject);
        Debug.Log("CurrWeapon (actual) name is: " + CurrWeapon);
        Debug.Log("Gunner's position is: " + transform.position);
        Debug.Log("Gunner's rotation is: " + transform.rotation.eulerAngles);
        */
    }

}
