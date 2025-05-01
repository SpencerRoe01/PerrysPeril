using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combo : MonoBehaviour
{ 
   
    

    [SerializeField] private string[] gradeLevels = { "D", "C", "B", "A", "S" };
    [SerializeField] private float maxComboValue = 100f;
    [SerializeField] private float decayRate = 5f;
    [SerializeField] private float decayDelay = 0.5f;
    [SerializeField] private float downgradeCooldown = 2f;
    [SerializeField] private float killValue = 22f;
    [SerializeField] private float stunValue = 12f;
    [SerializeField] private float parryValue = 17f;

    public TextMeshProUGUI ComboText;
    public Image ComboBar;

    private int currentGradeIndex = 0;
    private float comboValue = 0f;
    private bool canUpgrade = true;
    private float downgradeTimer = 0f;
    private float timeSinceAdd = Mathf.Infinity;



    void Awake(){
        DontDestroyOnLoad(this);
    }



    void Update()
    {
        


        timeSinceAdd += Time.deltaTime;

        if (timeSinceAdd >= decayDelay && comboValue > 0f)
        {
            comboValue -= decayRate * Time.deltaTime;
            comboValue = Mathf.Clamp(comboValue, 0f, maxComboValue);
        }

        if (comboValue >= maxComboValue && canUpgrade && currentGradeIndex < gradeLevels.Length - 1)
        {
            currentGradeIndex++;
            comboValue = maxComboValue * 0.2f;
            canUpgrade = false;
            downgradeTimer = downgradeCooldown;
        }

        if (comboValue < maxComboValue * 0.9f)
            canUpgrade = true;

        if (downgradeTimer > 0f)
            downgradeTimer -= Time.deltaTime;

        if (comboValue <= 0f && currentGradeIndex > 0 && downgradeTimer <= 0f)
        {
            currentGradeIndex--;
            comboValue = maxComboValue * 0.8f;
            downgradeTimer = downgradeCooldown;
        }

        ComboBar.fillAmount = comboValue / maxComboValue;
        ComboText.text = "Combo: " + gradeLevels[currentGradeIndex];
        ComboText.gameObject.SetActive(SceneManager.GetActiveScene().buildIndex >= 3);
        ComboBar.gameObject.SetActive(SceneManager.GetActiveScene().buildIndex >= 3);
    }

    public void RegisterKill() => AddCombo(killValue);
    public void RegisterStun() => AddCombo(stunValue);
    public void RegisterParry() => AddCombo(parryValue);

    private void AddCombo(float amount)
    {
        comboValue = Mathf.Clamp(comboValue + amount, 0f, maxComboValue);
        timeSinceAdd = 0f;
    }
}
