using UnityEngine;
using System.Collections;
public class UpgradeClass : MonoBehaviour
{
    public bool AllUpgradesUsed;
    public LevelManager LevelManager;

    private void Start()
    {
        LevelManager =  GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    private void Update()
    {
        if (AllUpgradesUsed) 
        {
            AllUpgradesUsed = false;
            StartCoroutine(DelayedMethodCoroutine(2f));
        }
    }

    IEnumerator DelayedMethodCoroutine(float delay)
    {
        
        yield return new WaitForSeconds(delay);

        
        LevelManager.LoadNextScene();
    }
}
