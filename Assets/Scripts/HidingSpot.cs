using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Transform hidePosition; //where player hides
    public bool hidingSpotActivated = false;
    public bool nearAHidingSpot = false;
    public PlayerHiding player;


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
