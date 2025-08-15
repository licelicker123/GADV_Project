using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public AudioSource bgAudio;
    public void Setup()
    {
        gameObject.SetActive(true);
        bgAudio.Play(); //play on default
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene("Game"); //press button to replay game
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu"); //press button to return to main menu
    }
}
