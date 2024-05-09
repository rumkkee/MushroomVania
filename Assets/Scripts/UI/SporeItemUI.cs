using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SporeItemUI : MonoBehaviour
{
    [SerializeField] private Slider chargebar;
    public Image sliderImageSlot;
    [SerializeField] private SporeItem sporeItem;

    private void Start()
    {
        // Have sporeItem be assigned on Awake from the SporeUIManager

        sliderImageSlot = chargebar.GetComponentInChildren<Image>();
        sliderImageSlot.sprite = sporeItem.sporeSprite;
    }

    private void Update()
    {
        SetCharge();
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
}
