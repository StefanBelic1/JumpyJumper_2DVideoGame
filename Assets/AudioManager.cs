using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] AudioSource musicSource;
   [SerializeField] AudioSource SFXSource;

   public AudioClip background;
   public AudioClip crouch;
   public AudioClip death;
   public AudioClip jump;
   public AudioClip coin;


   private void Start(){
    musicSource.clip = background;
    musicSource.volume = 0.1f;
    musicSource.Play();
   
    
   }

   public void PlaySFX (AudioClip clip){

        SFXSource.PlayOneShot(clip);
   }
   
}
