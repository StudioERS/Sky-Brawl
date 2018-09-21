using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterJump : MonoBehaviour {


     private int jumps = 2;//nombre de sauts permit

    //doit faire constructeur pour acceder en public
     private float jumpForce = 5.0f;
     private float gravity = 15f;

     private Rigidbody rb;

     //check pour sol
     private bool isGrounded;//valeur de la methode qui regarde si le joueur touche a objet qui est nommé "Floor" 
     private float hitdistance;
     private LayerMask Layer;

     private void Awake()
     {
         rb = GetComponent<Rigidbody>();
     }
    //****************************************************************************
    //methode pour le saut
    //****************************************************************************
    private void Update()
     {

         if ( isGrounded == true)
         {
             jumps = 2;
         }

        if ((Input.GetButtonDown("Jump")) && (jumps >= 1))
        {
            jumps--;
            rb.GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }
        else if (isGrounded == false)
        {
            Physics.gravity = new Vector3(0 , gravity * (-1),0); ;
        }
     }
    //****************************************************************************
    //methode pour verifier que le joueur touche a objet qui est nommé "Floor"
    //****************************************************************************
    private void OnCollisionEnter(Collision TheCollision)
     {
         if (TheCollision.gameObject.name == "floor")
         {
             isGrounded = true;
         }


     }

     void OnCollisionExit(Collision theCollision)
     {
         if (theCollision.gameObject.name == "floor")
         {
             isGrounded = false;
         }
     }
    //****************************************************************************


    

}
