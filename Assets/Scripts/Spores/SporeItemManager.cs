using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeItemManager : MonoBehaviour
{
    public static SporeItemManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

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
        SporeSelect.instance.mainSpore.TakeCharge();
    }
}
