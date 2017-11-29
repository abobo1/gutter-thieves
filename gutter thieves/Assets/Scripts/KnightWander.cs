using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class KnightWander : MonoBehaviour {

    private readonly string ATTACK = "Attack";
    private readonly string BACKWARDS = "Backwards";
    private readonly string IDLE = "Idle";
    private readonly string WALK = "Walk";
    private readonly string RUN = "Run";

    public float distanceFromWall = 10;
    public Vector3 destination1;
    public Vector3 destination2;
    public float distanceThreshholdToTurnAround = 3.0f;
    public float distanceToStartFollowingPlayer = 7.0f;
    public float attackDistanceThreshhold = 3.0f;

    public float walkSpeed = 1.0f;
    public float runSpeed = 2.0f;

    private NavMeshAgent NMAgent;
    private int currentDestination;

    private GameObject[] players;
    private int numberOfPlayers;

    private Animator AC;

    void Start() {
        AC = GetComponent<Animator>();
        NMAgent = GetComponent<NavMeshAgent>();
        NMAgent.destination = new Vector3(0.0f,0.0f,0.0f);
        currentDestination = 1;

        players = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = players.Length;
    }

    void Update() {
        NMAgent.speed = walkSpeed;
        GameObject closestPlayer = ShouldIFollowPlayer();
        if (closestPlayer != null) {
            currentDestination = 3;
            NMAgent.SetDestination(closestPlayer.transform.position);
            NMAgent.speed = runSpeed;
        } else if (ShouldITurnAround()) {
            if (currentDestination == 1) {
                NMAgent.destination = destination2;
                currentDestination = 2;
            } else {
                NMAgent.destination = destination1;
                currentDestination = 1;
            }
        }
        if (closestPlayer != null && Vector3.Distance(closestPlayer.transform.position, transform.position) < attackDistanceThreshhold) {
            AC.SetBool(ATTACK, true);
            AC.SetBool(WALK, false);
            AC.SetBool(RUN, false);
            AC.SetBool(IDLE, false);
            AC.SetBool(BACKWARDS, false);
        } else if (NMAgent.speed == walkSpeed) {
            AC.SetBool(ATTACK, false);
            AC.SetBool(WALK, true);
            AC.SetBool(RUN, false);
            AC.SetBool(IDLE, false);
            AC.SetBool(BACKWARDS, false);
        } else if (NMAgent.speed == runSpeed) {
            AC.SetBool(ATTACK, false);
            AC.SetBool(WALK, false);
            AC.SetBool(RUN, true);
            AC.SetBool(IDLE, false);
            AC.SetBool(BACKWARDS, false);
        } else {
            AC.SetBool(ATTACK, false);
            AC.SetBool(WALK, false);
            AC.SetBool(RUN, false);
            AC.SetBool(IDLE, true);
            AC.SetBool(BACKWARDS, false);
        }
    }

    private GameObject ShouldIFollowPlayer() {
        float[] playerDistances = new float[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++) {
            playerDistances[i] = Vector3.Distance(players[i].transform.position, transform.position);
        }
        float smallestDistance = playerDistances.Min();
        if (smallestDistance <= distanceToStartFollowingPlayer) {
            int smallestIndex = Array.IndexOf(playerDistances, smallestDistance);
            return players[smallestIndex];
        } else {
            return null;
        }
    }

    private bool ShouldITurnAround() {
        if (currentDestination == 1) {
            if (Vector3.Distance(transform.position, destination1) <= distanceThreshholdToTurnAround) {
                return true;
            }
        } else if (currentDestination == 2) {
            if (Vector3.Distance(transform.position, destination2) <= distanceThreshholdToTurnAround) {
                return true;
            }
        } else { //Needs to stop following player
            return true;
        }
        return false;
    }

}