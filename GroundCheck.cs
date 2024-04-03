using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGround;
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")){
            isGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground")  || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")){
            isGround = false;
        }
    }
}
