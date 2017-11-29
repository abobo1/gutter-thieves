using UnityEngine;
using System.Collections;

public class hashandegs : MonoBehaviour {
    public int speed, grab, attack, strafe;

	// Use this for initialization
	void Start () {
        speed =Animator.StringToHash("speed");
        grab = Animator.StringToHash("isgrab");
        attack = Animator.StringToHash("istokill");
        strafe = Animator.StringToHash("strafez");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
