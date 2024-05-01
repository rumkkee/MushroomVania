using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxscrolling : MonoBehaviour
{
   private float length, startPos;
   public Camera cam;
   public float parallaxEffect;
   public float startingOffset;
   public NewBehaviourScript worldManager;

   void Start()
   {
      startPos = transform.position.x;
      length = GetComponent<SpriteRenderer>().bounds.size.x ;
      
     if (cam == null)
     {
         cam = FindObjectOfType<Camera>();     
     }
     else
     {
         Debug.Log("no camera attached to background"); 
     }
     

   }

   private void FixedUpdate()
   {
       
       
       
       float dist = (cam.transform.position.x * parallaxEffect) + startingOffset;

       transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z ); 
   }
}
