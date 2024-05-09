using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    private Animator ar;
    private SpriteRenderer sr;
    private CharacterController playerController;
    private bool isPaused = false;
    private bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            playerController.enabled = false;
            Time.timeScale = 0f;
            pauseMenu.SetActive(!isPaused);
            isPaused = !isPaused;
            if (!isPaused)
            {
                Time.timeScale = 1.0f;
                playerController.enabled = true;
            }  
        }
        
    }

    public void TakeDamage(){
        ar.SetTrigger("TakeDamage");
    }

    public void Continue()
    {    
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        playerController.enabled = true;
    }

    public void Quit(){
         Time.timeScale = 1.0f;
          //StartCoroutine(BackMenuCoroutine());
         SceneManager.LoadScene("MainMenu");
    }

    // private IEnumerator BackMenuCoroutine()
    // {
    //     yield return new WaitForSeconds(.5f);

    //     SceneManager.LoadScene("MainMenu");
    // }

}
