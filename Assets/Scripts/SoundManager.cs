using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour{
    private static SoundManager instance;
    public static int MusicVolume = 100;
    public static int SFXVolume = 100;
    public Slider MSlider;
    public Slider SSlider;
    
    //Music Sounds
    public AudioSource MusicSource;

    //SFX Sounds
    public AudioSource ButtonClick;
    
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

        //Music Volumes
        MusicSource.volume = MusicVolume/100f;
        
        //SFX Volumes
        ButtonClick.volume = SFXVolume/100f;
    }
    public int GetMusicVolume(){
        return MusicVolume;
    }
    public int GetSFXVolume(){
        return SFXVolume;
    }
    //Play Music Methods

    //Play SFX Methods
    public void PlayButtonClick(){
        ButtonClick.Play();
    }
}
