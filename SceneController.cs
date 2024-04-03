using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator ani;
    public static SceneController instance;
    private Health health;
    public GameOver_Scene gameOver_Scene;
    
    private void Awake(){
        PlayerPrefs.SetFloat("HealthSave", 100f);
        
        // ani = GetComponent<Animator>();
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Update() {
        health = GameObject.Find("Player").GetComponent<Health>();
        PlayerPrefs.SetFloat("HealthSave", health.health);
        GameOver();
    }

    public void NextLevel(){
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame(){
        ResetCheckPoint();
        ResetBoss();
        ani.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        ani.SetTrigger("Start");
    }

    public void GameOver(){
        if(health.isDie == true){
            gameOver_Scene.GameOver();
        }
    }

    public void ResetCheckPoint(){
        PlayerPrefs.SetFloat("CheckPoint_x", 0);
        PlayerPrefs.SetFloat("CheckPoint_y", 0);
        PlayerPrefs.SetFloat("CheckPoint_z", 0);
    }

    public void ResetBoss(){
        PlayerPrefs.SetInt("isDefeatBoss", 0);
    }
}
