using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    private int randomPrefab;

    bool isDone = false;
    [SerializeField] private Transform spawnPoint;

    int numberoftwet = GameObject.FindGameObjectsWithTag("tweet").Length;
    void Update()
    {
       
        
            if (GameObject.FindGameObjectsWithTag("delete").Length <= 0 && !isDone)
            {
                randomPrefab = Random.Range(1, 10);
                GameObject newtweet = Instantiate(prefabs[randomPrefab], spawnPoint.position, Quaternion.identity) as GameObject;

                newtweet.transform.SetParent(GameObject.FindGameObjectWithTag("tweet").transform, false);

                isDone = true;
            }

        
           
    }
}
