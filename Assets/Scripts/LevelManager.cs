using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public List<GameObject> EnemiesToSpawn = new List<GameObject>();
    public List<GameObject> EnemiesInScene = new List<GameObject>();

    public int MaxEnemiesAllowedInScene;
    public Transform[] SpawmPointPositions;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (EnemiesInScene.Count == 0 && EnemiesToSpawn.Count == 0)
        {
            //PLayer Won
        }
        else if (EnemiesToSpawn.Count > 0 && EnemiesInScene.Count <= MaxEnemiesAllowedInScene) 
        {
            SpawnEnemy();
        }
    }
    public void SpawnEnemy() 
    {
        int SpawnPointIndex = Random.Range(0, SpawmPointPositions.Length);
        GameObject SpawnedEnemy = Instantiate(EnemiesToSpawn[0], SpawmPointPositions[SpawnPointIndex].position, Quaternion.identity);
        EnemiesToSpawn.RemoveAt(0);
        EnemiesInScene.Add(SpawnedEnemy);
    }
}
