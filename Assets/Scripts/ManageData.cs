using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ManageData : MonoBehaviour 
    {
        int AllPoint;
        int Record;
        uint Coins;
    public int plane;
    public void SelectPlane(int ind) { plane = ind;
        SaveGame();
    }
    public void Pay(uint Price)
    {
        Coins -= Price;
        SaveGame();
    }
        public bool SetData(int p, uint c)
        {
            AllPoint += p;
            Coins += c;
        bool NewRec= p > Record;
            if (NewRec) Record = p;
        return NewRec;
        }
        public int getAllPoint() { return AllPoint; }
        public int getRecord() { return Record; }
        public uint getCoins() { return Coins; }
    public int getPlane() { return plane; }
    void Start() { LoadGame(); }
        public void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/PointCoinData2.dat");
            SaveData data = new SaveData();
            data.AllPoint = AllPoint;
            data.Coins = Coins;
            data.Record = Record;
        data.plane = plane;
        bf.Serialize(file, data);
            file.Close();
        }
        public void LoadGame()
        {
        if (File.Exists(Application.persistentDataPath + "/PointCoinData2.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PointCoinData2.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            AllPoint = data.AllPoint;
            Record = data.Record;
            Coins = data.Coins;
            plane = data.plane;
        }
        else
        {
            plane = 0;
        }
    }
        public void Reset()
        {
            AllPoint = 0;
            Record = 0;
            Coins = 0;
            plane = 0;
            SaveGame();
        }
    }

    [Serializable]
    class SaveData
    {
        public int AllPoint;
        public int Record;
        public uint Coins;
    public int plane;
    }
