using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JohnMovement : MonoBehaviour
{
    public static JohnMovement instance;

    public float Speed;
    public float JumpForce;
    public GameObject BulletPrefab;
    public bool isCrouched = false;
    public bool isDead = false;
    public int MaxHealth;
    public int Health;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private bool CanDoubleJump;
    private float LastShoot;

    public bool stopInput;

    public string actualLevel;
    
   

   private void Awake()
   {
       instance = this;
   }

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!PauseMenu.instance.isPaused && !stopInput)
        {
            if(isDead == false){

                if(Grounded)
            {
                CanDoubleJump = true;
            }
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            Animator.SetBool("running", Horizontal != 0.0f);

            if (Input.GetKeyDown(KeyCode.S))
            {
                Animator.SetBool("crouching", true);
                isCrouched = true;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                Animator.SetBool("crouching", false);
                isCrouched = false;
            }
        
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            {
                Grounded = true; 
            }
            else Grounded = false;

            Animator.SetBool("jumping", Grounded == true);

            if (Input.GetKeyDown(KeyCode.W) && Grounded && isCrouched == false)
            {
                Jump();
            }
            else
            {
                if((CanDoubleJump) && Input.GetKeyDown(KeyCode.W))
                {
                    Jump();
                    CanDoubleJump = false;
                }
            }

            if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
            {
                Shoot();
                LastShoot = Time.time;
            }

            }  
        }
    }
    
    
    private void FixedUpdate() // Más constante que el update
    {
        if (isCrouched == false)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
        }
    }

     private void Shoot()
    {
        Vector3 direction;
        
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }


    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    public void Hit()
    {
        Health --;

        if (Health <= 0) 
        {
            //Animator.SetBool("dead", true);
            //isDead = true;
            StartCoroutine(Die());
            //Destroy(gameObject);
            //MainMenu.instance.StartGame();
        }

        UIController.instance.UpdateHealthDisplay();
        
    }

    public IEnumerator Die()
    {
        Animator.SetBool("dead", true);
        isDead = true;

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(actualLevel);
    }
}
