using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRound : MonoBehaviour
{
    public GameObject lockTheDoor;
    public GameObject doorContinues;
    public GameObject pointCheck;
    public List<GameObject> boss;
    public bool enterRightDoor;
    private bool isDefeatBoss = false;
    private Health bossHealth;
    public GameObject portal;
    private GameObject player;

    void Start()
    {
        if(PlayerPrefs.GetInt("isDefeatBoss") == 1){
            isDefeatBoss = true;
        } else {
            isDefeatBoss = false;
        }

        lockTheDoor.SetActive(false);
        doorContinues.SetActive(true);
        portal.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject obj in boss){
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDefeatBoss == false){
            DoorController();
            BossDieController();
        } else {
            doorContinues.SetActive(false);
            portal.SetActive(true);
        }
        
    }

    private void DoorController(){
        if(enterRightDoor){
            if(player.transform.position.x > pointCheck.transform.position.x && player.transform.position.y < pointCheck.transform.position.y){
                lockTheDoor.SetActive(true);
                foreach (GameObject obj in boss){
                    obj.SetActive(true);
                }
            }
        } else if(!enterRightDoor){
            if(player.transform.position.x < pointCheck.transform.position.x && player.transform.position.y < pointCheck.transform.position.y){
                
                lockTheDoor.SetActive(true);
                foreach (GameObject obj in boss){
                    obj.SetActive(true);
                }
            }
        }
    }

    private void BossDieController(){
        for(int i = 0; i < boss.Count; i++){
            bossHealth = boss[i].GetComponent<Health>();
            if(bossHealth.health <= 0){
                boss.RemoveAt(i);
            }
        }

        if(boss.Count == 0){
            doorContinues.SetActive(false);
            portal.SetActive(true);
            isDefeatBoss = true;
            PlayerPrefs.SetInt("isDefeatBoss", 1);
        }
    }
}
