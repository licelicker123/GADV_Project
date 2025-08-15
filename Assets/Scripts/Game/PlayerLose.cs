using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLose : MonoBehaviour
{
    public PlayJumpscare playJumpscare;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playJumpscare.Jumpscare();
            StartCoroutine(GameOverScreen()); 

        }
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(2.5f); //wait until jumpscare video finishes
        SceneManager.LoadScene("GameOver"); //go to game over scene!
    }

}
