using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockExit : MonoBehaviour
{
    public PlayerInteract player;
    public GameObject lockedDoor; // door locked ui

    //within range of the exit door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.hasKey == false)
            {
                lockedDoor.SetActive(true); //shows ui, indicates player still need to find key
            }
            else if (player.hasKey == true) {
                lockedDoor.SetActive(false);
                SceneManager.LoadScene("WinScreen"); //player unlocks the door, win!!!
            }
        }

    }

    //leaves range of door, no more ui
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lockedDoor.SetActive(false);
        }
    }
}
