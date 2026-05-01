using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float shakeMagnitude = 0.3f;
    [SerializeField] private float shakeDuration = 0.2f;

    private Camera cam;
    private Vector3 camOrigin;
    private float shakeTimer = 0f;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    void Update()
    {
        HandleCameraShake();
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
