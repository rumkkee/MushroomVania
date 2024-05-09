using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SporeSelect : MonoBehaviour
{
    public SporeItem mainSpore;
    SporeItem leftSpore;
    SporeItem rightSpore;
    private ThrowTest throwTest;
    bool isSwitching = false;
    [Space]

    public SporeItemUI mainSporeItemUI;
    public SporeItemUI leftSporeItemUI;
    public SporeItemUI rightSporeItemUI;

    int mainSporeIndex = 0;

    public List<SporeItem> sporesList = new List<SporeItem>();

    public Animator animator;

    public static SporeSelect instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Init();
        }
    }

    void Init()
    {
        throwTest = FindObjectOfType<ThrowTest>();
        SetSpores();
        mainSporeIndex = 0;  
        UpdateUI();
        UpdateEnabledSporeSprites();
    }

    void Update()
    {
        if(isSwitching)
            return;
        
        if(sporesList.Count == 0 || sporesList.Count == 1)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            LeftSwap();
            UpdateUI();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            RightSwap();
            UpdateUI();
        }
        throwTest.ChangeSpore(mainSpore);
    }

    private void SetSpores()
    {
        leftSpore = null;
        rightSpore = null;
        if (sporesList.Count == 0)
            return;

        if(sporesList.Count == 1)
        {
            mainSpore = sporesList[0];

            mainSporeItemUI.SetSporeItem(mainSpore);
        }
        else if(sporesList.Count == 2)
        {
            mainSpore = sporesList[1];
            leftSpore = sporesList[0];

            mainSporeItemUI.SetSporeItem(mainSpore);
            leftSporeItemUI.SetSporeItem(leftSpore);
        }
        else
        {
            mainSpore = sporesList[1];
            leftSpore = sporesList[0];
            rightSpore = sporesList[2];

            mainSporeItemUI.SetSporeItem(mainSpore);
            leftSporeItemUI.SetSporeItem(leftSpore);
            rightSporeItemUI.SetSporeItem(rightSpore);
        }
    }

    public void AddNewSpore(SporeItem spore)
    {
        sporesList.Add(spore);
        UpdateSpores();
        UpdateUI();
        UpdateEnabledSporeSprites();
    }

    private void LeftSwap()
    {
        if (isSwitching || sporesList.Count <= 1)
            return;

        mainSporeIndex++;
        if (mainSporeIndex >= sporesList.Count)
            mainSporeIndex = 0;

        if (sporesList.Count == 2)
        {
            animator.Play("LeftSwitchHalf");
            rightSpore = null;
            mainSpore = sporesList[mainSporeIndex];
            leftSpore = sporesList[(mainSporeIndex - 1 + sporesList.Count) % sporesList.Count];

            mainSporeItemUI.SetSporeItem(mainSpore);
            leftSporeItemUI.SetSporeItem(leftSpore);
        }
        else
        {
            animator.Play("LeftSwitchFull");
            mainSpore = sporesList[mainSporeIndex];
            rightSpore = sporesList[(mainSporeIndex + 1) % sporesList.Count];
            leftSpore = sporesList[(mainSporeIndex - 1 + sporesList.Count) % sporesList.Count];

            mainSporeItemUI.SetSporeItem(mainSpore);
            leftSporeItemUI.SetSporeItem(leftSpore);
            rightSporeItemUI.SetSporeItem(rightSpore);
        }
        isSwitching = true;
    }

    private void RightSwap()
    {
        if (isSwitching || sporesList.Count <= 1)
            return;

        mainSporeIndex--;
        if (mainSporeIndex < 0)
            mainSporeIndex = sporesList.Count - 1;

        if (sporesList.Count == 2)
        {
            animator.Play("RightSwitchHalf");
            rightSpore = null;
            mainSpore = sporesList[mainSporeIndex];
            leftSpore = sporesList[(mainSporeIndex - 1 + sporesList.Count) % sporesList.Count];

            mainSporeItemUI.SetSporeItem(mainSpore);
            leftSporeItemUI.SetSporeItem(leftSpore);
        }
        else
        {
            animator.Play("RightSwitchFull");
            mainSpore = sporesList[mainSporeIndex];
            rightSpore = sporesList[(mainSporeIndex + 1) % sporesList.Count];
            leftSpore = sporesList[(mainSporeIndex - 1 + sporesList.Count) % sporesList.Count];

            mainSporeItemUI.SetSporeItem(mainSpore);
            leftSporeItemUI.SetSporeItem(leftSpore);
            rightSporeItemUI.SetSporeItem(rightSpore);
        }
        isSwitching = true;
    }

    private void UpdateUI()
    {
        if (sporesList.Count == 0)
        {
            SetSlotValues(false, false, false);
        }
        else if (sporesList.Count == 1)
        {
            SetSlotValues(false, true, false);
            SetSporeSprites();
        }
        else if (sporesList.Count == 2)
        {
            SetSlotValues(true, true, false);
            SetSporeSprites();
        }
        else
        {
            SetSlotValues(true, true, true);
            SetSporeSprites();
        }
    }

    private void SetSlotValues(bool leftActive, bool mainActive, bool rightActive)
    {
        leftSporeItemUI.sliderImageSlot.enabled = leftActive;
        mainSporeItemUI.sliderImageSlot.enabled = mainActive;
        rightSporeItemUI.sliderImageSlot.enabled = rightActive;
    }

    private void SetSporeSprites()
    {
        if(leftSpore != null)
            leftSporeItemUI.sliderImageSlot.sprite = leftSpore.sporeSprite;
    
        if(mainSpore != null)
            mainSporeItemUI.sliderImageSlot.sprite = mainSpore.sporeSprite;
        
        if(rightSpore != null)
            rightSporeItemUI.sliderImageSlot.sprite = rightSpore.sporeSprite;
        UpdateEnabledSporeSprites();
    }

    private void UpdateEnabledSporeSprites()
    {
        Debug.Log("Updating Spore UI object activeness");
        //leftSporeItemUI.gameObject.SetActive((leftSpore != null) ? true : false);
        Debug.Log("Is left spore Present?: " + leftSpore != null);
        leftSporeItemUI.fill.gameObject.SetActive((leftSpore != null) ? true : false);

        //mainSporeItemUI.gameObject.SetActive((mainSpore != null) ? true : false);
        mainSporeItemUI.fill.gameObject.SetActive((mainSpore != null) ? true : false);
        //rightSporeItemUI.gameObject.SetActive((rightSpore != null) ? true : false);
        rightSporeItemUI.fill.gameObject.SetActive((rightSpore != null) ? true : false);
    }

    private void UpdateSpores()
    {
        if (mainSporeIndex >= sporesList.Count)
            mainSporeIndex = 0;
        if(sporesList.Count == 1)
        {
            rightSpore = null;
            mainSpore = sporesList[mainSporeIndex];
            leftSpore = null;
        }
        else if (sporesList.Count == 2)
        {
            rightSpore = null;
            mainSpore = sporesList[mainSporeIndex];
            leftSpore = sporesList[(mainSporeIndex - 1 + sporesList.Count) % sporesList.Count];
        }
        else if(sporesList.Count == 3)
        {
            mainSpore = sporesList[mainSporeIndex];
            rightSpore = sporesList[(mainSporeIndex + 1) % sporesList.Count];
            leftSpore = sporesList[(mainSporeIndex - 1 + sporesList.Count) % sporesList.Count];
        }
    }

    public void OnAnimationEnd()
    {
        //function so we can't switch 
        isSwitching = false;
    }

    public bool CanThrowCurrentSpore()
    {
        return mainSpore.CanThrowCurrentSpore();
    }

    private void OnValidate()
    {
        Init();
    }
}
