using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {
   public Sprite Oface;
    public Sprite Face;
    Vector3 direction;
    public float speed;
   GameObject  vehicle;
    SpriteRenderer ZSR;
    SpriteRenderer SR;
    // Use this for initialization
    void Start () {
        vehicle = GameObject.FindGameObjectWithTag("Vehicle");
         ZSR = this.GetComponent<SpriteRenderer>();
        SR = vehicle.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update () {

        Vector3 start = transform.position;


        Vector3 destin = SR.bounds.ClosestPoint(start);

        if (!SR.bounds.Contains(ZSR.bounds.ClosestPoint(destin)))
        {
            ZSR.sprite = Face;
            transform.position = Vector3.Lerp(start, destin, speed);
        }
        else {
            ZSR.sprite = Oface;
        }
	}
}
