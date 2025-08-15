using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Transform hidePosition; //where player hides
    public bool hidingSpotActivated = false;
    public bool nearAHidingSpot = false; //within range (in the box collider)
    public PlayerHiding player;
    public GameObject eToLeave; //ui

    private void Start()
    {
        if (eToLeave != null)
            eToLeave.SetActive(false); //no ui at the start
    }

    // player and hiding location collides! 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerHiding>();
            nearAHidingSpot = true;

            if (hidingSpotActivated) 
            {
                Debug.Log("Player is currently hiding in the hiding spot");

                player.eToHide.SetActive(false);
            }
            else
            {
                player.eToHide.SetActive(true);
                Debug.Log("Player is within range of a hiding spot!");
            }

        }

        
    }

    // player and hiding location no longer collides
    private void OnTriggerExit2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
           
                player = collision.GetComponent<PlayerHiding>();
                nearAHidingSpot = false;
                Debug.Log("Player has left the range of hiding spot");

                player.eToHide.SetActive(false);

                if (!hidingSpotActivated)
                {
                Debug.Log("Player has left the hiding spot");

                }
            }
    }
}
