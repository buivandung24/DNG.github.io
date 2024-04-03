using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    private Health health;
    public TextMeshProUGUI items_Healing;
    public TextMeshProUGUI items_Power;
    
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.Find("Player").GetComponent<Health>();
    }

    void Update() {
        health = GameObject.Find("Player").GetComponent<Health>();
        updateHealthBar();
        items_Healing.text = "x " + PlayerPrefs.GetInt("Items_Healing");
        items_Power.text = "x " + PlayerPrefs.GetInt("Items_Power");
    }

    private void updateHealthBar(){
        healthBar.fillAmount = health.health/health.MAX_HEALTH;
    }
}
