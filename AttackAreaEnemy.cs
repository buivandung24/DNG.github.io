using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaEnemy : MonoBehaviour
{
    public GameObject enemy;
    private AudioManager audioManager;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            Health health = other.gameObject.GetComponent<Health>();
            health.damage(enemy.GetComponent<Enemy>().damage);
            audioManager.PlaySFX(audioManager.takeHit);
        }
    }
}
