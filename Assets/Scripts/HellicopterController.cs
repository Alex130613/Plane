using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterController : MonoBehaviour {
    public float Speed = 0.03f;
    public float Usk = 0.001f;
    public int index=0;
    private float Starty=0.0f;
    private float Startx = 0.0f;
    // Update is called once per frame
    void Start() {
        Starty = transform.position.y;
        Startx = transform.position.x;
    }
    void FixedUpdate()
    {
        switch (index.ToString()) {
            case "1":
                {
                        if (transform.position.y <= Starty) Speed += Usk;
                        else Speed -= Usk;
                        transform.position += new Vector3(0, Speed, 0);
                    break;
                }
            case "2":
                {
                    if (transform.position.x <= Startx) { Speed += Usk; }
                    else { Speed -= Usk; }
                    transform.position += new Vector3(Speed, 0, 0);
                    break;
                }
            case "3":
                {
                        if (transform.position.y <= Starty) Speed += Usk;
                        else Speed -= Usk;
                        transform.position += new Vector3(0, Speed, 0);
                    transform.position += new Vector3(Speed, 0, 0);
                    break;
                }
            case "4":
                {
                    if (transform.position.y <= Starty) Speed += Usk;
                    else Speed -= Usk;
                    transform.position += new Vector3(0, Speed, 0);
                    transform.position -= new Vector3(Speed, 0, 0);
                    break;
                }
        } }
}
