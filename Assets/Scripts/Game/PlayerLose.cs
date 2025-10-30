using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLose : MonoBehaviour
{
    public PlayJumpscare playJumpscare;
    public GameObject player;
    public MonoBehaviour movementScript;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().simulated = false;
            movementScript.enabled = false;
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
