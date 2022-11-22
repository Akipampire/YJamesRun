using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    //----vars
    [SerializeField]
    GameObject PlaneGameObject;
    [SerializeField]
    Vector3 SpawnPosition;
    [SerializeField]
    List<GameObject> ObstacleList;

    //----vars

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject plane = Instantiate(PlaneGameObject, SpawnPosition, Quaternion.identity);

            SpawnObstacle(plane);
        }
    }

    void SpawnObstacle(GameObject plane)
    {
        int RandomListNb = Random.Range(0, ObstacleList.Count);

        //Debug.Log(RandomListNb);
        //Debug.Log(ObstacleList[RandomListNb]);

        Instantiate(ObstacleList[RandomListNb], plane.transform.localPosition + (plane.transform.right * Random.Range(-25, 25)), Quaternion.identity);

    }
}
