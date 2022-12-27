using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("SCRIPTS")]
    public GameUI gameUI;

    [Header("GAMEOBJECT")]
    public GameObject obstaclePrefab;
    public GameObject question;
    public GameObject[] obstacles;

    [Header("FLOAT")]
    public float time;
    public float repeatRate;

    [Header("INT")]
    public int spawnAmount;

    [Header("BOOL")]
    public bool isSpawnAvaible;

    void Start()
    {
        spawnAmount = 0;

        gameUI = FindObjectOfType<GameUI>();

        

         InvokeRepeating("SpawnObstacle", time, repeatRate);

        


    }


    void Update()
    {
        if (gameUI.isGameStart)
        {
            question = GameObject.FindGameObjectWithTag("Question");
            obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

            if (question != null)
            {
                spawnAmount = 0;
                for (int i = 0; i < obstacles.Length; i++)
                {
                    Destroy(obstacles[i]);
                }
            }
            else
            {
                spawnAmount = 1;
            }
        }


       

    }

    public void SpawnObstacle()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(Random.Range(-1f, 2f), 1.5f, transform.position.z), Quaternion.Euler(0, 180, 0));

        }




    }
}
