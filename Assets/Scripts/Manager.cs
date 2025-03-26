using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour {
    public void StartScene(){
        SceneManager.LoadScene(0);
    }
    public void SettingsScene(){
        SceneManager.LoadScene(1);
    }
    public void GameplayScene(){
        SceneManager.LoadScene(2);
    }
}
    
