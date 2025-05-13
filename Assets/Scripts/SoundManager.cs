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
    public AudioSource StartTheme;
    public AudioSource BattleTheme;
    public AudioSource BossTheme;

    //SFX Sounds
    public AudioSource ButtonClick;
    public AudioSource SwordSlash;
    public AudioSource BombExplosion;
    public AudioSource DashWoosh;
    public AudioSource ProjectileThrow;
    public AudioSource BombFuse;
    
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
        StartTheme.volume = MusicVolume/100f;
        BattleTheme.volume = MusicVolume/100f;
        BossTheme.volume = MusicVolume/100f;

        //SFX Volumes
        ButtonClick.volume = SFXVolume/100f;
        SwordSlash.volume = SFXVolume/100f;
        BombExplosion.volume = SFXVolume/100f;
        DashWoosh.volume = SFXVolume/100f;
        ProjectileThrow.volume = SFXVolume/100f;
        BombFuse.volume = SFXVolume/100f;
    }
     
    public int GetMusicVolume(){
        return MusicVolume;
    }
    public int GetSFXVolume(){
        return SFXVolume;
    }
    //Play Music Methods
    public void PlayBattleTheme(){
        StartTheme.Stop();
        BattleTheme.Play();
    }
    public void PlayBossTheme(){
        BattleTheme.Stop();
        BossTheme.Play();
    }

    //Play SFX Methods
    public void PlayButtonClick(){
        ButtonClick.Play();
    }
    public void PlaySwordSlash(){
        SwordSlash.Play();
    }
    public void PlayBombExplosion(){
        BombExplosion.Play();
    }
    public void PlayDashWoosh(){
        DashWoosh.Play();
    }
    public void PlayProjectileThrow(){
        ProjectileThrow.Play();
    }
    public void StopProjectileThrow(){
        ProjectileThrow.Stop();
    }
    public void PlayBombFuse(){
        BombFuse.Play();
    }
}
