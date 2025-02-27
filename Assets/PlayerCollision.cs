using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();                       
    }
    private void Start(){
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
    }
    private void ActivatePlayer(){
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D (Collision2D other){
        if(other.transform.tag=="Obstacle"){
            audioManager.PlaySFX(audioManager.death);
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }
}
