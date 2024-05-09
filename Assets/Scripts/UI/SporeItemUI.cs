using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SporeItemUI : MonoBehaviour
{
    [SerializeField] private Slider chargebar;
    public Image sliderImageSlot;
    [SerializeField] private SporeItem sporeItem;

    private Color white = Color.white;
    private Color grey = new Color(0.7f, 0.7f, 1f);

    private void Start()
    {
        // Have sporeItem be assigned on Awake from the SporeUIManager

        sliderImageSlot = chargebar.GetComponentInChildren<Image>();
        sliderImageSlot.sprite = sporeItem.sporeSprite;
    }

    private void Update()
    {
        SetCharge();
        SetColor();
    }

    public void SetSporeItem(SporeItem spore)
    {
        sporeItem = spore;
    }

    public void SetCharge()
    {
        float newValue = sporeItem.maxCharge - sporeItem.currentCharge;
        chargebar.value = newValue;
    }

    public void SetColor()
    {
        if(sporeItem.currentCharge >= sporeItem.chargeTakenPerUse)
        {
            sliderImageSlot.color = white;
        }
        else
        {
            sliderImageSlot.color = grey;
        }
    }
}
