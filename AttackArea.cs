using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    
    [SerializeField] float damage = 5;
    private AudioManager audioManager;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")){
            Health health = other.gameObject.GetComponent<Health>();
            health.damage(damage);
            audioManager.PlaySFX(audioManager.hit);
        }
    }
}
