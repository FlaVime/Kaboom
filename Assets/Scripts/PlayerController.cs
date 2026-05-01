using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePosition;
        HandleClick();
    }

    void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            ExplosionManager.Instance.SpawnExplosion(mousePos, Color.white);
        }
    }
}
