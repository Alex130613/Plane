using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject target;
    private float distanceToTarget;
    public int p;
    // Use this for initialization
    void Start () {
        distanceToTarget = transform.position.x - target.transform.position.x;
        }

    // Update is called once per frame
    void Update () {
        float targetx = target.transform.position.x;
        Vector3 newCam = transform.position;
        newCam.x = targetx+distanceToTarget;
        transform.position = newCam;
        p = (int)(transform.position.x*5);
	}
    public int getp() { return p; }
}
