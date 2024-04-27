using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonHoverChecker : MonoBehaviour
{
    public GameObject[] buttonsToCheck;
    public GameObject[] hoverObjects;

    private bool[] isButtonHovered;

    private void Start()
    {
        isButtonHovered = new bool[buttonsToCheck.Length];
        for (int i = 0; i < hoverObjects.Length; i++)
        {
            hoverObjects[i].SetActive(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < buttonsToCheck.Length; i++)
        {
            bool isCursorOverButton = IsCursorOverButton(buttonsToCheck[i]);
            if (isCursorOverButton && !isButtonHovered[i])
            {
                isButtonHovered[i] = true;
                SetHoverObjectActive(i, true);
            }
            else if (!isCursorOverButton && isButtonHovered[i])
            {
                isButtonHovered[i] = false;
                SetHoverObjectActive(i, false);
            }
        }
    }

    public bool IsCursorOverButton(GameObject button)
    {
        if (button == null)
        {
            return false;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (EventSystem.current.currentSelectedGameObject == button)
            {
                return true;
            }
        }

        return false;
    }

    public void SetHoverObjectActive(int buttonIndex, bool isActive)
    {
        if (buttonIndex < 0 || buttonIndex >= hoverObjects.Length)
        {
            return;
        }

        if (hoverObjects[buttonIndex] != null)
        {
            hoverObjects[buttonIndex].SetActive(isActive);
        }
    }
    
}
