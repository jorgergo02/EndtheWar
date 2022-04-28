using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    public AudioClip BossBulletSound;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(BossBulletSound);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        JohnMovement john = other.GetComponent<JohnMovement>();
        if (john != null)
        {
            john.Hit();
        }
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        
    }
}
