using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SFXSliderManager : MonoBehaviour{
    public Slider SSlider;
    public TextMeshProUGUI SText;
    public SoundManager SManager;
    void Start(){
        SManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        SSlider.value = SManager.GetSFXVolume();
    }
    void Update(){
        SText.text = "SFX Volume: " + SSlider.value.ToString();
    }
}
