using System.Collections;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject shockwavePrefab;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private int chainDepth = 3;

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

        // Chain explosion logic
        StartCoroutine(ChainExplosion(position, chainDepth));
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

        // Chain explosion logic
        StartCoroutine(ChainExplosion(position, chainDepth));
    }

    private IEnumerator ChainExplosion(Vector2 position, int depth)
    {
        if (depth <= 0) yield break;
        yield return new WaitForSeconds(0.15f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, explosionRadius);

        foreach (var hit in hits)
        {
            Creatures creature = hit.GetComponent<Creatures>();
            if (creature != null)
            {
                creature.ExplodeFromChain();
            }
        }

    }
}
