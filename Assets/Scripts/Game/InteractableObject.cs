using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool hasItem; // if the object has smt the player can find or is just empty
    public GameObject haveNothing; //ui
    public GameObject keyFound; //ui
    public PlayerInteract player;

    private void Start()
    {
        if (haveNothing != null)
        {
            haveNothing.SetActive(false);
        }

        if (keyFound != null)
        {
            keyFound.SetActive(false);
        }
    }

    public void Interact()
    {
        if (hasItem)
        {
            Debug.Log("Player has found a key in the object!");
            if (haveNothing != null)
            {
                haveNothing.SetActive(false);
            }
            if (keyFound != null)
            {
                keyFound.SetActive(true);
                player.hasKey = true; //key ui appears
            }
                

            hasItem = false;
        }
        else
        {
            Debug.Log("Player found nothing...");
            if (haveNothing != null)
            {
                haveNothing.SetActive(true); //nothing ui appears
            }
            if (keyFound != null)
            {
                keyFound.SetActive(false);
            }
        }
    }

    //out of range, both ui wont show unless press q again
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (haveNothing != null)
            {
                haveNothing.SetActive(false);
            }
            if (keyFound != null)
            {
                keyFound.SetActive(false);
            }
        }
    }
}
