using UnityEngine;
using UnityEngine.InputSystem;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float shakeMagnitude = 0.3f;
    [SerializeField] private float shakeDuration = 0.2f;

    private Vector3 camOriginalPos;
    private float shakeTimer = 0f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        camOriginalPos = cam.transform.position;
    }

    void Update()
    {
        HandleClick();
        HandleCameraShake();
    }

    void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Instantiate(explosionPrefab, mousePos, Quaternion.identity);
            shakeTimer = shakeDuration;
        }
    }

    void HandleCameraShake()
    {
        if (shakeTimer > 0)
        {
            cam.transform.position = camOriginalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            cam.transform.position = camOriginalPos;
        }
    }
}
