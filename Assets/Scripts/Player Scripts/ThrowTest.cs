using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowTest : MonoBehaviour
{
    public GameObject Arrow;
    public Spore sporePrefab;
    public float throwForce;
    public float sporeFlightDuration;

    /// <summary>
    /// The time the player must wait after a spore has been destroyed until they can throw again
    /// </summary>
    public float cooldownDuration;
    public float turnSpeed = 300f;
    private bool onCooldown;

    private Camera mainCamera;
    private bool cancel = false;
    private bool shooting = false;
    public static ThrowTest instance;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        onCooldown = false;
        Spore.OnSporeDestroyed += StartCooldown;
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));
            
        Vector3 direction = clickedPos - Arrow.transform.position;
        direction = new Vector3(-direction.y, direction.x, 0);

        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        Arrow.transform.rotation = Quaternion.Lerp(Arrow.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        if(Input.GetMouseButton(1) && !cancel){
            shooting = true;
            if(Input.GetMouseButtonDown(0)){
                Arrow.SetActive(false);
                cancel = true;
                return;
            }
            Arrow.SetActive(true);
        } 
        if(Input.GetMouseButtonUp(1)){
            Arrow.SetActive(false);
            ThrowSpore();
        }
    }

    private void ThrowSpore(){
        if(!cancel){
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));

            Vector3 throwDirection = new Vector3(clickedPos.x - transform.position.x, clickedPos.y - transform.position.y).normalized;
            

            if(Spore.instance == null && !onCooldown)
            {
                Spore sporeThrown = Instantiate(sporePrefab, transform.position, Quaternion.identity);
                Arrow.SetActive(false);
                sporeThrown.AddImpulse(throwDirection, throwForce);
                StartCoroutine(sporeThrown.Lifespan(sporeFlightDuration));
            }
        } else {
            cancel = false;
        }
        shooting = false;
    }

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