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
