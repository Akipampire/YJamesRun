using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfiniteForward : MonoBehaviour
{
    [SerializeField] private GameObject []Players;
    [SerializeField] private GameObject []Chuncks;
    [SerializeField] private GameObject []LoadedChuncks;
    private int currentZAxis = 20;


    [SerializeField]
    private List<GameObject> ObstacleList;

    [SerializeField]
    private GameObject[] Obstacles;


    [SerializeField] private GameObject[] WallsRight;
    [SerializeField] private GameObject[] LoadedWallRight;
    [SerializeField] private GameObject[] WallsLeft;
    [SerializeField] private GameObject[] LoadedWallLeft;

    private void FixedUpdate()
    {
        for(int i = 0; i <= LoadedChuncks.Length - 1; i++)
        {
            foreach (var player in Players)
            {
                if (player.transform.position.z > LoadedChuncks[i].transform.position.z + 10)
                { 
                    Destroy(LoadedChuncks[i]);
                    LoadedChuncks[i] = Instantiate(Chuncks[Random.Range(0, Chuncks.Length)],new Vector3(0, 0, currentZAxis), Quaternion.identity);
                    currentZAxis += 10;

                    //Spawn obstacles on random lanes
                    Destroy(Obstacles[i]);
                    SpawnObstacle(currentZAxis, i);

                    //spawn walls
                    Destroy(LoadedWallRight[i]);
                    Destroy(LoadedWallLeft[i]);
                    LoadedWallRight[i] = Instantiate(WallsRight[Random.Range(0, WallsRight.Length)], new Vector3(15, 5, currentZAxis - 5), Quaternion.Euler(0, 0, 90));
                    LoadedWallLeft[i] = Instantiate(WallsLeft[Random.Range(0, WallsLeft.Length)], new Vector3(-15, 5, currentZAxis - 5), Quaternion.Euler(0, 0, -90));
                }
            }
        }
    }

    void SpawnObstacle(int plane, int i)
    {
        int RandomListNb = Random.Range(0, ObstacleList.Count);

        int RandomLane = Players[0].GetComponent<PlayerMovement>().lanesXCoordinate[Random.Range(0, 3)];

        //Debug.Log(RandomListNb);
        //Debug.Log(ObstacleList[RandomListNb]);

        Obstacles[i] = Instantiate(ObstacleList[RandomListNb], new Vector3(RandomLane, 0, plane), Quaternion.identity);

    }
}
