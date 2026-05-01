using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private GameObject shockwavePrefab;

    private ParticleSystem particles;
    private Color color;
    private int chainDepth;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void Init(Color color, int chainDepth)
    {
        this.color = color;
        this.chainDepth = chainDepth;

        var main = particles.main;
        main.startColor = color;

        if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        }

        if (chainDepth > 0)
        {
            Invoke(nameof(ChainExplosion), 0.15f);
        }
    }

    void ChainExplosion()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            Creatures creature = hit.GetComponent<Creatures>();

            if (creature != null)
            {
                creature.ExplodeFromChain();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
