using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using utils;
public class InfiniteForward : MonoBehaviour
{
    //Players
    [Header("---------------- Player ----------------")]
    [SerializeField] private PlayerMovement []Players;
    [SerializeField] private float speedProgression = 1;
    [SerializeField] public float maxPlayerSpeed;
    [Header("---------------- Chunck ----------------")]
    [SerializeField] private GameObject []Chuncks;
    [SerializeField] private int NumberOfChunckToPreLoad = 3;
    [SerializeField] private List<GameObject> LoadedChuncks;
    private int currentZAxis = -10;
    [Header("---------------- Wall Prefabs ----------------")]
    [SerializeField] private GameObject[] WallsRight;
    [SerializeField] private GameObject[] LoadedWallRight;
    [SerializeField] private GameObject[] WallsLeft;
    [SerializeField] private GameObject[] LoadedWallLeft;
    [Header("---------------- Obstacle ----------------")]
    [SerializeField] private List<Obstacle> ObstacleList;
    [SerializeField] private float ObstacleSpawnProbability = 0.33f;
	[Header("---------------- Win Condition ----------------")]
    [SerializeField] public float distanceRequired = 30f;
	[SerializeField] public float durationRequired = 15f;
    public float actualDuration = 0f;
	[Header("---------------- Other ----------------")]
    [SerializeField] private PowerUP[] powerupPrefabs ;
    [SerializeField] private float powerUpSpawnChance = 5;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinSpawnChance = 10;


    private int[] lanesXCoordinate;


    private void Start() {
        lanesXCoordinate = Players[0].GetComponent<PlayerMovement>().lanesXCoordinate;
        for (int i = 0; i <= NumberOfChunckToPreLoad; i++) {
            if (i < 3) LoadChunck(true);
            else LoadChunck();
        }
    }
	private void FixedUpdate()
    {
		float mostAdvancedPlayerZ = Mathf.Max(Players[0].transform.position.z, Players[1].transform.position.z);
		float lessAdvancedPlayerZ = Mathf.Min(Players[0].transform.position.z, Players[1].transform.position.z);
        //Victory condition
        if (lessAdvancedPlayerZ < mostAdvancedPlayerZ - distanceRequired) actualDuration += Time.deltaTime;
        else actualDuration = 0;
        if (actualDuration >= durationRequired) GameManager.Instance.PlayerReachWinCondition();
		//New chunck
		while (mostAdvancedPlayerZ > LoadedChuncks[^NumberOfChunckToPreLoad].transform.position.z + 10) { 
            LoadChunck();
        }
        //Speeds
        float speedGain = Time.fixedDeltaTime / 100 * speedProgression;
		maxPlayerSpeed += speedGain;
        foreach (var player in Players) player.forwardSpeed += speedGain;
		//chunck gen
		for (int i = 0; i < LoadedChuncks.Count;) {
            GameObject Chunck = LoadedChuncks[i];
			if (Chunck.transform.position.z < lessAdvancedPlayerZ - 15) {
                Destroy(Chunck);
                LoadedChuncks.RemoveAt(i);
            }else i++; //If the list is not modified we can go to the next element
		}
	}

    void LoadChunck(bool removeObstacle = false) {
        var newChunck = Instantiate(Chuncks[Random.Range(0, Chuncks.Length)], new Vector3(0, 0, currentZAxis), Quaternion.identity);
        LoadedChuncks.Add(newChunck);
        //Walls
        Instantiate(WallsRight[Random.Range(0, WallsRight.Length)], new Vector3(15, 5, currentZAxis + 5), Quaternion.Euler(0, 0, 90),newChunck.transform);
        Instantiate(WallsLeft[Random.Range(0, WallsLeft.Length)], new Vector3(-15, 5, currentZAxis + 5), Quaternion.Euler(0, 0, -90), newChunck.transform);
        //Spawn obstacles on random lanes
        if (!removeObstacle) {
            SpawnObstacle(newChunck);
            if (Random.Range(0, 100) <= coinSpawnChance) SpawnCoin(newChunck);
            if (Random.Range(0, 100) <= powerUpSpawnChance) SpawnPowerUp(newChunck);
        }
        currentZAxis += 10;
    }
    void SpawnCoin(GameObject parent) {
        int RandomLane = lanesXCoordinate[Random.Range(0, lanesXCoordinate.Length)];
        Instantiate(coinPrefab, new Vector3(RandomLane, 1, currentZAxis - 5), Quaternion.identity, parent.transform);
    }
    void SpawnPowerUp(GameObject parent) {
        int RandomLane = lanesXCoordinate[Random.Range(0, lanesXCoordinate.Length)];
        Instantiate(powerupPrefabs.RandomElements(), new Vector3(RandomLane, 1, currentZAxis - 5), Quaternion.identity, parent.transform);
    }
    void SpawnObstacle(GameObject actualChunck)
    {
        bool allLanesAreStuck = true;
        List<Obstacle> obstaclesToInstantiate = new List<Obstacle>() { null, null, null };
		for(int i = 0; i < lanesXCoordinate.Length;i++) {
			if (Random.value < ObstacleSpawnProbability) {
				Obstacle newObstacle = ObstacleList.RandomElements();
                newObstacle.transform.position = new Vector3(lanesXCoordinate[i], 0, currentZAxis);//Positionnement sur la lane
                obstaclesToInstantiate[i] = newObstacle;
				//Si j'ai deja une voie de libre je fait pas le check, sinon je check si l'obstacle peut �tre esquiver
				if (allLanesAreStuck && (newObstacle.typeEsquive.Length > 1 || !newObstacle.typeEsquive.Contains(ESQUIVE_TYPE.NOT_ESQUIVABLE))) allLanesAreStuck = false;
			}
		}
		if (allLanesAreStuck) {
            int lane = Random.Range(0,lanesXCoordinate.Length);
            obstaclesToInstantiate[lane] = ObstacleList.Where(obstacle => obstacle.typeEsquive.Length > 1 || !obstacle.typeEsquive.Contains(ESQUIVE_TYPE.NOT_ESQUIVABLE)).ToList().RandomElements();
		}
        foreach(Obstacle obstacle in obstaclesToInstantiate) if(obstacle != null) Instantiate(obstacle, actualChunck.transform,true);
	}
}
