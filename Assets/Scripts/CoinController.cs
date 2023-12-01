using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {
    public float Speed=0.001f;
    public float Usk=0.0001f;
    public bool sobr=false;
    public float scale = 0.105f;
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.localScale.x >= scale)
        {
            Speed -= Usk;
        }
        else
        {
            Speed += Usk;
        }
        transform.localScale += new Vector3 (Speed,Speed,Speed);
	}
}
