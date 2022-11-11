using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfiniteForward : MonoBehaviour
{
    [SerializeField] private GameObject []Players;
    [SerializeField] private GameObject []Chuncks;
    [SerializeField] private GameObject []LoadedChuncks;
    private int currentZAxis = 20;

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
                }
            }
        }
    }
}
