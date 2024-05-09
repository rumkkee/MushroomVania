using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator ar;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        DamageFromEnemy.PlayerTakeDamage += TakeDamage;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            sr.flipX = true;
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            sr.flipX = false;
        }
        
    }

    public void TakeDamage(){
        ar.SetTrigger("TakeDamage");
    }
}
