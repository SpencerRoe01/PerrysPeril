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

    public int SUpgradeLevel;
    public int DCDUpgradeLevel;
    public int DLUpgradeLevel;
    public int HUpgradeLevel;
    public int PPRUpgradeLevel;


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
        switch (SUpgradeLevel) 
        { 
            case 1: Speed = 12;
                break;
            case 2: Speed = 14;
                break;
            case 3:
                Speed = 16;
                break;
            case 4:
                Speed = 18;
                break;
        }
        switch (DCDUpgradeLevel)
        {
            case 1:
                DashCD = 2.5f;
                break;
            case 2:
                DashCD = 2;
                break;
            case 3:
                DashCD = 1.5f;
                break;
            case 4:
                DashCD = 1;
                break;
        }
        switch (DLUpgradeLevel)
        {
            case 1:
                DashLength = .3f;
                break;
            case 2:
                DashLength = .4f;
                break;
            case 3:
                DashLength = .5f;
                break;
            case 4:
                DashLength = .6f;
                break;
        }






        Player = GameObject.Find("PerryRoot");

        if (SceneManager.GetActiveScene().name == "UpgradeScene") {
            GameObject.Find("UpgradeManager").GetComponent<UpgradeClass>().UpgradesAvalable = 4;
            


        }
        
        if (Player != null) 
        {
            
            Player.GetComponent<Player>().Health = Health;
            Player.GetComponent<Player>().PlayerMoveSpeed = Speed;
            Player.GetComponent<Player>().DashCooldown = DashCD;
            Player.GetComponent<Player>().DashLength = DashLength;
            
        }

        

    }

    




}
