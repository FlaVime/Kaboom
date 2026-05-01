using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int chainDepth = 3;

    public static ExplosionManager Instance { get; private set; }

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

    public void SpawnExplosion(Vector2 position, Color color)
    {
        GameObject boom = Instantiate(explosionPrefab, position, Quaternion.identity);
        boom.GetComponent<Explosion>().Init(color, chainDepth);
        GameManager.Instance.TriggerCameraShake();
    }
}
