using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiPlayerDeathTrigger : MonoBehaviour
{
    [SerializeField] private GameObject antiPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(antiPlayer.gameObject);
        }
    }
}
