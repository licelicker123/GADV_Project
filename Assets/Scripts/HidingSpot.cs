using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{

    public bool IsHiding {  get; private set; }
    public Sprite hiddenSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public bool CanInteract()
    {
        return !IsHiding;
    }

    public void Interact()
    {
        if (!CanInteract()) return; //hide
    }

    private void HideInLocation()
    {
        SetHidden(true);
    }

    public void SetHidden(bool hidden)
    {
        IsHiding = hidden;
        if (IsHiding)
        {
            GetComponent<SpriteRenderer>().sprite = hiddenSprite;
        }
    }
}
