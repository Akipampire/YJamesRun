using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfiniteForward : MonoBehaviour
{
    //Players
    [SerializeField] private PlayerMovement []Players;
    [SerializeField] private float speedProgression = 1;
    private float mostAdvancedPlayerZ = 0;
    //Chuncks
    [SerializeField] private GameObject []Chuncks;
    [SerializeField] private List<GameObject> LoadedChuncks;
    private int currentZAxis = 20;


    [SerializeField]
    private List<GameObject> ObstacleList;

    [SerializeField]
    private List<GameObject> SpawnedObstacles;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinSpawnChance = 10;

    [SerializeField] private GameObject[] WallsRight;
    [SerializeField] private GameObject[] LoadedWallRight;
    [SerializeField] private GameObject[] WallsLeft;
    [SerializeField] private GameObject[] LoadedWallLeft;

    private int[] lanesXCoordinate;

    private void Start() {
        lanesXCoordinate = Players[0].GetComponent<PlayerMovement>().lanesXCoordinate;
    }

    private void FixedUpdate()
    {
        var tempNewMostAdvancedZ = Mathf.Max(Players[0].transform.position.z, Players[1].transform.position.z);
        //New chunck
        if (tempNewMostAdvancedZ >= 10 + mostAdvancedPlayerZ) {
            mostAdvancedPlayerZ = tempNewMostAdvancedZ;
            LoadChunck();
            currentZAxis += 10;
        }

        for (int i = 0; i <= LoadedChuncks.Count - 1; i++)
        {
            var count = 0; 
            foreach (var player in Players)
            {
                player.forwardSpeed += Time.fixedDeltaTime / 100 * speedProgression;
                if (player.transform.position.z > LoadedChuncks[i].transform.position.z + 15)
                    count++;
            }
            if(count == Players.Length) {
                //Destroy lane that both player doesnt see
                Destroy(LoadedChuncks[i]);
                LoadedChuncks.RemoveAt(i);
            }
        }
    }
    void LoadChunck() {
        var newChunck = Instantiate(Chuncks[Random.Range(0, Chuncks.Length)], new Vector3(0, 0, currentZAxis), Quaternion.identity);
        LoadedChuncks.Add(newChunck);
        //Walls
        Instantiate(WallsRight[Random.Range(0, WallsRight.Length)], new Vector3(15, 5, currentZAxis + 5), Quaternion.Euler(0, 0, 90),newChunck.transform);
        Instantiate(WallsLeft[Random.Range(0, WallsLeft.Length)], new Vector3(-15, 5, currentZAxis + 5), Quaternion.Euler(0, 0, -90), newChunck.transform);

        //Spawn obstacles on random lanes
        SpawnObstacle(newChunck);
        if (Random.Range(0, 100) <= coinSpawnChance) SpawnCoin(newChunck);
    }
    void SpawnCoin(GameObject parent) {
        int RandomLane = lanesXCoordinate[Random.Range(0, lanesXCoordinate.Length)];
        Instantiate(coinPrefab, new Vector3(RandomLane, 0, currentZAxis-5), Quaternion.identity, parent.transform);
    }
    void SpawnObstacle(GameObject parent)
    {
        int RandomListNb = Random.Range(0, ObstacleList.Count);

        int RandomLane = lanesXCoordinate[Random.Range(0, lanesXCoordinate.Length)];

        SpawnedObstacles.Add(Instantiate(ObstacleList[RandomListNb], new Vector3(RandomLane, 0, currentZAxis), Quaternion.identity,parent.transform));
    }
}
