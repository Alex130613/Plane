using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelManadger : MonoBehaviour {
    public ManageData MD;
    public Slider LevelBar;
    public Text tLevel;
    public Text tPointTarg;
    int SpeedP;
    int AllPoint;
    int Level;
    int Begin;
    int Target;
    public int getLevel() { return Level; }
    void Start() {
        //LevelBar = GameObject.Find("Slider").GetComponent<Slider>();
        LoadLevel();
    }
    void Update() {
        SpeedP =(int) (MD.getAllPoint() - AllPoint)/20;
        if (SpeedP == 0) { SpeedP = (MD.getAllPoint() - AllPoint) % 20; }
        if (AllPoint < MD.getAllPoint()) {
            AllPoint += SpeedP;
        }
        if (AllPoint > Target) {
            int dist = Target - Begin;
            Begin = Target;
            Target +=dist+500;
            Level++;
        }
        LevelBar.maxValue = Target;
        LevelBar.minValue = Begin;
        LevelBar.value = AllPoint;
        tLevel.text = Level.ToString();
        tPointTarg.text = AllPoint.ToString() + "/" + Target.ToString();
        SaveLevel();
    }
    void OnDisable() {
        AllPoint=MD.getAllPoint();
        while (AllPoint > Target)
        {
            int dist = Target - Begin;
            Begin = Target;
            Target += dist + Level * 500;
            Level++;
        }
        SaveLevel();
    }
    public void SaveLevel()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/LevelData.dat");
        SaveData1 data = new SaveData1();
        data.AllPoint = AllPoint;
        data.Level = Level;
        data.Begin = Begin;
        data.Target = Target;
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/LevelData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/LevelData.dat", FileMode.Open);
            SaveData1 data = (SaveData1)bf.Deserialize(file);
            file.Close();
            AllPoint = data.AllPoint;
            Level = data.Level;
            Begin = data.Begin;
            Target = data.Target;
        }
        else { Level = 1;
            Target = 1000;
        }
        LevelBar.maxValue = Target;
        LevelBar.minValue = Begin;
        LevelBar.value = AllPoint;
        tLevel.text = Level.ToString();
        tPointTarg.text = AllPoint.ToString() + "/" + Target.ToString();

    }
    public void Reset() {
        AllPoint = 0;
        Level = 1;
        Begin = 0;
        Target = 1000;
        LevelBar.maxValue = Target;
        LevelBar.minValue = Begin;
        LevelBar.value = AllPoint;
        tLevel.text = Level.ToString();
        tPointTarg.text = AllPoint.ToString() + "/" + Target.ToString();
    }
}

[Serializable]
class SaveData1
{
    public int AllPoint;
    public int Level;
    public int Begin;
    public int Target;
}