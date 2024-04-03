using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float speedJump = 20.0f;
    private bool isFacingRight = true;
    private float horizontalInput;
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private Health health;
    //Dash
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr; 
    //heal
    public int items_Healing;
    public ParticleSystem Healing;
    //audio
    private AudioManager audioManager;
    private bool isFalling = false;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        health = GameObject.Find("Player").GetComponent<Health>();
        health.health = PlayerPrefs.GetFloat("HealthSave");
        items_Healing = PlayerPrefs.GetInt("Items_Healing");
        resetPosPlayer();
        Healing.Stop();
    }

    void Update()
    {
        move();
        jump();
        Dash();
        healing();
        PlayerPrefs.SetInt("Items_Healing", items_Healing);
        playerAnim.SetFloat("yVelocity", playerRb.velocity.y);
    }

    void move(){
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
        playerAnim.SetFloat("Move", Mathf.Abs(horizontalInput));

        if(isFacingRight && horizontalInput < 0 || !isFacingRight && horizontalInput > 0){
            isFacingRight = !isFacingRight;
            Vector2 scalePlayer = transform.localScale;
            scalePlayer.x *= -1;
            transform.localScale = scalePlayer;
        }
    }

    void jump(){
        if(Input.GetKeyDown(KeyCode.X) && GroundCheck() == true){
            audioManager.PlaySFX(audioManager.jump);
            playerRb.drag = 2;
            playerRb.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
            playerAnim.SetBool("Jump",true);
            playerAnim.SetBool("OnTheGround",false);
        }

        //falling down
        if(GroundCheck() == false){
            isFalling = true;
            playerAnim.SetBool("Jump",true);
        }

        if(GroundCheck() == true){
            if(isFalling == true){
                audioManager.PlaySFX(audioManager.landing);
                isFalling = false;
            }
            playerAnim.SetBool("OnTheGround",true);
            playerAnim.SetBool("Jump",false);
        }
    }
    IEnumerator DashController(){
        playerAnim.SetTrigger("Dash");
        canDash = false;
        isDashing = true;
        float gravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.drag = 5;
        playerRb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        playerRb.gravityScale = gravity;
        playerRb.drag = 2;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void Dash(){
        if(isDashing){
            return ;
        }
        if(Input.GetKeyDown(KeyCode.Z) && canDash){
            audioManager.PlaySFX(audioManager.dash);
            StartCoroutine(DashController());
        }
    }
    //healing
    private void healing(){
        if(Input.GetKeyDown(KeyCode.F) && items_Healing > 0){
            items_Healing--;
            Healing.Play();
            health.heal(40);
            audioManager.PlaySFX(audioManager.healing);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Items_Healing")){
            items_Healing++;
            audioManager.PlaySFX(audioManager.receive);
            Destroy(other.gameObject);
        }
    }
    private bool GroundCheck(){
        return transform.Find("CheckGround").GetComponent<GroundCheck>().isGround;
    }
    private void resetPosPlayer(){
        transform.position = new Vector3(PlayerPrefs.GetFloat("CheckPoint_x"),PlayerPrefs.GetFloat("CheckPoint_y"),PlayerPrefs.GetFloat("CheckPoint_z"));
    }
    public void DeathSound(){
        audioManager.PlaySFX(audioManager.playerDie);
    }
}
