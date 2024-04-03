using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] private float speed;
    [SerializeField] private EnemyData data;
    
    private Transform player;
    public GameObject attackArea;
    [Header("----------Audio clip-----------")]
    private AudioManager audioManager;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        setEnemyValues();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
    }
    //look at player
    private void lookAtPlayer(){
        if(player.position.x < transform.position.x){
            transform.localScale = new Vector2(-1, transform.localScale.y);
        } else if(player.position.x > transform.position.x){
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }
    //attack
    public void StartAttack(){
        attackArea.SetActive(true);
    }

    public void EndAttack(){
        attackArea.SetActive(false);
    }
    // Enemy data
    private void setEnemyValues(){
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    public void DeathSound(){
        audioManager.PlaySFX(audioManager.bossDie);
    }
}
