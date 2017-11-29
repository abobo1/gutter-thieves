using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    
    public Transform player;

    public float distance = 1.75f;
    public float height = 1.5f;
    public float forwardDistance = 0.5f;
    private float smoothness = 5.0f;
    private float rotationSmoothing = 10.0f;

    private Vector3 cameraRotation;

    private float distanceToWall = 1.0f;
    private float heightWhileBumping = 2.0f;

    public float initialRotation = 1.4f;


    void Awake () {
        cameraRotation = new Vector3(0, initialRotation, 0);
        GetComponent<Camera>().transform.parent = player;
    }
	
	void FixedUpdate () {
        Vector3 wantedPosition = player.TransformPoint(0, height, -distance);

        Vector3 back = player.transform.TransformDirection(-1 * Vector3.forward);


        RaycastHit rcHit;
        if (Physics.Raycast(player.position, back, out rcHit, distanceToWall) && rcHit.transform != player) {
            wantedPosition.x = rcHit.point.x;
            wantedPosition.z = rcHit.point.z;
            wantedPosition.y = Mathf.Lerp(rcHit.point.y + heightWhileBumping, wantedPosition.y, Time.deltaTime * smoothness);
        }

        transform.position = Vector3.Lerp(transform.position, wantedPosition + transform.forward * forwardDistance, Time.deltaTime * smoothness);

        Vector3 lookPosition = player.TransformPoint(cameraRotation);

        Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, player.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationSmoothing);
    }
}
