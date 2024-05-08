using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    int number;
    bool attacking = false;

   //public GameObject venombeam;
    public delegate void SecondAttack();
    public static event SecondAttack SpiderBall;

    public delegate void FirstAttack();
    public static event FirstAttack VenomBeam;

    void Start()
    {
        StartCoroutine(PickAttack());
    }

    IEnumerator PickAttack()
    {
        yield return new WaitForSeconds(10f);
        number = Random.RandomRange(1, 3);
        if (number == 1)
        {
            VenomBeam.Invoke();
        }
        else if (number == 2)
        {
            SpiderBall.Invoke();
        }
        StartCoroutine(PickAttack());
    }
}
