using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject SoundManager;

    public bool NewSceneLoading;
    public bool isUpgradeLevel;

    public bool IsBossThemePlaying;

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

        StartCoroutine(CheckIfEnemiesAreDead(2f));
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        NewSceneLoading = false;

        // Load enemies based on the current level
        switch (level)
        {
            case 3:
                EnemiesToSpawn = Level1EnemiesToSpawn;
                break;
            case 4:
                EnemiesToSpawn = Level2EnemiesToSpawn;
                break;
            case 5:
                EnemiesToSpawn = Level3EnemiesToSpawn;
                break;
            case 6:
                EnemiesToSpawn = Level4EnemiesToSpawn;
                break;
        }
    }

    void Update()
    {

        {

        }
        if (EnemiesToSpawn.Count > 0 && EnemiesInScene.Count <= MaxEnemiesAllowedInScene && level != 1)
        {
            SpawnEnemy();
        }

        


        if (isUpgradeLevel)
        {
            CheckForUpgradeCompletion();
        }
        if(Level3EnemiesToSpawn.Count == 0 && !IsBossThemePlaying){
            SoundManager = GameObject.Find("SoundManager");
            SoundManager.GetComponent<SoundManager>().PlayBossTheme();
            IsBossThemePlaying = true;
        }
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
        if (NewSceneLoading)
            return;

        NewSceneLoading = true;

        if (level == 0)
        {
            level = 3;
            SceneManager.LoadScene(3);
        }
        else if (!isUpgradeLevel)
        {
            Debug.Log("Loading upgrade level");
            
            StartCoroutine(DelaySceneLoad(4f, "UpgradeScene"));
            GameObject.Find("StatManager").GetComponent<StatManager>().Health = GameObject.Find("PerryRoot").GetComponent<Player>().Health;
        }
        else if (GameObject.Find("UpgradeManager") != null && GameObject.Find("UpgradeManager").GetComponent<UpgradeClass>().AllUpgradesUsed)
        {
            Debug.Log("Loading regular level");
            GameObject.Find("UpgradeManager").GetComponent<UpgradeClass>().AllUpgradesUsed = false;
            level++;
            StartCoroutine(DelaySceneLoad(4f, level));
        }
    }

    IEnumerator CheckIfEnemiesAreDead(float delay)
    {
        while (true)
        {

            yield return new WaitForSeconds(delay);
            

            if (EnemiesInScene.Count == 0 && EnemiesToSpawn.Count == 0 && level != 0 && level != 1 && !NewSceneLoading)
            {
                if (!NewSceneLoading && SceneManager.GetActiveScene().name != "SettingsMenu" && SceneManager.GetActiveScene().name != "StartMenu")
                {
                    LoadNextScene();
                }
            }
        }
    }

    IEnumerator DelaySceneLoad(float delay, object levelOrSceneName)
    {
        yield return new WaitForSeconds(delay);

        if (levelOrSceneName is int)
        {
            SceneManager.LoadScene((int)levelOrSceneName);
            GameObject.Find("ComboManager").GetComponent<Combo>().ClearCombo();
        }
        else if (levelOrSceneName is string && (string)levelOrSceneName == "UpgradeScene")
        {

            SceneManager.LoadScene("UpgradeScene");
            isUpgradeLevel = true;
        }

        NewSceneLoading = false;
    }


    void CheckForUpgradeCompletion()
    {
        if (GameObject.Find("UpgradeManager") != null)
        {
            var upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeClass>();

            if (upgradeManager.AllUpgradesUsed)
            {

                GameObject.Find("UpgradeManager").GetComponent<UpgradeClass>().AllUpgradesUsed = false;
                level++;
                StartCoroutine(DelaySceneLoad(4f, level));
                isUpgradeLevel = false;
            }
        }
    }
    public void LoadSettingScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevel1()
    {
        SoundManager = GameObject.Find("SoundManager");
        SoundManager.GetComponent<SoundManager>().PlayBattleTheme();
        SceneManager.LoadScene(3);
        level = 3;
        
    }
    public void LoadIntroCutscene()
    {
        SceneManager.LoadScene(6);
    }
}