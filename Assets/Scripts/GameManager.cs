using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float shakeMagnitude = 0.3f;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private PhysicsMaterial2D bounceMaterial;


    private Camera cam;
    private Vector3 camOrigin;
    private float shakeTimer = 0f;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cam = Camera.main;
        camOrigin = cam.transform.position;
        CreateBounds();
    }

    void Update()
    {
        HandleCameraShake();
    }

    private void CreateBounds()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        CreateWall(new Vector2(min.x - 0.5f, 0), new Vector2(1, max.y - min.y + 2)); // Left
        CreateWall(new Vector2(max.x + 0.5f, 0), new Vector2(1, max.y - min.y + 2)); // Right
        CreateWall(new Vector2(0, min.y - 0.5f), new Vector2(max.x - min.x + 2, 1)); // Bottom
        CreateWall(new Vector2(0, max.y + 0.5f), new Vector2(max.x - min.x + 2, 1)); // Top
    }

    void CreateWall(Vector2 position, Vector2 size)
    {
        GameObject wall = new GameObject("Wall");
        wall.transform.position = position;

        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.size = size;
        collider.sharedMaterial = bounceMaterial;
    }

    void HandleCameraShake()
    {
        if (shakeTimer > 0)
        {
            cam.transform.position = camOrigin + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            cam.transform.position = camOrigin;
        }
    }

    public void TriggerCameraShake()
    {
        shakeTimer = shakeDuration;
    }
}
