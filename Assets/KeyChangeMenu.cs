using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class InputManager : MonoBehaviour
{
    public InputActionAsset playerActions;
    public TMP_Text buttonText; // Utilisation de TMP_Text pour TextMeshProUGUI

    // Fonction pour changer le binding d'une action à partir du chemin de l'input
    public void ChangeBinding(string actionPath, InputBinding newBinding)
    {
        InputAction action = playerActions.FindAction(actionPath);
        if (action != null)
        {
            action.RemoveAllBindingOverrides();
            action.ApplyBindingOverride(0, newBinding);
        }
        else
        {
            Debug.LogError("Action not found with path: " + actionPath);
        }
    }

    // Fonction appelée lorsque le bouton est cliqué
    public void OnButtonClick(string actionPath)
    {
        StartCoroutine(WaitForButtonPress(actionPath, buttonText));
    }

    // Fonction pour attendre que le joueur appuie sur un bouton pour changer le binding
    private IEnumerator WaitForButtonPress(string actionPath, TMP_Text buttonText) // Utilisation de TMP_Text pour TextMeshProUGUI
    {
        InputAction action = playerActions.FindAction(actionPath);
        if (action != null)
        {
            while (true)
            {
                if (action.triggered)
                {
                    InputControl control = action.controls[0];
                    if (control != null)
                    {
                        if (control is ButtonControl buttonControl)
                        {
                            // Appeler ChangeBinding avec le nouveau binding correspondant au bouton pressé
                            ChangeBinding(actionPath, new InputBinding { overridePath = buttonControl.path });
                            buttonText.text = buttonControl.displayName; // Mettre à jour le texte avec le nom du bouton pressé
                            break;
                        }
                    }
                }
                yield return null;
            }
        }
        else
        {
            Debug.LogError("Action not found with path: " + actionPath);
        }
    }
}
