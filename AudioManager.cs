using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("----------Audio clip-----------")]
    public AudioClip backGround;
    [Header("----------Player-----------")]
    public AudioClip attack;
    public AudioClip jump;
    public AudioClip landing;
    public AudioClip dash;
    public AudioClip hit;
    public AudioClip takeHit;
    public AudioClip playerDie;
    
    [Header("----------Items-----------")]
    public AudioClip receive;
    public AudioClip healing;
    public AudioClip power;
    [Header("----------Enemy-----------")]
    public AudioClip enemyDie;
    public AudioClip bossDie;
    
    [Header("----------Game Event-----------")]
    public AudioClip checkPoint;
    public AudioClip portal;
    [Header("----------Menu-----------")]
    public AudioClip button;
    //
    private void Start() {
        normalMusicSource();
    }
    private void Update() {
        
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }

    public void normalMusicSource(){
        musicSource.clip = backGround;
        musicSource.Play();
    }
}
