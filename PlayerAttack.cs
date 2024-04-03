using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackArea;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    private Animator playerAttack;
    public ParticleSystem slashAni;
    private Health health;
    private AudioManager audioManager;
    private Collider2D playerCollider;
    //power
    public int items_Power;
    private bool HasPower = false;
    public ParticleSystem buffPower;
    public ParticleSystem attackPower;
    // public ParticleSystem hitAttackPower;
    public GameObject attackAreaPar;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<Animator>();
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        playerCollider = GetComponent<Collider2D>();
        items_Power = PlayerPrefs.GetInt("Items_Power", items_Power);
    }

    // Update is called once per frame
    void Update()
    {
        Power();
        if(Input.GetKeyDown(KeyCode.C) && !attacking && GroundCheck()){
            
            Attack();
            playerAttack.SetTrigger("Attack");
            timer = 0;
        }
        if(attacking){
            timer += Time.deltaTime;
            if(timer >=  timeToAttack){
                timer = 0;
                attacking = false;
            }
        }
        PlayerPrefs.SetInt("Items_Power", items_Power);
    }

    public void Attack(){
        if(Input.GetKeyDown(KeyCode.C) && !attacking){
            attacking = true;
        }
    }
    //Particle
    public void Slash(){
        audioManager.PlaySFX(audioManager.attack);
        if(HasPower){
            attackPower.Play();
            attackAreaPar.SetActive(true);
        } else {
            slashAni.Play();
            attackArea.SetActive(true);
        }
    }

    public void attackAreaPar_End(){
        attackAreaPar.SetActive(false);
        attackArea.SetActive(false);
    }

    private bool GroundCheck(){
        return transform.Find("CheckGround").GetComponent<GroundCheck>().isGround;
    }

    //power
    private void Power(){
        if(Input.GetKeyDown(KeyCode.V) && items_Power > 0){
            items_Power--;
            HasPower = true;
            audioManager.PlaySFX(audioManager.power);
            StartCoroutine(countDownBuffPower());
        }
    }

    IEnumerator countDownBuffPower(){
        buffPower.Play();
        yield return new WaitForSeconds(5);
        buffPower.Stop();
        HasPower = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Items_Power")){
            items_Power++;
            audioManager.PlaySFX(audioManager.receive);
            Destroy(other.gameObject);
        }
    }
}
