using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject par;
    private AudioManager audioManager;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            par.SetActive(true);
            particle.Play();
            UpdateCheckPoint();
            audioManager.PlaySFX(audioManager.checkPoint);
        }
    }

    public void UpdateCheckPoint(){
        PlayerPrefs.SetFloat("CheckPoint_x", gameObject.transform.position.x);
        PlayerPrefs.SetFloat("CheckPoint_y", gameObject.transform.position.y);
        PlayerPrefs.SetFloat("CheckPoint_z", gameObject.transform.position.z);
    }
}
