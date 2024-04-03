using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Data Enemy")]
    [SerializeField] public int damage;
    [SerializeField] private float speed;
    [SerializeField] private EnemyData data;
    [Header("Chase Player")]
    
    // public Transform playerTransform;
    public GameObject posA;
    public GameObject posB;
    public Transform currentPoint;
    [Header("Attack")]
    public GameObject attackAreaEnemy;
    private bool attacking = false;
    public float timeToAttack = 0.5f;
    private float timer = 0f;
    public float attackRange;
    //
    private Animator ani;
    private Rigidbody2D rb;
    private GameObject player;
    [Header("AI Enemy")]
    public EnemyState currentState;
    public float chaseRange;
    public enum EnemyState { Patrol, Chase , Attack}
    [Header("----------Audio clip-----------")]
    private AudioManager audioManager;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setEnemyValues();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = posB.transform;
        currentState = EnemyState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        // ChangeScale();
        switch (currentState) {
        case EnemyState.Patrol:
            Patrol();
            break;
        case EnemyState.Chase:
            Chase();
            break;
        case EnemyState.Attack:
            attackPlayer();
            break;
        }
        
    }
    //data enemy
    private void setEnemyValues(){
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }
    //chase
    void Chase() {
        EnemyFollow();
        ChangeScale();
        NextState();
    }
    private void EnemyFollow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    //Patrol
    private void Patrol(){
        UpdatePatrol();
        // Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == posB.transform){
            rb.velocity = new Vector2(speed, 0);
        } else {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == posB.transform){
            currentPoint = posA.transform;
        } 
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == posA.transform){
            currentPoint = posB.transform;
        } 
        NextState();
    }

    private void UpdatePatrol(){
        if(currentPoint == posA.transform){
            transform.localScale = new Vector2(-1, transform.localScale.y);
        } else if(currentPoint == posB.transform) {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(posA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(posB.transform.position, 0.5f);
        Gizmos.DrawLine(posA.transform.position, posB.transform.position);
    }
    //attack
    private void attackPlayer(){
        ChangeScale();
        if(!attacking){
            Attack();
            ani.SetTrigger("Attack");
            timer = 0;
        }

        if(attacking){
            timer += Time.deltaTime;
            if(timer >=  timeToAttack){
                timer = 0;
                attacking = false;
            }
        }
        NextState();
    }

    private void Attack(){
        attacking = true;
    }

    public void AttackArea(){
        attackAreaEnemy.SetActive(true);
    }
    public void EndAttackArea(){
        attackAreaEnemy.SetActive(false);
    }

    //check enemy AI
    private void NextState(){
        if (!IsPlayerDetected() || IsPlayerOutOfRange()) {
            currentState = EnemyState.Patrol;
        } else if (IsPlayerDetected() && !IsEnoughToAttack()) {
            currentState = EnemyState.Chase;
        } else if (IsEnoughToAttack() && IsPlayerDetected()) {
            currentState = EnemyState.Attack;
        }
    }

    //check bool
    bool IsPlayerDetected() {
        // Kiểm tra xem người chơi có trong phạm vi phát hiện hay không
        return Vector2.Distance(transform.position, player.transform.position) <= chaseRange;
    }
    bool IsPlayerOutOfRange() {
        // Kiểm tra xem người chơi có ra khỏi tầm nhìn hay không
        return Vector2.Distance(transform.position, player.transform.position) > chaseRange * 2;
    }
    bool IsEnoughToAttack(){
        // Kiểm tra xem người chơi đủ gần để tấn công hay không
        return Vector2.Distance(transform.position, player.transform.position) <= attackRange;
    }
    // khac
    private void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.CompareTag("Player"))
        {
            if(collider.gameObject.GetComponent<Health>() != null){
                collider.gameObject.GetComponent<Health>().damage(damage);
                audioManager.PlaySFX(audioManager.takeHit);
            }
        }

        if(collider.gameObject.CompareTag("Enemy")){
            // UpdatePatrol();
            if(currentPoint == posB.transform){
                currentPoint = posA.transform;
            } else if(currentPoint == posA.transform){
                currentPoint = posB.transform;
            }
        }
    }

    private void ChangeScale(){
        if(transform.position.x < player.transform.position.x){
            transform.localScale = new Vector2(1, transform.localScale.y);
        } else if(transform.position.x > player.transform.position.x){
            transform.localScale = new Vector2(-1,transform.localScale.y);
        }
    }
    public void DeathSound(){
        audioManager.PlaySFX(audioManager.enemyDie);
    }
}