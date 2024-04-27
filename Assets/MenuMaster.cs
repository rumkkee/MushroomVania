using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuMaster : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject quitMenu;
    public GameObject mainMenu;
    
    
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void BackMenu()
    {
        StartCoroutine(BackMenuCoroutine());
    }


    public void SettingsButton()
    {
        StartCoroutine(SettingsButtonCoroutine());
    }

    public void QuitBoxButton()
    {
        StartCoroutine(QuitBoxButtonCoroutine());
    }

    public void CancelButton()
    {
        StartCoroutine(CancelButtonCoroutine());
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Test Scene");
    }

    private IEnumerator BackMenuCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator SettingsButtonCoroutine()
    {
        yield return new WaitForSeconds(1f);
        mainMenu.SetActive(false);

        settingsMenu.SetActive(true);
    }

    private IEnumerator QuitBoxButtonCoroutine()
    {
        yield return new WaitForSeconds(1f);
        mainMenu.SetActive(false);

        quitMenu.SetActive(true);
    }

    private IEnumerator CancelButtonCoroutine()
    {
        yield return new WaitForSeconds(1f);
        mainMenu.SetActive(true);

        quitMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private IEnumerator QuitGameCoroutine()
    {
        yield return new WaitForSeconds(1f);

        Application.Quit();
    }
}
