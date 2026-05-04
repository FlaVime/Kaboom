using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float explosionCooldown = 80f;
    [SerializeField] private UnityEngine.UI.Image cooldownRing;
    private float explosionTimer = 0f;
    private Camera cam;

    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;
        explosionTimer = explosionCooldown;
    }

    void Update()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePosition;

        HandleSlice(mousePosition);
        HandleExplosion(mousePosition);
    }

    void HandleSlice(Vector2 mousePos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            Creatures creature = hit.collider.GetComponent<Creatures>();
            if (creature != null)
            {
                creature.SliceKill();
            }
        }
    }

    void HandleExplosion(Vector2 mousePos)
    {
        explosionTimer = Mathf.Max(explosionTimer - Time.deltaTime, -1f);

        cooldownRing.fillAmount = 1f - Mathf.Clamp01(explosionTimer / explosionCooldown);

        if (Mouse.current.leftButton.wasPressedThisFrame && explosionTimer <= 0f)
        {
            ExplosionManager.Instance.SpawnShockwave(mousePos);
            explosionTimer = explosionCooldown;
        }
    }
}
