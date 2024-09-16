using UnityEngine;

public class SortingOrderController : MonoBehaviour
{
    public Transform player;
    public Transform blacksmith;
    private SpriteRenderer playerSprite;
    private SpriteRenderer blacksmithSprite;

    void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
        blacksmithSprite = blacksmith.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If player's Y is greater than the blacksmith's Y, the player is behind
        if (player.position.y > blacksmith.position.y)
        {
            playerSprite.sortingOrder = 1;  // Player behind blacksmith
            blacksmithSprite.sortingOrder = 2;
        }
        else
        {
            playerSprite.sortingOrder = 2;  // Player in front of blacksmith
            blacksmithSprite.sortingOrder = 1;
        }
    }
}
