using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; //find closest interactable thing
    public GameObject interactionIcon;
    // Start is called before the first frame update
    void Start()
    {
        interactionIcon.SetActive(false);   
    }

    public void OnInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactableInRange?.Interact();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactableInRange = null;
        interactionIcon.SetActive(false);
    }
}
