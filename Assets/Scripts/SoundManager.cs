using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour{
    public Sound[] Music;
    public Sound[] SFX;
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    void VolumeUpdate(){
        MusicSource.volume = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
    }
}
