using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config" )]  //gives and option in menu to create enemy wave config
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab; 
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f; //time between two enemies
    [SerializeField] float spawnRandomFactor = 0.3f; //random time between two enemies
    [SerializeField] int numberOfEnemies = 5; //no of enemies
    [SerializeField] float moveSpeed = 2f; //speed with which enemy moves

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWayPoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints; 
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }

}
