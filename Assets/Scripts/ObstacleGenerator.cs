using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
    
    public GameObject[] easyObstacles;
    public GameObject[] hardObstacles;
    public GameObject floor;

    private int floorX;
	// Use this for initialization
	void Start () {
        InitFloor();
        InvokeRepeating("SpawnFloor", 0, 0.5f);
        Invoke("GenerateObstacle", 3);
	}

    private void InitFloor() {
        GameObject tempFloor;
        float time = 0.5f;
        for (int i = 0; i < 10; i++) {
            floorX = i * 3;
            tempFloor = (GameObject)Instantiate(floor, new Vector3(floorX, 0, 0), Quaternion.identity);
            tempFloor.GetComponent<FloorController>().fallTime = time;
            time += 0.5f;
        }
    }

    private void SpawnFloor() {
        GameObject tempFloor;
        floorX += 3;
        tempFloor = (GameObject)Instantiate(floor, new Vector3(floorX, 0, 0), Quaternion.identity);
        tempFloor.GetComponent<FloorController>().fallTime = 5.5f;
    }

    private void GenerateObstacle() {
        int rolled = Random.Range(0, easyObstacles.Length);
        //Change rotation after getting new prefab assets
        Instantiate(easyObstacles[rolled], new Vector3(floorX, 2.5f, -1), Quaternion.identity);
        int time = Random.Range(0, 3);
        Invoke("GenerateObstacle", time + 1);
    }
}
