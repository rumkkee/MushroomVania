using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxscrolling : MonoBehaviour
{
   private float length, startPos;
   public GameObject cam;
   public float parallaxEffect;
   public float startingOffset;

   void start()
   {
      startPos = transform.position.x;
      length = GetComponent<SpriteRenderer>().bounds.size.x ;
      
   }

   private void FixedUpdate()
   {
       float dist = (cam.transform.position.x * parallaxEffect) + startingOffset;

       transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z ); 
   }
}
