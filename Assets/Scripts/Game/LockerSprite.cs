using UnityEngine;

public class LockerSprite : MonoBehaviour
{
    public Sprite openLocker;
    public Sprite closedLocker;

    public PlayerHiding playerHiding;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = openLocker;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHiding != null && playerHiding.isHidden == true)
        {
            spriteRenderer.sprite = closedLocker;
        }
        else
        {
            spriteRenderer.sprite = openLocker;
        }
    }
}
