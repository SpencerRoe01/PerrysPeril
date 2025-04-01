using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour{
    public Sound[] Music;
    public Sound[] SFX;
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public static int MusicVolume = 100;
    public static int SFXVolume = 100;
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    void VolumeUpdate(){
        MusicSource.volume = MusicVolume/100f;
        SFXSource.volume = SFXVolume/100f;
    }
}
