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
        if(sporesList.Count == 0)
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
            /*mainSporeSlot.enabled = false;
            leftSporeSlot.enabled = false;
            rightSporeSlot.enabled = false;*/
        }
        else if (sporesList.Count == 1)
        {
            SetSlotValues(false, true, false);
            SetSporeSprites();
            //mainSporeSlot.enabled = true;
            //mainSporeSlot.sprite = mainSpore.sporeSprite;
            /*leftSporeSlot.enabled = false;
            rightSporeSlot.enabled = false;*/
        }
        else if (sporesList.Count == 2)
        {
            SetSlotValues(true, true, false);
            SetSporeSprites();
            /*mainSporeSlot.enabled = true;
            leftSporeSlot.enabled = true;*/
            /*mainSporeSlot.sprite = mainSpore.sporeSprite;
            leftSporeSlot.sprite = leftSpore.sporeSprite;*/
            /*rightSporeSlot.enabled = false;*/
        }
        else
        {
            SetSlotValues(true, true, true);
            SetSporeSprites();
            /*mainSporeSlot.enabled = true;
            leftSporeSlot.enabled = true;
            rightSporeSlot.enabled = true;*/
            /*mainSporeSlot.sprite = mainSpore.sporeSprite;
            leftSporeSlot.sprite = leftSpore.sporeSprite;
            rightSporeSlot.sprite = rightSpore.sporeSprite;*/
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

    }

    private void UpdateSpores()
    {
        if (mainSporeIndex >= sporesList.Count)
            mainSporeIndex = 0;

        if (sporesList.Count == 2)
        {
            rightSpore = null;
            mainSpore = sporesList[mainSporeIndex];
            leftSpore = sporesList[(mainSporeIndex - 1 + sporesList.Count) % sporesList.Count];
        }
        else
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
}
