using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowTest : MonoBehaviour
{
    public SporeTrajectoryRenderer sporeTrajectoryRenderer;
    public Spore sporePrefab;
    public float sporeFlightDuration;
    public Vector3 direction;

    /// <summary>
    /// The time the player must wait after a spore has been destroyed until they can throw again
    /// </summary>
    public float cooldownDuration;
    private bool onCooldown = false;

    private Camera mainCamera;
    private bool cancel = false;
    private bool shooting = false;
    public static ThrowTest instance;

    private void Awake()
    {
        //Sets up variables to be used later
        instance = this;
        mainCamera = Camera.main;
        onCooldown = false;
        Spore.OnSporeDestroyed += StartCooldown;
    }

    private void Start()
    {
        sporeTrajectoryRenderer.enabled = false;
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));
            
        direction = clickedPos - this.transform.position;
        direction = new Vector3(-direction.y, direction.x, 0);

        if(Input.GetMouseButton(1) && !cancel){
            shooting = true;
            if(Input.GetMouseButtonDown(0)){
                sporeTrajectoryRenderer.enabled = false;
                cancel = true;
                return;
            }
            sporeTrajectoryRenderer.enabled = true;
        } 
        if(Input.GetMouseButtonUp(1)){
            sporeTrajectoryRenderer.enabled = false;
            ThrowSpore();
        }
    }

    private void ThrowSpore(){
        if(!cancel){
            //These get the direction of where the mouse is for the spore to shoot will only run if it isn't canceled.
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));

            Vector3 throwDirection = new Vector3(clickedPos.x - transform.position.x, clickedPos.y - transform.position.y).normalized;
            
            //Wont shoot anything if on cooldown or you have no spore.
            if(Spore.instance == null && !onCooldown)
            {
                Spore sporeThrown = Instantiate(sporePrefab, transform.position, Quaternion.identity);
                sporeTrajectoryRenderer.enabled = false;
                sporeThrown.AddImpulse(throwDirection, sporePrefab.GetThrowSpeed());
                StartCoroutine(sporeThrown.Lifespan(sporeFlightDuration));
            }
        } else {
            cancel = false; // allows for shooting to be used again.
        }
        shooting = false;
    }

    //Cooldown functions for shooting and function for sword to not swing when cancelling shot.
    private void StartCooldown() => StartCoroutine(StartCooldownHelper());
    private IEnumerator StartCooldownHelper()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
    public bool ShootingState(){
        return !shooting;
    } 
}