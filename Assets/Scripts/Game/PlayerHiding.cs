using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public GameObject player;
    public MonoBehaviour movementScript;

    private HidingSpot currentHidingSpot; 
    public bool isHidden = false;
    public GameObject eToHide; //ui
    private Vector3 originalPlayerPos; //og player position before hiding

    // Start is called before the first frame update
    void Start()
    {
        if (eToHide != null)
        {
            eToHide.SetActive(false); // hide ui at the start
        }
        if (currentHidingSpot != null && currentHidingSpot.eToLeave != null)
        {
            currentHidingSpot.eToLeave.SetActive(false); //hide ui at the start
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
                originalPlayerPos = player.transform.position; //store og player position

                //disable all so wont affect director 
                player.transform.position = currentHidingSpot.hidePosition  .position;
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<Rigidbody2D>().simulated = false;
                movementScript.enabled = false;

                eToHide.SetActive(false);
                currentHidingSpot.eToLeave.SetActive(true); //e to leave ui shows
                isHidden = true;
            }
            else
            {
                currentHidingSpot.hidingSpotActivated = false;
                Debug.Log("Player has left the hiding spot");
                player.transform.position = originalPlayerPos; //return to og position

                //enable all again, can be detected by director again
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<Rigidbody2D>().simulated = true;
                movementScript.enabled = true;

                eToHide.SetActive(true); //e to hide ui shows within range of hideable spot
                currentHidingSpot.eToLeave.SetActive(false);
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
