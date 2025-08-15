using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen: MonoBehaviour
{
    public AudioSource bgAudio;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        bgAudio.Play(); //play bg music on default
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game"); //press play to start game 
    }
}
