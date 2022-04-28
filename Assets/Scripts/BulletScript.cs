using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public static BulletScript instance;
    public float Speed;
    public AudioClip Sound;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    private void OnBecameInvisible() {

        DestroyBullet();

    }
    
    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PotinController potin = other.GetComponent<PotinController>();
        GruntScript grunt = other.GetComponent<GruntScript>();
        JohnMovement john = other.GetComponent<JohnMovement>();
        if (grunt != null)
        {
            grunt.Hit();
        }
        if (john != null)
        {
            john.Hit();
        }
        DestroyBullet();
    }

}
