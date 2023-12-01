using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAnim : MonoBehaviour {
    public GameObject Shop;
    public PlaneController PC;
    public void CloseShop()
    {
       Shop.SetActive(false);
    }
    public void OpenMenu()
    {
       Application.LoadLevel("Start");
    }
    public void OpenStart()
    {
        Application.LoadLevel("Game");
    }
    public void Record() {
        PC.NewRecordStart();
    }
}
