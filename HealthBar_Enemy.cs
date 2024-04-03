using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Enemy : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    private Health health;
    public GameObject healthObject;
    
    // Start is called before the first frame update
    void Start()
    {
        health = healthObject.GetComponent<Health>();
    }

    void Update() {
        health = healthObject.GetComponent<Health>();
        updateHealthBar();
    }

    private void updateHealthBar(){
        healthBar.fillAmount = health.health/health.MAX_HEALTH;
    }
}
