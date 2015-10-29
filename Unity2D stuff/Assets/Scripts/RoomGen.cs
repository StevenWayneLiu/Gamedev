using UnityEngine;
using System.Collections;

public class RoomGen : MonoBehaviour {

    public byte[,] rooms;

    public int mapSize = 5;

    public int roomSize = 5;

	// Use this for initialization
	void Start () {
        rooms = new byte[mapSize, mapSize];

        //fill up the map
        for (int x = -2; x < 3; x++)
        {
            for (int y = -2; y < 3; y++)
            {
                Vector3 pos = new Vector3(x * roomSize, y * roomSize, 0);//position to spawn object/room
                //spawn map and object
                GameObject room = (GameObject)Instantiate(Resources.Load("Room"), pos, Quaternion.identity);//spawn a room
                if (x != 0 || y != 0)//don't put anything in the middle room
                {
                    FillRoom(pos, room);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //randomly instantiates room content in the middle of a room
    void FillRoom(Vector3 pos, GameObject room)
    {
        int content = (int)(Random.value * 3);
        switch (content)
        {
            //add room content to the room script
            default:
                break;
        }
    }
}
