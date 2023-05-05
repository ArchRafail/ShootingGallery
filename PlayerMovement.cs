using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float SAFE_EXTREME_CLEARENCE = 0.2f;
    private Vector3 playerPosition;
    private float extremeMoveDistance;
    private Texture2D cursorText;

    [Header("Normal speed 2")] public float playerSpeed;
    [Header("Cursor pointer")] public Texture2D aimCursor;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = transform.position;
        var rightMostObject = GameObject.Find("RightWall");
        var halfWidthOfArea = rightMostObject.transform.position.x - (rightMostObject.transform.localScale.x / 2);
        extremeMoveDistance = halfWidthOfArea - SAFE_EXTREME_CLEARENCE;
        var cursorPosition = playerPosition;
        cursorPosition.x += 10.5f;
        cursorPosition.y += 9.5f;
        Cursor.SetCursor(aimCursor, cursorPosition, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && playerPosition.x > (extremeMoveDistance * -1))
        {
            playerPosition.x -= playerSpeed / 50;
        }
        if (Input.GetKey(KeyCode.D) && playerPosition.x < extremeMoveDistance)
        {
            playerPosition.x += playerSpeed / 50;
        }
        transform.position = playerPosition;
        
        if (Input.GetButton("Cancel"))
        {
            Cursor.SetCursor(null, playerPosition, CursorMode.Auto);
        }
    }
}
