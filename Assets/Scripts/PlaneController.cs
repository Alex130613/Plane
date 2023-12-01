using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlaneController : MonoBehaviour {
    public GameObject[] Planes;
    private bool dead1 = false;
    private bool dead2 = false;
    public ParallaxScroll parallax;
    public float Speed = 0.05f;
    public float SpeedD;
    private float Speedfall = 0f;
    private GameObject PlaneObj;
    public PlaneDethController plane;
    public bool start;
    public GameObject restDialog;
    public GameObject Menu1;
    public GameObject Menu2;
    public GameObject NowScore;
    public GameObject StartButton;
    public GameObject Shop;
    public Text coin;
    public Text point;
    public Text coin1;
    public Text point1;
    public Text mCoin;
    public Text record;
    public GameObject NewRecordImage;
    public CameraFollow cam;
    public GameObject man;
    float MaxRot;
    float AccelSpeed;
    float rot=0.0f;
    float AccelRot;
    private bool isSave=false;
    public float SpeedR;
    private bool m = false;
    private float TFall=0.0f;
    public float FallAng;
    public ManageData MD;
    public bool isMenu;
    bool NewRec;


    public float planex() {
        return transform.position.x;
    }
    public void ChangePlane()
    {
        Destroy(PlaneObj);
        PlaneObj = Instantiate(Planes[MD.getPlane()]) as GameObject;
        PlaneObj.transform.SetParent(this.transform, false);
        plane = PlaneObj.GetComponent<PlaneDethController>();
    }
    void Start() {
        if (!isMenu)
        {
            Menu1 = new GameObject();
            Menu2 = new GameObject();
            NowScore.SetActive(true);
            Shop = new GameObject();
        }
        else
        {
            Menu1.SetActive(true);
            Menu2.SetActive(true);
            NowScore.SetActive(false);
        }
        PlaneObj=Instantiate(Planes[MD.getPlane()]) as GameObject;
        PlaneObj.transform.SetParent(this.transform,false);
        plane = PlaneObj.GetComponent<PlaneDethController>();
        Shop.SetActive(false);
        restDialog.SetActive(false);
        mCoin.text = MD.getCoins().ToString();
        record.text =MD.getRecord().ToString();
        SpeedD = Speed;
       start = false;
        MaxRot = Speed * 225/ 3.14f;
        AccelSpeed = Speed / 10;
        AccelRot = MaxRot / 10;
        parallax.offset = transform.position.x;
    }
    void FixedUpdate()
    {
        if (isMenu)
        {
            mCoin.text = MD.getCoins().ToString();
        }
        coin1.text = "x" + plane.getcoin().ToString();
        point1.text = "point: " + cam.getp().ToString();
        MaxRot = Speed * 225 / 3.14f;
        AccelSpeed = Speed / 10;
        AccelRot = MaxRot / 10;
        plane.AdjustJetack(dead1);
        if (start)
        {
            dead2 = plane.getDead();
            dead1 = plane.UpdateGroundedStatus();
            bool jetpackActive = Input.GetButton("Fire1");
            if (!dead1)
            {
                if (jetpackActive && !dead2 && !plane.dead3)
                {
                    float AS = (Speed - SpeedD) / 5;
                    if (Speed != SpeedD) Speed -= AS;
                    TFall = 0.0f;
                    if (rot == 0.0f) SpeedR = Speed;
                    transform.Rotate(new Vector3(0, 0, rot));
                    plane.ang += rot;
                    if (rot < MaxRot) rot += AccelRot;
                    if (SpeedR > 0) SpeedR -= AccelSpeed;
                    else SpeedR = 0.0f;
                    transform.Translate(new Vector3(SpeedR, 0, 0));
                    parallax.offset = transform.position.x;
                    // ParticleSystem.ShapeModule sh= plane.Smog.shape;
                    //  sh.rotation += new Vector3 (rot,0,0);
                }
                else
                {
                    Speed -= (Mathf.Sin(plane.ang / 180 * Mathf.PI) / 5000 - 0.0001f);
                    rot = 0.0f;
                    float PlanAng = plane.ang % 360;
                    FallAng = (Mathf.Abs(PlanAng - 180) - 90) / 90;
                    if (!dead2 && (PlanAng >= 300 || PlanAng <= 90))
                    {
                        TFall += 0.03f;
                        FallAng = (Mathf.Atan(TFall) / Mathf.PI) * 2 * FallAng;
                        plane.ang -= FallAng;
                        transform.RotateAround(plane.GetPos(), Vector3.back, FallAng);
                    }
                    if (!dead2) plane.Dead3Update(PlanAng > 90 && PlanAng < 270);
                    SpeedD += 0.00001f;
                    Speed += 0.00001f;
                    transform.Translate(new Vector3(Speed, 0, 0));
                    if (!plane.dead3) parallax.offset = transform.position.x;
                    if (dead2)
                    {
                        Speedfall += 0.002f;
                        transform.position -= new Vector3(0, Speedfall, 0);
                        if ((plane.dead3 || plane.getDead()) && !m)
                        {
                            GameObject ContrMan = (GameObject)Instantiate(man);
                            ContrMan.transform.position = transform.position;
                            ContrMan.transform.rotation = transform.rotation;
                            ContrMan.transform.Find("man").gameObject.transform.Rotate(0, 0, -plane.ang);
                            cam.target = ContrMan;
                            m = true;
                        }
                    }
                }
            }
        }
        if (dead1 || plane.dead3) {
            if(!isSave){
                NewRec= MD.SetData(cam.getp(), plane.getcoin());
                MD.SaveGame();
                isSave = true;
            }
            restDialog.SetActive(true);
            restDialog.GetComponent<Animator>().SetBool("Open", true);
            coin.text = "x" + plane.getcoin().ToString();
            point.text = "point: " + cam.getp().ToString();
        }
    }
    public void RestartGame() {
        restDialog.GetComponent<Animator>().SetBool("Return",true);
    }
    public void Exit()
    {
        restDialog.GetComponent<Animator>().SetBool("Menu", true);
    }
    public void ReserData() { MD.Reset();
        MD.LoadGame();
        mCoin.text = MD.getCoins().ToString();
        record.text = MD.getRecord().ToString();
    }
    public void StartGame() { start = true;
        if (isMenu)
        {
            Animator a = Menu1.GetComponent<Animator>();
            a.SetBool("Start", true);
            a = Menu2.GetComponent<Animator>();
            a.SetBool("Start", true);
        }
        else { Menu1.SetActive(false);
            Menu1.SetActive(false);
        }
        NowScore.SetActive(true);
        StartButton.SetActive(false);
    }
    public void OpenShop() { Shop.SetActive(true);
       Animator Anim = Shop.GetComponent<Animator>();
        Anim.SetBool("isOpen", true);
    }
    public void NewRecordStart() {
        NewRecordImage.SetActive(NewRec);
    }
    public void CloseShop()
    {
        Animator Anim = Shop.GetComponent<Animator>();
        Anim.SetBool("isOpen", false);}
}
