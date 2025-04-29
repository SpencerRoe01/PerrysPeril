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

        if (UpgradesAvalable == 0) 
        { 
            AllUpgradesUsed = true;
        }
        UpgradePoints.text = "Upgrades: " + UpgradesAvalable;
        UpgradeValues.text = "Speed: " + StatManager.Speed + "\n" +
                             "Dash CD: " + StatManager.DashCD + "\n" +
                             "Dash Length: " + StatManager.DashLength + "\n" +
                             "Health: " + StatManager.Health + "\n" +
                             "PP Radius: " + StatManager.PerfectParryRad;
    }
    public void UpgradeSpeed()
    {
        if(UpgradesAvalable>0){
            if(StatManager.Speed<MaxSpeed) {
                StatManager.Speed++;
            }
            UpgradesAvalable--;
        }
    }
    public void UpgradeDashCD()
    {
        if(UpgradesAvalable>0){
            if (StatManager.DashCD < MaxDashCD){
                StatManager.DashCD -= 0.3f;
            }
        UpgradesAvalable--;
        }
    }
    public void UpgradeDashLength()
    {
        if(UpgradesAvalable>0){
            if (StatManager.DashLength < MaxDashLen) {
                StatManager.DashLength -= 0.1f;
            }
            UpgradesAvalable--;
        }
    }
    public void UpgradeHealth()
    {
        if(UpgradesAvalable>0){
            if(StatManager.Health < 5) {
                StatManager.Health++;
            }
            UpgradesAvalable--;
        }
    }
    public void UpgradePerfectParryRad()
    {
        if(UpgradesAvalable>0){
            if(StatManager.PerfectParryRad < MaxPerfectParryLen){
                StatManager.PerfectParryRad += 3;
            }
        UpgradesAvalable--;
        }
    }
}
