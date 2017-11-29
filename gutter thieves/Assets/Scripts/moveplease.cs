using UnityEngine;
using System.Collections;

public class moveplease : MonoBehaviour {
    public Animator ani;
    public float speed, turning;
    public hashandegs hash;
    public bool grab;


	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        hash = GetComponent<hashandegs>();


	
	}
	
	// Update is called once per frame
	void Update () {
        //movethatplayer
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");
        float grabz = Input.GetAxis("Jump");
        float killz = Input.GetAxis("Fire1");
        //animation 
        if (!vert.Equals(0))
        {
            ani.SetFloat(hash.speed, 5.5f);
        }
        else
            ani.SetFloat(hash.speed, 0f);
        //movementforhorz
        if(! horz.Equals(0))
        {
            ani.SetFloat(hash.strafe, horz);
        }
        else ani.SetFloat(hash.strafe, 0);
        //grab
        if (!grabz.Equals(0))
        {
            ani.SetBool(hash.grab, true);

        }
        else ani.SetBool(hash.grab, false);
        //attack
        if(!killz.Equals(0))
        {
            ani.SetBool(hash.attack, true);
        }
        else ani.SetBool(hash.attack, false);

        //move model
        transform.Translate(horz * speed * Time.deltaTime, 0, vert * speed * Time.deltaTime);
        //this is turing the player turn up for whatbrah
        //transform.Rotate(0, horz * turning * Time.deltaTime, 0);
            

	}
}
