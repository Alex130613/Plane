using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManController : MonoBehaviour {
    public bool ground = false;
    public Transform groundCheckTransform;
    private Animator animator;
    public LayerMask groundCheckLaerMask;
    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
    }
	// Update is called once per frame
	void FixedUpdate () {
        if (!ground)
        {
            transform.position -= new Vector3(0, 0.01f, 0);
            ground = Physics2D.OverlapCircle(groundCheckTransform.position, 0.3f, groundCheckLaerMask);
            animator.SetBool("ground", ground);
        }
    }
}
