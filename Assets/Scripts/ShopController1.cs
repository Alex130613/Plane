using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController1 : MonoBehaviour {

    public Image Present;
    public GameObject LockBuy;
    public PlaneController PC;
    public Text PriceTx;
    public GameObject BuyBtt;
    public GameObject ChooseBt;
    public GameObject Selected;
    public ManageData MD;
    public LevelManadger LM;
    GameObject NowPlane;
    DataPlaneButton NowPlaneButton;
    public GameObject[] PlanesButton;
    public void SetNowPlane(GameObject NP) { NowPlane = NP;
        NowPlaneButton = NowPlane.GetComponent<DataPlaneButton>();
    }
    void Start() {
        LM.LoadLevel();
        MD.LoadGame();
        NowPlane = PlanesButton[MD.getPlane()];
        NowPlaneButton = NowPlane.GetComponent<DataPlaneButton>();
        NowPlaneButton.LoadGame();
        NowPlaneButton.IsLock(LM.getLevel());
        NowPlaneButton.IsSelect = true;
        NowPlaneButton.Choose();
    }
    void Update()
    {
        for (int i = 0; i < PlanesButton.Length; ++i)
        {
            GameObject NowPlane1 = PlanesButton[i];
            DataPlaneButton NowPlaneButton1 = NowPlane1.GetComponent<DataPlaneButton>();
            NowPlaneButton1.IsLock(LM.getLevel());
        }
    }
    public void BuyBt() { if (MD.getCoins() >= NowPlaneButton.Price) {
            MD.Pay(NowPlaneButton.Price);
            NowPlaneButton.SetIsBuy(true);
            NowPlaneButton.SaveGame();
            NowPlaneButton.Choose();
        } }
    public void SelectBt() {
        PlanesButton[MD.getPlane()].GetComponent<DataPlaneButton>().IsSelect=false;
        MD.SelectPlane(NowPlaneButton.ind);
        NowPlaneButton.IsSelect = true;
        NowPlaneButton.Choose();
        PC.ChangePlane();
    }
    public void Reset() {
        for (int i = 1; i < PlanesButton.Length; ++i) {
            PlanesButton[i].GetComponent<DataPlaneButton>().Reset();
        }
    }
}
