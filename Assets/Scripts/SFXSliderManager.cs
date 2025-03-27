using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SFXSliderManager : MonoBehaviour{
    public Slider sSlider;
    public TextMeshProUGUI sText;
    void Start(){
        
    }
    void Update(){
        sText.text = "SFX Volume: " + sSlider.value.ToString();
    }
}
