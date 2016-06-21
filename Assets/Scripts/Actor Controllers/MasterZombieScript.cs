using UnityEngine;
using System.Collections;

public abstract class MasterZombieScript : MonoBehaviour {
    GameObject vehicle;
   protected SpriteRenderer ZSR;
 protected   SpriteRenderer VSR;
    public Sprite Oface;
    public Sprite Face;
    // Use this for initialization
   public virtual void Start () {
        vehicle = GameObject.FindGameObjectWithTag("Vehicle");
        ZSR = this.GetComponent<SpriteRenderer>();
        VSR = vehicle.GetComponent<SpriteRenderer>();

    }

    public SpriteRenderer getVehicleSprite() {
        return this.VSR;
    }

    public SpriteRenderer getZombieSprite() {
        return this.ZSR;

    }
}
