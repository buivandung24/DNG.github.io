using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void playGame(){
        audioManager.PlaySFX(audioManager.button);
        ResetCheckPoint();
        ResetItems();
        ResetBoss();
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame(){
        audioManager.PlaySFX(audioManager.button);
        Application.Quit();
    }

    public void ResetCheckPoint(){
        PlayerPrefs.SetFloat("CheckPoint_x", 0);
        PlayerPrefs.SetFloat("CheckPoint_y", 0);
        PlayerPrefs.SetFloat("CheckPoint_z", 0);
    }

    public void ResetItems(){
        PlayerPrefs.SetInt("Items_Power", 0);
        PlayerPrefs.SetInt("Items_Healing", 0);
    }

    public void ResetBoss(){
        PlayerPrefs.SetInt("isDefeatBoss", 0);
    }
}
