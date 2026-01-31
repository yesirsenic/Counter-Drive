using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    [SerializeField]
    GameObject resumeButton;

    [SerializeField]
    GameObject mainMenuButton;

    public void ClearButtonOn()
    {
        resumeButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClearButtonOff()
    {
        resumeButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }
}
