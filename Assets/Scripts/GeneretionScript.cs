using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneretionScript : MonoBehaviour {

    public GameObject[] StartavalableRooms;
    public GameObject[] MediumavalableRooms;
    public GameObject[] HardavalableRooms;
    public GameObject BirdPrefab;
    public int ind=0;
    public int indx1 = 0;
    public int indx2 = 0;
    public List<GameObject> currentRooms;
    List<GameObject> AllBirds;
    private float screenWidthInPoints;
    void AddRoom(float farhtestRoomEndx)
    {
        float roomWidth;
        float roomCenter;
        int roomIndex;
        GameObject room;
        ++ind;
        if (ind < 3)
        {
            do
            {
                roomIndex = Random.Range(0, StartavalableRooms.Length);
            } while (indx1 == roomIndex || indx2 == roomIndex);
            indx2 = indx1;
            indx1 = roomIndex;
            room = (GameObject)Instantiate(StartavalableRooms[roomIndex]);
            var coll = room.transform.Find("ground").GetComponent<BoxCollider2D>().size;
            roomWidth = (float)coll.x * room.transform.Find("ground").transform.localScale.x;
            roomCenter = farhtestRoomEndx + roomWidth * 0.5f;
            room.transform.position = new Vector3(roomCenter, 0, 0);
            currentRooms.Add(room);
        }
        else
        {

            if (ind < 20)
            {
                do
                {
                    roomIndex = Random.Range(0, MediumavalableRooms.Length);
                } while (indx1 == roomIndex || indx2 == roomIndex);
                indx2 = indx1;
                indx1 = roomIndex;
                room = (GameObject)Instantiate(MediumavalableRooms[roomIndex]);
                var coll = room.transform.Find("ground").GetComponent<BoxCollider2D>().size;
                roomWidth = (float)coll.x * room.transform.Find("ground").transform.localScale.x;
                roomCenter = farhtestRoomEndx + roomWidth * 0.5f;
                room.transform.position = new Vector3(roomCenter, 0, 0);
                currentRooms.Add(room);
            }
            else
            {
                do
                {
                    roomIndex = Random.Range(0, HardavalableRooms.Length);
                } while (indx1 == roomIndex || indx2 == roomIndex);
                indx2 = indx1;
                indx1 = roomIndex;
                room = (GameObject)Instantiate(HardavalableRooms[roomIndex]);
                var coll = room.transform.Find("ground").GetComponent<BoxCollider2D>().size;
                roomWidth = (float)coll.x * room.transform.Find("ground").transform.localScale.x;
                roomCenter = farhtestRoomEndx + roomWidth * 0.5f;
                room.transform.position = new Vector3(roomCenter, 0, 0);
                currentRooms.Add(room);
            }
            int value =(int) Random.Range(1.0f,4.0f);
            if (value == 1) {
                GameObject Bird= (GameObject)Instantiate(BirdPrefab);
                float By = Random.Range(-1.0f, 2.5f);
                float Bx = room.transform.position.x;
                Bird.transform.position = new Vector3(Bx, By, -2);
                AllBirds.Add(Bird);
            }
        }
    }
    void AddRoom()
    {
        ++ind;
        int roomIndex = Random.Range(0, StartavalableRooms.Length);
        indx1 = roomIndex;
        indx2 = indx1;
        GameObject room = (GameObject)Instantiate(StartavalableRooms[roomIndex]);
        var coll = room.transform.Find("ground").GetComponent<BoxCollider2D>().size;
        float roomWidth = (float)coll.x * room.transform.Find("ground").transform.localScale.x;
        room.transform.position = new Vector3(0, 0, 0);
        currentRooms.Add(room);
    }
    public void GenerateRoomIfRequred()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;
        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
        float farhtestRoomEndX = 0;
        foreach (var room in currentRooms)
        {
            var coll = room.transform.Find("ground").GetComponent<BoxCollider2D>().size;
            float roomWidth =(float)coll.x* room.transform.Find("ground").transform.localScale.x;
            float roomStartX = room.transform.position.x - roomWidth * 0.5f;
            float roomEndX = roomStartX + roomWidth;
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            farhtestRoomEndX = Mathf.Max(farhtestRoomEndX, roomEndX);
        }
        foreach (var Bird in AllBirds) {
            if (Bird.transform.position.x < transform.position.x - 8) {
                AllBirds.Remove(Bird);
                Destroy(Bird);
            }
        }
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        if (addRooms)
        {
            AddRoom(farhtestRoomEndX);
        }
    }
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        AllBirds = new List<GameObject>();
        AddRoom();
    }
    void FixedUpdate()
    {
        GenerateRoomIfRequred();
    }
   
}

