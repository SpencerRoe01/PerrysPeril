using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour{
    public Sound[] Music;
    public Sound[] SFX;
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public static int MusicVolume = 100;
    public static int SFXVolume = 100;
    public Slider MSlider;
    public Slider SSlider;
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    void Update(){
        if(SceneManager.GetActiveScene().name == "Settings Menu"){
            VolumeUpdate();
        }
    }
    void VolumeUpdate(){
        MSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        MusicVolume = (int) MSlider.value;
        SSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        SFXVolume = (int) SSlider.value;
        MusicSource.volume = MusicVolume/100f;
        SFXSource.volume = SFXVolume/100f;
    }
}
