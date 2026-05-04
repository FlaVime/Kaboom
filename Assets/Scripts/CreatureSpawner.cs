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
        // Spawn just outside the screen on a random side
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

        // Set initial velocity towards center
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
    }
}
