using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour

{
    public AudioClip LevelEndSound; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(LevelEndSound);
            LevelManager.instance.EndLevel();
        }
    }
}
