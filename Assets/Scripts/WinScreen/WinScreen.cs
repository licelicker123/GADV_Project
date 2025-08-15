using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public AudioSource bgAudio;
    public void Setup()
    {
        gameObject.SetActive(true);
        bgAudio.Play(); //play music on default
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Game"); //press button to replay game
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu"); //press button to go main menu
    }
}
