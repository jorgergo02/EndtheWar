using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    
    public GameObject explosion;
    public AudioClip BossMineSound;   
    // Start is called before the first frame update
    void Start()
    {
        
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
            
            Explode();
            john.Hit();
        }
    }

    public void Explode()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(BossMineSound);
        Destroy(gameObject);

        Instantiate(explosion, transform.position, transform.rotation);
    }
}
