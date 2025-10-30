using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool hasItem; // if the object has something
    public GameObject haveNothing; // UI
    public GameObject keyFound; // UI
    public PlayerInteract player;
    public GameObject key; // key UI icon

    private void Start()
    {
        haveNothing?.SetActive(false);
        keyFound?.SetActive(false);
        key?.SetActive(false);
    }

    public void Interact()
    {
        if (hasItem)
        {
            Debug.Log("Player has found a key!");

            // Hide "nothing" UI
            haveNothing?.SetActive(false);

            // Show "key found" UI
            keyFound?.SetActive(true);

            // Update Player inventory
            player.hasKey = true;

            // ✅ Show key permanently after found
            key?.SetActive(true);

            // Mark this object as empty after
            hasItem = false;
        }
        else
        {
            Debug.Log("Player found nothing...");

            // Show nothing UI
            haveNothing?.SetActive(true);

            // Hide “key found” UI
            keyFound?.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hide both popups when player leaves range
            haveNothing?.SetActive(false);
            keyFound?.SetActive(false);
        }
    }
}
