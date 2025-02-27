using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Transform kv;
   [SerializeField] private float jump = 10f;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private Transform feetPos;
   [SerializeField] private float groundDistance=0.25f;
   [SerializeField] private float jumpTime=0.3f;
   [SerializeField] private float crouchHeight=0.5f;
    


   AudioManager audioManager;
   public ParticleSystem dustCloud;

   private bool isGrounded = false;
   private bool isJumping=false;
   private float jumpTimer;


   private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();                       
    }

   private void Update(){

        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance,groundLayer);
        #region JUMP

        Debug.Log("Is Grounded: " + isGrounded);

        if(isGrounded && Input.GetButtonDown("Jump")){
            audioManager.PlaySFX(audioManager.jump);
            isJumping=true;
            rb.velocity = Vector2.up*jump;
            CreateDust();
        }
        if(isJumping && Input.GetButton("Jump")){
            if(jumpTimer<jumpTime){
                rb.velocity = Vector2.up* jump;
                jumpTimer += Time.deltaTime;
            } else{
                isJumping=false;
            }
        }
        if(Input.GetButtonUp("Jump")){
            isJumping=false;
            jumpTimer=0;
        }
        #endregion
       
       
        #region CROUCH
        if(isGrounded && Input.GetButtonDown("Crouch")) {
            audioManager.PlaySFX(audioManager.crouch);
            kv.localScale= new Vector3(kv.localScale.x, crouchHeight, kv.localScale.z);
        }
        if(isJumping && Input.GetButton("Crouch")){
            kv.localScale= new Vector3(kv.localScale.x, 1f, kv.localScale.z);
        }
        if (!isGrounded && Input.GetButton("Crouch")) {
            rb.velocity = new Vector2(rb.velocity.x, -jump * 1.5f); // Povećajte brzinu prema dolje
            kv.localScale= new Vector3(kv.localScale.x, 1f, kv.localScale.z);
        }
        if(Input.GetButtonUp("Crouch")){
            kv.localScale= new Vector3(kv.localScale.x, 1f, kv.localScale.z);
        }
        #endregion
   } 
   void CreateDust()
    {
        if (dustCloud != null)
        {
            dustCloud.Play();  
        }
    }
    
   

}
