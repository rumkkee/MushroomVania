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
        //sets every little lines that are around the buttons to false so we don't see them
        isButtonHovered = new bool[buttonsToCheck.Length];
        for (int i = 0; i < hoverObjects.Length; i++)
        {
            hoverObjects[i].SetActive(false);
        }
    }

    private void Update()
    {
        // Check each button for cursor hover
        for (int i = 0; i < buttonsToCheck.Length; i++)
        {
            bool isCursorOverButton = IsCursorOverButton(buttonsToCheck[i]);
            
            // If cursor is over the button and it wasn't hovered before, activate hover object
            if (isCursorOverButton && !isButtonHovered[i])
            {
                isButtonHovered[i] = true;
                SetHoverObjectActive(i, true);
            }
            // If cursor is not over the button and it was hovered before, deactivate hover object
            else if (!isCursorOverButton && isButtonHovered[i])
            {
                isButtonHovered[i] = false;
                SetHoverObjectActive(i, false);
            }
        }
    }

    // Check if cursor is over the given button
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

    // Activate or deactivate the hover object associated with the button
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
