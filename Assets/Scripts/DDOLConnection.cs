using UnityEngine;
public class DDOLConnection : MonoBehaviour{
    public GameObject LevelManager;
    public void LoadSettings(){
        LevelManager = GameObject.Find("LevelManager");
        LevelManager.GetComponent<LevelManager>().LoadSettingScene();
    }
}
