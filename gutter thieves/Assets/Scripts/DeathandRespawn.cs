using UnityEngine;
using System.Collections;

public class DeathandRespawn : MonoBehaviour {

    public GameObject Spawner;
    public Transform secondarySpawner;
    public float SpawnDistanceThreshhold = 4.0f;

    private PlayerController MyPC;
    private GlobalController GC;

    void Start() {
        MyPC = GetComponent<PlayerController>();
        GC = GameObject.Find("GameController").GetComponent<GlobalController>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Knight"))
        {
            if (MyPC.hasCrown) {
                GC.ResetTheCrown();
                MyPC.hasCrown = false;
            }
            RespawnMe();
        }
    }

    public void RespawnMe() {
        GameObject[] knights = GameObject.FindGameObjectsWithTag("Knight");
        bool isThereAKnightInMyHome = false;
        foreach (GameObject knight in knights) {
            if (Vector3.Distance(knight.transform.position, Spawner.transform.position) < SpawnDistanceThreshhold) {
                isThereAKnightInMyHome = true;
                this.transform.position = Spawner.transform.position;
            }
        }
        if (isThereAKnightInMyHome) {
            this.transform.position = secondarySpawner.transform.position;
        } else {
            this.transform.position = Spawner.transform.position;
        }
    }

}
