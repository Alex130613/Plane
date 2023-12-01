using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class DataPlaneButton : MonoBehaviour
{
    public Image Present;
    public int TargLevel;
    public int ind;
    public uint Price;
    public bool IsSelect;
    public bool IsUnLocked;
    public GameObject Select;
    public GameObject Lock;
    public Text TargLevTx;
    ShopController1 SC;
    bool IsBuy;
    public GameObject Activ;
    void Start() {
        LoadGame();
        SC = GameObject.Find("Shop").GetComponent<ShopController1>();
            Select.SetActive(IsSelect);
    }
    public void IsLock(int level) {
        IsUnLocked = (level >= TargLevel);
    }
    void Update() {
        Select.SetActive(IsSelect);
        TargLevTx.text = TargLevel.ToString();
        Lock.SetActive(!IsUnLocked);
        if (!IsUnLocked)
        {
            Image s2 = GameObject.Find("PlaneIm" + ind.ToString()).GetComponent<Image>();
            s2.color = new Vector4(15 / 255.0f, 15 / 255.0f, 15 / 255.0f, 1.0f);
        }
        else {
            Image s2 = GameObject.Find("PlaneIm" + ind.ToString()).GetComponent<Image>();
            s2.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
    public void Choose()
    {
        SC = GameObject.Find("Shop").GetComponent<ShopController1>();
        Select.SetActive(IsSelect);
        SC.SetNowPlane(gameObject);
        Activ.transform.position = transform.position;
        SC.LockBuy.SetActive(!IsUnLocked);
        SC.BuyBtt.SetActive(!IsBuy&&IsUnLocked);
        SC.ChooseBt.SetActive(IsBuy&&!IsSelect);
        SC.Selected.SetActive(IsSelect);
        if (!IsBuy) SC.PriceTx.text = Price.ToString();
        SC.PriceTx.gameObject.SetActive(!IsBuy);
        Image s2 = GameObject.Find("PlaneIm"+ind.ToString()).GetComponent<Image>();
        SC.Present.sprite = s2.sprite;
        if (!IsUnLocked)
        {
            SC.Present.color = new Vector4(15 / 255.0f, 15 / 255.0f, 15 / 255.0f, 1.0f);
        }
        else
        {
            SC.Present.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
    
    public void SetIsBuy(bool IB) { IsBuy=IB; }
    public bool getIsBuy() { return IsBuy; }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlaneData"+ind.ToString()+".dat");
        DataPlane data = new DataPlane();
        data.IsBuy = IsBuy;
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/PlaneData" + ind.ToString() + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlaneData" + ind.ToString() + ".dat", FileMode.Open);
            DataPlane data = (DataPlane)bf.Deserialize(file);
            file.Close();
            IsBuy = data.IsBuy;
        }
        else
        {
            IsBuy = (ind == 0);
        }
    }
    public void Reset()
    {
        IsBuy = (ind == 0);
        SaveGame();
    }
}

[Serializable]
class DataPlane {
    public bool IsBuy;
}