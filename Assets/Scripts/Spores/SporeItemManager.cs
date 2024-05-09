using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeItemManager : MonoBehaviour
{
    public static SporeItemManager instance;
    public SporeItem fireSporeItem;
    public SporeItem cordycepsSporeItem;
    public SporeItem telesporeItem;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public SporeItem GetFireSporeItem() => fireSporeItem;
    public SporeItem GetCordycepsSporeItem() => cordycepsSporeItem;
    public SporeItem GetTelesporeItem() => telesporeItem;

    public bool CanThrow()
    {
        return SporeSelect.instance.CanThrowCurrentSpore();
    }

    public Spore GetCurrentSpore()
    {
        PayCharge();
        return SporeSelect.instance.mainSpore.sporePrefab;
    }

    public void PayCharge()
    {
        SporeSelect.instance.mainSpore.HandleCharge();
    }
}
