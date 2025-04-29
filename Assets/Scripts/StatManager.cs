using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatManager : MonoBehaviour{
    private static StatManager instance;


    public int Speed;
    public float DashCD;
    public float DashLength;
    public int Health;
    public int PerfectParryRad;


    public int Score;

    private GameObject Player;

    void Awake(){
        DontDestroyOnLoad(this);
        if(instance==null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
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
        Player = GameObject.Find("Player");

        if (SceneManager.GetActiveScene().name == "UpgradeScene")
        {
            GameObject.Find("UpgradeManager").GetComponent<UpgradeClass>().UpgradesAvalable = 4;
        }

    }

    




}
