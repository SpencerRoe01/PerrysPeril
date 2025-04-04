using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{

    public List<GameObject> EnemiesToSpawn = new List<GameObject>();
    public List<GameObject> EnemiesInScene = new List<GameObject>();

    public List<GameObject> Level1EnemiesToSpawn = new List<GameObject>();
    public List<GameObject> Level2EnemiesToSpawn = new List<GameObject>();
    public List<GameObject> Level3EnemiesToSpawn = new List<GameObject>();
    public List<GameObject> Level4EnemiesToSpawn = new List<GameObject>();

    public int MaxEnemiesAllowedInScene;
    public Transform[] SpawmPointPositions;

    public static int level;

    private static LevelManager instance;

    void Awake()
    {  
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

    }

    void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the event when the object is disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (level) 
        { 
            case 2:
                EnemiesToSpawn = Level1EnemiesToSpawn; break;
            case 3:
                EnemiesToSpawn = Level2EnemiesToSpawn; break;
            case 4:
                EnemiesToSpawn = Level3EnemiesToSpawn; break;
            case 5:
                EnemiesToSpawn = Level4EnemiesToSpawn; break;
        }
    }





    void Update()
    {
        
        
         if (EnemiesToSpawn.Count > 0 && EnemiesInScene.Count <= MaxEnemiesAllowedInScene && level != 1) 
        {
            SpawnEnemy();
        }
        StartCoroutine(CheckIfEnemiesAreDead(2f));

    }
    public void SpawnEnemy() 
    {
        int SpawnPointIndex = Random.Range(0, SpawmPointPositions.Length);
        GameObject SpawnedEnemy = Instantiate(EnemiesToSpawn[0], SpawmPointPositions[SpawnPointIndex].position, Quaternion.identity);
        EnemiesToSpawn.RemoveAt(0);
        EnemiesInScene.Add(SpawnedEnemy);
    }
    public void LoadNextScene()
    {
         if (level == 0)
        {
            level = 2;
            SceneManager.LoadScene(2);
        }
        else if (level != 1)
        {
            StartCoroutine(DelaySceneLoad(4f, 1));
        }
        else
        {
            level++;
            StartCoroutine(DelaySceneLoad(4f,level));


        }
        

    }
    IEnumerator CheckIfEnemiesAreDead(float delay)
    {

        yield return new WaitForSeconds(delay);


        if (EnemiesInScene.Count == 0 && EnemiesToSpawn.Count == 0 && level != 0 && level != 1)
        {

            LoadNextScene();

        }
    }
    IEnumerator DelaySceneLoad(float delay, int Level)
    {

        yield return new WaitForSeconds(delay);
        

        SceneManager.LoadScene(Level);
    }
}
