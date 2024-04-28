using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public delegate void CollectHealth();
    public static event CollectHealth HealthUps; 
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
            HealthUps.Invoke();
        }
    }
}
