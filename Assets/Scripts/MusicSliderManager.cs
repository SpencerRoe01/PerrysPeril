using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MusicSliderManager : MonoBehaviour{
    public Slider mSlider;
    public TextMeshProUGUI mText;
    void Start(){
        
    }
    void Update(){
        mText.text = "Music Volume: " + mSlider.value.ToString();
    }
}
