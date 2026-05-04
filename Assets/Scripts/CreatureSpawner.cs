using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject creaturePrefab;
    [SerializeField] private int initialCount = 5;
    [SerializeField] private int maxCount = 5;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private int killsPerWave = 15;

    private float spawnTimer = 0f;
    private int currentCount = 0;
    private int waveCount = 0;
    private int killCount = 0;

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
        Vector2 leftSide = Camera.main.ViewportToWorldPoint(new Vector2(0.05f, Random.Range(0.1f, 0.9f)));
        Vector2 rightSide = Camera.main.ViewportToWorldPoint(new Vector2(0.95f, Random.Range(0.1f, 0.9f)));
        Vector2 topSide = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.1f, 0.9f), 0.95f));
        Vector2 bottomSide = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.1f, 0.9f), 0.05f));

        int side = Random.Range(0, 4);
        Vector2 randomPos;

        switch (side)
        {
            case 0: randomPos = leftSide; break;
            case 1: randomPos = rightSide; break;
            case 2: randomPos = topSide; break;
            default: randomPos = bottomSide; break;
        }

        GameObject obj = Instantiate(creaturePrefab, randomPos, Quaternion.identity);
        Vector2 center = Camera.main.ViewportToWorldPoint(new Vector2(
            Random.Range(0.3f, 0.7f),
            Random.Range(0.3f, 0.7f)));
        Vector2 direction = ((Vector2)center - randomPos).normalized;
        obj.GetComponent<Creatures>().Init(direction);
        currentCount++;
    }

    public void OnCreatureDestroyed()
    {
        currentCount--;
        killCount++;

        if (killCount >= killsPerWave)
        {
            killCount = 0;
            NextWave();
        }
    }

    void NextWave()
    {
        waveCount++;
        maxCount = Mathf.Min(maxCount + 2, 30);
        spawnInterval = Mathf.Max(0.2f, spawnInterval - 0.1f);

        Creatures[] alive = FindObjectsByType<Creatures>(FindObjectsSortMode.None);
        foreach (var creature in alive)
        {
            creature.IncreaseSpeed(0.7f);
        }

        Debug.Log($"Wave {waveCount} started! Max: {maxCount}, Interval: {spawnInterval}");
    }
}