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
    public TextMeshProUGUI UpgradeValues;

    private void Start()
    {
        LevelManager =  GameObject.Find("LevelManager").GetComponent<LevelManager>();
        StatManager = GameObject.Find("StatManager").GetComponent<StatManager>();
    }
    private void Update()
    {

        if (UpgradesAvalable == 0){ 
            AllUpgradesUsed = true;
            LevelManager.LoadNextScene();
        }
        UpgradePoints.text = "Upgrades: " + UpgradesAvalable;
        UpgradeValues.text = "Speed: " + StatManager.SUpgradeLevel + "\n" +
                             "Dash CD: " + StatManager.DCDUpgradeLevel + "\n" +
                             "Dash Length: " + StatManager.DLUpgradeLevel + "\n" +
                             "Health: " + StatManager.HUpgradeLevel + "\n" +
                             "PP Radius: " + StatManager.PPRUpgradeLevel;
    }

    public void UpgradeSpeed()
    {
        if (StatManager.Speed < MaxSpeed && !AllUpgradesUsed) 
        {
            StatManager.Speed++;
            StatManager.SUpgradeLevel++;
            UpgradesAvalable--;
        }
    }
    public void UpgradeDashCD()
    {
        if (StatManager.DashCD < MaxDashCD && !AllUpgradesUsed)
        {
            StatManager.DashCD -= 0.3f;
            StatManager.DCDUpgradeLevel++;
            UpgradesAvalable--;
        }
    }
    public void UpgradeDashLength()
    {
        if (StatManager.DashLength < MaxDashLen && !AllUpgradesUsed) 
        {
            StatManager.DashLength -= 0.1f;
            StatManager.DLUpgradeLevel++;
            UpgradesAvalable--;
        }
    }
    public void UpgradeHealth()
    {
        if (StatManager.Health < 5 && !AllUpgradesUsed) 
        {
            StatManager.Health++;
            StatManager.HUpgradeLevel++;
            UpgradesAvalable--;
        }
    }
    public void UpgradePerfectParryRad()
    {
        if (StatManager.PerfectParryRad < MaxPerfectParryLen && !AllUpgradesUsed)
        {
            StatManager.PerfectParryRad += 3;
            StatManager.PPRUpgradeLevel++;
            UpgradesAvalable--;
        }
    }
}
