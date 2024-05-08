using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnabler : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    void Start()
    {
        platform1.SetActive(false);
        platform2.SetActive(false);
        platform3.SetActive(false);

        InvokeRepeating("TogglePlatform", 10f, 30f);
    }

    void TogglePlatform()
    {
        EnablePlatforms();
    }

    IEnumerator EnablePlatformTimer()
    {
        platform1.SetActive(true);
        platform2.SetActive(true);
        platform3.SetActive(true);
        yield return new WaitForSeconds(10f);
        platform1.SetActive(false);
        platform2.SetActive(false);
        platform3.SetActive(false);
    }

    public void EnablePlatforms()
    {
        StartCoroutine(EnablePlatformTimer());
    }
}
