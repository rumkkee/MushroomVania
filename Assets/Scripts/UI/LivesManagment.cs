using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManagement : MonoBehaviour
{
    public int maxLife = 3;
    public int lifeCount = 3;
    public Image[] lifeImages;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLifeUI();
    }

    void Update()
    {
        //test to view when the player takes damage
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        lifeCount--;
        if (lifeCount < 0)
            lifeCount = 0;
    
        UpdateLifeUI();

        if (lifeCount <= 0)
        {
            // Death Menu
        }
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < lifeCount)
                lifeImages[i].enabled = true;
            else
                lifeImages[i].enabled = false;
        }
    }
}