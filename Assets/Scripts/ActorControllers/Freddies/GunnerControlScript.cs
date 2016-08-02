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
        CurrWeapon = (GameObject)Instantiate(CurrWeaponScript.gameObject, Vector3.zero, tf.rotation);
        CurrWeapon.transform.parent = gameObject.transform;
        CurrWeapon.transform.localPosition = CurrWeaponScript.pos;
        //Debug.Log("Gunner's parent is: " + transform.parent);
    }

    void Update()
    {
        
    }

    public override void Move(int direction)
    {
        tf.Rotate(0, 0, moveFactor * -direction); //rotate left or right
    }

    public override void Shoot()
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
