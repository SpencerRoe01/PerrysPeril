using UnityEngine;
public class DDOLConnection : MonoBehaviour{
    public GameObject LevelManager;
    public GameObject SoundManager;
    public void LoadSettings(){
        LevelManager = GameObject.Find("LevelManager");
        LevelManager.GetComponent <LevelManager>().LoadSettingScene();
    }
    public void PlayButtonClick(){
        SoundManager = GameObject.Find("SoundManager");
        SoundManager.GetComponent<SoundManager>().PlayButtonClick();
    }
}
