using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowTest : MonoBehaviour
{
    public Spore teleportSporePrefab;
    public Spore fireSporePrefab;
    public Spore cordycepsSporePrefab;
    public Spore currentSpore;
    public SporeTrajectoryRenderer sporeTrajectoryRenderer;

    public Vector3 direction;

    public float throwForce;
    public float sporeFlightDuration;
    
    /// The time the player must wait after a spore has been destroyed until they can throw again
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
    }

    private void Start()
    {
        sporeTrajectoryRenderer.enabled = false;
        currentSpore = cordycepsSporePrefab;
    }

    void Update()
    {
        

        if (Input.GetMouseButton(1) && !cancel)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));

            direction = (clickedPos - this.transform.position).normalized;

            shooting = true;
            if (Input.GetMouseButtonDown(0))
            {
                sporeTrajectoryRenderer.enabled = false;
                cancel = true;
                return;
            }
            sporeTrajectoryRenderer.enabled = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            sporeTrajectoryRenderer.enabled = false;
            if (SporeItemManager.instance.CanThrow())
            {
                Spore sporeToThrow = SporeItemManager.instance.GetCurrentSpore();
                ThrowSpore(sporeToThrow);
            }

        }
    }

    private void ThrowSpore(Spore sporePrefab){
        if(!cancel){
            //These get the direction of where the mouse is for the spore to shoot will only run if it isn't canceled.
            
            //Wont shoot anything if on cooldown or you have no spore.
            Spore sporeThrown = Instantiate(sporePrefab, transform.position, Quaternion.identity);
            sporeTrajectoryRenderer.enabled = false;
            sporeThrown.AddImpulse(direction, sporePrefab.GetThrowSpeed());
            StartCoroutine(sporeThrown.Lifespan(sporeFlightDuration));
        } else {
            cancel = false; // allows for shooting to be used again.
        }
        shooting = false;
    }

    //Cooldown functions for shooting and function for sword to not swing when cancelling shot.
    public void StartCooldown() => StartCoroutine(StartCooldownHelper());
    private IEnumerator StartCooldownHelper()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
    public bool ShootingState(){
        return !shooting;
    }
    public void ChangeSpore(SporeItem sporeItem)
    {
        if (sporeItem.sporeType == SporeType.Cordyceps)
            currentSpore= cordycepsSporePrefab;
        if (sporeItem.sporeType == SporeType.Fire)
            currentSpore = fireSporePrefab;
        if (sporeItem.sporeType == SporeType.Teleport)
            currentSpore = teleportSporePrefab;
    }
}