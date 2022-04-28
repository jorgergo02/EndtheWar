using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class PotinController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended };

    public bossStates currentStates;

    public Transform theBoss;
    public Transform John;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public AudioClip HurtSound;

    public GameObject hitBox;

    [Header("Health")]
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;

    // Start is called before the first frame update
    void Start()
    {
        currentStates = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(John.position.x - theBoss.position.x);
         if (distance <= 3.5f)
         {
            switch(currentStates)
            {
                case bossStates.shooting:

                    shotCounter -= Time.deltaTime;

                    if(shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots;

                        var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                        newBullet.transform.localScale = theBoss.localScale;
                    }

                    break;

                case bossStates.hurt:
                    if(hurtCounter > 0)
                    {
                        hurtCounter -= Time.deltaTime;

                        if(hurtCounter <= 0)
                        {
                            currentStates = bossStates.moving;

                            mineCounter = 0;

                            if(isDefeated)
                            {
                                theBoss.gameObject.SetActive(false);
                                Instantiate(explosion, theBoss.position, theBoss.rotation);

                                winPlatform.SetActive(true);

                                currentStates = bossStates.ended;
                            }
                        }
                    }
                    break;

                case bossStates.moving:

                    if(moveRight)
                    {
                        theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                        if(theBoss.position.x > rightPoint.position.x)
                        {
                            theBoss.localScale = new Vector3(0.1533943f, 0.1533943f, 0.1533943f);

                            moveRight = false;

                            EndMovement();
                        }
                    } else
                    {
                        theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                        if (theBoss.position.x < leftPoint.position.x)
                        {

                            theBoss.localScale = new Vector3(-0.1533943f, 0.1533943f, 0.1533943f);

                            moveRight = true;

                            EndMovement();
                        }
                    }

                    mineCounter -= Time.deltaTime;

                    if(mineCounter <= 0)
                    {
                        mineCounter = timeBetweenMines;

                        Instantiate(mine, minePoint.position, minePoint.rotation);
                    }

                    break;
        }
         }
        
    }
    public void TakeHit()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HurtSound);
        currentStates = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");
        
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if(mines.Length > 0)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        health--;

        if(health <= 0)
        {
            isDefeated = true;
        } else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }
    private void EndMovement()
    {
        currentStates = bossStates.shooting;

        shotCounter = 0f;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }
}
