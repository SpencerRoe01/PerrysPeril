using UnityEngine;

public class StatManager : MonoBehaviour{
    private static StatManager instance;
    void Awake(){
        DontDestroyOnLoad(this);
        if(instance==null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
}
