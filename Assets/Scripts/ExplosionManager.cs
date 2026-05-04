using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject shockwavePrefab;

    public static ExplosionManager Instance { get; private set; }

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

    public void SpawnShockwave(Vector2 position)
    {
        if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, position, Quaternion.identity);
        }

        // Trigger camera shake
        GameManager.Instance.TriggerCameraShake();
    }

    public void SpawnExplosion(Vector2 position, Color color)
    {
        // Spawn explosion
        GameObject boom = Instantiate(explosionPrefab, position, Quaternion.identity);
        boom.GetComponent<Explosion>().Init(color);

        // Spawn shockwave
        if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, position, Quaternion.identity);
        }

        // Trigger camera shake
        GameManager.Instance.TriggerCameraShake();
    }
}
