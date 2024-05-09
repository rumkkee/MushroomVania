using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator ar;
    private SpriteRenderer sr;
    private bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        DamageFromEnemy.PlayerTakeDamage += TakeDamage;
    }

    void Update()
    {
        Vector3 startPoint = transform.position;
        float halfHeight = .6f;
        RaycastHit hit;
        // Debug.DrawRay(startPoint, Vector3.down * halfHeight, Color.red);
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight))
        {
            if (hit.collider.isTrigger){return;}
            grounded = true;
        } else {
            grounded = false;
        }


        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            sr.flipX = true;
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            sr.flipX = false;
        }

        if (Input.GetKeyUp(KeyCode.W) || grounded){
            ar.SetBool("Glide", false);
            ar.SetTrigger("FinishGlide");
        }

        if (Input.GetKey(KeyCode.W) && !grounded){
            ar.SetBool("Glide", true);
        }
        
    }

    public void TakeDamage(){
        ar.SetTrigger("TakeDamage");
    }
}
