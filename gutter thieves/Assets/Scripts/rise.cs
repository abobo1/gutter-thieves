using UnityEngine;
using System.Collections;

public class rise : MonoBehaviour
{

    private GlobalController GC;

    public float max;
    public float speed;
    public Vector3 direction;
    // Use this for initialization
    void Start() {
        GC = GameObject.Find("GameController").GetComponent<GlobalController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= max)
        {
            speed = 0.0f;
            if (!GC.gameReady) {
                GC.SpawnKnights();
            }
            GC.gameReady = true;
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
