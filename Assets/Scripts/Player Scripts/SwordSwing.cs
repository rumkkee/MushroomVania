using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject sword;//game objects for the sword to activate and switch sides if need be.
    public GameObject sideSwitch;
    public float swingSpeed = 60f;//Sword Swing variables
    private float speed;
    public float cooldownDuration; //Cooldown for each sword swings
    private bool onCooldown = false; //Booleans to allow sword to swing or not swing
    private bool swinging = false;
    private float targetAngle = -90f; //Final angle for sword.
    private Camera mainCamera; 
    ThrowTest swordAvailability;
    private bool isGrounded;

    public AudioSource swingSound;
    
    void Start()
    {
        mainCamera = Camera.main; //These lines set up mainCamera and ability to swing if not in shooting state.
        swordAvailability = ThrowTest.instance;
        speed = swingSpeed * 15; //These set the start position of the sword and increases the speed of the swing.
        sword.transform.eulerAngles = new Vector3(0, 0, 90);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swinging && !onCooldown && swordAvailability.ShootingState())
        {
            //Gets mouse direction
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));
            Vector3 throwDirection = new Vector3(clickedPos.x - transform.position.x, clickedPos.y - transform.position.y).normalized;

            // Debug.LogError(throwDirection.y);
            
            if(throwDirection.y > .9)
            {
                sideSwitch.transform.eulerAngles = new Vector3(0,0,106);
                sword.transform.eulerAngles = new Vector3(0,0,-40);
            }
            else if(throwDirection.y < -.9 && !isGrounded)
            {
                sideSwitch.transform.eulerAngles = new Vector3(0,0,-60);
                sword.transform.eulerAngles = new Vector3(0,0,-30);
            }
            else if(throwDirection.x < 0)
            {
                sideSwitch.transform.eulerAngles = new Vector3(0,-180,0);
                sword.transform.eulerAngles = new Vector3(0, -180, 90);
            } 
            //Switches side of sword depending on where the mouse is.
            else 
            {
                sideSwitch.transform.eulerAngles = new Vector3(0,0,0);
                sword.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            
            sword.SetActive(true);
            swinging = true;
            StartCoroutine(Swing());
            
            swingSound.Play();
        }

        Vector3 startPoint = transform.position;
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.1f;
        
        RaycastHit hit;
        if (Physics.Raycast(startPoint, Vector3.down, out hit, halfHeight))
        {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    private IEnumerator Swing()
    {
        //This gets the current angle of the sword and makes it so it doesn't spin the wrong way.
        // float currentAngle = sword.transform.eulerAngles.z;
        // currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;

        // while (currentAngle > targetAngle)
        // {
        //     float step = speed * Time.deltaTime;
        //     sword.transform.Rotate(0, 0, -step);
        //     currentAngle -= step;
        //     yield return null;

        //     //This gets the current angle of the sword and makes it so it doesn't spin the wrong way.
        //     currentAngle = sword.transform.eulerAngles.z;
        //     currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;
        // }

        // //sets the sword in the right original position and deactivates it. Calls the cooldown function for sword swing.
        // sword.transform.eulerAngles = new Vector3(0, 0, 90);
        yield return new WaitForSeconds(.15f);
        sword.SetActive(false);
        swinging = false;
        StartCoroutine(SwordCooldown());
    }

    private IEnumerator SwordCooldown()
    {
        //This is just a cooldown on the time between swings when previous swing finishes.
        onCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
}