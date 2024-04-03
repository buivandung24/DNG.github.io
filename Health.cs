using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public float MAX_HEALTH = 100f;
    private Animator ani;
    public bool isDie;
    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsDie();
    }

    public void SetHealth(int maxHealth, int health){
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    public void damage(float amount){
        if(amount < 0){
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }
        if(isDie == false){
            this.health -= amount;
            ani.SetTrigger("TakeHit");
        }
        
    }

    public void heal(float amount){
        if(amount < 0){
            throw new System.ArgumentOutOfRangeException("Cannot have negative heal");
        }

        this.health += amount;
        if(health > MAX_HEALTH){
            health = MAX_HEALTH;
        }
    }

    void die(){
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    IEnumerator IsDie_Coroutine(float seconds){
        rb.bodyType = RigidbodyType2D.Static;
        col.isTrigger = true;
        yield return new WaitForSeconds(seconds);
        die();
    }

    public void CheckIsDie(){
        if(health > 0){
            ani.SetBool("IsDie", false);
            isDie = false;
        }
        if(health <= 0){
            ani.SetBool("IsDie", true);
            StartCoroutine(IsDie_Coroutine(1f));
            isDie = true;
        }
    }
}
