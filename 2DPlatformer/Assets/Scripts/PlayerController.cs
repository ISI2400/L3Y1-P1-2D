using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timerTxt;
    public float timer;

    [Header("Health")]

    public Slider delayedslider;
    public Slider healthSlider;
    public int maxHealth;
    public int currentHealth;

    [Header("Shooting")]
    public Transform shootingPoint;
    public GameObject bullet;
    bool isFacingRight;

    [Header("Main")]

    public int jumps;
    public float moveSpeed;
    public float jumpForce;
    float inputs;
    public Rigidbody2D rb;
    public float groundDistance;
    public LayerMask layerMask;

    RaycastHit2D hit;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        delayedslider.maxValue = maxHealth;

        healthSlider.maxValue = maxHealth;
        startPos = transform.position;

        
        currentHealth = maxHealth;
        isFacingRight = true;

        delayedslider.value = currentHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerTxt.text = timer.ToString("F2");
        
        Movement();
        Health();
        Shoot();
        MovementDirection();
        Jump();
  
    }

    void Movement()
    {
        inputs = Input.GetAxisRaw("Horizontal");
        rb.velocity = new UnityEngine.Vector2(inputs * moveSpeed, rb.velocity.y);

        hit = Physics2D.Raycast(transform.position, -transform.up, groundDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.up * groundDistance, Color.yellow);

        if (hit.collider)
        {
            jumps = 2;
        }
    }

    void Jump(){

        if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            jumps =- 1;
            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Health(){
        healthSlider.value = currentHealth;
        if (currentHealth <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Healthbardelay(){
        yield return new WaitForSeconds(.5f);
        Debug.Log("delayover");
        delayedslider.value = currentHealth;
    }

    void Shoot(){
        if (Input.GetKeyDown(KeyCode.X)){
            Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        }
    }

    void MovementDirection(){
        if (isFacingRight && inputs < -.1f){
            Flip();
        }
        else if (!isFacingRight && inputs > .1f){
            Flip();
        }
    }

    void Flip(){
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);


    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            transform.position = startPos;
        }
        if (other.gameObject.CompareTag("Exit"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth--;
            Destroy(other.gameObject);
            StartCoroutine(Healthbardelay());
        }
    }
}
