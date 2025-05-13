using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ISceneManagement : MonoBehaviour{
    public GameObject LevelManager;
    void Start(){
        StartCoroutine(WaitTimeAndLoadLevel());
    }

    public IEnumerator WaitTimeAndLoadLevel(){
        yield return new WaitForSecondsRealtime(51f);
        LevelManager = GameObject.Find("LevelManager");
        LevelManager.GetComponent<LevelManager>().LoadLevel1();
    }
}
