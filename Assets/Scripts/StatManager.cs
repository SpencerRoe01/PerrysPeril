using UnityEngine;

public class StatManager : MonoBehaviour{
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
}
