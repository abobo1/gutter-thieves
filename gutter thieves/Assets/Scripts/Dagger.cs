using UnityEngine;
using System.Collections;

public class Dagger : MonoBehaviour {

    private int playerNumber;

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "Dagger1") {
            playerNumber = 1;
        } else if (gameObject.tag == "Dagger2") {
            playerNumber = 2;
        } else {
            print("ERROR: DAGGER NEEDS CORRECT TAG");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (other.gameObject.GetComponent<PlayerController>().playerNumber != playerNumber) {
                other.gameObject.GetComponent<DeathandRespawn>().RespawnMe();
            }
        }
    }
}
