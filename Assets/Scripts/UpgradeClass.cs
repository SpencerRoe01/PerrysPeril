using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditorInternal;
public class UpgradeClass : MonoBehaviour
{
    public bool AllUpgradesUsed;
    public LevelManager LevelManager;

    public int UpgradesAvalable;

    public int MaxSpeed;
    public int MaxDashCD;
    public int MaxDashLen;
    public int MaxPerfectParryLen;

    public StatManager StatManager;

    public TextMeshProUGUI UpgradePoints;

    private void Start()
    {
        LevelManager =  GameObject.Find("LevelManager").GetComponent<LevelManager>();
        StatManager = GameObject.Find("StatManager").GetComponent<StatManager>();
    }
    private void Update()
    {

        if (UpgradesAvalable == 0) 
        { 
            AllUpgradesUsed = true;
        }



        if (AllUpgradesUsed) 
        {
            
            UpgradesAvalable = 1;
            StartCoroutine(DelayedMethodCoroutine(2f));
            
        }
        UpgradePoints.text = "Upgrades: " + UpgradesAvalable;
    }

    IEnumerator DelayedMethodCoroutine(float delay)
    {
        
        yield return new WaitForSeconds(delay);

        Debug.Log("Loading");
        LevelManager.LoadNextScene();
    }

    public void UpgradeSpeed()
    {
        if (StatManager.speed < MaxSpeed) 
        {
            StatManager.speed++;
        }
        UpgradesAvalable--;
    }
    public void UpgradeDashCD()
    {
        if (StatManager.DashCD < MaxDashCD)
        {
            StatManager.DashCD -= 0.3f;
        }
        UpgradesAvalable--;
    }
    public void UpgradeDashLength()
    {
        if (StatManager.DashLength < MaxDashLen) 
        {
            StatManager.DashCD -= 0.1f;
        }
        UpgradesAvalable--;
    }
    public void UpgradeHealth()
    {
        if (StatManager.Health < 5) 
        {
            StatManager.Health++;
        }
        UpgradesAvalable--;
    }
    public void UpgradePerfectParryRad()
    {
        if (StatManager.PerfectParryRad < MaxPerfectParryLen)
        {
            StatManager.PerfectParryRad += 3;
        }
        UpgradesAvalable--;
    }
}
