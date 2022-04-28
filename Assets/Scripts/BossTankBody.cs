using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankBody : MonoBehaviour
{
     private Rigidbody2D Rigidbody2D;
    public AudioClip HitSound;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        JohnMovement john = other.GetComponent<JohnMovement>();
        if (john != null)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(HitSound);
            john.Hit();
        }
    }
}
