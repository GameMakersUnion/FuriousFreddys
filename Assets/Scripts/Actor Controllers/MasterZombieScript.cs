using UnityEngine;
using System.Collections;

public abstract class MasterZombieScript : MonoBehaviour
{
    //I know i did it again... 
    GameObject van;
    protected VehicleControlScript vehicle;
    protected SpriteRenderer ZSR;
    protected SpriteRenderer VSR;
    protected Collider2D vC;
    protected Collider2D zC;
    public Sprite Oface;
    public Sprite Face;
    protected Vector3 start;
    protected Vector3 destin;
    public int damage;
    protected bool contact;
    protected int counter;
    protected Rigidbody2D rb;
    protected int colcount;
    // Use this for initialization
    public virtual void Start()
    {
        van = GameObject.FindGameObjectWithTag("Vehicle");
        vehicle = van.GetComponent<VehicleControlScript>();
        ZSR = this.GetComponent<SpriteRenderer>();
        VSR = van.GetComponent<SpriteRenderer>();
        zC = this.GetComponent<Collider2D>();
        vC = van.GetComponent<Collider2D>();
        damage = 1;
        this.contact = false;
        counter = 10;
        colcount = 10;
        rb = this.GetComponent<Rigidbody2D>();

    }

    public virtual void Update()
    {
        this.start = transform.position;
        this.destin = VSR.bounds.ClosestPoint(start);



        // Debug.Log(this.counter + " " + this.contact);
        //print("staying");
        if (this.contact)
        {
            if (counter == 0)
            {
                this.counter = 30;
                vehicle.updateHealth(damage, this.name);
            }
            else
            {
                counter--;
            }
        }
        else
        {
            if (colcount == 0)
            {
                ZSR.sprite = Face;
            }
            else
            {
                colcount--;
            }
        }
    }





    /*
     * Intial contact of the zombie to the car, stops it from moving 
     */
    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {
            this.contact = true;

            this.transform.position = this.transform.position;
        }
    }





    /*
     * Resets the zombie attack period to half
     * enables the rigidbody on the zombies
     */
    public virtual void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {

            this.contact = false;
            //this.counter = 15;

        }
    }





    /*
     *every 30 or so frames damage the car
     * this gives the zombie an attack period of approx 30 frames
     */
    public virtual void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Vehicle")
        {

        }
    }



}
