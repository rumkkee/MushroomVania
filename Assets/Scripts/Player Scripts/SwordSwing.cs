using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject sword;
    public float swingSpeed = 60f;
    public float cooldownDuration;
    private bool onCooldown;
    private bool swinging = false;
    private float targetAngle = -60f;
    private float speed;
    ThrowTest swordAvailability;

    // Start is called before the first frame update
    void Start()
    {
        swordAvailability = ThrowTest.instance;
        speed = swingSpeed * 10;
        sword.transform.eulerAngles = new Vector3(0, 0, 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swinging && !onCooldown && swordAvailability.ShootingState()){
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

        sword.transform.eulerAngles = new Vector3(0, 0, 60);
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