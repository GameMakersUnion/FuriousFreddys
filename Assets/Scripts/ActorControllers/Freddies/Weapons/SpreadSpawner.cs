using UnityEngine;

public class SpreadSpawner : BulletSpawner {

    private float arcAngle;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        arcAngle = 45;
	}

    public override void Spawn(GameObject projectile, GameObject muzzleFlash, GunnerControlScript freddy)
    {
        Debug.Log("Shotty firing");
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, tf.localPosition, tf.localRotation);

        //algorithm to spawn pellets in a pattern
        float angle = arcAngle / -2;
        float increment = arcAngle / 4;
        tf.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 pos = tf.position;
        Quaternion rot = tf.rotation;

        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = (GameObject)Instantiate(projectile, tf.position, tf.rotation);
            if (bullet == null) Debug.Log("wut");
            bullet.GetComponent<ProjectileController>().Owner = freddy;
            angle += increment;
            tf.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        tf.localRotation = Quaternion.identity; //reset

    }

}
