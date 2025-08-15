using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private InteractableObject interactableObject;
    public GameObject qToInteract; //ui
    public bool hasKey; //if player has found the key yet or not

    private void Start()
    {
        if (qToInteract != null)
        {
            qToInteract.SetActive(false);
        }
        hasKey = false;
    }

    private void Update()
    {
        //if press q can interact
        if (Input.GetKeyDown(KeyCode.Q) && interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    //within range of an interactable object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableObject obj = collision.GetComponent<InteractableObject>();
        if (obj != null)
        {
            interactableObject = obj;
            if (qToInteract != null)
            {
                qToInteract.SetActive(true); //shows ui
            }
        }
    }

    //left the range of interactable object
    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableObject obj = collision.GetComponent<InteractableObject>();
        if (obj != null && obj == interactableObject)
        {
            interactableObject = null;
            if (qToInteract != null)
            {
                qToInteract.SetActive(false); //hide ui
            }
        }
    }
}
