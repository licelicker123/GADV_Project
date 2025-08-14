using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public GameObject player;
    public MonoBehaviour movementScript;

    private HidingSpot currentHidingSpot;
    public bool isHidden = false;
    public GameObject eToHide;
    public GameObject eToLeave;
    private Vector3 originalPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
            if (eToHide != null)
            {
                eToHide.SetActive(false); //hide ui at the start
                eToLeave.SetActive(false);
            }
    }

    private void Update()
    {
        //press e to hide
        if (Input.GetKeyUp(KeyCode.E) && currentHidingSpot !=null)
        {
            if (!currentHidingSpot.hidingSpotActivated)//hide
            {
                currentHidingSpot.hidingSpotActivated = true;
                Debug.Log("Player is currently hiding in the hiding spot");
                originalPlayerPos = player.transform.position;

                player.transform.position = currentHidingSpot.hidePosition  .position;
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<Rigidbody2D>().simulated = false;
                movementScript.enabled = false;

                eToHide.SetActive(false);
                eToLeave.SetActive(true);
                isHidden = true;
            }
            else
            {
                currentHidingSpot.hidingSpotActivated = false;
                Debug.Log("Player has left the hiding spot");
                player.transform.position = originalPlayerPos;
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<Rigidbody2D>().simulated = true;
                movementScript.enabled = true;

                eToHide.SetActive(true);
                eToLeave.SetActive(false);
                isHidden = false;
            }
        }
    }

    //detecting entering range of hiding spot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HidingSpot spot = collision.GetComponent<HidingSpot>();
        if (spot != null)
        {
            currentHidingSpot = spot;
            if (eToHide != null)
            {
                eToHide.SetActive(true);
            }
        }
    }

    //detect leaving range
    private void OnTriggerExit2D(Collider2D collision)
    {
        HidingSpot spot = collision.GetComponent<HidingSpot>();
        if (spot != null && spot == currentHidingSpot)
        {
            if (!currentHidingSpot.hidingSpotActivated)
            {
                currentHidingSpot = null;
                if (eToHide != null)
                {
                    eToHide.SetActive(false);
                }
            }
        }
    }
}
