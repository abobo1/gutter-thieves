using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private readonly string JOGGING = "isJogging";
    //private readonly string JUMPING = "isJumping";
    private readonly string ATTACK = "attack";
    private readonly string BACKWARDS = "isWalkingBackwards";
    private readonly string STRAFINGRIGHT = "isStrafingRight";
    private readonly string STRAFINGLEFT = "isStrafingLeft";

    public float forwardSpeed;
    public float backwardsSpeed;
    public float strafeSpeed;
    public float turnSpeed;
    public float axisThreshold;
    public int fireWaitTime;
    public float strafeThreshold;
    public float crownPickupDistance;

    public float crownSpeedReducer = 0.5f;

    public AudioClip footsteps;
    public AudioClip knifeSlash;

    private AudioSource footstepsAS;
    private AudioSource knifeSlashAS;

    public Transform crownPosition;

    [HideInInspector]
    public bool hasCrown;

    public int playerNumber;

    private Animator ani;
    private float hMove;
    private float vMove;
    private float angH;
    private float angV;
    private int fireTime;

    private GlobalController GC;
    private GameObject crown;
    private GameObject exit;
    private GameObject dagger;

	public AudioSource FootSteps;

    void Start () {
        GC = GameObject.Find("GameController").GetComponent<GlobalController>();
        crown = GameObject.FindGameObjectWithTag("Crown");
        exit = GameObject.Find("WallDestroyerP" + playerNumber + "Exit");
        ani = GetComponent<Animator>();
        dagger = GameObject.FindGameObjectWithTag("Dagger"+playerNumber);
        footstepsAS = AddAudio(footsteps, true, false, 0.7f);
        knifeSlashAS = AddAudio(knifeSlash, false, false, 0.9f);
        fireTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        hMove = Input.GetAxis("Horizontal" + playerNumber);
        vMove = Input.GetAxis("Vertical" + playerNumber);
        angH = Input.GetAxis("RightH" + playerNumber);
        angV = Input.GetAxis("RightV" + playerNumber);
        
        if (GC.gameReady) {
            Move();

            Turn();
            if (InRangeOfCrown()) {
                GiveMeTheCrown();
            }
            if (Input.GetButtonDown("Fire" + playerNumber) && !hasCrown) {
                Fire();
            }
            if (!ReadyToFire()) {
                fireTime++;
            }
            if (Input.GetButtonDown("Jump" + playerNumber)) {
            }
            if (hasCrown) {
                dagger.SetActive(false);
            } else {
                dagger.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == exit && hasCrown) {
            GC.GameOver(playerNumber);
        }
    }

    private bool InRangeOfCrown() {
        float currentDistance = Vector3.Distance(transform.position, crown.transform.position);
        return (currentDistance < crownPickupDistance);
    }

    private void GiveMeTheCrown() {
        if (!GC.IsCrownPossessed()) {
            hasCrown = true;
            GC.GiveMeTheCrown(gameObject, crownPosition);
         }
    }

    private void Move() {
        /*TODO Fix bugs:
        1. Make isIdle a bool. This should stop the weird transitions
         */
        float speed = 0.0f;

        if (vMove > strafeThreshold) { //Moving forward
            ani.SetBool(JOGGING, true);
            ani.SetBool(BACKWARDS, false);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(STRAFINGLEFT, false);
            speed = forwardSpeed;
            PlayFootsteps();
        } else if (vMove < -strafeThreshold) { //Moving backwards
            ani.SetBool(BACKWARDS, true);
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(STRAFINGLEFT, false);
            speed = backwardsSpeed;
            PlayFootsteps();
        } else if (hMove > 0) {
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGRIGHT, true);
            ani.SetBool(STRAFINGLEFT, false);
            ani.SetBool(BACKWARDS, false);
            PlayFootsteps();
        } else if (hMove < 0) {
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGLEFT, true);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(BACKWARDS, false);
            PlayFootsteps();
        } else {
            footstepsAS.Stop();
            ani.SetBool(JOGGING, false);
            ani.SetBool(STRAFINGLEFT, false);
            ani.SetBool(STRAFINGRIGHT, false);
            ani.SetBool(BACKWARDS, false);
        }
        if (hasCrown) {
            speed = speed * crownSpeedReducer;
            strafeSpeed = strafeSpeed * crownSpeedReducer;
        }
        transform.Translate(hMove * strafeSpeed * Time.deltaTime, 0.0f, vMove * speed * Time.deltaTime);
    }

    private void PlayFootsteps() {
        if (!footstepsAS.isPlaying) {
            footstepsAS.Play();
        }
    }

    private void Turn() {
        if (Mathf.Abs(angV) > axisThreshold || Mathf.Abs(angH) > axisThreshold) {
            transform.Rotate(0, angH * turnSpeed * Time.deltaTime, 0);
        }
    }

    private void Fire() {
        if (ReadyToFire()) {
            fireTime = 1;
            ani.SetTrigger(ATTACK);
            knifeSlashAS.PlayDelayed(0.4f);
        }
    }

    private bool ReadyToFire() {
        return fireTime % fireWaitTime == 0;
    }

    private AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip; 
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol; 
        return newAudio;
    }
}
