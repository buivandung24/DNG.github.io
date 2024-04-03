using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    public GameObject explosion_FB;
    public GameObject fireBall;
    public float speedUp;
    public float startDelay;
    public float spawnTime;
    void Start() {
        InvokeRepeating("SpawnFireBall", startDelay, spawnTime);   
    }

    void Update(){
        
    }

    private void SpawnFireBall(){
        GameObject fireB = Instantiate(fireBall, transform.position, transform.rotation);
        switch (transform.rotation.z)
        {
            case 0.7071068f:
                fireB.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speedUp, ForceMode2D.Impulse);
                break;
            case -0.7071068f:
                fireB.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speedUp, ForceMode2D.Impulse);
                break;
            case 1f:
                fireB.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speedUp, ForceMode2D.Impulse);
                break;
            case 0f:
                fireB.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speedUp, ForceMode2D.Impulse);
                break;
            default:
                Debug.Log("Wrong Rotation or need to add rotation");
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground")){
            Instantiate(explosion_FB, transform.position, Quaternion.identity);
            Destroy(fireBall);
        }
    }
}
