using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public Image collectableSprite;
    public bool isSpore;
    public SporeItem sporeItem;

    SporeSelect sporeSelect;
    CoinManagement coinManager;

    void Start()
    {
        if (isSpore)
        {
            sporeSelect = FindObjectOfType<SporeSelect>();
            if (sporeItem != null && collectableSprite != null)
            {
                collectableSprite.sprite = sporeItem.sporeSprite;
            }
            else
            {
                Debug.LogError("Missing sprite or sporeItem reference in Collectable!");
            }
        }
        else
        {
            coinManager = FindObjectOfType<CoinManagement>();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (isSpore && sporeItem != null && sporeSelect != null)
            {
                sporeSelect.AddNewSpore(sporeItem);
            }

            if(!isSpore)
            {
                Debug.Log("Collected coin");
                if (coinManager != null)
                {
                    coinManager.AddCoin();
                }
            }
        }
        Destroy(gameObject);
    }
}
