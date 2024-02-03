using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikesPopup : Singleton<MonoBehaviour>
{
    public GameObject heartPrefab;
    public GameObject hatePrefab;

    public int particleNumber = 10;
    public float particleDuration = 5f;

    private static LikesPopup instance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    public static void SpawnLikes()
    {
        Transform canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;

        for (int i = 0; i < instance.particleNumber; i++)
        {
            Instantiate(instance.heartPrefab, canvas, false);
            Destroy(instance.heartPrefab, 5f);
            //Instantiate(heartPrefab, canvas.position, Quaternion.identity);
        }
    }
    public static void SpawnDislikes()
    {
        Transform canvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;

        for (int i = 0; i < instance.particleNumber; i++)
        {
            Instantiate(instance.hatePrefab, canvas, false);
            Destroy(instance.hatePrefab, 5f);
            //Instantiate(heartPrefab, canvas.position, Quaternion.identity);
        }
    }
}
