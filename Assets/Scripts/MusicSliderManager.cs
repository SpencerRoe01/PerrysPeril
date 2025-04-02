using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MusicSliderManager : MonoBehaviour{
    public Slider MSlider;
    public TextMeshProUGUI MText;
    public SoundManager SManager;
    void Start(){
        SManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        MSlider.value = SManager.GetMusicVolume();
    }
    void Update(){
        MText.text = "Music Volume: " + MSlider.value.ToString();
    }
}
