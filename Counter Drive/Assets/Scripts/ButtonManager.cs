using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOverRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void BtnClickSound()
    {
        SFXManager.Instance.PlayShot(SFXType.ButtonClick);
    }


}
