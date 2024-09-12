using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] PillarsPrefabArray;
    [SerializeField] private Vector3 spawnPosition;

    [SerializeField] private int[] spawnFrequency; 
    [SerializeField] private int[] spawnTime;

    private float currentTime;

    private void Start()
    {
        StartCoroutine(SpawnPillarsEveryXSeconds());
        currentTime = Time.timeSinceLevelLoad;
    }

    private IEnumerator SpawnPillarsEveryXSeconds()
    {
        while (!GameManager.Instance.gameOver) 
        {
            currentTime = Time.timeSinceLevelLoad;
            int randomNumber = Random.Range(0, PillarsPrefabArray.Length);
            int spawnTimer = spawnTime[0];


            if (currentTime > spawnFrequency[1] && currentTime < spawnFrequency[2])
            {
                spawnTimer = spawnTime[1];
            }

            else if (currentTime > spawnFrequency[2]) 
            {
                spawnTimer = spawnTime[2];
            }
            //Debug.Log("current Time: " + currentTime);
            //Debug.Log("Spawn Timer: " + spawnTimer);
            Instantiate(PillarsPrefabArray[randomNumber], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
