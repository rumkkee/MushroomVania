using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject sword;
    public GameObject sideSwitch;
    public float swingSpeed = 60f;
    public float cooldownDuration;
    private bool onCooldown;
    private bool swinging = false;
    private float targetAngle = -90f;
    private float speed;
    private Camera mainCamera;
    ThrowTest swordAvailability;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        swordAvailability = ThrowTest.instance;
        speed = swingSpeed * 10;
        sword.transform.eulerAngles = new Vector3(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swinging && !onCooldown && swordAvailability.ShootingState()){
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 clickedPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.transform.position.z * -1f));
            Vector3 throwDirection = new Vector3(clickedPos.x - transform.position.x, clickedPos.y - transform.position.y).normalized;
            if(throwDirection.x < 0){
                sideSwitch.transform.eulerAngles = new Vector3(0,-180,0);
                sword.transform.eulerAngles = new Vector3(0, -180, 90);
            } else {
                sideSwitch.transform.eulerAngles = new Vector3(0,0,0);
                sword.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            sword.SetActive(true);
            swinging = true;
            StartCoroutine(Swing());
        }
    }

    private IEnumerator Swing()
    {
        float currentAngle = sword.transform.eulerAngles.z;
        currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;

        while (currentAngle > targetAngle){
            float step = speed * Time.deltaTime;
            sword.transform.Rotate(0, 0, -step);
            currentAngle -= step;
            yield return null;

            currentAngle = sword.transform.eulerAngles.z;
            currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;
        }

        sword.transform.eulerAngles = new Vector3(0, 0, 90);
        sword.SetActive(false);
        swinging = false;
        StartCoroutine(SwordCooldown());
    }

    private IEnumerator SwordCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
}