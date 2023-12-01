using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDethController : MonoBehaviour
{
    private uint coins = 0;
    private bool dead1 = false;
    private bool dead2 = false;
    public bool dead3 = false;

    public Transform groundCheckTransform;
    public Transform groundCheckTransform1;
    private Animator animator;
    public LayerMask groundCheckLaerMask;
    public ParticleSystem Smog;
    //public CoinController coincontr;
    public float ang=360.0f;
    // Use this for initialization
    public Texture2D coinIconTexture;

    public uint getcoin() {
        return coins;
    }
    void Start () {
        animator = GetComponent<Animator>();
    }
    void CollectCoin(Collider2D coinCollider) {
        coins++;
        GameObject coin = coinCollider.gameObject;
        Destroy( coin);
        // = coin;
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Coins")) {
            CollectCoin(collider);
        }
        else HitByObj(collider); }
    void HitByObj(Collider2D hellicopterCollider)
    {
        dead3 = true;
        dead2 = true;
        animator.SetBool("dead3", dead3);
         }
    public bool UpdateGroundedStatus()
    {
        dead1 = Physics2D.OverlapCircle(groundCheckTransform.position, 0.3f, groundCheckLaerMask)|| Physics2D.OverlapCircle(groundCheckTransform1.position, 0.3f, groundCheckLaerMask);
        animator.SetBool("dead1", dead1);
        return dead1;
    }
    public bool getDead() { return dead2; }
    public void AdjustJetack(bool dead1)
    {
        ParticleSystem.ShapeModule sh = Smog.shape;
        sh.rotation = new Vector3 (ang,0,0);
        ParticleSystem.EmissionModule jpEmission = Smog.emission;
        jpEmission.enabled = dead1;
    }
    public void Dead3Update(bool resh) {
        dead3 = resh;
        animator.SetBool("dead3", dead3);
        if (resh) dead2 = resh;
    }
    public Vector3 GetPos() {
        return transform.position;
    }
}
