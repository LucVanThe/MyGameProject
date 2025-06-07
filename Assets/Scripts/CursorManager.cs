using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorNormal;
    [SerializeField] Texture2D cursorShoot;
    [SerializeField] Texture2D cursorReload;
    private Vector2 hospost = new Vector2(16, 48);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cursorNormal, hospost, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorShoot, hospost, CursorMode.Auto);

        } else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNormal, hospost, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(cursorReload, hospost, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.SetCursor(cursorNormal, hospost, CursorMode.Auto);
        }
    }
}
