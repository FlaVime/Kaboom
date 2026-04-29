using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject creaturePrefab;
    [SerializeField] private int initialCount = 10;
    [SerializeField] private int maxCount = 10;
    [SerializeField] private float spawnInterval = 1f;

    private float spawnTimer = 0f;
    private int currentCount = 0;

    public static CreatureSpawner Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < initialCount; i++)
        {
            SpawnCreature();
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && currentCount < maxCount)
        {
            spawnTimer = 0f;
            SpawnCreature();
        }
    }

    void SpawnCreature()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 0.1f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.9f, 0.9f));

        Vector2 randomPos = new Vector2(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y)
        );

        Instantiate(creaturePrefab, randomPos, Quaternion.identity);
        currentCount++;
    }

    public void OnCreatureDestroyed()
    {
        currentCount--;
    }
}
