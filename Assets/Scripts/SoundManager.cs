using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour{
    private static SoundManager instance;
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public static int MusicVolume = 100;
    public static int SFXVolume = 100;
    public Slider MSlider;
    public Slider SSlider;

    //SFX Sounds
    public AudioClip ButtonClick;
    void Awake(){
        DontDestroyOnLoad(this);
        if(instance==null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
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
    public int GetMusicVolume(){
        return MusicVolume;
    }
    public int GetSFXVolume(){
        return SFXVolume;
    }
    public void PlayButtonClick(){
        SFXSource.Play();
    }
}
