using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerObject : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Health health = other.GetComponent<Health>();
            health.damage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            Health health = other.gameObject.GetComponent<Health>();
            health.damage(damage);
        }
    }
}
