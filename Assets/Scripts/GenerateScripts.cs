using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScripts : MonoBehaviour {
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private float screenWidthInPoints;

    // Use this for initialization
    void Start () {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddRoom(float farthestRoomEndX)
    {
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
        float roomWidth = room.transform.Find("floor").localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;
        room.transform.position = new Vector3(roomCenter, 0, 0);
        currentRooms.Add(room);
    }

    private void GenerateRoomIfRequired()
    {
        
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;

        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
  
        float farthestRoomEndX = 0;
        foreach (var room in currentRooms)
        {
           
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;
            
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }

}
