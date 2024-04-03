using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver_Scene : MonoBehaviour
{
    private Health health;
    void Update() {
        health = GameObject.Find("Player").GetComponent<Health>();
        if(health.isDie == false){
            gameObject.SetActive(false);
        }
        if(health.health <= 0){
            ResetItems();
        }
    }

    public void GameOver(){
        gameObject.SetActive(true);
        PlayerPrefs.SetFloat("HealthSave", health.MAX_HEALTH);
        
    }
    public void RestartGame(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        gameObject.SetActive(false);
    }

    public void MenuGame(){
        SceneManager.LoadScene("Main Menu");
        gameObject.SetActive(false);
    }

    public void ResetItems(){
        PlayerPrefs.SetInt("Items_Power", 0);
        PlayerPrefs.SetInt("Items_Healing", 0);
    }
}
